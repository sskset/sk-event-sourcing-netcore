using NEventStore;
using Newtonsoft.Json;
using SK.EventSourcing;

namespace Wallet.API.Domain;

public class WalletDomainRepository : IDomainRepository<Wallet>
{
    private readonly IStoreEvents _storeEvents;

    public WalletDomainRepository(IStoreEvents storeEvents)
    {
        _storeEvents = storeEvents;
    }
    public Wallet Get(Guid walletId)
    {
        using (var stream = _storeEvents.OpenStream(walletId, 0, int.MaxValue))
        {
            var wallet = new Wallet();
            var domainEvents = new List<DomainEvent>();
            foreach (var e in stream.CommittedEvents)
            {
                if (e.Headers.TryGetValue("type", out var typeName))
                {
                    var de = (DomainEvent)JsonConvert.DeserializeObject(e.Body.ToString(), Type.GetType(typeName.ToString()));
                    domainEvents.Add(de);
                }
            }

            wallet.LoadFromHistory(domainEvents);

            return wallet;
        }
    }

    public void Save(Wallet wallet)
    {

        using (var stream = _storeEvents.OpenStream(wallet.Id, 0, int.MaxValue))
        {
            foreach (var e in wallet.GetUncommittedChanges())
            {
                var payload = JsonConvert.SerializeObject(e);
                stream.Add(new EventMessage
                {
                    Body = payload,
                    Headers = new Dictionary<string, object>
                    {
                        { "type", e.GetType().FullName }
                    }
                });
            }
            stream.CommitChanges(Guid.NewGuid());
        }
    }
}