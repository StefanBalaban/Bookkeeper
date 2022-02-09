import { NgModule } from '@angular/core';

import { DailyEarningRoutingModule } from './daily-earning-routing.module';
import { DailyEarningComponent } from './daily-earning.component';
import { SharedModule } from '../shared/shared.module';
import { NgbDatepickerModule } from '@ng-bootstrap/ng-bootstrap';
import { MatExpansionModule } from '@angular/material/expansion';

@NgModule({
  declarations: [DailyEarningComponent],
  imports: [SharedModule, DailyEarningRoutingModule, NgbDatepickerModule, MatExpansionModule],
})
export class DailyEarningModule {}
