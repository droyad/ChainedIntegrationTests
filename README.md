# Chained Integration Tests Proof of Concept

## Context
In this context, integration tests as tests that run test your system from the UI or API level down to the database and represent a use case. They are commonly also called end to end tests.

## The Problem
A common problem encountered when writing and maintaining integration tests it setting up the initial state of the data. Common solutions include a database in a known state, shared scripts/code to insert data and stubbing out the database. These solutions tend to create a large amount of shared state between tests, which causes problems when the application structure needs to change. Additionally the last solution misses potential problems with the database integrations.

## The Theory
By chaining the execution of use cases (ie integration test) the system can be setup in the way needed for the new use case that is being written. This means that the setup of the system is performed from scratch by running a use cases until we reach the state we want. 

The advantage of this is that by re-using real use cases to insert data, it becomes less brittle. Coupling is also reduced as it is a chain of dependencies, instead of a all linking to a central set of scripts.

When an requirement changes, it's integration test can be changed in isolation. Any dependent tests may need to be altered, but that is due to the business domain changing. In other words, the only tests that should need to change are those whose behaviour was affected by the domain change. 

To give a simple scenario, the `Login User` test would depend on the `Register User` test:

`RegisterUser -> LoginUser`

A more complicated scenario could be a 'Leave Comment' test. This test requires a `Post` created by a user and a login session by a different uesr:

```
RegisterUser -> LoginUser -> CreatePost
									|
									v
	    RegisterUser -> LoginUser -> LeaveComment
```

The `LeaveComment` test would only need to define `CreatePost` and `LoginUser` as it's dependencies.

If the business domain changed so that a confirmation email step was required after registering, only the `LoginUser` test would need to be modified.

If the business domain changed to allow comments to be turned off for posts, the `CreatePost` and `LeaveComment` tests would need to be modified as they are directly affected by this changed, but something like a `LikeComment` test would not as by running the (now modified) `LeaveComment` dependency, it the system would be in the right state.

## The Practice
In this example, NancyFX is used as the API host, running in- memory using it's test framework. Since it is running in-memory (and in-thread), a TransactionScope can be setup to roll back any changes to the external process SQL database, giving a quick way of getting back to a clean state.

The domain of this example are `Widgets` and `Thingimabobs`. A `Thingimabob` requires a `Widget` when it is created. The test dependencies are:

```
AddWidget -> RenameWidget
        |
		\-> AddThingimabob -> RenameThingimabob
```

The `AddThingimabob` test is setup to purposefully fail to show the use of the `Inconclusive` test outcome to indicate the that a dependency of `RenameThingimabob` has failed, not the test itself. This allows quick identification of where in the chain the test has failed (the red one).
