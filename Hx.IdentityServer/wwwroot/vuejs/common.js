window.HxCommon = {
    /**
    * 判断给定值是否为空
    * @param {any} value 要判断的值
    * @param {Boolean} allowEmptyString 是否允许空字符串
    * @returns {Boolean} true为空，false不为空
    */
    isEmpty(value, allowEmptyString) {
        return (value === undefined || value === null) || (!allowEmptyString ? value === '' : false) || ($.isArray(value) && value.length === 0);
    },
}

window.HxCommonVue = {
    data: {
        isCollapse: false,
        loading: false,
        appId: null
    },
    methods: {
        onMenuClick(menu) {
            window.location.href = menu.route;
        },
        onMenuSelect(menu) {
            window.location.href = menu
        },
        handleCommand(command) {
            window.location.href = command;
        }
    },
    mounted: function () {
        var that = this;
        debugger
        //this.$refs.rootDiv.dataset.id
    }
}
