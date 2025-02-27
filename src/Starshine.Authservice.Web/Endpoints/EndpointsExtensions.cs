using Microsoft.Extensions.Primitives;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Starshine.Abp.AspNetCore;
using MediatR;
using Volo.Abp;
using System.Text.Json;

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
                return await context.EndpointHandle<TRequest, TResponse>(() => string.Empty, () => model);
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
                return await context.EndpointHandle<TRequest, TResponse>(() => string.Empty, () => model);
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
               .Produces<RESTfulResult<TResponse>>(StatusCodes.Status200OK, "application/json")
               .WithName(typeof(TRequest).FullName ?? endpointName)
               .WithDescription(description ?? endpointName)
               .WithSummary(description ?? endpointName);
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
                if (form.Files != null)
                {
                    var files = form.Files.Select(file => new
                    {
                        Stream = file.OpenReadStream(),
                        file.FileName,
                        file.ContentType,
                        file.Length,
                    }).ToList();
                    otherFields.Add(nameof(form.Files), files);
                }
                var model = JsonSerializer.Deserialize<TRequest>(JsonSerializer.Serialize(otherFields));
                return model;
            }
            // 处理 JSON 请求
            using var reader = new StreamReader(context.Request.Body);
            var body = await reader.ReadToEndAsync();
            return string.IsNullOrWhiteSpace(body) ? default : JsonSerializer.Deserialize<TRequest>(body);
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
            RESTfulResult<TResponse> result = default!;
            Stopwatch stopwatch = Stopwatch.StartNew();
            string? responseBody;
            try
            {
                var sender = context.RequestServices.GetRequiredService<ISender>();
                var response = await sender.Send(requestFunc() ?? Activator.CreateInstance<TRequest>(), context.RequestAborted);
                result = RESTfulResult.Successed(response);
            }
            catch (Exception ex)
            {
                if (ex is UserFriendlyException friendlyException)
                {
                    result = RESTfulResult.Failed<TResponse>(friendlyException.Code, friendlyException.Message);
                }
                else if (ex is ArgumentException argumentException)
                {
                    result = RESTfulResult.Failed<TResponse>("arg_errors", argumentException.Message);
                }
                else
                {
                    logger.LogError(ex, "syserror");
                    result = RESTfulResult.Failed<TResponse>("sys_errors", "系统异常");
                }
            }
            finally
            {
                stopwatch.Stop();
                responseBody = JsonSerializer.Serialize(result);
                logger.LogInformation("{Method} {Url} {Cost}ms\r\n{RequestBody}\r\n{ResponseBody}", context.Request.Method, stopwatch.ElapsedMilliseconds,requestBody, responseBody);
            }
            return Results.Json(result);
        }
    }
}
