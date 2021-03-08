var hxVue = window.hxVue || {}
window.hxApp = new Vue({
    data: {
        ...window.hxVue.data,
        isCollapse: false
    },
    methods: {
        ...window.hxVue.methods,
        handleOpen(key, keyPath) {
            console.log(key, keyPath);
        },
        handleClose(key, keyPath) {
            console.log(key, keyPath);
        },
        toggleMenu() {
            this.isCollapse = !this.isCollapse
        }
    }
}).$mount('#app')