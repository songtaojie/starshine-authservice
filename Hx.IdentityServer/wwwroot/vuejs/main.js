; (function (global) {
    var toast = ELEMENT.Message
    //asiox配置
    // 设置请求超时
    axios.defaults.timeout = 30000
    // 设置post请求头
    //axios.defaults.headers.post['Content-Type'] =
    //    'application/x-www-form-urlencoded;charset=UTF-8'

    // 返回状态判断(添加响应拦截器)
    axios.interceptors.response.use(
        (res) => {
            // 对响应数据做些事
            if (res.data.success === false) {
                //toast({
                //    message: res.data.message,
                //    type: 'error',
                //})
                return Promise.reject(res)
            }

            return res.data.data
        },
        (e) => {
            switch (e.response && e.response.status) {
                case 403:
                    toast({
                        message: '您没有该操作的权限!',
                        type: 'error',
                    })
                    return null
                default:
                    break
            }

            return Promise.reject(e)
        }
    )
    var enumerables = [//'hasOwnProperty', 'isPrototypeOf', 'propertyIsEnumerable', 
        'valueOf', 'toLocaleString', 'toString', 'constructor'];
    var hxCommon = {
        /**
       * 如果传递的值是JavaScript数组，则返回' true '，否则返回' false '.
       * @param {Object} target The target to test.
       * @return {Boolean}
       * @method
       */
        isArray: ('isArray' in Array) ? Array.isArray : function (value) {
            return toString.call(value) === '[object Array]';
        },
        /**
        * 判断给定值是否为空
        * @param {any} value 要判断的值
        * @param {Boolean} allowEmptyString 是否允许空字符串
        * @returns {Boolean} true为空，false不为空
        */
        isEmpty(value, allowEmptyString) {
            return (value === undefined || value === null) || (!allowEmptyString ? value === '' : false) || (isArray(value) && value.length === 0);
        },
        /**
        * 判断给定值是否是字符串
        * @param {any} value 要验证的值
        * @returns {Boolean} true代表是字符串，false代表不是字符串
        */
        isString(value) {
            return typeof value === 'string';
        },
        /**
         * 判断给定制是否是对象
         * @param {any} value 要验证的值
         * @returns {Boolean} true代表是对象，false代表不是对象
         */
        isObject(value) {
            if (toString.call(null) === '[object Object]') {
                //在这里检查ownerDocument以排除DOM节点
                return value !== null && toString.call(value) === '[object Object]' && value.ownerDocument === undefined;
            } else {
                return toString.call(value) === '[object Object]';
            }
        },
        /**
         * 是否是简单地对象
         * @param {any} value 要验证的值
         * @returns {Boolean} true代表是简单对象，false代表不是简单对象
         */
        isSimpleObject(value) {
            return value instanceof Object && value.constructor === Object;
        },
        /**
         * 检查对象是否为空
         * @param {Object} object 要检查的对象
         * @return {Boolean} true不为空
         */
        isEmptyObject(object) {
            var key
            for (key in object) {
                if (object.hasOwnProperty(key)) {
                    return false
                }
            }
            return true
        },
        /**
         * 将config的所有属性复制到指定的对象。支持两种级别的默认值:
         * @param {Object} object 接受方对象
         * @param {Object} config 属性的来源对象
         * @param {Object} defaults 应用于默认值的对象
         */
        apply(object, config, defaults) {
            if (object) {
                if (defaults) {
                    Ext.apply(object, defaults);
                }

                if (config && typeof config === 'object') {
                    var i, j, k;

                    for (i in config) {
                        object[i] = config[i];
                    }

                    if (enumerables) {
                        for (j = enumerables.length; j--;) {
                            k = enumerables[j];
                            if (config.hasOwnProperty(k)) {
                                object[k] = config[k];
                            }
                        }
                    }
                }
            }

            return object;
        },
        /**
         * 将config的所有属性复制到对象（如果它们尚不存在）。
         * @param {Object} object 属性的接收者
         * @param {Object} config 属性的来源
         * @return {Object} 操作后的对象
         */
        applyIf(object, config) {
            if (object && config && typeof config === 'object') {
                for (var property in config) {
                    if (object[property] === undefined) {
                        object[property] = config[property];
                    }
                }
            }
            return object;
        },
    }
    var commonConfig = {
        data: {
            isCollapse: false,
            loading: false,
            showDrawer: false
        },
        methods: {
            ...hxCommon,
            onMenuSelect(menu) {
                global.location.href = menu
            },
        }
    }
    function HxVue(options) {
        if (hxCommon.isFunction(options.data)) {
            this.data = function () {
                return hxCommon.apply({}, options.data(), commonConfig.data)
            }
        } else if (hxCommon.isObject(options.data)) {
            this.data = function () {
                return hxCommon.apply({}, options.data, commonConfig.data)
            }
        }
        if (hxCommon.isObject(options.methods)) {
            this.methods = hxCommon.apply({}, options, commonConfig.methods)
        } else {
            this.methods = commonConfig.methods
        }
        return this
    }
    if (!global.HxVue) {
        global.HxVue = HxVue;
    }
})(typeof window !== 'undefined' ? window : this,Vue);




