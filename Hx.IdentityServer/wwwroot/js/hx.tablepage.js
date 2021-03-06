; if (!jQuery) { throw new Error("hx.tablepage requires jQuery"); }
; (function ($, window) {
	var hxTable = function (ele, opt) {
		this.$element = ele;
		this.defaults = {
			title: null,
			body: null,
			datas: null,
			page: {
				currentPage: 1,
				pageSize: 10,
				totalPage: 4,
				totalRecord: 35
			},
			firstPage: function () {
				alert('firstPage');
			},
			previousPage: function () {
				alert('previousPage');
			},
			nextPage: function () {
				alert('nextPage');
			},
			endPage: function () {
				alert('endPage');
			},
			setPageSize: function (pageSize) {
				alert('setPageSize:' + pageSize);
			},
			setCurrentPage: function (currentPage) {
				alert('setCurrentPage:' + currentPage);
			},
			editRecord: function (id) {
				alert('editRecord:' + id);
			},
			deleteRecord: function (id) {
				alert('deleteRecord:' + id);
			},
			insertRecord: function () {
				alert('insertRecord');
			},
			deleteBatch: function (values) {
				alert('deleteBatch:' + values);
			}

		};
		this.options = $.extend({}, this.defaults, opt);
	}
	hxTable.prototype = {
		init: function () {

		}
	}

	$.fn.yzzTbalePager = function (options) {
		var defaults = {
			thead: ['<input type="checkbox" class="yzz-checkbox">', '列一', '列二', '操作'], //要使全选事件有效复选框checkbox的类名要求一致
			datas: [ //启动编辑点击事件必须带class：yzz-edit、启动删除点击事件必须带class：yzz-delete
				['<input type="checkbox" class="yzz-checkbox" value="0dfg">', '列一', '列二', '<a href="javascript:void(0);" class="yzz-edit">编辑</a><a href="javascript:void(0);" class="yzz-delete">删除</a>'],
				['<input type="checkbox" class="yzz-checkbox" value="1ecfe">', '列一', '列二', '<a href="javascript:void(0);" class="yzz-edit">编辑</a><a href="javascript:void(0);" class="yzz-delete">删除</a>'],
				['<input type="checkbox" class="yzz-checkbox" value="2ererg">', '列一', '列二', '<a href="javascript:void(0);" class="yzz-edit">编辑</a><a href="javascript:void(0);" class="yzz-delete">删除</a>']
			],
			page: {
				'currentPage': 1,
				'pageSize': 10,
				'totalPage': 4,
				'totalRecord': 35
			},
			cellHeight: '20px',
			cellWidths: ['5%', '30%', '30%', '5%'],
			tableAlign: "center",
			cellTextAlign: "center",
			addInsertButton: true,
			addDeleteButton: true,
			pageAmount: 10, //页大小间隔量，一定要和第一次的pageSize值一样
			$this: $(this),
			noDatasImgTip: 'http://ww2.sinaimg.cn/mw690/0060tXcFgw1f06d5ziejaj30o016otfp.jpg',
			showPager: true,
			firstPage: function () {
				alert('firstPage');
			},
			previousPage: function () {
				alert('previousPage');
			},
			nextPage: function () {
				alert('nextPage');
			},
			endPage: function () {
				alert('endPage');
			},
			setPageSize: function (pageSize) {
				alert('setPageSize:' + pageSize);
			},
			setCurrentPage: function (currentPage) {
				alert('setCurrentPage:' + currentPage);
			},
			editRecord: function (id) {
				alert('editRecord:' + id);
			},
			deleteRecord: function (id) {
				alert('deleteRecord:' + id);
			},
			insertRecord: function () {
				alert('insertRecord');
			},
			deleteBatch: function (values) {
				alert('deleteBatch:' + values);
			}
		};
		//将一个空对象做为第一个参数
		var settings = $.extend({}, defaults, options);

		// 在每个调用该插件的元素中执行以下代码
		return $(this).each(function () {
			settings.$this.empty(); //先清空原来的内容
			settings.$this.css('padding', '10px');

			if (settings.datas.length > 0) {

				//列表头列数与内容列数不对等
				if (settings.thead.length != settings.datas[0].length) {
					alert('列表头列数与内容列数不对等')
					return;
				} else if (settings.thead.length != settings.cellWidths.length) {
					alert('列表头列数与单元格宽度列数不对等')
					return;
				}

				//是否增加添加按钮
				if (settings.addInsertButton) {
					var insertButton = '<button class="yzz-button yzz-insert-button">添加</button>';
					settings.$this.append(insertButton);
					settings.$this.find('.yzz-insert-button').click(function () {
						settings.insertRecord();
					});
				}

				//是否添加批量删除按钮
				if (settings.addDeleteButton) {
					var deleteButton = '<button class="yzz-button yzz-delete-button">删除</button>';
					settings.$this.append(deleteButton);
					settings.$this.find('.yzz-delete-button').click(function () {
						var $checkbox = settings.$this.find('table thead input[type=checkbox]').eq(0);
						var values = '';
						settings.$this.find('.' + $checkbox.attr('class')).each(function (i) {
							if ($(this).prop('checked') && i != 0) {
								values += $(this).val() + ',';
							}
						});
						if (values.length > 0) {
							values = values.substring(0, values.length - 1);
							settings.deleteBatch(values);
						} else {
							alert('请先选择操作项');
						}

					});
				}
			}

			//组装列表数据
			var tableHtml = '<table align="' + settings.tableAlign + '" class="yzz-paging-plugin-table"><thead><tr>';;
			for (var i in settings.thead) {
				tableHtml += '<th width="' + settings.cellWidths[i] + '" height="' + settings.cellHeight + '">' + settings.thead[i] + '</th>';
			}

			tableHtml += '</tr></thead><tbody>';
			if (settings.datas.length <= 0) {
				settings.showPager = false;
				settings.$this.after('<div  style="width:' + (settings.$this.width() - 20) + 'px;" ><img style="margin-left:12px;"  width="100%" src="' + settings.noDatasImgTip + '"  /></div>');
			} else {
				for (var i in settings.datas) {
					tableHtml += '<tr>';
					for (var j in settings.datas[i]) {
						tableHtml += '<td width="' + settings.cellWidths[j] + '" height="' + settings.cellHeight + '">' + settings.datas[i][j] + '</td>';
					}
					tableHtml += '</tr>';
				}
			}
			tableHtml += '</tbody></table>';
			settings.$this.append(tableHtml);
			if (settings.datas.length > 0) {

				//判断在添加批量删除按钮后是否有添加全选复选框
				if (settings.addDeleteButton) {
					if (settings.$this.find('table thead input[type=checkbox]').eq(0).length <= 0) {
						alert('请先设置全选复选框');
						return;
					}
				}

				//单元格对齐格式
				settings.$this.find('table th').css('text-align', settings.cellTextAlign);
				settings.$this.find('table td').css('text-align', settings.cellTextAlign);

				if (settings.showPager) {
					//添加分页器
					var pagingPlugin = '<div class="yzz-paging-plugin-pager"><div class="yzz-page-total-record"><span>合计：</span><span>10</span><span>条</span></div><div class="yzz-page-size"><span>每页显示条数：</span><select><option value="10" selected="selected">10</option><option value="20">20</option></select></div><div class="yzz-page-turn"><a href="javascript:void(0);" class="firstPage">首页</a><a href="javascript:void(0);" class="previousPage">上一页</a><a href="javascript:void(0);" class="nextPage">下一页</a><a href="javascript:void(0);" class="endPage">尾页</a><div class="yzz-goto-page-div"><span>到第</span><select><option value="1" selected="selected">1</option><option value="2">2</option></select><span>页</span></div><div><span>第</span><span>1</span><span>/</span><span>2</span><span>页</span></div></div></div>';
					settings.$this.find('table').after(pagingPlugin);
					//设置页面列表相关分页数据
					settings.$this.find('.yzz-page-total-record span').eq(1).html(settings.page['totalRecord']);
					var options = '';
					var size = null;

					for (var i = 1; i <= Math.ceil(settings.page['totalRecord'] / settings.pageAmount); i++) {
						size = i * settings.pageAmount;
						if (size == settings.page['pageSize']) {
							options += '<option value="' + size + '" selected="selected">' + size + '</option>';
						} else {
							options += '<option value="' + size + '">' + size + '</option>';
						}
					}
					settings.$this.find('.yzz-page-size select').html(options);

					options = '';
					for (var i = 1; i <= settings.page['totalPage']; i++) {
						if (i == settings.page['currentPage']) {
							options += '<option value="' + i + '" selected="selected">' + i + '</option>';
						} else {
							options += '<option value="' + i + '">' + i + '</option>';
						}
					}
					settings.$this.find('.yzz-page-turn .yzz-goto-page-div select').html(options);
					settings.$this.find('.yzz-page-turn div').eq(1).find('span').eq(1).html(settings.page['currentPage']);
					settings.$this.find('.yzz-page-turn div').eq(1).find('span').eq(3).html(settings.page['totalPage']);

					//设置事件
					//设置全选事件
					var $checkbox = settings.$this.find('table thead input[type=checkbox]').eq(0);
					if ($checkbox.length > 0) {
						$checkbox.eq(0).click(function () {
							settings.$this.find('.' + $(this).attr("class")).prop('checked', $(this).prop('checked'));
						});
					}
					//设置每页显示条数事件
					settings.$this.find('.yzz-page-size select').change(function () {
						settings.setPageSize(settings.$this.find('.yzz-page-size select').val());
					});
					//设置页面跳转事件
					settings.$this.find('.yzz-goto-page-div select').change(function () {
						settings.setCurrentPage(settings.$this.find('.yzz-goto-page-div select').val());
					});
					if (settings.page['currentPage'] == 1) {
						settings.$this.find('.yzz-page-turn .firstPage').css({
							'cursor': 'not-allowed',
							'color': '#444'
						});
						settings.$this.find('.yzz-page-turn .previousPage').css({
							'cursor': 'not-allowed',
							'color': '#444'
						});
						//绑定下一页事件
						settings.$this.find('.yzz-page-turn .nextPage').bind('click', function () {
							settings.nextPage();
						});
						//绑定尾页事件
						settings.$this.find('.yzz-page-turn .endPage').bind('click', function () {
							settings.endPage();
						});
					} else if (settings.page['currentPage'] > 1 && settings.page['currentPage'] < settings.page['totalPage']) {
						//绑定首页事件
						settings.$this.find('.yzz-page-turn .firstPage').bind('click', function () {
							settings.firstPage();
						});
						//绑定上一页事件
						settings.$this.find('.yzz-page-turn .previousPage').bind('click', function () {
							settings.previousPage();
						});
						//绑定下一页事件
						settings.$this.find('.yzz-page-turn .nextPage').bind('click', function () {
							settings.nextPage();
						});
						//绑定尾页事件
						settings.$this.find('.yzz-page-turn .endPage').bind('click', function () {
							settings.endPage();
						});
					} else if (settings.page['currentPage'] == settings.page['totalPage']) {
						//绑定首页事件
						settings.$this.find('.yzz-page-turn .firstPage').bind('click', function () {
							settings.firstPage();
						});
						//绑定上一页事件
						settings.$this.find('.yzz-page-turn .previousPage').bind('click', function () {
							settings.previousPage();
						});
						settings.$this.find('.yzz-page-turn .nextPage').css({
							'cursor': 'not-allowed',
							'color': '#444'
						});
						settings.$this.find('.yzz-page-turn .endPage').css({
							'cursor': 'not-allowed',
							'color': '#444'
						});
					}

					//设置编辑事件
					if (settings.$this.find('.yzz-paging-plugin-table .yzz-edit').length > 0) {
						var $checkbox = settings.$this.find('table thead input[type=checkbox]').eq(0);
						if ($checkbox.length <= 0) {
							alert('请先设置全选复选框');
							return;
						}
						settings.$this.find('.yzz-paging-plugin-table .yzz-edit').each(function (index) {
							$(this).click(function () {
								settings.editRecord(settings.$this.find('table tr').find('.' + $checkbox.attr('class')).eq(index + 1).val());
							});
						});
					}
					//设置删除事件
					if (settings.$this.find('.yzz-paging-plugin-table .yzz-delete').length > 0) {
						var $checkbox = settings.$this.find('table thead input[type=checkbox]').eq(0);
						if ($checkbox.length <= 0) {
							alert('请先设置全选复选框');
							return;
						}
						settings.$this.find('.yzz-paging-plugin-table .yzz-delete').each(function (index) {
							$(this).click(function () {
								settings.deleteRecord(settings.$this.find('table tr').find('.' + $checkbox.attr('class')).eq(index + 1).val());
							});
						});
					}
				}
			}
		});
	}
}) (jQuery, window);
