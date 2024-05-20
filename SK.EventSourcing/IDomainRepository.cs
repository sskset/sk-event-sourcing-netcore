
using System;

namespace SK.EventSourcing;

public interface IDomainRepository<T> where T: AggregateRoot, new()
{
    T Get(Guid aggregateId);
    void Save(T aggregateRoot);
}