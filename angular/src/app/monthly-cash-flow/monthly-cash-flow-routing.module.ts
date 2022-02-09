import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MonthlyCashFlowComponent } from './monthly-cash-flow.component';

const routes: Routes = [{ path: '', component: MonthlyCashFlowComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MonthlyCashFlowRoutingModule { }
