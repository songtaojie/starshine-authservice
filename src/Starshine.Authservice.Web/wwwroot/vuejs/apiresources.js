window.hxVue = new HxVue({
    data() {
        return {
            tableData: [],
            totalCount: 0,
            queryParam: {
                pageIndex: 1,
                pageSize: 10
            },
            //添加编辑
            isAdd: true,
            form: this.initFrom(),
            rules: {
                clientId: [
                    { required: true, message: '请输入客户端id', trigger: 'blur' },
                ]
            },
        }
    },
    methods: {
        handleDelete(row) {
            var that = this
            that.$confirm('确定删除当前Api资源?', '提示', {
                confirmButtonText: '确定',
                cancelButtonText: '取消',
                type: 'warning'
            }).then(() => {
                var url = '/apiresource/delete/' + row.id
                axios.post(url)
                    .then(function (response) {
                        that.$message({
                            type: 'success',
                            message: '删除成功!'
                        });
                        that.getPageList()
                    })
                    .catch(function (error) {
                        if (error.data) {
                            that.$message({
                                type: 'error',
                                message: error.data.message
                            });
                        }
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
            axios.post('/apiresource/getpage', that.queryParam)
                .then(function (data) {
                    that.tableData = data.items
                    that.totalCount = data.totalCount
                })
                .catch(function (error) {
                    console.log(error);
                });
        },
        //表格
        initFrom() {
            return {
                name: '',
                displayName: '',
                description: '',
                userClaims: '',
                enabled:true
            }
        },
        onAdd() {
            this.isAdd = true;
            this.showDrawer = true;
        },
        onEdit(row) {
            var that = this;
            var url = '/apiresource/get/' + row.id;
            axios.get(url)
                .then(function (data) {
                    that.form = data;
                    that.isAdd = false;
                    that.showDrawer = true;
                })
                .catch(function (error) {
                    if (error.data) {
                        that.$message({
                            type: 'error',
                            message: error.data.message
                        });
                    }
                });
        },
        //添加编辑
        handleSubmit(formName) {
            var that = this,
                url = '/apiresource/addorupdate';
            if (that.loading) return;
            this.$refs[formName].validate(valid => {
                if (valid) {
                    that.loading = true;
                    axios.post(url, that.form)
                        .then(function (data) {
                            that.$message({
                                type: 'success',
                                message: '保存成功!'
                            });
                            that.loading = false
                            that.showDrawer = false
                            that.getPageList()
                        })
                        .catch(function (error) {
                            that.loading = false
                            if (error.data) {
                                that.$message({
                                    type: 'error',
                                    message: error.data.message
                                });
                            }
                            console.log(error);
                        });
                }
            })
        },
        handleClose(done) {
            var that = this
            if (that.loading) return;
            if (!that.isEmptyObject(that.form)) {
                this.$confirm('数据未提交，确定关闭窗体？')
                    .then(_ => {
                        //this.loading = true;
                        done();
                    })
                    .catch(_ => { });
            }
            else {
                done();
            }
        },
        handleOpened() {
            if (this.isAdd) {
                this.form = this.initFrom();
                this.$refs.ruleForm.resetFields();
            }
        }
    },
    created() {
        this.getPageList();
    }
})