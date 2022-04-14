import { RestService } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { CreateUpdateExpenseDto, ExpenseDto, GetExpenseListDto } from '../contracts/dtos/expense/models';

@Injectable({
  providedIn: 'root',
})
export class ExpenseService {
  apiName = 'Default';

  create = (input: CreateUpdateExpenseDto) =>
    this.restService.request<any, ExpenseDto>({
      method: 'POST',
      url: '/api/app/expense',
      body: input,
    },
    { apiName: this.apiName });

  delete = (id: string) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/expense/${id}`,
    },
    { apiName: this.apiName });

  get = (id: string) =>
    this.restService.request<any, ExpenseDto>({
      method: 'GET',
      url: `/api/app/expense/${id}`,
    },
    { apiName: this.apiName });

  getList = (input: GetExpenseListDto) =>
    this.restService.request<any, PagedResultDto<ExpenseDto>>({
      method: 'GET',
      url: '/api/app/expense',
      params: { expenseTypeId: input.expenseTypeId, dateGTE: input.dateGTE, dateLTE: input.dateLTE, shopId: input.shopId, employeeId: input.employeeId, amount: input.amount, expenseCategory: input.expenseCategory, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName });

  update = (id: string, input: CreateUpdateExpenseDto) =>
    this.restService.request<any, ExpenseDto>({
      method: 'PUT',
      url: `/api/app/expense/${id}`,
      body: input,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
