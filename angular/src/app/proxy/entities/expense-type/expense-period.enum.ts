import { mapEnumToOptions } from '@abp/ng.core';

export enum ExpensePeriod {
  Dnevni = 0,
  Mjesecni = 1,
}

export const expensePeriodOptions = mapEnumToOptions(ExpensePeriod);
