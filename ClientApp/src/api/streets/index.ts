import { IListQueryParam } from '../iapiresult';
import request from '/@/utils/request';

/**
 * 小区api接口集合
 * @method streetList 小区列表
 * @method getStreet 获取小区
 * @method postStreet 新增小区
 * @method putStreet 修改小区
 * @method deleteStreet 删除小区
 */
export function streetApi() {
	return {
		streetList: (params: CustomerQueryParam) => {
			return request({
				url: `/api/Streets`,
				method: 'post',
				data: params,
			});
		},
		getStreet: (id: string) => {
			return request({
				url: '/api/Streets/' + id,
				method: 'get',
			});
		},

		postStreet: (params: any) => {
			return request({
				url: '/api/Streets',
				method: 'post',
				data: params,
			});
		},

		putStreet: (data: any) => {
			return request({
				url: '/api/Streets'+ data.id,
				method: 'put',
				data: data,
			});
		},
		deleteStreet: (id: string) => {
			return request({
				url: '/api/Streets/' + id,
				method: 'delete',
				data: {},
			});
		},
	};
}

export interface CustomerQueryParam extends IListQueryParam {
	name?: string;
	customerId?: string;
}
