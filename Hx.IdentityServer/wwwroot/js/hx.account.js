; if (!jQuery) { throw new Error("hxAccount requires jQuery"); }
; (function ($, window) {
    "use strict";
    var hxCore = window.hxCore;
    //默认的配置
    var defaults = {
        login: {
            class: '.hx-login',
            validatecodeurl:''
        },
        register: {
            class: '.hx-register',
            usernameurl: '/account/checkusername',
            emailurl:'/account/checkemail'
        },
        getFrom: function (className) {
            var $form = $(className + ' form');
            if ($form.length == 0) {
                $form = $('form' + className)
            }
            return $form;
        },
        loginForm: null,
        registerForm:null
    };
    defaults.loginForm = defaults.getFrom(defaults.login.class);
    defaults.registerForm = defaults.getFrom(defaults.register.class);
    var loginValid = function (instance) {
        var $form = defaults.loginForm,
            validateurl = defaults.login.validatecodeurl;
        var rules = {
            UserName: { required: true },
            PassWord: { required: true }
        };
        
        instance.validator = $form.validate({
            errorElement: 'span',
            errorClass: 'text-danger',
            //提交表单后，（第一个）未通过验证的表单获得焦点
            focusInvalid: true,
            //当未通过验证的元素获得焦点时，移除错误提示
            //focusCleanup: true,
            rules,
            messages: {
                UserName: '用户名不能为空!',
                PassWord: '密码不能为空!',
                //ValidateCode: {
                //    required: '验证码不能为空!',
                //    remote: '验证码不正确!'
                //}
            },
            errorPlacement: function (error, element) {
                error.addClass('small').insertAfter(element.closest('.input-group'));
            },
            ////display error alert on form submit
            invalidHandler: function (event, validator) {
                $('.alert-error', $form).show();
            },
            //highlight: function (element) {
            //    $(element).closest('.form-row').addClass('error'); 
            //},
            success: function (error) {
                //error.closest('.form-row').removeClass('error');
                error.remove();
            }
        });
        return instance;
    }
    var getSubmitBtn = function(className) {
        var sumitBtn = $(className + ' button[type=submit]');
        if (sumitBtn.length === 0) {
            sumitBtn = $(className + ' input[type=submit]');
        }
        return sumitBtn;
    };
    var login = {
        init: function (options) {
            var opt = {};
            if (options === true || options === false || hxCore.isString(options)) {
                opt.enter = options;
            } else {
                opt = $.extend(true, opt, options);
            }
            loginValid(this);
            if (opt.enter === true) {
                $(document).keypress(function (e) {
                    if (e.which === 13) {
                        defaults.loginForm.submit();
                        return false;
                    }
                });
            } else if (hxCore.isString(opt.enter)) {
                $(opt.enter).keypress(function (e) {
                    if (e.which === 13) {
                        defaults.loginForm.submit();
                        return false;
                    }
                });
            }
            if (hxCore.isFunction(opt.oninitend)) {
                opt.oninitend.call(window.hxAccount);
            }
            return this;
        },
        begin: function (a, b) {
            var sumitBtn = getSubmitBtn(defaults.login.class);
            if (sumitBtn.hasClass('disabled')) {
                return false;
            }
            sumitBtn.addClass('disabled');
            if (hxCore.blockUI)
                hxCore.blockUI({ label: '登陆中...' });
        },
        finish: function (op, success, r) {
            if (hxCore.unblockUI)
                hxCore.unblockUI();
            var sumitBtn = getSubmitBtn(defaults.login.class);
            if (sumitBtn.hasClass('disabled')) {
                sumitBtn.removeClass('disabled');
            }
        },
        success: function (data, textStatus, jqXHR) {
            hxCore.ajaxSuccess(jqXHR, function (d) {
                window.location.href = d && decodeURIComponent(d) || "/";
                if (hxCore.unblockUI)
                    hxCore.unblockUI();
            }, function (data, result) {
                hxCore.remindError(data);
            });
        },
        failure(data, err) {
            hxCore.ajaxError(data);
        }
    };

    $.validator.addMethod("checkName", function (value, element, params) {
        var regular = /^([a-zA-Z]|[\u4E00-\u9FA5])([a-zA-Z0-9]|[\u4E00-\u9FA5]|[_]){4,31}$/;
        return this.optional(element) || (regular.test(value));
    }, "用户名由字母、数字、下划线和中文组成，以中文或字母开头，且长度为4~30");

    $.validator.addMethod("checkPwd", function (value, element, params) {
        var regular = /^.*(?=.*[0-9])(?=.*[A-Z])(?=.*[a-z])(?=.*[!@#$%^&*?]).{6,16}/;
        return this.optional(element) || (regular.test(value));
    }, "密码必须包含数字、大小写字母、和一个特殊符号,且长度为6~16！"); 

    var registerValid = function (instance) {
        var $form = defaults.registerForm,
            usernameurl = defaults.register.usernameurl,
            emailurl = defaults.register.emailurl;
        instance.validator = $form.validate({
            errorElement: 'span',
            errorClass: 'text-danger',
            //提交表单后，（第一个）未通过验证的表单获得焦点
            focusInvalid: true,
            //当未通过验证的元素获得焦点时，移除错误提示
            //focusCleanup: true,
            rules: {
                RealName: {
                    required:true
                },
                UserName: {
                    required: true,
                    checkName: true,
                    remote: {
                        url: usernameurl,
                        type: 'post',
                        dataType: 'text'
                    }
                },
                Email: {
                    required: true,
                    email: true,
                    remote: {
                        url: emailurl,
                        type: 'post',
                        dataType: 'text'
                    }
                },
                Password: {
                    required: true,
                    checkPwd:true
                },
                ConfirmPassword: {
                    required: true,
                    equalTo: 'input[name="Password"]'
                }
            },
            messages: {
                RealName: '昵称不能为空!',
                UserName: {
                    required: '用户名不能为空!'
                },
                Email: {
                    required: '邮箱不能为空!'
                },
                Password: {
                    required: '密码不能为空!'
                },
                ConfirmPassword: {
                    required: '确认密码不能为空!',
                    equalTo: '确认密码和密码不一致!'
                }
            },
            errorPlacement: function (error, element) {
                error.addClass('small').insertAfter(element);
            },
            ////display error alert on form submit
            invalidHandler: function (event, validator) {
                $('.alert-error', $form).show();
            },
            //highlight: function (element) {
            //    $(element).closest('.form-row').addClass('error'); 
            //},
            success: function (error) {
                //error.closest('.form-row').removeClass('error');
                error.remove();
            }
        });
        return instance;
    }
    var register = {
        init: function (options) {
            var opt = {};
            if (options === true || options === false || hxCore.isString(options)) {
                opt.enter = options;
            } else {
                opt = $.extend(true, opt, options);
            }
            registerValid(this);
            if (opt.enter === true) {
                $(document).keypress(function (e) {
                    if (e.which === 13) {
                        defaults.registerForm.submit();
                        return false;
                    }
                });
            } else if (hxCore.isString(opt.enter)) {
                $(opt.enter).keypress(function (e) {
                    if (e.which === 13) {
                        defaults.registerForm.submit();
                        return false;
                    }
                });
            }
            if (hxCore.isFunction(opt.oninitend)) {
                opt.oninitend.call(window.hxAccount);
            }
            return this;
        },
        //如果按钮时禁用状态，禁止提交
        begin: function () {
            var submitBtn = getSubmitBtn(defaults.register.class);
            if (submitBtn.hasClass('disabled')) {
                return false;
            }
            submitBtn.addClass('disabled');
        },
        success: function (r) {
            debugger
            hxCore.ajaxSuccess(r, function (data) {
                window.location.href = data && decodeURIComponent(data) || "/";
            });
        },
        //修改按钮状态为可用
        finish: function () {
            var submitBtn = getSubmitBtn(defaults.register.class);
            if (submitBtn.hasClass('disabled')) {
                submitBtn.removeClass('disabled');
            }
        }
    }
    /**
    * 
    * 是否显示登录页
    * @param { boolean } show true显示登录页，隐藏注册账号页
    */
    var switchShow = function (show) {
        var $login = $(defaults.login.class),
            $register = $(defaults.register.class),
            loginShow = show === true;
        if (loginShow) {
            if (hxAccount.register.validator)
                hxAccount.register.validator.resetForm();
        } else {
            if (hxAccount.login.validator)
                hxAccount.login.validator.resetForm();
        }
        $login.attr('hidden', !loginShow);
        $register.attr('hidden', loginShow);
        return loginShow;
    };
    var HxAccount = function () {
        return {
            switchShow,
            login,
            register
        };
    };
    window.hxAccount = new HxAccount();
}(jQuery, window));

