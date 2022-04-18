import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ChartRoutingModule } from './chart-routing.module';
import { ChartComponent } from './chart.component';
import { SharedModule } from '../shared/shared.module';
import {NgChartsModule} from "ng2-charts";
import {NgbDatepickerModule} from "@ng-bootstrap/ng-bootstrap";
import {MatExpansionModule} from "@angular/material/expansion";

@NgModule({
  declarations: [
    ChartComponent
  ],
  imports: [
    CommonModule,
    ChartRoutingModule,
    SharedModule,
    NgChartsModule,
    NgbDatepickerModule,
    MatExpansionModule
  ]
})
export class ChartModule { }
