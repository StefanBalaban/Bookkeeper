import { ListService, PagedResultDto } from '@abp/ng.core';
import { Confirmation, ConfirmationService } from '@abp/ng.theme.shared';
import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DailyEarningService, ShopService } from '@proxy/application/app-services';
import { DailyEarningDto } from '@proxy/application/contracts/dtos/daily-earning';
import { NgbDateNativeAdapter, NgbDateAdapter } from '@ng-bootstrap/ng-bootstrap';
import { ShopDto } from '@proxy/application/contracts/dtos/shop';
import { switchMap } from 'rxjs/operators';

@Component({
  selector: 'app-daily-earning',
  templateUrl: './daily-earning.component.html',
  styleUrls: ['./daily-earning.component.scss'],
  providers: [
    { provide: 'DAILY_EARNING_LIST', useClass: ListService },
    { provide: 'SHOP_LIST', useClass: ListService },
    { provide: NgbDateAdapter, useClass: NgbDateNativeAdapter },
  ],
})
export class DailyEarningComponent implements OnInit {
  dailyEarning = { items: [], totalCount: 0 } as PagedResultDto<DailyEarningDto>;
  form: FormGroup;
  isModalOpen = false;
  selectedDailyEarning = {} as DailyEarningDto;
  shops: ShopDto[] = [];
  sum = 0;
  query: any = {};
  filterForm: FormGroup;

  constructor(
    @Inject('DAILY_EARNING_LIST') public readonly list: ListService,
    @Inject('SHOP_LIST') public readonly shopList: ListService,
    private dailyEarningService: DailyEarningService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService, // inject the ConfirmationService
    private shopService: ShopService
  ) {}

  ngOnInit() {
    this.createFilterForm();
    const dailyEarningStreamCreator = query => this.dailyEarningService.getList({...query, ...this.query});

    this.list.hookToQuery(dailyEarningStreamCreator).subscribe(response => {
      this.dailyEarning = response;
      this.getSum();
    });

    let shopRequest = this.shopService.getList({ maxResultCount: 1, skipCount: 0 }).pipe(
      switchMap(response =>
        this.shopService.getList({
          maxResultCount: response.totalCount !== 0 ? response.totalCount : 1,
          skipCount: 0,
        })
      )
    );

    shopRequest.subscribe(response => (this.shops = response.items));
  }

  getSum() {
    this.sum = 0;
    this.dailyEarningService
      .getList({ maxResultCount: this.dailyEarning.totalCount !== 0 ? this.dailyEarning.totalCount : 1, skipCount: 0, ...this.query })
      .subscribe(x => {
        x.items.forEach(y => (this.sum += y.earningAmount));
      });
  }

  refreshFilter() {
    this.query = {};
    this.createFilterForm();
    this.list.get();
  }
  filter() {
    this.query.shopId = this.filterForm.controls['shopId'].value || '';
    this.query.dateLTE = this.filterForm.controls['dateLTE'].value
      ? new Date(this.filterForm.controls['dateLTE'].value).toDateString()
      : '';
    this.query.dateGTE = this.filterForm.controls['dateGTE'].value
      ? new Date(this.filterForm.controls['dateGTE'].value).toDateString()
      : '';
    this.list.get();
  }

  createFilterForm() {
    this.filterForm = this.fb.group({
      dateGTE: [],
      dateLTE: [],
      shopId: [''],
    });
  }

  getShopName(shopId) {
    return this.shops.find(x => x.id === shopId)?.name;
  }

  createDailyEarning() {
    this.selectedDailyEarning = {} as DailyEarningDto;
    this.buildForm(); // add this line
    this.isModalOpen = true;
  }

  editDailyEarning(id: string) {
    this.dailyEarningService.get(id).subscribe(DailyEarning => {
      this.selectedDailyEarning = DailyEarning;
      this.buildForm();
      this.isModalOpen = true;
    });
  }

  // Add a delete method
  delete(id: string) {
    this.confirmation.warn('::AreYouSureToDelete', '::AreYouSure').subscribe(status => {
      if (status === Confirmation.Status.confirm) {
        this.dailyEarningService.delete(id).subscribe(() => this.list.get());
      }
    });
  }

  buildForm() {
    this.form = this.fb.group({
      date: [
        this.selectedDailyEarning.date ? new Date(this.selectedDailyEarning.date) : null,
        Validators.required,
      ],
      earningAmount: [this.selectedDailyEarning.earningAmount || 0, Validators.required],
      shopId: [this.selectedDailyEarning.shopId || '', Validators.required],
    });
  }

  save() {
    if (this.form.invalid) {
      return;
    }

    const request = this.selectedDailyEarning.id
      ? this.dailyEarningService.update(this.selectedDailyEarning.id, this.form.value)
      : this.dailyEarningService.create(this.form.value);

    request.subscribe(() => {
      this.isModalOpen = false;
      this.form.reset();
      this.list.get();
    });
  }
}
