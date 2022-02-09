import { mapEnumToOptions } from '@abp/ng.core';

export enum SalaryType {
  Dnevnica = 0,
  Mjesecno = 1,
}

export const salaryTypeOptions = mapEnumToOptions(SalaryType);
