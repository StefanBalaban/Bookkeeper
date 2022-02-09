import type { SalaryType } from '../../../../entities/employee/salary-type.enum';
import type { AuditedEntityDto } from '@abp/ng.core';

export interface CreateUpdateEmployeeDto {
  name: string;
  salaryType: SalaryType;
  salaryAmount: number;
  active: boolean;
  shopId: string;
}

export interface EmployeeDto extends AuditedEntityDto<string> {
  name?: string;
  salaryType: SalaryType;
  salaryAmount: number;
  active: boolean;
  shopId?: string;
}
