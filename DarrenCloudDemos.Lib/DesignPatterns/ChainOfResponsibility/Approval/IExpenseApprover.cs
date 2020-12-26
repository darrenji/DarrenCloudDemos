using System;
using System.Collections.Generic;
using System.Text;

namespace DarrenCloudDemos.Lib.DesignPatterns.ChainOfResponsibility.Approval
{
    /// <summary>
    /// 报告审核人
    /// </summary>
    public interface IExpenseApprover
    {
        ApprovalResponse ApproveExpense(IExpenseReport expenseReport);
    }
}
