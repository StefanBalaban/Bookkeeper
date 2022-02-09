import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MonthlyCashFlowRoutingModule } from './monthly-cash-flow-routing.module';
import { MonthlyCashFlowComponent } from './monthly-cash-flow.component';
import { SharedModule } from '../shared/shared.module';
import { NgbDatepickerModule } from '@ng-bootstrap/ng-bootstrap';
import { MatExpansionModule } from '@angular/material/expansion';

@NgModule({
  declarations: [MonthlyCashFlowComponent],
  imports: [SharedModule, MonthlyCashFlowRoutingModule, NgbDatepickerModule, MatExpansionModule],
})
export class MonthlyCashFlowModule {}
