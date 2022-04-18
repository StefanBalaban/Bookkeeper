import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    loadChildren: () => import('./home/home.module').then(m => m.HomeModule),
  },
  {
    path: 'account',
    loadChildren: () => import('@abp/ng.account').then(m => m.AccountModule.forLazy()),
  },
  {
    path: 'identity',
    loadChildren: () => import('@abp/ng.identity').then(m => m.IdentityModule.forLazy()),
  },
  {
    path: 'tenant-management',
    loadChildren: () =>
      import('@abp/ng.tenant-management').then(m => m.TenantManagementModule.forLazy()),
  },
  {
    path: 'setting-management',
    loadChildren: () =>
      import('@abp/ng.setting-management').then(m => m.SettingManagementModule.forLazy()),
  },
  { path: 'shops', loadChildren: () => import('./shop/shop.module').then(m => m.ShopModule) },
  { path: 'employees', loadChildren: () => import('./employee/employee.module').then(m => m.EmployeeModule) },
  { path: 'daily-earnings', loadChildren: () => import('./daily-earning/daily-earning.module').then(m => m.DailyEarningModule) },
  { path: 'monthly-cash-flows', loadChildren: () => import('./monthly-cash-flow/monthly-cash-flow.module').then(m => m.MonthlyCashFlowModule) },
  { path: 'expense-types', loadChildren: () => import('./expense-type/expense-type.module').then(m => m.ExpenseTypeModule) },
  { path: 'expenses', loadChildren: () => import('./expense/expense.module').then(m => m.ExpenseModule) },
  { path: 'daily-cash-flows', loadChildren: () => import('./daily-cash-flow/daily-cash-flow.module').then(m => m.DailyCashFlowModule) },
  { path: 'recurring-expenses', loadChildren: () => import('./recurring-expense/recurring-expense.module').then(m => m.RecurringExpenseModule) },
  { path: 'charts', loadChildren: () => import('./chart/chart.module').then(m => m.ChartModule) },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],
  exports: [RouterModule],
})
export class AppRoutingModule {}
