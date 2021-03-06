; (function (global) {
    "use strict";
    var defaults = {
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
         * 是否是boolean
         * @param {any} value 要验证的值
         * @returns {Boolean} true代表是Bool值，false代表不是Bool值
         */
        isBoolean(value) {
            return typeof value === 'boolean';
        },
        /**
         * 如果传递的值是数字，则返回“true”。 对于非有限数，返回`false`。
         * @param {Object} value 要测试的值。
         * @return {Boolean} 如果传递的值是数字，则返回“true”。 对于非有限数，返回`false`。
         */
        isNumber(value) {
            return typeof value === 'number' && isFinite(value);
        },
        /**
         * 如果传递的值是JavaScript函数则返回“true”，否则返回“false”。
         * @param {Object} value 要测试的值.
         * @return {Boolean} 如果传递的值是JavaScript函数则返回“true”，否则返回“false”。
         */
        isFunction:
            (typeof document !== 'undefined' && typeof document.getElementsByTagName('body') === 'function') ? function (value) {
                return !!value && toString.call(value) === '[object Function]';
            } : function (value) {
                return !!value && typeof value === 'function';
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
    function HxVue(options) {
        options = options || {};
        this.setData(options.data);
        this.methods = options.methods
    }
    HxVue.prototype = {
        constructor: HxVue,  
        init: function () {
        },
        setData: function (data) {
            if (defaults.isObject(data)) {
                this.data = data
            }
            else if (defaults.isFunction(data)) {
                this.data = data()
            }
            return this;
        },
        getData() {
            return this.data;
        }
    }
    global.HxVue = HxVue
})(window);