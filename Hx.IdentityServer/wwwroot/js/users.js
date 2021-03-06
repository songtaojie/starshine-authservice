$(function () {
    var className = 'text-center',
        isSuperAdmin = false;
    var getColumns = function (isSuperAdmin) {
        var columns = [
            { data: "userName", className },
            { data: "realName", className },
            { data: "createTime", className },
            { data: "accessFailedCount", className },
        ];
        columns.push({
            data: "id",
            className: 'text-center',
            visible: isSuperAdmin,
            render: function (data, type, row, meta) {
                var edit = `<a href= '#' data-id='${data}' class='btn btn-warning btn-sm mr-1' data-type='edit' title='Edit'>
                                    <span class='glyphicon glyphicon-pencil'>编辑</span></a>`,
                    del = `<a href= '#' data-id='${data}' class='btn btn-danger btn-delete btn-sm' data-type='delete'>
                                    <span class='glyphicon glyphicon-pencil'>删除</span></a>`;
                return edit + del;
            }
        });
        return columns;
    }
    _hxTable = $('table').DataTable({
        oLanguage: {
            sUrl: '/lib/dataTables/i18n/Chinese.json'
        },
        //// 是否允许检索
        //"searching": false,
        // 件数选择功能 默认true
        //lengthChange: false,
        // 每页的初期件数 用户可以操作lengthMenu上的值覆盖
        "lengthMenu": [1, 10, 50, 75, 100],
        //pageLength: 1,
        serverSide: true,
        ordering: false,
        pagingType: "full_numbers",
        ajax: function (data, callback, settings) {
            hxCore.ajax('/account/QueryUserPage', {
                data,
                success: function (data) {
                    isSuperAdmin = data.isSuperAdmin || false;
                    isSuperAdmin = true;
                    callback(data);
                }
            })
        },
        initComplete: function () {
            var that = this;
            that.fnSetColumnVis(4, isSuperAdmin, false)
        },
        columns: getColumns(false)
    });

    
    $('table').on('click', '[data-type=delete]', function () {
        var $me = $(this),
            url = '/account/' + $me.data('type') + '/' + $me.data('id');
        debugger
        alertify.confirm(`确定删除当前用户?`, function () {
            hxCore.ajax(url, {
                success() {
                    hxCore.remindSuccess("删除成功");
                    if (_hxTable) _hxTable.draw();
                }
            });
        });

    })
})

