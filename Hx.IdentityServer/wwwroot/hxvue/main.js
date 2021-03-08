var hxVue = window.hxVue || {}
window.hxApp = new Vue({
    data: {
        ...window.hxVue.data,
        isCollapse: false
    },
    methods: {
        ...window.hxVue.methods,
        onMenuClick(menu) {
            window.location.href = menu.route;
        },
        handleCommand(command) {
            window.location.href = command;
        }
    }
}).$mount('#app')