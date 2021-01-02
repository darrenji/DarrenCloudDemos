using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.Domain.Abstractions
{
    /// <summary>
    /// 领域的事件处理
    /// </summary>
    /// <typeparam name="TDomainEvent"></typeparam>
    public interface IDomainEventHandler<TDomainEvent> : INotificationHandler<TDomainEvent> where TDomainEvent : IDomainEvent
    {
    }
}
