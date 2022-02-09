import type { AuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface CreateUpdateMonthlyCashFlowDto {
  date: string;
  shopId: string;
}

export interface GetMonthlyCashFlowListDto extends PagedAndSortedResultRequestDto {
  dateGTE?: string;
  dateLTE?: string;
  shopId?: string;
}

export interface MonthlyCashFlowDto extends AuditedEntityDto<string> {
  date?: string;
  shopId?: string;
}
