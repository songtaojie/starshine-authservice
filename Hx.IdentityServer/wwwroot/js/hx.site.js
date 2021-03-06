var blogIdentity = {
    onLogout: function () {
        alertify.confirm(`确定注销?`, function () {
            hxCore.ajax(url, {
                success() {
                    hxCore.remindSuccess("删除成功");
                    if (_hxTable) _hxTable.draw();
                }
            });
        });
    }
}