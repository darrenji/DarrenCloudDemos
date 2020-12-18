using DDD.Domain.OrderAggregate;
using DDD.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.Infrastructure.Repositories
{
    public class OrderRepository : Repository<Order, long, OrderingContext>, IOrderRepository
    {
        public OrderRepository(OrderingContext context) : base(context)
        {
        }
    }
}
