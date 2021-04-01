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
            form: this.initFrom()
        }
    },
    methods: {
        handleCurrentChange(val) {
            this.queryParam.pageIndex = val;
            this.getPage();
        },
        getPage() {
            var that = this
            axios.post('/client/GetScPage', that.queryParam)
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
                clientId: '',
                clientName: '',
                allowedScopes: '',
                allowedCorsOrigins: '',
            }
        },
        handleEdit(row) {
            var that = this;
            var url = '/client/getsc/' + row.clientId;
            axios.get(url)
                .then(function (data) {
                    that.form = data;
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
                url = '/client/updatesc';
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
            if (this.$refs.ruleForm) {
                this.$refs.ruleForm.resetFields();
            }
        }
    },
    created() {
        this.getPage();
    }
})