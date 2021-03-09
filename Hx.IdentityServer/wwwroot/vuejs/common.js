window.HxCommonVue = {
    data: {
        isCollapse: false
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
    }
}