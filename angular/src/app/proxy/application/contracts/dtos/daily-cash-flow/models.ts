import type { AuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface CreateUpdateDailyCashFlowDto {
  date: string;
  shopId: string;
}

export interface DailyCashFlowDto extends AuditedEntityDto<string> {
  date?: string;
  shopId?: string;
  monthlyCashFlowId?: string;
}

export interface GetDailyCashFlowListDto extends PagedAndSortedResultRequestDto {
  dateGTE?: string;
  dateLTE?: string;
  shopId?: string;
}
