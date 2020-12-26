using System;
using System.Collections.Generic;
using System.Text;

namespace DarrenCloudDemos.Lib.DesignPatterns.ChainOfResponsibility.Approval
{
    /// <summary>
    /// 处理报告的动作管道的最后一个
    /// </summary>
    class EndOfChainExpenseHandler : IExpenseHandler
    {
        private EndOfChainExpenseHandler() { }

        public static EndOfChainExpenseHandler Instance
        {
            get { return _instance; }
        }

        public ApprovalResponse Approve(IExpenseReport expenseReport)
        {
            return ApprovalResponse.Denied;
        }

        public void RegisterNext(IExpenseHandler next)
        {
            throw new InvalidOperationException("End of chain handler must be the end of the chain!");
        }

        private static readonly EndOfChainExpenseHandler _instance = new EndOfChainExpenseHandler();
    }
}
