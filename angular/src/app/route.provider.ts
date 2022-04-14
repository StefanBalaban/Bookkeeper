import {eLayoutType, RoutesService} from '@abp/ng.core';
import {APP_INITIALIZER} from '@angular/core';

export const APP_ROUTE_PROVIDER = [
  {provide: APP_INITIALIZER, useFactory: configureRoutes, deps: [RoutesService], multi: true},
];

function configureRoutes(routesService: RoutesService) {
  return () => {
    routesService.add([
      {
        path: '/',
        name: '::Menu:Home',
        iconClass: 'fas fa-home',
        order: 1,
        layout: eLayoutType.application,
      },
      {
        path: '/tulumba',
        name: '::Menu:TulumbaMenu',
        iconClass: 'fas fa-book',
        order: 2,
        layout: eLayoutType.application,
      },
      {
        path: '/shops',
        name: '::Menu:Shops',
        parentName: '::Menu:TulumbaMenu',
        layout: eLayoutType.application,
        requiredPolicy: 'Tulumba.Shops',
      },
      {
        path: '/employees',
        name: '::Menu:Employees',
        parentName: '::Menu:TulumbaMenu',
        layout: eLayoutType.application,
        requiredPolicy: 'Tulumba.Employees',
      },
      {
        path: '/recurring-expenses',
        name: '::Menu:RecurringExpenses',
        parentName: '::Menu:TulumbaMenu',
        layout: eLayoutType.application,
        requiredPolicy: 'Tulumba.RecurringExpenses',
      },
      {
        path: '/expenses',
        name: '::Menu:Expenses',
        parentName: '::Menu:TulumbaMenu',
        layout: eLayoutType.application,
        requiredPolicy: 'Tulumba.Expenses',
      },
      {
        path: '/daily-earnings',
        name: '::Menu:DailyEarnings',
        parentName: '::Menu:TulumbaMenu',
        layout: eLayoutType.application,
        requiredPolicy: 'Tulumba.DailyEarnings',
      },
      {
        path: '/daily-cash-flows',
        name: '::Menu:DailyCashFlows',
        parentName: '::Menu:TulumbaMenu',
        layout: eLayoutType.application,
        requiredPolicy: 'Tulumba.DailyCashFlows',
      },
      {
        path: '/monthly-cash-flows',
        name: '::Menu:MonthlyCashFlows',
        parentName: '::Menu:TulumbaMenu',
        layout: eLayoutType.application,
        requiredPolicy: 'Tulumba.MonthlyCashFlows',
      },
      {
        path: '/expense-types',
        name: '::Menu:ExpenseTypes',
        parentName: '::Menu:TulumbaMenu',
        layout: eLayoutType.application,
        requiredPolicy: 'Tulumba.ExpenseTypes',
      },
    ]);
  };
}
