import {ListService, PagedResultDto} from '@abp/ng.core';
import {Confirmation, ConfirmationService} from '@abp/ng.theme.shared';
import {Component, Inject, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {EmployeeService, ExpenseService, ExpenseTypeService, ShopService,} from '@proxy/application/app-services';
import {ExpenseDto} from '@proxy/application/contracts/dtos/expense';
import {NgbDateAdapter, NgbDateNativeAdapter} from '@ng-bootstrap/ng-bootstrap';
import {ShopDto} from '@proxy/application/contracts/dtos/shop';
import {ExpenseTypeDto} from '@proxy/application/contracts/dtos/expense-type';
import {ExpenseCategory, expenseCategoryOptions, ExpensePeriod} from '@proxy/entities/expense-type';
import {EmployeeDto} from '@proxy/application/contracts/dtos/employee';
import {SalaryType} from '@proxy/entities/employee';
import {switchMap} from 'rxjs/operators';

@Component({
  selector: 'app-expense',
  templateUrl: './expense.component.html',
  styleUrls: ['./expense.component.scss'],
  providers: [
    {provide: 'EXPENSE_LIST', useClass: ListService},
    {provide: 'EXPENSE_TYPE_LIST', useClass: ListService},
    {provide: 'EMPLOYEE_LIST', useClass: ListService},
    {provide: 'SHOP_LIST', useClass: ListService},
    {provide: NgbDateAdapter, useClass: NgbDateNativeAdapter},
  ],
})
export class ExpenseComponent implements OnInit {
  expense = {items: [], totalCount: 0} as PagedResultDto<ExpenseDto>;
  form: FormGroup;
  filterForm: FormGroup;
  isModalOpen = false;
  selectedExpense : ExpenseDto;
  shops: ShopDto[] = [];
  expenseTypes: ExpenseTypeDto[] = [];
  expenseTypesFilter: ExpenseTypeDto[] = [];
  expenseTypesForm: ExpenseTypeDto[] = [];
  selectedExpenseType: string;
  employees: EmployeeDto[] = [];
  salaryExpenseSelected = false;
  salaryAmount = 0;
  employeeForSelect: EmployeeDto[] = [];
  selectedSalaryType: SalaryType = null;
  selectedShopId: string;
  query: any = {};
  maxResultCount = 0;
  sum = 0;
  expenseCategories = expenseCategoryOptions;

  constructor(
    @Inject('EXPENSE_LIST') public readonly list: ListService,
    @Inject('EXPENSE_TYPE_LIST') public readonly expenseTypeList: ListService,
    @Inject('EMPLOYEE_LIST') public readonly employeeList: ListService,
    @Inject('SHOP_LIST') public readonly shopList: ListService,
    private expenseService: ExpenseService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService, // inject the ConfirmationService
    private shopService: ShopService,
    private expenseTypeService: ExpenseTypeService,
    private employeeService: EmployeeService
  ) {
  }

  ngOnInit() {
    this.createFilterForm();
    const expenseStreamCreator = query => this.expenseService.getList({...query, ...this.query});

    this.list.hookToQuery(expenseStreamCreator).subscribe(response => {
      this.expense = response;
      this.getSum();
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

    expenseTypeRequest.subscribe(response => {
      this.expenseTypes = response.items;
      this.expenseTypesFilter = response.items;
      this.expenseTypesForm = response.items;
    });

    let employeeRequest = this.employeeService.getList({maxResultCount: 1, skipCount: 0}).pipe(
      switchMap(response =>
        this.employeeService.getList({
          maxResultCount: response.totalCount !== 0 ? response.totalCount : 1,
          skipCount: 0,
        })
      )
    );

    employeeRequest.subscribe(response => (this.employees = response.items));
  }

  getSum() {
    this.sum = 0;
    this.expenseService
      .getList({
        maxResultCount: this.expense.totalCount !== 0 ? this.expense.totalCount : 1,
        skipCount: 0,
        ...this.query,
      })
      .subscribe(x => {
        x.items.forEach(y => (this.sum += y.amount));
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
    this.query.expenseTypeId = this.filterForm.controls['expenseTypeId'].value || '';
    this.query.expenseCategory = this.filterForm.controls['expenseCategory'].value >= 0 ? this.filterForm.controls['expenseCategory'].value : '';
    this.query.employeeId = this.filterForm.controls['employeeId'].value || '';
    this.list.get();
  }

  setEmployeesForShop(shopId: string) {
    this.selectedShopId = this.shops.find(x => shopId.includes(x.id))?.id;
    this.employeeForSelect = this.employees.filter(
      x => x.shopId == this.selectedShopId && x.salaryType === this.selectedSalaryType
    );
  }

  setEmployeesForSalaryType(salaryType: SalaryType) {
    this.selectedSalaryType = salaryType;
    this.employeeForSelect = this.employees.filter(
      x =>
        x.salaryType === this.selectedSalaryType && x.shopId === this.selectedShopId
    );
  }

  setAmountForSelectedEmployee(employeeId: string) {
    if (employeeId) {
      const employee = this.employees.find(x => employeeId.includes(x.id));
      this.salaryAmount = employee.salaryAmount;
    }
  }

  getSalaryAmountForSelectedEmployee(employeeId: string) : number {
    if (employeeId) {
      const employee = this.employees.find(x => employeeId.includes(x.id));
      return employee.salaryAmount;
    }
    return 0;
  }

  setInputforExpenseType(selectedExpenseTypeId: string) {
    if (!selectedExpenseTypeId) {
      this.setInputForRegularExpense();
      return;
    }
    const expenseType = this.expenseTypes.find(x => selectedExpenseTypeId.includes(x.id));
    if (expenseType?.expenseCategory === ExpenseCategory.Plata) {
      this.setInputForSalary(expenseType);
    } else {
      this.setInputForRegularExpense();
    }
  }

  setInputForRegularExpense() {
    this.salaryExpenseSelected = false;
    this.form.controls['employeeId'].setValue(null);
    this.form.controls['amount'].reset();
    this.form.controls['amount'].enable();
  }

  setInputForSalary(expenseType: ExpenseTypeDto) {
    this.setEmployeesForSalaryType(
      expenseType.expensePeriod == ExpensePeriod.Mjesecni
        ? SalaryType.Mjesecno
        : SalaryType.Dnevnica
    );
    this.salaryExpenseSelected = true;
    this.form.controls['amount'].disable();
  }

  getShopName(shopId) {
    return this.shops.find(x => x.id === shopId)?.name;
  }

  getExpenseTypeName(expense: ExpenseDto) {
    const expenseType = this.expenseTypes.find(x => x.id === expense.expenseTypeId);
    let name = expenseType?.name;
    if (expenseType) {
      if (expenseType.expenseCategory === ExpenseCategory.Plata) {
        var empName = this.employees.find(x => x.id === expense.employeeId)?.name;
        if (empName) {
          name += ' ' + empName;
        }
      }
    }
    return name;
  }

  createExpense() {
    this.selectedExpense = null;
    this.buildForm();
    this.isModalOpen = true;
  }

  editExpense(id: string) {
    this.expenseService.get(id).subscribe(Expense => {
      this.selectedExpense = Expense;
      this.buildForm();
      this.isModalOpen = true;
    });
  }

  delete(id: string) {
    this.confirmation.warn('::AreYouSureToDelete', '::AreYouSure').subscribe(status => {
      if (status === Confirmation.Status.confirm) {
        this.expenseService.delete(id).subscribe(() => this.list.get());
      }
    });
  }

  buildForm() {
    this.form = this.fb.group({
      date: [
        this.selectedExpense?.date ? new Date(this.selectedExpense?.date) : null,
        Validators.required,
      ],
      expenseCategory:[this.expenseTypes.find(x => x.id === this.selectedExpense?.expenseTypeId)?.expenseCategory  || ''],
      expenseTypeId: [this.selectedExpense?.expenseTypeId || '', Validators.required],
      shopId: [this.selectedExpense?.shopId || '', Validators.required],
      employeeId: [this.selectedExpense?.employeeId || ''],
      amount: [this.selectedExpense?.amount || 0, Validators.required],
    });

    if (this.selectedExpense) {
      if (this.selectedExpense.employeeId) {
        this.setEmployeesForSelect();
        this.setFormForEmployeeSelected(this.selectedExpense.employeeId);
      }
      else {
        this.setFormForEmployeeNotSelected(this.selectedExpense.amount);
      }
    }
    else {
      this.setFormForEmployeeNotSelected(0);
    }
  }

  setFormForEmployeeNotSelected(amount :number) {
    this.salaryExpenseSelected = false;
    this.form.controls['amount'].setValue(amount);
    this.form.controls['employeeId'].setValue(null);
    this.form.controls['employeeId'].setValidators(null);
  }

  setEmployeesForSelect() {
    const expenseType = this.expenseTypes.find(x => x.id === this.form.controls['expenseTypeId'].value);
    const salaryType = expenseType?.expensePeriod == ExpensePeriod.Mjesecni
    ? SalaryType.Mjesecno
    : SalaryType.Dnevnica

    this.employeeForSelect = this.employees
      .filter(x => !this.form.controls['shopId'].value || x.shopId === this.form.controls['shopId'].value)
      .filter(x => !expenseType || x.salaryType === salaryType)
  }
  setFormForEmployeeSelected(employeeId?: string) {
    this.salaryExpenseSelected = true;
    this.form.controls['employeeId'].setValidators(Validators.required);

    if (employeeId) {
      this.form.controls['amount'].setValue(this.getSalaryAmountForSelectedEmployee(employeeId));
      this.form.controls['employeeId'].setValue(employeeId);
    }
    else {
      this.form.controls['amount'].setValue(0);
      this.form.controls['employeeId'].setValue(null);
    }
  }

  setFormForChangedShop() {
    if (this.salaryExpenseSelected) {
      this.setEmployeesForSelect();
      this.setFormForEmployeeSelected(this.employeeForSelect[0]?.id)
    }
  }

  setFormForChangedExpenseType() {
    const expenseType = this.expenseTypes.find(x => x.id === this.form.controls['expenseTypeId'].value);
    if (expenseType?.expenseCategory == ExpenseCategory.Plata) {
      this.setEmployeesForSelect();
      this.setFormForEmployeeSelected(this.employeeForSelect[0]?.id);
    }
    else {
      if (this.form.controls['employeeId'].value) {
        this.setFormForEmployeeNotSelected(0);
      }
      else {
        this.setFormForEmployeeNotSelected(this.form.controls['amount'].value);
      }
    }
  }

  save() {
    if (this.form.invalid) {
      return;
    }

    const request = this.selectedExpense?.id
      ? this.expenseService.update(this.selectedExpense.id, this.form.value)
      : this.expenseService.create(this.form.value);


    request.subscribe(() => {
      this.isModalOpen = false;
      this.form.reset();
      this.list.get();
    });
  }

  createFilterForm() {
    this.filterForm = this.fb.group({
      dateGTE: [],
      dateLTE: [],
      expenseTypeId: [''],
      shopId: [''],
      employeeId: [''],
      expenseCategory: ['']
    });
  }

  setExpenseTypeForCategoryInFilter(value: string) {
    this.expenseTypesFilter = this.expenseTypes.filter(x => x.expenseCategory === +value.split(':')[1]);
  }

  setExpenseTypeForCategoryInForm() {
    this.expenseTypesForm = this.expenseTypes.filter(x => x.expenseCategory === this.form.controls['expenseCategory'].value)
    this.form.controls['expenseTypeId'].setValue(this.expenseTypesForm[0]?.id);
    this.setFormForChangedExpenseType();
  }
}
