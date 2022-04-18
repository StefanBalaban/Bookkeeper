import {Component, Inject, OnInit} from '@angular/core';
import {ChartConfiguration, ChartData, ChartType} from 'chart.js';
import DataLabelsPlugin from 'chartjs-plugin-datalabels';
import {
  MonthlyCashFlowService,
  ShopService
} from "@proxy/application/app-services";
import {ShopDto} from "@proxy/application/contracts/dtos/shop";
import {MonthlyCashFlowDto} from "@proxy/application/contracts/dtos/monthly-cash-flow";
import {switchMap} from "rxjs/operators";
import {forkJoin} from "rxjs";

@Component({
  selector: 'app-chart',
  templateUrl: './chart.component.html',
  styleUrls: ['./chart.component.scss']
})
export class ChartComponent implements OnInit {
  shops: ShopDto[] = [];
  monthlyCashFlows: MonthlyCashFlowDto[] = [];
  barChartData: ChartData<'bar'>;
  startDate: Date;
  endDate: Date;
  dateRangeLabel: string[] = [];
  public barChartOptions: ChartConfiguration['options'] = {

    responsive: true,
    // We use these empty structures as placeholders for dynamic theming.
    scales: {
      x: {},
      y: {}
    },
    plugins: {
      legend: {
        display: true,
      },
      datalabels: {
        anchor: 'end',
        align: 'end'
      },
    }
  };
  public barChartType: ChartType = 'bar';
  public barChartPlugins = [
    DataLabelsPlugin,
  ];

  constructor(
    private monthlyCashFlowService: MonthlyCashFlowService,
    private shopService: ShopService,
  ) {
  }

  ngOnInit(): void {
    var shopRequest = this.shopService.getList({maxResultCount: 1, skipCount: 0}).pipe(
      switchMap(response =>
        this.shopService.getList({
          maxResultCount: response.totalCount !== 0 ? response.totalCount : 1,
          skipCount: 0,
        })
      )
    );
    var monthlyCashFlowRequest = this.monthlyCashFlowService.getList({maxResultCount: 1, skipCount: 0}).pipe(
      switchMap(response =>
        this.monthlyCashFlowService.getList({
          maxResultCount: response.totalCount !== 0 ? response.totalCount : 1,
          skipCount: 0,
        })
      )
    );

    forkJoin([monthlyCashFlowRequest, shopRequest])
      .subscribe(results => {
        this.monthlyCashFlows = results[0].items;
        this.shops = results[1].items;
        this.getDateRangeLabel();
        this.populateBarChartData();
      });
  }

  getDataSetsFromMonthlyCashFlow(): { data: [], label: string }[] {
    const monthlyCashFlowGrouped = this.groupByKey(this.monthlyCashFlows, 'shopId');

    const shopsWithMonthlyReports = this.shops.filter(x => monthlyCashFlowGrouped[x.id]);

    // If there are missing months for a shop, this will insert 0 at the position.
    shopsWithMonthlyReports.forEach(x => {
        if (monthlyCashFlowGrouped[x.id] && monthlyCashFlowGrouped[x.id].length != this.dateRangeLabel.length)
          this.addMissingMonths(monthlyCashFlowGrouped[x.id])
      }
    );

    return shopsWithMonthlyReports.map(x => {
      return {
        data: monthlyCashFlowGrouped[x.id]
          .map(y => y.sum),
        label: this.getShopName(x.id)
      }
    })
  }

  private sortByDate(x, y) {
    return new Date(x.date).getTime() - new Date(y.date).getTime();
  }

  private getDateRangeLabel() {
    if (this.monthlyCashFlows.length > 0) {
      this.monthlyCashFlows = this.monthlyCashFlows.sort((x, y) => this.sortByDate(x, y));
      const startDate = new Date(this.monthlyCashFlows[0].date);
      const endDate = new Date(this.monthlyCashFlows[this.monthlyCashFlows.length - 1].date);
      this.startDate = new Date(startDate.getFullYear(), startDate.getMonth());
      this.endDate = new Date(endDate.getFullYear(), endDate.getMonth());

      const dateIterator = new Date(startDate.getFullYear(), startDate.getMonth());

      while (this.getDateRangeKey(this.endDate) !== this.getDateRangeKey(dateIterator)) {
        this.dateRangeLabel.push(this.getDateRangeKey(dateIterator));
        dateIterator.setMonth(dateIterator.getMonth() + 1);
      }
      this.dateRangeLabel.push(this.getDateRangeKey(dateIterator));
    }
  }

  private getDateRangeKey(date: Date): string {
    return `${date.getFullYear()} - ${date.getMonth() + 1}`;
  }

  private populateBarChartData() {

    const datasets = this.getDataSetsFromMonthlyCashFlow();
    this.barChartData = {
      labels: this.dateRangeLabel,
      datasets: datasets
    }

  }

  private groupByKey(array, key) {
    return array
      .reduce((hash, obj) => {
        if (obj[key] === undefined) return hash;
        return Object.assign(hash, {[obj[key]]: (hash[obj[key]] || []).concat(obj)})
      }, {})
  }

  private getShopName(shopId) {
    return this.shops.find(x => x.id === shopId)?.name;
  }

  private addMissingMonths(monthlyCashFlowGroupedElement: any[]) {
    let index = 0
    const dateIterator = new Date(this.startDate.getFullYear(), this.startDate.getMonth());

    while (this.getDateRangeKey(this.endDate) !== this.getDateRangeKey(dateIterator)) {

      if (!monthlyCashFlowGroupedElement[index] ||
        this.getDateRangeKey(dateIterator) !== this.getDateRangeKey(new Date(monthlyCashFlowGroupedElement[index]?.date))) {
        monthlyCashFlowGroupedElement.splice(index, 0, {date: new Date(dateIterator), id: 0, sum: 0});
      }
      dateIterator.setMonth(dateIterator.getMonth() + 1);
      index++;
    }

  }
}
