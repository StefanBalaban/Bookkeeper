import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';

import { EmployeeRoutingModule } from './employee-routing.module';
import { EmployeeComponent } from './employee.component';


@NgModule({
  declarations: [
    EmployeeComponent
  ],
  imports: [
    SharedModule,
    EmployeeRoutingModule
  ]
})
export class EmployeeModule { }
