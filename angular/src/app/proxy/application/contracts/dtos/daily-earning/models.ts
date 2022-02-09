import type { AuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface CreateUpdateDailyEarningDto {
  date: string;
  shopId: string;
  earningAmount: number;
}

export interface DailyEarningDto extends AuditedEntityDto<string> {
  date?: string;
  shopId?: string;
  earningAmount: number;
  dailyCashFlowId?: string;
}

export interface GetDailyEarningListDto extends PagedAndSortedResultRequestDto {
  dateGTE?: string;
  dateLTE?: string;
  shopId?: string;
}
