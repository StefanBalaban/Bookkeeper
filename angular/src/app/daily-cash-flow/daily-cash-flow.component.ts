import { ListService, PagedResultDto } from '@abp/ng.core';
import { Confirmation, ConfirmationService } from '@abp/ng.theme.shared';
import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {
  DailyCashFlowService,
  DailyEarningService,
  ExpenseService,
  MonthlyCashFlowService,
  ShopService,
} from '@proxy/application/app-services';
import { DailyCashFlowDto } from '@proxy/application/contracts/dtos/daily-cash-flow';
import { ShopDto } from '@proxy/application/contracts/dtos/shop';
import { NgbDateNativeAdapter, NgbDateAdapter } from '@ng-bootstrap/ng-bootstrap';
import { MonthlyCashFlowDto } from '@proxy/application/contracts/dtos/monthly-cash-flow';
import { DailyEarning } from '@proxy/application/contracts/dtos';
import { switchMap } from 'rxjs/operators';
import { ExpenseDto } from '@proxy/application/contracts/dtos/expense';
import { DailyEarningDto } from '@proxy/application/contracts/dtos/daily-earning';

@Component({
  selector: 'app-daily-cash-flow',
  templateUrl: './daily-cash-flow.component.html',
  styleUrls: ['./daily-cash-flow.component.scss'],
  providers: [
    { provide: 'DAILY_CASH_FLOW_LIST', useClass: ListService },
    { provide: NgbDateAdapter, useClass: NgbDateNativeAdapter },
  ],
})
export class DailyCashFlowComponent implements OnInit {
  dailyCashFlow = { items: [], totalCount: 0 } as PagedResultDto<DailyCashFlowDto>;
  form: FormGroup;
  isModalOpen = false;
  selectedDailyCashFlow = {} as DailyCashFlowDto;
  shops: ShopDto[] = [];
  sum = 0;
  query: any = {};
  filterForm: FormGroup;

  constructor(
    @Inject('DAILY_CASH_FLOW_LIST') public readonly list: ListService,
    private dailyCashFlowService: DailyCashFlowService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService, // inject the ConfirmationService
    private shopService: ShopService,
  ) {}

  ngOnInit() {
    let shopRequest = this.shopService.getList({ maxResultCount: 1, skipCount: 0 }).pipe(
      switchMap(response =>
        this.shopService.getList({
          maxResultCount: response.totalCount !== 0 ? response.totalCount : 1,
          skipCount: 0,
        })
      )
    );

    shopRequest.subscribe(response => (this.shops = response.items));

    this.createFilterForm();
    const dailyCashFlowStreamCreator = query =>
      this.dailyCashFlowService.getList({ ...query, ...this.query });

    this.list.hookToQuery(dailyCashFlowStreamCreator).subscribe(response => {
      this.dailyCashFlow = response;
      this.getSum();
    });
  }

  getSum() {
    this.sum = 0;
    this.dailyCashFlowService
      .getList({
        maxResultCount: this.dailyCashFlow.totalCount !== 0 ? this.dailyCashFlow.totalCount : 1,
        skipCount: 0,
        ...this.query,
      })
      .subscribe(response => {
        response.items.forEach(dailyCashFlow => {
          this.sum += dailyCashFlow.sum
        });
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

  getMonthlyCashFlowDate(dailyCashFlowId) {
    if (!dailyCashFlowId) return '';
    const date = new Date(this.dailyCashFlow.items.find(x => x.id == dailyCashFlowId)?.date);
    return ` ${date?.getUTCMonth() + 1}.${date?.getFullYear()}.`;
  }

  createDailyCashFlow() {
    this.selectedDailyCashFlow = {};
    this.buildForm(); // add this line
    this.isModalOpen = true;
  }

  editDailyCashFlow(id: string) {
    this.dailyCashFlowService.get(id).subscribe(dailyCashFlow => {
      this.selectedDailyCashFlow = dailyCashFlow;
      this.buildForm();
      this.isModalOpen = true;
    });
  }

  // Add a delete method
  delete(id: string) {
    this.confirmation.warn('::AreYouSureToDelete', '::AreYouSure').subscribe(status => {
      if (status === Confirmation.Status.confirm) {
        this.dailyCashFlowService.delete(id).subscribe(() => this.list.get());
      }
    });
  }

  buildForm() {
    this.form = this.fb.group({
      date: [
        this.selectedDailyCashFlow.date ? new Date(this.selectedDailyCashFlow.date) : null,
        Validators.required,
      ],
      shopId: [this.selectedDailyCashFlow.shopId || '', Validators.required],
    });
  }

  save() {
    if (this.form.invalid) {
      return;
    }

    const request = this.selectedDailyCashFlow.id
      ? this.dailyCashFlowService.update(this.selectedDailyCashFlow.id, this.form.value)
      : this.dailyCashFlowService.create(this.form.value);

    request.subscribe(() => {
      this.isModalOpen = false;
      this.form.reset();
      this.list.get();
    });
  }
}
