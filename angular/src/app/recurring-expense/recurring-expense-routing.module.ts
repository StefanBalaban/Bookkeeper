import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RecurringExpenseComponent } from './recurring-expense.component';

const routes: Routes = [{ path: '', component: RecurringExpenseComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class RecurringExpenseRoutingModule { }
