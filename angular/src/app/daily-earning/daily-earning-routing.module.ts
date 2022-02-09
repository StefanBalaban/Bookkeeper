import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DailyEarningComponent } from './daily-earning.component';

const routes: Routes = [{ path: '', component: DailyEarningComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DailyEarningRoutingModule { }
