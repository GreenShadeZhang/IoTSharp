// 定义接口来定义对象的类型
export interface TableDataRow {
	id?: string;
	provinceCode?: number;
	provinceName?: string;
	cityCode?: number;
	cityName?: string;
	districtCode?: number;
	districtName?: string;
	neighName?: string;
	addressDetail?: string;
	manager?: string;
	managerPhone?: string;
	managerEmail?: string;
	peopleNum?: number;
	olderNum?: number;
	remark: string;
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
