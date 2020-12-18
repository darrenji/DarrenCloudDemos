using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.Domain.Abstractions
{
    public interface IDomainEvent : INotification
    {
    }
}
