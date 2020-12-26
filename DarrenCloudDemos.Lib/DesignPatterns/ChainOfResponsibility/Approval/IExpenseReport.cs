using System;
using System.Collections.Generic;
using System.Text;

namespace DarrenCloudDemos.Lib.DesignPatterns.ChainOfResponsibility.Approval
{
    /// <summary>
    /// 报告
    /// </summary>
    public interface IExpenseReport
    {
        Decimal Total { get; }
    }
}
