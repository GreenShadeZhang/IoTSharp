import { getAlarmList } from '/@/api/alarm';
import _ from 'lodash-es';
import { TableDataRow } from '../model/radaralarmListModel';
import { ElMessage } from 'element-plus';
import { compute } from '@fast-crud/fast-crud';
export const createRadarAlarmListCrudOptions = function ({ expose }, customerId,tenantId) {
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

        const res = await getAlarmList(params);
        return {
            records: res.data.rows,
            currentPage: currentPage,
            pageSize: limit,
            total: res.data.total,
        };
    };

    return {
        crudOptions: {
            request: {
                pageRequest
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
                alarmTime: {
                    title: '告警时间',
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
                alarmType: {
                    title: '异常类型',
                    type: 'text',
                    column: { width: 100 },
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
                uid: {
                    title: '告警设备',
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
                eqtName: {
                    title: '设备名称',
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

                alarmMsg: {
                    title: '告警描述',
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
                processingResult: {
                    title: '处理结果',
                    column: { width: 100 },
                    type: 'text',
                    addForm: {
                        show: true,
                    },
                    editForm: {
                        show: true,
                    },
                },
                processingOpinions: {
                    title: '处理意见',
                    column: { width: 100 },
                    type: 'text',
                    addForm: {
                        show: true,
                    },
                    editForm: {
                        show: true,
                    },
                },
                processingPeople: {
                    title: '处理人',
                    column: { width: 100 },
                    addForm: {
                        show: true,
                    },
                    editForm: {
                        show: true,
                    },
                },
                processingTime: {
                    title: '处理时间',
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
