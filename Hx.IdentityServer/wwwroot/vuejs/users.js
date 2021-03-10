var addOrUpdate = {
    data: {
        form: {
            id:null
        },
    },
    methods: {
        onSubmit() {
            var that = this,
                url = '/account/addorupdate';
            if (that.loading) return;
            axios.post(url, that.form)
                .then(function (response) {
                    debugger
                    if (response.success) {
                        that.loading = false
                        that.drawerShow = false
                        that.$message({
                            type: 'success',
                            message: '添加成功!'
                        });
                    }
                    else {
                        that.$message({
                            type: 'error',
                            message: response.data.message
                        });
                    }
                    
                    that.getPageList()
                })
                .catch(function (error) {
                    debugger
                    that.loading = false
                    console.log(error);
                });
        },
        handleClose(done) {
            //if (this.loading) return;
            this.$confirm('数据未提交，确定关闭窗体？')
                .then(_ => {
                    //this.loading = true;
                    done();
                })
                .catch(_ => { });
        },
        onCancelForm() {
            this.drawerShow = false
        }
    }

}

window.HxVue = new Vue({
    data() {
        return {
            ...window.HxCommonVue.data,
            ...addOrUpdate.data,
            drawerShow: false,
            tableData: [],
            totalCount: 0,
            queryParam: {
                pageIndex: 1,
                pageSize: 1
            }
        }
    },
    methods: {
        isEmpty:window.HxCommon.isEmpty,
        ...window.HxCommonVue.methods,
        ...addOrUpdate.methods,
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
            axios.post('/account/queryuserpage', that.queryParam)
                .then(function (response) {
                    that.tableData = response.data.items
                    that.totalCount = response.data.totalCount
                })
                .catch(function (error) {
                    console.log(error);
                });
        }
    },
    created() {
        this.getPageList();
    }
}).$mount('#app');
