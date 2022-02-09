import { RestService } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { CreateUpdateDailyCashFlowDto, DailyCashFlowDto, GetDailyCashFlowListDto } from '../contracts/dtos/daily-cash-flow/models';

@Injectable({
  providedIn: 'root',
})
export class DailyCashFlowService {
  apiName = 'Default';

  create = (input: CreateUpdateDailyCashFlowDto) =>
    this.restService.request<any, DailyCashFlowDto>({
      method: 'POST',
      url: '/api/app/daily-cash-flow',
      body: input,
    },
    { apiName: this.apiName });

  delete = (id: string) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/daily-cash-flow/${id}`,
    },
    { apiName: this.apiName });

  get = (id: string) =>
    this.restService.request<any, DailyCashFlowDto>({
      method: 'GET',
      url: `/api/app/daily-cash-flow/${id}`,
    },
    { apiName: this.apiName });

  getList = (input: GetDailyCashFlowListDto) =>
    this.restService.request<any, PagedResultDto<DailyCashFlowDto>>({
      method: 'GET',
      url: '/api/app/daily-cash-flow',
      params: { dateGTE: input.dateGTE, dateLTE: input.dateLTE, shopId: input.shopId, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName });

  update = (id: string, input: CreateUpdateDailyCashFlowDto) =>
    this.restService.request<any, DailyCashFlowDto>({
      method: 'PUT',
      url: `/api/app/daily-cash-flow/${id}`,
      body: input,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
