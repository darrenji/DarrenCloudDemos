using System;
using System.Collections.Generic;
using System.Text;

namespace DarrenCloudDemos.Lib.DesignPatterns.ChainOfResponsibility.Approval
{
    public enum ApprovalResponse
    {
        Denied,
        Approved,
        BeyondApprovalLimit,
    }
}
