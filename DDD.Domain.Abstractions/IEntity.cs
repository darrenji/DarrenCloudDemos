using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.Domain.Abstractions
{
    /// <summary>
    /// 非泛型实体接口
    /// </summary>
    public interface IEntity
    {
        object[] GetKeys();
    }

    /// <summary>
    /// 泛型实体接口，实现非泛型实体杰克欧
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IEntity<TKey> : IEntity
    {
        TKey Id { get; }
    }
}
