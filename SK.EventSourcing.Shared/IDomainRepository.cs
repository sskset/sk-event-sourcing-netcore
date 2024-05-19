
using System.Threading.Tasks;

namespace SK.EventSourcing.Shared;

public interface IDomainRepository<T> where T: SK.EventSourcing.Shared.AggregateRoot
{
    Task SaveAsync(T aggregateRoot);
}