using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Primitives;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace Starshine.Authservice.Web.Endpoints
{
    public static class EndpointsExtensions
    {
        /// <summary>
        /// 带特性的终结处理
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="builder"></param>
        /// <param name="endpointName"></param>
        /// <param name="description"></param>
        /// <param name="metadata"></param>
        /// <param name="allowAnonymous"></param>
        public static void MapPostRequest<TRequest, TResponse>(this IEndpointRouteBuilder builder, string endpointName, string? description = null, IEnumerable<Attribute>? metadata = null, bool allowAnonymous = false) where TRequest : IRequest<TResponse>
        {
            var endpoint = builder.MapPost(endpointName, async ([FromBody] TRequest model, HttpContext context) =>
            {
                return await context.EndpointHandle<TRequest, TResponse>(() => context.GetRawRequestBody(), () => model);
            })
            .Accepts<TRequest>("application/json")
            .ConfigureOpenApiMetadata<TRequest, TResponse>(endpointName, description, metadata, allowAnonymous);
        }


        /// <summary>
        /// 文件上传
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="builder"></param>
        /// <param name="endpointName"></param>
        /// <param name="description"></param>
        /// <param name="metadata"></param>
        /// <param name="allowAnonymous"></param>
        public static void MapPostFileRequest<TRequest, TResponse>(this IEndpointRouteBuilder builder, string endpointName, string? description = null, IEnumerable<Attribute>? metadata = null, bool allowAnonymous = false) where TRequest : IRequest<TResponse>
        {
            var endpoint = builder.MapPost(endpointName, async (HttpContext context) =>
            {
                // 解析请求体
                var model = await ParseRequest<TRequest, TResponse>(context);
                return await context.EndpointHandle<TRequest, TResponse>(() => context.GetRawRequestBody(), () => model);
            })
            .Accepts<TRequest>("multipart/form-data")
            .ConfigureOpenApiMetadata<TRequest, TResponse>(endpointName, description, metadata, allowAnonymous);
        }

        /// <summary>
        /// 配置openAI元数据
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="endpoint"></param>
        /// <param name="endpointName"></param>
        /// <param name="description"></param>
        /// <param name="metadata"></param>
        /// <param name="allowAnonymous"></param>
        /// <returns></returns>
        private static RouteHandlerBuilder ConfigureOpenApiMetadata<TRequest, TResponse>(this RouteHandlerBuilder endpoint, string endpointName, string? description = null, IEnumerable<Attribute>? metadata = null, bool allowAnonymous = false) where TRequest : IRequest<TResponse>
        {
            endpoint
               .Produces<TmcResult<TResponse>>(StatusCodes.Status200OK, "application/json")
               .WithName(typeof(TRequest).FullName ?? endpointName)
               .WithDescription(description ?? endpointName)
               .WithSummary(description ?? endpointName)
               .WithOpenApi();
            if (allowAnonymous)
            {
                endpoint.AllowAnonymous();
            }

            // 如果提供了额外的元数据，则附加到终结点
            if (metadata != null && metadata.Any())
            {
                foreach (var meta in metadata)
                {
                    endpoint.WithMetadata(meta);
                }
            }
            return endpoint;
        }

        private static async Task<TRequest?> ParseRequest<TRequest, TResponse>(HttpContext context) where TRequest : IRequest<TResponse>
        {
            if (context.Request.HasFormContentType)
            {
                // 处理 multipart/form-data
                var form = await context.Request.ReadFormAsync();
                var otherFields = form.Where(x => x.Value.Count > 0)
                                      .ToDictionary<KeyValuePair<string, StringValues>, string, object>(x => x.Key, x => x.Value.ToString());
                var model = otherFields.ToJson().FromJson<TRequest>();
                if (form.Files != null && model is IFormFileUploadRequest fileUploadRequest)
                {
                    fileUploadRequest.Files = form.Files.Select(file => new FormFileRequest
                    {
                        Stream = file.OpenReadStream(),
                        FileName = file.FileName,
                        ContentType = file.ContentType,
                        Length = file.Length,
                    }).ToList();
                }
                return model;
            }
            // 处理 JSON 请求
            using var reader = new StreamReader(context.Request.Body);
            var body = await reader.ReadToEndAsync();
            return string.IsNullOrWhiteSpace(body) ? default : body.FromJson<TRequest>();
        }

        /// <summary>
        /// 终结点处理
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="context"></param>
        /// <param name="requestBodyFunc"></param>
        /// <param name="requestFunc"></param>
        /// <returns></returns>
        public static async Task<IResult> EndpointHandle<TRequest, TResponse>(this HttpContext context, Func<string?> requestBodyFunc, Func<TRequest?> requestFunc) where TRequest : IRequest<TResponse>
        {
            var logger = context.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger("API");
            string? requestBody = requestBodyFunc();
            TmcResult<TResponse> result = default!;
            Stopwatch stopwatch = Stopwatch.StartNew();
            string? responseBody;
            var ignoreLog = typeof(TRequest).GetCustomAttribute<IgnoreLogAttribute>() != null;
            var ignoreResponse = typeof(TRequest).GetCustomAttribute<IgnoreResponseAttribute>() != null;
            try
            {
                var sender = context.RequestServices.GetRequiredService<IRequestSender>();
                var response = await sender.Send(requestFunc() ?? Activator.CreateInstance<TRequest>(), context.RequestAborted);
                result = TmcResult.Successed(response);
            }
            catch (Exception ex)
            {
                if (ex is FriendlyException friendlyException)
                {
                    result = TmcResult.Failed<TResponse>(friendlyException.Code, friendlyException.Message);
                }
                else if (ex is ArgumentException argumentException)
                {
                    result = TmcResult.Failed<TResponse>("arg_errors", argumentException.Message);
                }
                else
                {
                    logger.LogError(ex, "syserror");
                    result = TmcResult.Failed<TResponse>("sys_errors", "系统异常".I18n());
                }
            }
            finally
            {
                stopwatch.Stop();
                responseBody = result.ToJson();
                context.SetRawResponseBody(responseBody);
                if (!ignoreLog)
                {
                    var headers = context.Request.Headers.GetRawText();
                    logger.LogInformation("{Method} {Url} {Cost}ms\r\n{Headers}\r\n{RequestBody}\r\n{ResponseBody}", context.Request.Method, context.Request.GetDisplayUrl(), stopwatch.ElapsedMilliseconds, headers, requestBody, ignoreResponse ? "ignore" : responseBody);
                }
            }
            return Results.Json(result);
        }
    }
}
