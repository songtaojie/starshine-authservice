# Hx.IdentityServer

#### 介绍
IdentityServer,用户认证中心管理

#### 软件架构
软件架构说明


#### 安装教程

1. 这里是列表文本 拉取代码(dev-vue分支)
2.  Appsetting.json中配置下数据库链子字符串ConnectionStrings，例如：
    使用sqlserver数据库：`
    "ConnectionStrings": {
        "DbType": "sqlserver",
        "DefaultConnection": "Server=.;Database=HxIdentityServer;User ID=sa;Password=123456;MultipleActiveResultSets=True"
    }`
    使用mySql数据库：
    `
    "ConnectionStrings": {
    "DbType": "mysql",
    "DefaultConnection": "server=localhost;database=HxIdentityServer;user=root;password=song123"
    }`
3. 生成迁移文件，用于数据库的生成映射，定位到Hx.IdentityServer.Model文件夹cmd执行如下生成命令（要先安装ef工具dotnet tool install --global dotnet-ef）
    3.1、生成ApplicationDbContext的迁移文件
    dotnet ef --startup-project ../Hx.IdentityServer/ migrations add InitApplicationDb -c ApplicationDbContext
    3.2、生成PersistedGrantDbContext的迁移文件
    dotnet ef --startup-project ../Hx.IdentityServer/ migrations add InitPersistedGrantDb -c PersistedGrantDbContext -o         Migrations/IdentityServer/PersistedGrantDb
    3.3、生成ConfigurationDbContext的迁移文件
dotnet ef --startup-project ../Hx.IdentityServer/ migrations add InitConfigurationDb -c ConfigurationDbContext -o Migrations/IdentityServer/ConfigurationDb
    
4. 执行迁移，初始化数据，定位到启动文件Hx.IdentityServer中cmd执行如下命令
dotnet run /seed

#### 使用说明

1.  xxxx
2.  xxxx
3.  xxxx

#### 参与贡献

1.  Fork 本仓库
2.  新建 Feat_xxx 分支
3.  提交代码
4.  新建 Pull Request


#### 特技

1.  使用 Readme\_XXX.md 来支持不同的语言，例如 Readme\_en.md, Readme\_zh.md
2.  Gitee 官方博客 [blog.gitee.com](https://blog.gitee.com)
3.  你可以 [https://gitee.com/explore](https://gitee.com/explore) 这个地址来了解 Gitee 上的优秀开源项目
4.  [GVP](https://gitee.com/gvp) 全称是 Gitee 最有价值开源项目，是综合评定出的优秀开源项目
5.  Gitee 官方提供的使用手册 [https://gitee.com/help](https://gitee.com/help)
6.  Gitee 封面人物是一档用来展示 Gitee 会员风采的栏目 [https://gitee.com/gitee-stars/](https://gitee.com/gitee-stars/)
