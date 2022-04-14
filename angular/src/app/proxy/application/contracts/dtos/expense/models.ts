import type { AuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';
import type { ExpenseCategory } from '../../../../entities/expense-type/expense-category.enum';

export interface CreateUpdateExpenseDto {
  expenseTypeId: string;
  date: string;
  shopId: string;
  employeeId?: string;
  amount: number;
}

export interface ExpenseDto extends AuditedEntityDto<string> {
  expenseTypeId?: string;
  date?: string;
  shopId?: string;
  monthlyCashFlowId?: string;
  dailyCashFlowId?: string;
  amount: number;
  employeeId?: string;
}

export interface GetExpenseListDto extends PagedAndSortedResultRequestDto {
  expenseTypeId?: string;
  dateGTE?: string;
  dateLTE?: string;
  shopId?: string;
  employeeId?: string;
  amount?: number;
  expenseCategory?: ExpenseCategory;
}
