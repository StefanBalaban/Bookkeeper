import {ListService, PagedResultDto} from '@abp/ng.core';
import {Confirmation, ConfirmationService} from '@abp/ng.theme.shared';
import {Component, Inject, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {ExpenseTypeService, RecurringExpenseService, ShopService} from '@proxy/application/app-services';
import {RecurringExpenseDto} from '@proxy/application/contracts/dtos/recurring-expense';
import {ShopDto} from "@proxy/application/contracts/dtos/shop";
import {ExpenseTypeDto} from "@proxy/application/contracts/dtos/expense-type";
import {switchMap} from "rxjs/operators";
import {ExpenseDto} from "@proxy/application/contracts/dtos/expense";

@Component({
  selector: 'app-recurring-expense',
  templateUrl: './recurring-expense.component.html',
  styleUrls: ['./recurring-expense.component.scss'],
  providers: [
    {provide: 'EXPENSE_TYPE_LIST', useClass: ListService},
    {provide: 'SHOP_LIST', useClass: ListService},
    {provide: 'RECURRING_EXPENSE_LIST', useClass: ListService}],
})
export class RecurringExpenseComponent implements OnInit {
  recurringExpense = {items: [], totalCount: 0} as PagedResultDto<RecurringExpenseDto>;
  form: FormGroup;
  isModalOpen = false;
  shops: ShopDto[] = [];
  expenseTypes: ExpenseTypeDto[] = [];
  selectedRecurringExpense = {} as RecurringExpenseDto;

  constructor(
    @Inject('EXPENSE_TYPE_LIST') public readonly expenseTypeList: ListService,
    @Inject('SHOP_LIST') public readonly shopList: ListService,
    @Inject('RECURRING_EXPENSE_LIST') public readonly list: ListService,
    private recurringExpenseService: RecurringExpenseService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService, // inject the ConfirmationService
    private shopService: ShopService,
    private expenseTypeService: ExpenseTypeService,
  ) {
  }

  ngOnInit() {
    const recurringExpenseStreamCreator = query => this.recurringExpenseService.getList(query);

    this.list.hookToQuery(recurringExpenseStreamCreator).subscribe(response => {
      this.recurringExpense = response;
    });

    let shopRequest = this.shopService.getList({maxResultCount: 1, skipCount: 0}).pipe(
      switchMap(response =>
        this.shopService.getList({
          maxResultCount: response.totalCount !== 0 ? response.totalCount : 1,
          skipCount: 0,
        })
      )
    );

    shopRequest.subscribe(response => (this.shops = response.items));

    let expenseTypeRequest = this.expenseTypeService
      .getList({maxResultCount: 1, skipCount: 0})
      .pipe(
        switchMap(response =>
          this.expenseTypeService.getList({
            maxResultCount: response.totalCount !== 0 ? response.totalCount : 1,
            skipCount: 0,
          })
        )
      );

    expenseTypeRequest.subscribe(response => (this.expenseTypes = response.items));
  }

  createRecurringExpense() {
    this.selectedRecurringExpense = {} as RecurringExpenseDto;
    this.buildForm();
    this.isModalOpen = true;
  }

  editRecurringExpense(id: string) {
    this.recurringExpenseService.get(id).subscribe(recurringExpense => {
      this.selectedRecurringExpense = recurringExpense;
      this.buildForm();
      this.form.controls['expenseTypeId'].disable();
      this.form.controls['shopId'].disable();
      this.isModalOpen = true;
    });
  }

  // Add a delete method
  delete(id: string) {
    this.confirmation.warn('::AreYouSureToDelete', '::AreYouSure').subscribe(status => {
      if (status === Confirmation.Status.confirm) {
        this.recurringExpenseService.delete(id).subscribe(() => this.list.get());
      }
    });
  }

  buildForm() {
    this.form = this.fb.group({
      expenseTypeId: [this.selectedRecurringExpense.expenseTypeId || '', Validators.required],
      shopId: [this.selectedRecurringExpense.shopId || '', Validators.required],
      amount: [this.selectedRecurringExpense.amount || 0, Validators.required],
      active: [this.selectedRecurringExpense.active || true, Validators.required],
    });
  }

  save() {
    if (this.form.invalid) {
      return;
    }
    this.form.controls['expenseTypeId'].enable();
    this.form.controls['shopId'].enable();

    const request = this.selectedRecurringExpense.id
      ? this.recurringExpenseService.update(this.selectedRecurringExpense.id, this.form.value)
      : this.recurringExpenseService.create(this.form.value);

    request.subscribe(() => {
      this.isModalOpen = false;
      this.form.reset();
      this.list.get();
    });
  }

  getShopName(shopId) {
    return this.shops.find(x => x.id === shopId)?.name;
  }

  getExpenseTypeName(expense: ExpenseDto) {
    return this.expenseTypes.find(x => x.id === expense.expenseTypeId)?.name;
  }
}
