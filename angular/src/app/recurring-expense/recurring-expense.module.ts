import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';
import { RecurringExpenseRoutingModule } from './recurring-expense-routing.module';
import { RecurringExpenseComponent } from './recurring-expense.component';


@NgModule({
  declarations: [
    RecurringExpenseComponent
  ],
  imports: [
    CommonModule,
    RecurringExpenseRoutingModule,
    SharedModule
  ]
})
export class RecurringExpenseModule { }
