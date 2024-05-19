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
    public Task<Wallet> GetAsync(Guid walletId)
    {
        using (var stream = _storeEvents.OpenStream(walletId, 0, int.MaxValue))
        {
            stream.CommittedEvents
        }

        return Task.FromResult(null as Wallet);
    }

    public Task SaveAsync(Wallet wallet)
    {

        using (var stream = _storeEvents.CreateStream(wallet.Id))
        {
            foreach(var e in wallet.GetUncommittedChanges())
            {
                var payload = JsonConvert.SerializeObject(e);
                stream.Add(new EventMessage {  Body = payload });
            }

            stream.CommitChanges(Guid.NewGuid());
        }


        return Task.CompletedTask;
    }
}