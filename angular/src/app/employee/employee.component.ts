import { ListService, PagedResultDto } from '@abp/ng.core';
import { Confirmation, ConfirmationService } from '@abp/ng.theme.shared';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { EmployeeService, ShopService } from '@proxy/application/app-services';
import { EmployeeDto } from '@proxy/application/contracts/dtos/employee';
import { ShopDto } from '@proxy/application/contracts/dtos/shop';
import { SalaryType, salaryTypeOptions } from '@proxy/entities/employee';
import { switchMap } from 'rxjs/operators';

@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html',
  styleUrls: ['./employee.component.scss'],
  providers: [ListService],
})
export class EmployeeComponent implements OnInit {
  employee = { items: [], totalCount: 0 } as PagedResultDto<EmployeeDto>;
  form: FormGroup;
  isModalOpen = false;
  selectedEmployee = {} as EmployeeDto;
  salaryTypes = salaryTypeOptions;
  shops: ShopDto[] = [];

  constructor(
    public readonly list: ListService,
    private employeeService: EmployeeService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService, // inject the ConfirmationService
    private shopService: ShopService
  ) {}

  ngOnInit() {
    const employeeStreamCreator = query => this.employeeService.getList(query);

    this.list.hookToQuery(employeeStreamCreator).subscribe(response => {
      this.employee = response;
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

  

  getShopName(shopId) {
    return this.shops.find(x => x.id === shopId)?.name;
  }

  createEmployee() {
    this.selectedEmployee = {} as EmployeeDto;
    this.buildForm(); // add this line
    this.isModalOpen = true;
  }

  editEmployee(id: string) {
    this.employeeService.get(id).subscribe(employee => {
      this.selectedEmployee = employee;
      this.buildForm();
      this.isModalOpen = true;
    });
  }

  // Add a delete method
  delete(id: string) {
    this.confirmation.warn('::AreYouSureToDelete', '::AreYouSure').subscribe(status => {
      if (status === Confirmation.Status.confirm) {
        this.employeeService.delete(id).subscribe(() => this.list.get());
      }
    });
  }

  buildForm() {
    this.form = this.fb.group({
      name: [this.selectedEmployee.name || '', Validators.required],
      salaryType: [this.selectedEmployee.salaryType || SalaryType.Dnevnica, Validators.required],
      salaryAmount: [this.selectedEmployee.salaryAmount || 0, Validators.required],
      active: [this.selectedEmployee.active || true, Validators.required],
      shopId: [this.selectedEmployee.shopId || '', Validators.required],
    });
  }

  save() {
    if (this.form.invalid) {
      return;
    }

    const request = this.selectedEmployee.id
      ? this.employeeService.update(this.selectedEmployee.id, this.form.value)
      : this.employeeService.create(this.form.value);

    request.subscribe(() => {
      this.isModalOpen = false;
      this.form.reset();
      this.list.get();
    });
  }
}