namespace SK.EventSourcing.Shared
{
    using System;
    
    public abstract class DomainEvent
    {
        public Guid Id {get; private set;} = Guid.NewGuid();
        public DateTime Timestamp {get; private set;} = DateTime.UtcNow;
    }

}