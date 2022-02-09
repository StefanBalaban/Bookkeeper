import { RestService } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { CreateUpdateDailyEarningDto, DailyEarningDto, GetDailyEarningListDto } from '../contracts/dtos/daily-earning/models';

@Injectable({
  providedIn: 'root',
})
export class DailyEarningService {
  apiName = 'Default';

  create = (input: CreateUpdateDailyEarningDto) =>
    this.restService.request<any, DailyEarningDto>({
      method: 'POST',
      url: '/api/app/daily-earning',
      body: input,
    },
    { apiName: this.apiName });

  delete = (id: string) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/daily-earning/${id}`,
    },
    { apiName: this.apiName });

  get = (id: string) =>
    this.restService.request<any, DailyEarningDto>({
      method: 'GET',
      url: `/api/app/daily-earning/${id}`,
    },
    { apiName: this.apiName });

  getList = (input: GetDailyEarningListDto) =>
    this.restService.request<any, PagedResultDto<DailyEarningDto>>({
      method: 'GET',
      url: '/api/app/daily-earning',
      params: { dateGTE: input.dateGTE, dateLTE: input.dateLTE, shopId: input.shopId, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName });

  update = (id: string, input: CreateUpdateDailyEarningDto) =>
    this.restService.request<any, DailyEarningDto>({
      method: 'PUT',
      url: `/api/app/daily-earning/${id}`,
      body: input,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
