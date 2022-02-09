import { ListService, PagedResultDto } from '@abp/ng.core';
import { Confirmation, ConfirmationService } from '@abp/ng.theme.shared';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {
  DailyCashFlowService,
  DailyEarningService,
  ExpenseService,
  MonthlyCashFlowService,
  ShopService,
} from '@proxy/application/app-services';
import { MonthlyCashFlowDto } from '@proxy/application/contracts/dtos/monthly-cash-flow';
import { ShopDto } from '@proxy/application/contracts/dtos/shop';
import { NgbDateNativeAdapter, NgbDateAdapter } from '@ng-bootstrap/ng-bootstrap';
import { DailyEarningDto } from '@proxy/application/contracts/dtos/daily-earning';
import { ExpenseDto } from '@proxy/application/contracts/dtos/expense';
import { DailyCashFlowDto } from '@proxy/application/contracts/dtos/daily-cash-flow';
import { switchMap } from 'rxjs/operators';

@Component({
  selector: 'app-monthly-cash-flow',
  templateUrl: './monthly-cash-flow.component.html',
  styleUrls: ['./monthly-cash-flow.component.scss'],
  providers: [ListService, { provide: NgbDateAdapter, useClass: NgbDateNativeAdapter }],
})
export class MonthlyCashFlowComponent implements OnInit {
  monthlyCashFlow = { items: [], totalCount: 0 } as PagedResultDto<MonthlyCashFlowDto>;
  form: FormGroup;
  isModalOpen = false;
  selectedMonthlyCashFlow = {} as MonthlyCashFlowDto;
  shops: ShopDto[] = [];
  expenses: ExpenseDto[] = [];
  dailyEarnings: DailyEarningDto[] = [];
  dailyCashFlows: DailyCashFlowDto[] = [];
  expenseRequestFinished = false;
  dailyEarningRequestFinished = false;
  dailyCashFlowRequestFinished = false;
  sum = 0;
  query: any = {};
  filterForm: FormGroup;

  constructor(
    public readonly list: ListService,
    private monthlyCashFlowService: MonthlyCashFlowService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService, // inject the ConfirmationService
    private shopService: ShopService,
    private dailyCashFlowService: DailyCashFlowService,
    private expenseService: ExpenseService,
    private dailyEarningService: DailyEarningService
  ) {}

  ngOnInit() {
    var shopRequest = this.shopService.getList({ maxResultCount: 1, skipCount: 0 }).pipe(
      switchMap(response =>
        this.shopService.getList({
          maxResultCount: response.totalCount !== 0 ? response.totalCount : 1,
          skipCount: 0,
        })
      )
    );

    shopRequest.subscribe(response => (this.shops = response.items));

    var dailyCashFlowRequest = this.dailyCashFlowService
      .getList({ maxResultCount: 1, skipCount: 0 })
      .pipe(
        switchMap(response =>
          this.dailyCashFlowService.getList({
            maxResultCount: response.totalCount !== 0 ? response.totalCount : 1,
            skipCount: 0,
          })
        )
      );

    dailyCashFlowRequest.subscribe(response => {
      this.dailyCashFlows = response.items;
      this.dailyCashFlowRequestFinished = true;
    });

    var expenseRequest = this.expenseService.getList({ maxResultCount: 1, skipCount: 0 }).pipe(
      switchMap(response =>
        this.expenseService.getList({
          maxResultCount: response.totalCount !== 0 ? response.totalCount : 1,
          skipCount: 0,
        })
      )
    );

    expenseRequest.subscribe(response => {
      this.expenses = response.items;
      this.expenseRequestFinished = true;
    });

    var dailyEarningRequest = this.dailyEarningService
      .getList({ maxResultCount: 1, skipCount: 0 })
      .pipe(
        switchMap(response =>
          this.dailyEarningService.getList({
            maxResultCount: response.totalCount !== 0 ? response.totalCount : 1,
            skipCount: 0,
          })
        )
      );

    dailyEarningRequest.subscribe(response => {
      this.dailyEarnings = response.items;
      this.dailyEarningRequestFinished = true;
    });

    this.createFilterForm();
    const monthlyCashFlowStreamCreator = query =>
      this.monthlyCashFlowService.getList({ ...query, ...this.query });

    this.list.hookToQuery(monthlyCashFlowStreamCreator).subscribe(response => {
      this.monthlyCashFlow = response;
      this.getSum();
    });
  }

  getMonthlyCashFlowDate(id) {
    if (!id) return '';
    const date = new Date(this.monthlyCashFlow.items.find(x => x.id == id)?.date);
    return ` ${date?.getUTCMonth() + 1}.${date?.getFullYear()}.`;
  }

  getMonthlyCashFlowSum(monthlyCashFlowId) {
    let sum = 0;
    this.dailyCashFlows
      .filter(dailyCashFlow => dailyCashFlow.monthlyCashFlowId === monthlyCashFlowId)
      .forEach(dailyCashFlow => {
        this.expenses
          .filter(expense => expense.dailyCashFlowId === dailyCashFlow.id)
          .forEach(expense => (sum -= expense.amount));
        this.dailyEarnings
          .filter(dailyEarning => dailyEarning.dailyCashFlowId === dailyCashFlow.id)
          .forEach(dailyEarning => (sum += dailyEarning.earningAmount));
      });

    this.expenses
      .filter(x => x.monthlyCashFlowId === monthlyCashFlowId)
      .forEach(x => (sum -= x.amount));

    return sum;
  }

  getSum() {
    this.sum = 0;
    this.monthlyCashFlowService
      .getList({
        maxResultCount: this.monthlyCashFlow.totalCount !== 0 ? this.monthlyCashFlow.totalCount : 1,
        skipCount: 0,
        ...this.query,
      })
      .subscribe(response => {
        response.items.forEach(monthlyCashFlow => {
          this.sum += this.getMonthlyCashFlowSum(monthlyCashFlow.id);
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

  createMonthlyCashFlow() {
    this.selectedMonthlyCashFlow = {};
    this.buildForm(); // add this line
    this.isModalOpen = true;
  }

  editMonthlyCashFlow(id: string) {
    this.monthlyCashFlowService.get(id).subscribe(monthlyCashFlow => {
      this.selectedMonthlyCashFlow = monthlyCashFlow;
      this.buildForm();
      this.isModalOpen = true;
    });
  }

  // Add a delete method
  delete(id: string) {
    this.confirmation.warn('::AreYouSureToDelete', '::AreYouSure').subscribe(status => {
      if (status === Confirmation.Status.confirm) {
        this.monthlyCashFlowService.delete(id).subscribe(() => this.list.get());
      }
    });
  }

  buildForm() {
    this.form = this.fb.group({
      date: [
        this.selectedMonthlyCashFlow.date ? new Date(this.selectedMonthlyCashFlow.date) : null,
        Validators.required,
      ],
      shopId: [this.selectedMonthlyCashFlow.shopId || '', Validators.required],
    });
  }

  save() {
    if (this.form.invalid) {
      return;
    }

    const request = this.selectedMonthlyCashFlow.id
      ? this.monthlyCashFlowService.update(this.selectedMonthlyCashFlow.id, this.form.value)
      : this.monthlyCashFlowService.create(this.form.value);

    request.subscribe(() => {
      this.isModalOpen = false;
      this.form.reset();
      this.list.get();
    });
  }
}
