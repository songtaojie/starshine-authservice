/* tslint:disable */
/* eslint-disable */

/**
 * 代码生成参数类
 * @export
 * @interface CodeGenInput
 */
export interface CodeGenInput {
    /**
     * 当前页码
     * @type {number}
     * @memberof CodeGenInput
     */
    page?: number;
    /**
     * 页码容量
     * @type {number}
     * @memberof CodeGenInput
     */
    pageSize?: number;
    /**
     * 排序字段
     * @type {string}
     * @memberof CodeGenInput
     */
    field?: string | null;
    /**
     * 排序方向
     * @type {string}
     * @memberof CodeGenInput
     */
    order?: string | null;
    /**
     * 降序排序
     * @type {string}
     * @memberof CodeGenInput
     */
    descStr?: string | null;
    /**
     * 作者姓名
     * @type {string}
     * @memberof CodeGenInput
     */
    authorName?: string | null;
    /**
     * 类名
     * @type {string}
     * @memberof CodeGenInput
     */
    className?: string | null;
    /**
     * 是否移除表前缀
     * @type {string}
     * @memberof CodeGenInput
     */
    tablePrefix?: string | null;
    /**
     * 库定位器名
     * @type {string}
     * @memberof CodeGenInput
     */
    configId?: string | null;
    /**
     * 数据库名(保留字段)
     * @type {string}
     * @memberof CodeGenInput
     */
    dbName?: string | null;
    /**
     * 数据库类型
     * @type {string}
     * @memberof CodeGenInput
     */
    dbType?: string | null;
    /**
     * 数据库链接
     * @type {string}
     * @memberof CodeGenInput
     */
    connectionString?: string | null;
    /**
     * 生成方式
     * @type {string}
     * @memberof CodeGenInput
     */
    generateType?: string | null;
    /**
     * 数据库表名
     * @type {string}
     * @memberof CodeGenInput
     */
    tableName?: string | null;
    /**
     * 命名空间
     * @type {string}
     * @memberof CodeGenInput
     */
    nameSpace?: string | null;
    /**
     * 业务名（业务代码包名称）
     * @type {string}
     * @memberof CodeGenInput
     */
    busName?: string | null;
    /**
     * 功能名（数据库表名称）
     * @type {string}
     * @memberof CodeGenInput
     */
    tableComment?: string | null;
    /**
     * 菜单应用分类（应用编码）
     * @type {string}
     * @memberof CodeGenInput
     */
    menuApplication?: string | null;
    /**
     * 菜单父级
     * @type {number}
     * @memberof CodeGenInput
     */
    menuPid?: number;
}
