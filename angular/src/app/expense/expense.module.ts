import { NgModule } from '@angular/core';

import { ExpenseRoutingModule } from './expense-routing.module';
import { ExpenseComponent } from './expense.component';
import { SharedModule } from '../shared/shared.module';
import { MatExpansionModule } from '@angular/material/expansion'
import { NgbDatepickerModule } from '@ng-bootstrap/ng-bootstrap';

@NgModule({
  declarations: [ExpenseComponent],
  imports: [SharedModule, ExpenseRoutingModule, NgbDatepickerModule, MatExpansionModule],
})
export class ExpenseModule {}
