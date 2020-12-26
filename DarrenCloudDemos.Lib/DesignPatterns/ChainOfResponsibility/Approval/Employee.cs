using System;
using System.Collections.Generic;
using System.Text;

namespace DarrenCloudDemos.Lib.DesignPatterns.ChainOfResponsibility.Approval
{
    public class Employee : IExpenseApprover
    {
        public Employee(string name, Decimal approvalLimit)
        {
            Name = name;
            _approvalLimit = approvalLimit;
        }

        public string Name { get; private set; }

        public ApprovalResponse ApproveExpense(IExpenseReport expenseReport)
        {
            return expenseReport.Total > _approvalLimit
                    ? ApprovalResponse.BeyondApprovalLimit
                    : ApprovalResponse.Approved;
        }

        private readonly Decimal _approvalLimit;
    }
}
