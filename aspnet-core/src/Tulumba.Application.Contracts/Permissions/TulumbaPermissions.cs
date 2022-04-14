namespace Tulumba.Permissions
{
    public static class TulumbaPermissions
    {
        public const string GroupName = "Tulumba";

        public static class Shops
        {
            public const string Default = GroupName + ".Shops";
            public const string Create = Default + ".Create";
            public const string Edit = Default + ".Edit";
            public const string Delete = Default + ".Delete";
        }

        public static class Employees
        {
            public const string Default = GroupName + ".Employees";
            public const string Create = Default + ".Create";
            public const string Edit = Default + ".Edit";
            public const string Delete = Default + ".Delete";
        }

        public static class DailyEarnings
        {
            public const string Default = GroupName + ".DailyEarnings";
            public const string Get = Default + ".Get";
            public const string Create = Default + ".Create";
            public const string Edit = Default + ".Edit";
            public const string Delete = Default + ".Delete";
        }

        public static class MonthlyCashFlows
        {
            public const string Default = GroupName + ".MonthlyCashFlows";
            public const string Get = Default + ".Get";
            public const string Create = Default + ".Create";
            public const string Edit = Default + ".Edit";
            public const string Delete = Default + ".Delete";
        }

        public static class ExpenseTypes
        {
            public const string Default = GroupName + ".ExpenseTypes";
            public const string Get = Default + ".Get";
            public const string Create = Default + ".Create";
            public const string Edit = Default + ".Edit";
            public const string Delete = Default + ".Delete";
        }

        public static class Expenses
        {
            public const string Default = GroupName + ".Expenses";
            public const string Get = Default + ".Get";
            public const string Create = Default + ".Create";
            public const string Edit = Default + ".Edit";
            public const string Delete = Default + ".Delete";
        }

        public static class DailyCashFlows
        {
            public const string Default = GroupName + ".DailyCashFlows";
            public const string Get = Default + ".Get";
            public const string Create = Default + ".Create";
            public const string Edit = Default + ".Edit";
            public const string Delete = Default + ".Delete";
        }
        
        public static class RecurringExpenses
        {
            public const string Default = GroupName + ".RecurringExpenses";
            public const string Get = Default + ".Get";
            public const string Create = Default + ".Create";
            public const string Edit = Default + ".Edit";
            public const string Delete = Default + ".Delete";
        }
    }
}