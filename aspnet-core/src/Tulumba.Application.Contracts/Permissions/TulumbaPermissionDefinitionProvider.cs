using Tulumba.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Tulumba.Permissions;

public class TulumbaPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(TulumbaPermissions.GroupName);

        var shopPermission = myGroup.AddPermission(TulumbaPermissions.Shops.Default, L("Permission:Shops"));
        shopPermission.AddChild(TulumbaPermissions.Shops.Create, L("Permission:Shops.Create"));
        shopPermission.AddChild(TulumbaPermissions.Shops.Edit, L("Permission:Shops.Edit"));
        shopPermission.AddChild(TulumbaPermissions.Shops.Delete, L("Permission:Shops.Delete"));

        var employeePermission =
            myGroup.AddPermission(TulumbaPermissions.Employees.Default, L("Permission:Employees"));
        employeePermission.AddChild(TulumbaPermissions.Employees.Create, L("Permission:Employees.Create"));
        employeePermission.AddChild(TulumbaPermissions.Employees.Edit, L("Permission:Employees.Edit"));
        employeePermission.AddChild(TulumbaPermissions.Employees.Delete, L("Permission:Employees.Delete"));

        var dailyEarningPermission =
            myGroup.AddPermission(TulumbaPermissions.DailyEarnings.Default, L("Permission:DailyEarnings"));
        dailyEarningPermission.AddChild(TulumbaPermissions.DailyEarnings.Get, L("Permission:DailyEarnings.Get"));
        dailyEarningPermission.AddChild(TulumbaPermissions.DailyEarnings.Create,
            L("Permission:DailyEarnings.Create"));
        dailyEarningPermission.AddChild(TulumbaPermissions.DailyEarnings.Edit, L("Permission:DailyEarnings.Edit"));
        dailyEarningPermission.AddChild(TulumbaPermissions.DailyEarnings.Delete,
            L("Permission:DailyEarnings.Delete"));

        var monthlyCashFlowPermission = myGroup.AddPermission(TulumbaPermissions.MonthlyCashFlows.Default,
            L("Permission:MonthlyCashFlows"));
        monthlyCashFlowPermission.AddChild(TulumbaPermissions.MonthlyCashFlows.Get,
            L("Permission:MonthlyCashFlows.Get"));
        monthlyCashFlowPermission.AddChild(TulumbaPermissions.MonthlyCashFlows.Create,
            L("Permission:MonthlyCashFlows.Create"));
        monthlyCashFlowPermission.AddChild(TulumbaPermissions.MonthlyCashFlows.Edit,
            L("Permission:MonthlyCashFlows.Edit"));
        monthlyCashFlowPermission.AddChild(TulumbaPermissions.MonthlyCashFlows.Delete,
            L("Permission:MonthlyCashFlows.Delete"));

        var expenseTypePermission =
            myGroup.AddPermission(TulumbaPermissions.ExpenseTypes.Default, L("Permission:ExpenseTypes"));
        expenseTypePermission.AddChild(TulumbaPermissions.ExpenseTypes.Get, L("Permission:ExpenseTypes.Get"));
        expenseTypePermission.AddChild(TulumbaPermissions.ExpenseTypes.Create, L("Permission:ExpenseTypes.Create"));
        expenseTypePermission.AddChild(TulumbaPermissions.ExpenseTypes.Edit, L("Permission:ExpenseTypes.Edit"));
        expenseTypePermission.AddChild(TulumbaPermissions.ExpenseTypes.Delete, L("Permission:ExpenseTypes.Delete"));

        var expensePermission =
            myGroup.AddPermission(TulumbaPermissions.Expenses.Default, L("Permission:Expenses"));
        expensePermission.AddChild(TulumbaPermissions.Expenses.Get, L("Permission:Expenses.Get"));
        expensePermission.AddChild(TulumbaPermissions.Expenses.Create, L("Permission:Expenses.Create"));
        expensePermission.AddChild(TulumbaPermissions.Expenses.Edit, L("Permission:Expenses.Edit"));
        expensePermission.AddChild(TulumbaPermissions.Expenses.Delete, L("Permission:Expenses.Delete"));

        var dailyCashFlowPermission = myGroup.AddPermission(TulumbaPermissions.DailyCashFlows.Default,
            L("Permission:DailyCashFlows"));
        dailyCashFlowPermission.AddChild(TulumbaPermissions.DailyCashFlows.Get,
            L("Permission:DailyCashFlows.Get"));
        dailyCashFlowPermission.AddChild(TulumbaPermissions.DailyCashFlows.Create,
            L("Permission:DailyCashFlows.Create"));
        dailyCashFlowPermission.AddChild(TulumbaPermissions.DailyCashFlows.Edit,
            L("Permission:DailyCashFlows.Edit"));
        dailyCashFlowPermission.AddChild(TulumbaPermissions.DailyCashFlows.Delete,
            L("Permission:DailyCashFlows.Delete"));
        
        var recurringExpensePermission = myGroup.AddPermission(TulumbaPermissions.RecurringExpenses.Default,
            L("Permission:RecurringExpenses"));
        recurringExpensePermission.AddChild(TulumbaPermissions.RecurringExpenses.Get,
            L("Permission:RecurringExpenses.Get"));
        recurringExpensePermission.AddChild(TulumbaPermissions.RecurringExpenses.Create,
            L("Permission:RecurringExpenses.Create"));
        recurringExpensePermission.AddChild(TulumbaPermissions.RecurringExpenses.Edit,
            L("Permission:RecurringExpenses.Edit"));
        recurringExpensePermission.AddChild(TulumbaPermissions.RecurringExpenses.Delete,
            L("Permission:RecurringExpenses.Delete"));
        
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<TulumbaResource>(name);
    }
}