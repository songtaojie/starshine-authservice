new Vue({
    data: {
        ...window.HxCommonVue.data,
        tableData: [],
        totalCount: 0,
        queryParam: {
            pageIndex: 1,
            pageSize: 10
        }
    },
    methods: {
        ...window.HxCommonVue.methods,
        handleEdit() {

        },
        handleDelete(row) {
            var that = this
            that.$confirm('确定删除当前用户?', '提示', {
                confirmButtonText: '确定',
                cancelButtonText: '取消',
                type: 'warning'
            }).then(() => {
                var url = '/account/delete/' + row.id
                axios.post(url)
                    .then(function (response) {
                        debugger
                        that.$message({
                            type: 'success',
                            message: '删除成功!'
                        });
                        that.getPageList()
                    })
                    .catch(function (error) {
                        debugger
                        console.log(error);
                    });
            }).catch(() => {
            });
        },
        handleCurrentChange(val) {
            this.queryParam.pageIndex = val;
            this.getPageList();
        },
        getPageList() {
            var that = this
            axios.post('/clientsmanager/querypage', that.queryParam)
                .then(function (response) {
                    debugger
                    that.tableData = response.data.items
                    that.totalCount = response.data.totalCount
                })
                .catch(function (error) {
                    console.log(error);
                });
        },
        onClolumnFormatter(row, column, cellValue) {
            return cellValue + '<br/>';
        }
    },
    created() {
        this.getPageList();
    }
}).$mount('#app')