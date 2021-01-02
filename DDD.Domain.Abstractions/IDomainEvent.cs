using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.Domain.Abstractions
{
    /// <summary>
    /// 领域事件，是INotification类型
    /// </summary>
    public interface IDomainEvent : INotification
    {
    }
}
