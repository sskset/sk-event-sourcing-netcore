﻿namespace SK.EventSourcing.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

public abstract class AggregateRoot
{
    private readonly List<DomainEvent> _changes = new List<DomainEvent>();

    public Guid Id { get; protected set; } = Guid.NewGuid();
    public int Version { get; protected set; } = -1;

    public IEnumerable<DomainEvent> GetUncommittedChanges() => _changes.AsEnumerable();

    public void MarkChangesAsCommitted() => _changes.Clear();

    protected void ApplyChange(DomainEvent @event, bool isNew = true)
    {
        dynamic self = this;
        self.Apply(@event);

        if (isNew)
        {
            _changes.Add(@event);
        }
    }

    public void LoadFromHistory(IEnumerable<DomainEvent> history)
    {
        foreach (var @event in history)
        {
            ApplyChange(@event, isNew: false);
            Version++;
        }
    }
}
