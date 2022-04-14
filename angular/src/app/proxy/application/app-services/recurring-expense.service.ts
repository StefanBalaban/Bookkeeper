import { RestService } from '@abp/ng.core';
import type { PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { CreateUpdateRecurringExpenseDto, RecurringExpenseDto } from '../contracts/dtos/recurring-expense/models';

@Injectable({
  providedIn: 'root',
})
export class RecurringExpenseService {
  apiName = 'Default';

  create = (input: CreateUpdateRecurringExpenseDto) =>
    this.restService.request<any, RecurringExpenseDto>({
      method: 'POST',
      url: '/api/app/recurring-expense',
      body: input,
    },
    { apiName: this.apiName });

  delete = (id: string) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/recurring-expense/${id}`,
    },
    { apiName: this.apiName });

  get = (id: string) =>
    this.restService.request<any, RecurringExpenseDto>({
      method: 'GET',
      url: `/api/app/recurring-expense/${id}`,
    },
    { apiName: this.apiName });

  getList = (input: PagedAndSortedResultRequestDto) =>
    this.restService.request<any, PagedResultDto<RecurringExpenseDto>>({
      method: 'GET',
      url: '/api/app/recurring-expense',
      params: { skipCount: input.skipCount, maxResultCount: input.maxResultCount, sorting: input.sorting },
    },
    { apiName: this.apiName });

  update = (id: string, input: CreateUpdateRecurringExpenseDto) =>
    this.restService.request<any, RecurringExpenseDto>({
      method: 'PUT',
      url: `/api/app/recurring-expense/${id}`,
      body: input,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
