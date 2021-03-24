; (function (global) {
    var app, hxVue = global.hxVue || {};
    app = new Vue({
        data() {
            var hxData = {}
            if (hxVue.data) {
                const data = hxVue.data();
                hxData = data.call(this)
            }
            return hxVue.methods.apply({
                isCollapse: false,
                loading: false,
                showDrawer: false,},hxData) 
        },
        methods: hxVue.methods.apply({
            onMenuSelect(menu) {
                global.location.href = menu
            },
            handleCommand(command) {
                global.location.href = command;
            }, 
        }, hxVue.methods),
        created: hxVue.created
    });
    app.$mount('#app');
})(typeof window !== 'undefined' ? window : this,Vue);
