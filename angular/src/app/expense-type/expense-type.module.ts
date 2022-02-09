import { NgModule } from '@angular/core';

import { ExpenseTypeRoutingModule } from './expense-type-routing.module';
import { ExpenseTypeComponent } from './expense-type.component';
import { SharedModule } from '../shared/shared.module';

@NgModule({
  declarations: [
    ExpenseTypeComponent
  ],
  imports: [
    SharedModule,
    ExpenseTypeRoutingModule
  ]
})
export class ExpenseTypeModule { }
