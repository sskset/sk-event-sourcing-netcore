# Event Sourcing

## What is Event Sourcing?

Event Sourcing is a design pattern in which results of business operations are stored as a series of events.

It is an alternative way to persist data. In contrast with state-oriented persistence that only keeps the latest version of the entity state, Event Sourcing stores each state change as a separate event.

Thanks for that, no business data is lost. Each operation results in the event stored in the database. That enables extended auditing and diagnostics capabilities (both technically and business-wise). What's more, as events contains the business context, it allows business wide analysis and reporting.

## Terminology and Concepts

First, some terminology that we're going to use throughout this section:

* Event - a persisted business event representing a change in state or record of an action taken in the system
* Stream - a related "stream" of events representing a single aggregate
* Aggregate - a type of projection that "aggregates" data from multiple events to create a single read-side view document
* Projection - any strategy for generating "read side" views from the raw events
* Inline Projections - a projection that executes "inline" as part of any event capture transaction to build read-side views that are persisted as a document
* Async Projections - a projection that runs in a background process using an eventual consistency strategy, and is stored as a document
* Live Projections - evaluates a projected view from the raw event data on demand within Marten without persisting the created view

## To-do List:

* Add `ISnapShot` support