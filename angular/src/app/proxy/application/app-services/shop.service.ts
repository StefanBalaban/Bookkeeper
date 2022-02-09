import { RestService } from '@abp/ng.core';
import type { PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { CreateUpdateShopDto, ShopDto } from '../contracts/dtos/shop/models';

@Injectable({
  providedIn: 'root',
})
export class ShopService {
  apiName = 'Default';

  create = (input: CreateUpdateShopDto) =>
    this.restService.request<any, ShopDto>({
      method: 'POST',
      url: '/api/app/shop',
      body: input,
    },
    { apiName: this.apiName });

  delete = (id: string) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/shop/${id}`,
    },
    { apiName: this.apiName });

  get = (id: string) =>
    this.restService.request<any, ShopDto>({
      method: 'GET',
      url: `/api/app/shop/${id}`,
    },
    { apiName: this.apiName });

  getList = (input: PagedAndSortedResultRequestDto) =>
    this.restService.request<any, PagedResultDto<ShopDto>>({
      method: 'GET',
      url: '/api/app/shop',
      params: { skipCount: input.skipCount, maxResultCount: input.maxResultCount, sorting: input.sorting },
    },
    { apiName: this.apiName });

  update = (id: string, input: CreateUpdateShopDto) =>
    this.restService.request<any, ShopDto>({
      method: 'PUT',
      url: `/api/app/shop/${id}`,
      body: input,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
