using System;
using System.Collections.Generic;
using System.Text;

namespace DarrenCloudDemos.Lib.DesignPatterns.ChainOfResponsibility.Approval
{
    class Final
    {
        static void Main()
        {
            ExpenseHandler william = new ExpenseHandler(new Employee("William Worker", Decimal.Zero));
            ExpenseHandler mary = new ExpenseHandler(new Employee("Mary Manager", new Decimal(1000)));
            ExpenseHandler victor = new ExpenseHandler(new Employee("Victor Vicepres", new Decimal(5000)));
            ExpenseHandler paula = new ExpenseHandler(new Employee("Paula President", new Decimal(20000)));

            william.RegisterNext(mary);
            mary.RegisterNext(victor);
            victor.RegisterNext(paula);

            Decimal expenseReportAmount;
            if (ConsoleInput.TryReadDecimal("Expense report amount:", out expenseReportAmount))
            {
                IExpenseReport expense = new ExpenseReport(expenseReportAmount);

                ApprovalResponse response = william.Approve(expense);

                Console.WriteLine("The request was {0}.", response);
            }
        }
    }
}
