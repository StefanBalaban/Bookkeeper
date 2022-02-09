import { ListService, PagedResultDto } from '@abp/ng.core';
import { Confirmation, ConfirmationService } from '@abp/ng.theme.shared';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ShopService } from '@proxy/application/app-services';
import { ShopDto } from '@proxy/application/contracts/dtos/shop';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss'],
  providers: [ListService],
})
export class ShopComponent implements OnInit {
  shop = { items: [], totalCount: 0 } as PagedResultDto<ShopDto>;
  form: FormGroup;
  isModalOpen = false;
  selectedShop = {} as ShopDto;

  constructor(
    public readonly list: ListService,
    private shopService: ShopService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService // inject the ConfirmationService
  ) {}

  ngOnInit() {
    const shopStreamCreator = query => this.shopService.getList(query);

    this.list.hookToQuery(shopStreamCreator).subscribe(response => {
      this.shop = response;
    });
  }

  createShop() {
    this.selectedShop = {};
    this.buildForm(); // add this line
    this.isModalOpen = true;
  }

  editShop(id: string) {
    this.shopService.get(id).subscribe(shop => {
      this.selectedShop = shop;
      this.buildForm();
      this.isModalOpen = true;
    });
  }

  // Add a delete method
  delete(id: string) {
    this.confirmation.warn('::AreYouSureToDelete', '::AreYouSure').subscribe(status => {
      if (status === Confirmation.Status.confirm) {
        this.shopService.delete(id).subscribe(() => this.list.get());
      }
    });
  }

  buildForm() {
    this.form = this.fb.group({
      name: [this.selectedShop.name || '', Validators.required],
      address: [this.selectedShop.address || '', Validators.required],
    });
  }

  save() {
    if (this.form.invalid) {
      return;
    }

    const request = this.selectedShop.id
      ? this.shopService.update(this.selectedShop.id, this.form.value)
      : this.shopService.create(this.form.value);

    request.subscribe(() => {
      this.isModalOpen = false;
      this.form.reset();
      this.list.get();
    });
  }
}
