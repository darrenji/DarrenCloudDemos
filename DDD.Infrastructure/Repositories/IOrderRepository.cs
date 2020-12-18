using DDD.Domain.OrderAggregate;
using DDD.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.Infrastructure.Repositories
{
    public interface IOrderRepository : IRepository<Order, long>
    {

    }
}
