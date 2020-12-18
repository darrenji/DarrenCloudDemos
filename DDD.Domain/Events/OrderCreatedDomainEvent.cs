using DDD.Domain.Abstractions;
using DDD.Domain.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.Domain.Events
{
    public class OrderCreatedDomainEvent : IDomainEvent
    {
        public Order Order { get; private set; }
        public OrderCreatedDomainEvent(Order order)
        {
            this.Order = order;
        }
    }
}
