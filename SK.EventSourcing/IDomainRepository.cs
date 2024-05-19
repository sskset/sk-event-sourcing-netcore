
using System;
using System.Threading.Tasks;

namespace SK.EventSourcing;

public interface IDomainRepository<T> where T: AggregateRoot
{
    Task<T> GetAsync(Guid aggregateId);
    Task SaveAsync(T aggregateRoot);
}