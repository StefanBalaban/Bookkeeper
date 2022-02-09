import type { ExpenseCategory } from '../../../../entities/expense-type/expense-category.enum';
import type { ExpensePeriod } from '../../../../entities/expense-type/expense-period.enum';
import type { AuditedEntityDto } from '@abp/ng.core';

export interface CreateUpdateExpenseTypeDto {
  name: string;
  expenseCategory: ExpenseCategory;
  expensePeriod: ExpensePeriod;
}

export interface ExpenseTypeDto extends AuditedEntityDto<string> {
  name?: string;
  expenseCategory: ExpenseCategory;
  expensePeriod: ExpensePeriod;
}
