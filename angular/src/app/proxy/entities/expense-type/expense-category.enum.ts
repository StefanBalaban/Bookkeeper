import { mapEnumToOptions } from '@abp/ng.core';

export enum ExpenseCategory {
  Materijalni = 0,
  Transport = 1,
  Ostali = 2,
  Plata = 3,
}

export const expenseCategoryOptions = mapEnumToOptions(ExpenseCategory);
