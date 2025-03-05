using System;

namespace Fretefy.Test.Domain.Entities
{
    public interface IEntity<T>
    {
        T Id { get; set; }
    }
}
