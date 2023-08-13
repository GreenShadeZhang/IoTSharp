// 定义接口来定义对象的类型
export interface TableDataRow {
	id?: string;
	alarmType?: string;
	alarmDetail?: string;
	ackDateTime?: Date;
	clearDateTime?: Date;
	startDateTime?: Date;
	endDateTime?: Date;
	alarmStatus?: string;
	serverity?: string;
	propagate?: true;
	originatorId?: string;
	originatorType?: string;
	originator?: string;
	alarmId?: number;
	alarmTime: Date;
	alarmMsg?: string;
	eqtTypeId?: string;
	uid?: string;
	eqtName?: string;
	processingResult: string;

	processingOpinions?: string;
	processingPeople?: number;
	processingTime?: string;
	deptId: number;
}

export interface TableDataState {
	tableData: {
		rows: Array<TableDataRow>;
		total: number;
		loading: boolean;
		param: {
			pageNum: number;
			pageSize: number;
		};
	};
}
