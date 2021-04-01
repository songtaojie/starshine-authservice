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
            grantOptions: [
                { value: 'implicit', label: 'Implicit' },
                { value: 'ImplicitAndClientCredentials', label: 'ImplicitAndClientCredentials' },
                { value: 'authorization_code', label: 'Code' },
                { value: 'CodeAndClientCredentials', label: 'CodeAndClientCredentials' },
                { value: 'hybrid', label: 'Hybrid' },
                { value: 'HybridAndClientCredentials', label: 'HybridAndClientCredentials' },
                { value: 'client_credentials', label: 'ClientCredentials' },
                { value: 'ResourceOwnerPassword', label: 'ResourceOwnerPassword' },
                { value: 'ResourceOwnerPasswordAndClientCredentials', label: 'ResourceOwnerPasswordAndClientCredentials' },
                { value: 'DeviceFlow', label: 'DeviceFlow' },
            ],
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
            that.$confirm('确定删除当前客户端管理?', '提示', {
                confirmButtonText: '确定',
                cancelButtonText: '取消',
                type: 'warning'
            }).then(() => {
                var url = '/client/delete/' + row.id
                axios.post(url)
                    .then(function (response) {
                        that.$message({
                            type: 'success',
                            message: '删除成功!'
                        });
                        that.getPage()
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
            this.getPage();
        },
        getPage() {
            var that = this
            axios.post('/client/getpage', that.queryParam)
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
                enabled:true,
                clientId: '',
                clientName: '',
                clientSecrets: '',
                description: '',
                allowedGrantTypes: [],
            }
        },
        onAdd() {
            this.isAdd = true;
            this.showDrawer = true;
            if (this.$refs.ruleForm) {
                this.$refs.ruleForm.resetFields();
            }
        },
        onEdit(row) {
            var that = this;
            var url = '/client/get/' + row.id;
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
                url = '/client/addorupdate';
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
                            that.getPage()
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
        this.getPage();
    }
})