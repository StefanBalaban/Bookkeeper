import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DailyCashFlowRoutingModule } from './daily-cash-flow-routing.module';
import { DailyCashFlowComponent } from './daily-cash-flow.component';
import { SharedModule } from '../shared/shared.module';
import { NgbDatepickerModule } from '@ng-bootstrap/ng-bootstrap'; // add this line
import { MatExpansionModule } from '@angular/material/expansion';

@NgModule({
  declarations: [DailyCashFlowComponent],
  imports: [SharedModule, DailyCashFlowRoutingModule, NgbDatepickerModule, MatExpansionModule],
})
export class DailyCashFlowModule {}
