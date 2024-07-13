
using System;
using System.Collections.Generic;
using System.Linq;
namespace SK.EventSourcing
{
    public abstract class AggregateRoot
    {
        private readonly List<DomainEvent> _changes = [];

        public Guid Id { get; protected set; } = Guid.NewGuid();
        public int Version { get; protected set; } = 0;

        public IEnumerable<DomainEvent> GetUncommittedChanges() => _changes.AsEnumerable();

        public void MarkChangesAsCommitted() => _changes.Clear();

        protected void ApplyChange(DomainEvent @event, bool isNew = true)
        {
            this.GetType()
                .GetMethod("Apply", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance, null, [@event.GetType()], null)
                .Invoke(this, new[] { @event });

            if (isNew)
            {
                _changes.Add(@event);
            }

            Version++;
        }

        public void LoadFromHistory(IEnumerable<DomainEvent> history)
        {
            foreach (var @event in history)
            {
                ApplyChange(@event, isNew: false);
            }
        }
    }
}