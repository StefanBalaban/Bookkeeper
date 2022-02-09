import type { AuditedEntityDto } from '@abp/ng.core';

export interface CreateUpdateShopDto {
  name: string;
  address?: string;
}

export interface ShopDto extends AuditedEntityDto<string> {
  name?: string;
  address?: string;
}
