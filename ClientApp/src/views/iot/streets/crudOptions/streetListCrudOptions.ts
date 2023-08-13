import { streetApi } from '/@/api/streets';
import _ from 'lodash-es';
import { TableDataRow } from '../model/streetListModel';
import { ElMessage } from 'element-plus';
import { compute } from '@fast-crud/fast-crud';
export const createStreetListCrudOptions = function ({ expose }, customerId,tenantId) {
    let records: any[] = [];
    const FsButton = {
        link: true,
    };
    const customSwitchComponent = {
        activeColor: 'var(--el-color-primary)',
        inactiveColor: 'var(el-switch-of-color)',
    };
    const requiredCustomSwitchComponent = {
        required: 'required',
        activeColor: 'var(--el-color-primary)',
        inactiveColor: 'var(el-switch-of-color)',
    };
    const pageRequest = async (query) => {

        const params = reactive({
			offset: query.page.currentPage - 1,
			limit: query.page.pageSize,
			customerId,
            tenantId,
			neighName: query.form.neighName ?? '',
            manager: query.form.manager ?? '',
            managerPhone: query.form.managerPhone ?? '',
		});

        let {
            form: { userName: name },
            page: { currentPage: currentPage, pageSize: limit },
        } = query;

        let offset = currentPage === 1 ? 0 : currentPage - 1;
        //const res = await streetApi().streetList({ name, limit, offset, customerId ,neighName: query.form.neighName ?? '',});

        const res = await streetApi().streetList(params);
        return {
            records: res.data.rows,
            currentPage: currentPage,
            pageSize: limit,
            total: res.data.total,
        };
    };
    const editRequest = async ({ form, row }) => {
        form.id = row.id;
        try {
            await streetApi().putStreet(form);
            return form;
        } catch (e) {
            ElMessage.error(e.response.msg);
        }
    };
    const delRequest = async ({ row }) => {
        try {
            await streetApi().deleteStreet(row.id);
            _.remove(records, (item: TableDataRow) => {
                return item.id === row.id;
            });
        } catch (e) {
            ElMessage.error(e.response.msg);
        }
    };
    const addRequest = async ({ form }) => {
        try {
            //验证
            var neighName = form.neighName;
            var managerEmail = form.managerEmail;
            if (neighName) {
                if (managerEmail) {
                    await streetApi().postStreet({
                        ...form,
                        customerId,
                    });
                    records.push(form);
                    return form;
                    // const regEmail = /^([a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+(\.[a-zA-Z0-9_-])+/;
                    // if (regEmail.test(managerEmail)) {
                    //     await streetApi().postStreet({
                    //         ...form,
                    //         customerId,
                    //     });
                    //     records.push(form);
                    //     return form;
                    // } else {
                    //     ElMessage.error('请输入合法邮箱');
                    // }
                } else {
                    ElMessage.error('请填写邮箱');
                }
            } else {
                ElMessage.error('请填写小区名称');
            }
        } catch (e) {
            ElMessage.error(e.response.msg);
        }
    };
    return {
        crudOptions: {
            request: {
                pageRequest,
                addRequest,
                delRequest,
                editRequest,
            },
            table: {
                border: false,
            },
            actionbar: {
                buttons: {
                    add: {

                    },
                },
            },
            form: {
                labelWidth: '80px', //
                beforeSubmit: function (subParam) {
                    var form = subParam.form;
                    if (subParam.mode == 'add') {
                        //验证
                        var neighName = form.neighName;
                        var managerEmail = form.managerEmail;
                        if (neighName) {
                            if (managerEmail) {
                                const regEmail = /^([a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+(\.[a-zA-Z0-9_-])+/;
                                if (regEmail.test(managerEmail)) {
                                   //需要验证是否重复

                                } else {
                                    //ElMessage.error('请输入合法邮箱');
                                    //throw new Error('请输入合法邮箱');
                                }
                            } else {
                                ElMessage.error('请填写邮箱');
                                throw new Error('请填写邮箱');
                            }
                        } else {
                            ElMessage.error('请填写小区名');
                            throw new Error('请填写小区名');
                        }
                    }
                }
            },
            search: {
                show: true,
            },
            rowHandle: {
                width: 180,
                buttons: {
                    view: {
                        icon: 'View',
                        ...FsButton,
                        show: false,
                    },
                    edit: {
                        icon: 'EditPen',
                        ...FsButton,
                        order: 1,
                    },
                    remove: {
                        icon: 'Delete',
                        ...FsButton,
                        order: 2,
                    }, //删除按钮
                },
            },
            columns: {
                neighName: {
                    title: '小区名称',
                    type: 'text',
                    column: { width: 140 },
                    search: { show: true }, //显示查询
                    addForm: {
                        show: true,
                    },
                    editForm: {
                        show: true,
                    },
                },
                addressDetail: {
                    title: '小区地址',
                    type: 'text',
                    column: { width: 240 },
                    addForm: {
                        show: true,
                    },
                    editForm: {
                        show: true,
                    },
                },
                //id: {
                //	title: 'Id',
                //	type: 'text',
                //	column: { width: 300 },
                //	addForm: {
                //		show: false,
                //	},
                //	editForm: {
                //		show: false,
                //	},
                //},
                manager: {
                    title: '负责人',
                    type: 'text',
                    column: { width: 100 },
                    search: { show: true }, //显示查询
                    addForm: {
                        show: true
                    },
                    editForm: {
                        show: true
                    },
                },
                managerPhone: {
                    title: '负责人电话号码',
                    column: { width: 140 },
                    search: { show: true }, //显示查询
                    type: 'text',
                    addForm: {
                        show: true,
                        component: requiredCustomSwitchComponent,
                    },
                    editForm: {
                        show: true,
                        component: requiredCustomSwitchComponent,
                    },
                },
                managerEmail: {
                    title: '负责人邮箱',
                    column: { width: 200 },
                    type: 'text',
                    addForm: {
                        show: true,
                        component: requiredCustomSwitchComponent,
                    },
                    editForm: {
                        show: true,
                        component: requiredCustomSwitchComponent,
                    },
                },
                peopleNum: {
                    title: '小区人数',
                    column: { width: 100 },
                    type: 'text',
                    addForm: {
                        show: true,
                    },
                    editForm: {
                        show: true,
                    },
                },
                olderNum: {
                    title: '康养老人数',
                    column: { width: 100 },
                    addForm: {
                        show: true,
                    },
                    editForm: {
                        show: true,
                    },
                },
                remark: {
                    title: '简介',
                    column: { width: 140 },
                    type: 'text',
                    addForm: {
                        show: true,
                    },
                    editForm: {
                        show: true,
                    },
                },
            },
        },
    };
};
