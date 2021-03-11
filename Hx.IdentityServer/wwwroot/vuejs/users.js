window.HxVue = new Vue({
    data() {
        var that = this;
        var remoteRule = function (url,field,value,callback) {
            if (!that.remoteRuleCache.hasOwnProperty(field)) that.remoteRuleCache[field] = [];
            var cacheArray = that.remoteRuleCache[field];
            var cacheValue = cacheArray.find(r => { return r.value === value; });
            if (!that.isEmptyObject(cacheValue)) {
                if (cacheValue.valid) {
                    callback()
                } else {
                    callback(cacheValue.message)
                }
            } else {
                axios.get(url)
                    .then(data => {
                        that.remoteRuleCache[field].push({
                            value: value,
                            valid: true,
                        })
                        callback()
                    }).catch(e => {
                        if (e.data) {
                            that.remoteRuleCache[field].push({
                                value: value,
                                valid: false,
                                message: e.data.message
                            })
                            callback(e.data.message)
                        }
                    })
            }
        }
        var validateUserName = (rule, value, callback) => {
            var url = '/account/CheckUserName?userName=' + value
            remoteRule(url, rule.field, value, callback)
        };
        var validateEmail = (rule, value, callback) => {
            var url = '/account/CheckEmail?email=' + value
            remoteRule(url, rule.field, value, callback)
        };
        var validatePass = (rule, value, callback) => {
            var regular = /^.*(?=.*[0-9])(?=.*[A-Z])(?=.*[a-z])(?=.*[!@#$%^&*?]).{6,16}/;
            if (value === '') {
                callback(new Error('请输入密码'));
            } else if (!regular.test(value)) {
                callback(new Error('密码必须包含数字、大小写字母、和一个特殊符号,且长度为6~16！'));
            } else {
                callback();
            }
        };
        var validatePass2 = (rule, value, callback) => {
            if (value === '') {
                callback(new Error('请输入确认密码'));
            } else if (value !== this.form.password) {
                callback(new Error('两次输入密码不一致!'));
            } else {
                callback();
            }
        };
        return {
            ...window.HxCommonVue.data,
            drawerShow: false,
            tableData: [],
            totalCount: 0,
            queryParam: {
                pageIndex: 1,
                pageSize: 1
            },
            //表格
            form: {
                userName: 'songtaojie',
                realName: '宋涛杰',
                email: 'songtaojie@qq.com',
                password: 'Pwd$123',
                confirmPassword: 'Pwd$123',
                sex: "1",
                birthday:'2020-05-06'
            },
            remoteRuleCache: {
            },
            rules: {
                userName: [
                    { required: true, message: '请输入用户名', trigger: 'blur' },
                    { validator: validateUserName, trigger: 'blur' }
                ],
                realName: [
                    { required: true, message: '请输入真实姓名', trigger: 'blur' }
                ],
                email: [
                    { required: true, message: '请输入邮箱', trigger: 'blur' },
                    { type: 'email', message: '请输入正确的邮箱地址', trigger: 'blur'},
                    { validator: validateEmail, trigger: 'blur' }
                ],

                password: [
                    { required: true, message: '请输入确认密码', trigger: 'blur' },
                    { validator: validatePass, trigger: 'blur' }
                ],
                confirmPassword: [
                    { required: true, message: '请输入确认密码', trigger: 'blur' },
                    { validator: validatePass2, trigger: 'blur' }
                ]
            }
        }
    },
    methods: {
        ...window.HxCommonVue.methods,
        handleEdit(row) {
            var url = '/account/delete/' + row.id
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
                });
            debugger
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
                .then(function (data) {
                    that.tableData = data.items
                    that.totalCount = data.totalCount
                })
                .catch(function (error) {
                    console.log(error);
                });
        },
        //表格
        onSubmit(formName) {
            var that = this,
                url = '/account/createorupdate';
            if (that.loading) return;
            this.$refs[formName].validate(valid => {
                if (valid) {
                    that.loading = true;
                    axios.post(url, that.form)
                        .then(function (data) {
                            that.loading = false
                            that.drawerShow = false
                            that.getPageList()
                        })
                        .catch(function (error) {
                            that.loading = false
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
        onCancelForm() {
            this.drawerShow = false
        }
    },
    created() {
        this.getPageList();
    }
}).$mount('#app');
