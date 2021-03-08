window.HxCommonVue = {
    data: {
        isCollapse: false
    },
    methods: {
        onMenuClick(menu) {
            window.location.href = menu.route;
        },
        handleCommand(command) {
            window.location.href = command;
        }
    }
}