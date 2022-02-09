import { ListService, PagedResultDto } from '@abp/ng.core';
import { Confirmation, ConfirmationService } from '@abp/ng.theme.shared';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ExpenseTypeService, ShopService } from '@proxy/application/app-services';
import { ExpenseTypeDto } from '@proxy/application/contracts/dtos/expense-type';
import { ExpenseCategory, ExpensePeriod, expenseCategoryOptions, expensePeriodOptions } from '@proxy/entities/expense-type';

@Component({
  selector: 'app-expense-type',
  templateUrl: './expense-type.component.html',
  styleUrls: ['./expense-type.component.scss'],
  providers: [ListService],
})
export class ExpenseTypeComponent implements OnInit {
  expenseType = { items: [], totalCount: 0 } as PagedResultDto<ExpenseTypeDto>;
  form: FormGroup;
  isModalOpen = false;
  selectedExpenseType = {} as ExpenseTypeDto;
  expenseCategories = expenseCategoryOptions;
  expensePeriods = expensePeriodOptions;

  constructor(
    public readonly list: ListService,
    private expenseTypeService: ExpenseTypeService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService, // inject the ConfirmationService
  ) {}

  ngOnInit() {
    const expenseTypeStreamCreator = query => this.expenseTypeService.getList(query);

    this.list.hookToQuery(expenseTypeStreamCreator).subscribe(response => {
      this.expenseType = response;
    });
  }

  createExpenseType() {
    this.selectedExpenseType = {} as ExpenseTypeDto;
    this.buildForm(); // add this line
    this.isModalOpen = true;
  }

  editExpenseType(id: string) {
    this.expenseTypeService.get(id).subscribe(expenseType => {
      this.selectedExpenseType = expenseType;
      this.buildForm();
      this.isModalOpen = true;
    });
  }

  // Add a delete method
  delete(id: string) {
    this.confirmation.warn('::AreYouSureToDelete', '::AreYouSure').subscribe(status => {
      if (status === Confirmation.Status.confirm) {
        this.expenseTypeService.delete(id).subscribe(() => this.list.get());
      }
    });
  }

  buildForm() {
    this.form = this.fb.group({
      name: [this.selectedExpenseType.name || '', Validators.required],
      expenseCategory: [this.selectedExpenseType.expenseCategory || ExpenseCategory.Materijalni, Validators.required],
      expensePeriod: [this.selectedExpenseType.expensePeriod || ExpensePeriod.Dnevni, Validators.required],
    });
  }

  save() {
    if (this.form.invalid) {
      return;
    }

    const request = this.selectedExpenseType.id
      ? this.expenseTypeService.update(this.selectedExpenseType.id, this.form.value)
      : this.expenseTypeService.create(this.form.value);

    request.subscribe(() => {
      this.isModalOpen = false;
      this.form.reset();
      this.list.get();
    });
  }
}