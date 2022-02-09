import { RestService } from '@abp/ng.core';
import type { PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { CreateUpdateExpenseTypeDto, ExpenseTypeDto } from '../contracts/dtos/expense-type/models';

@Injectable({
  providedIn: 'root',
})
export class ExpenseTypeService {
  apiName = 'Default';

  create = (input: CreateUpdateExpenseTypeDto) =>
    this.restService.request<any, ExpenseTypeDto>({
      method: 'POST',
      url: '/api/app/expense-type',
      body: input,
    },
    { apiName: this.apiName });

  delete = (id: string) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/expense-type/${id}`,
    },
    { apiName: this.apiName });

  get = (id: string) =>
    this.restService.request<any, ExpenseTypeDto>({
      method: 'GET',
      url: `/api/app/expense-type/${id}`,
    },
    { apiName: this.apiName });

  getList = (input: PagedAndSortedResultRequestDto) =>
    this.restService.request<any, PagedResultDto<ExpenseTypeDto>>({
      method: 'GET',
      url: '/api/app/expense-type',
      params: { skipCount: input.skipCount, maxResultCount: input.maxResultCount, sorting: input.sorting },
    },
    { apiName: this.apiName });

  update = (id: string, input: CreateUpdateExpenseTypeDto) =>
    this.restService.request<any, ExpenseTypeDto>({
      method: 'PUT',
      url: `/api/app/expense-type/${id}`,
      body: input,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
