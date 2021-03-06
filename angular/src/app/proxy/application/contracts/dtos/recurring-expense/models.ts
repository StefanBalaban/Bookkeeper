import type { AuditedEntityDto } from '@abp/ng.core';

export interface CreateUpdateRecurringExpenseDto {
  expenseTypeId?: string;
  shopId?: string;
  amount: number;
  active: boolean;
}

export interface RecurringExpenseDto extends AuditedEntityDto<string> {
  expenseTypeId?: string;
  shopId?: string;
  amount: number;
  active: boolean;
}
