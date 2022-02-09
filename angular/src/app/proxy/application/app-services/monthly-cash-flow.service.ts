import { RestService } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { CreateUpdateMonthlyCashFlowDto, GetMonthlyCashFlowListDto, MonthlyCashFlowDto } from '../contracts/dtos/monthly-cash-flow/models';

@Injectable({
  providedIn: 'root',
})
export class MonthlyCashFlowService {
  apiName = 'Default';

  create = (input: CreateUpdateMonthlyCashFlowDto) =>
    this.restService.request<any, MonthlyCashFlowDto>({
      method: 'POST',
      url: '/api/app/monthly-cash-flow',
      body: input,
    },
    { apiName: this.apiName });

  delete = (id: string) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/monthly-cash-flow/${id}`,
    },
    { apiName: this.apiName });

  get = (id: string) =>
    this.restService.request<any, MonthlyCashFlowDto>({
      method: 'GET',
      url: `/api/app/monthly-cash-flow/${id}`,
    },
    { apiName: this.apiName });

  getList = (input: GetMonthlyCashFlowListDto) =>
    this.restService.request<any, PagedResultDto<MonthlyCashFlowDto>>({
      method: 'GET',
      url: '/api/app/monthly-cash-flow',
      params: { dateGTE: input.dateGTE, dateLTE: input.dateLTE, shopId: input.shopId, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName });

  update = (id: string, input: CreateUpdateMonthlyCashFlowDto) =>
    this.restService.request<any, MonthlyCashFlowDto>({
      method: 'PUT',
      url: `/api/app/monthly-cash-flow/${id}`,
      body: input,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
