import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DailyCashFlowComponent } from './daily-cash-flow.component';

const routes: Routes = [{ path: '', component: DailyCashFlowComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DailyCashFlowRoutingModule { }
