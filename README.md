# PACTNET - Contract Testing for .Net Services.

This repository is a simple example to create contract tests in .Net services with PACT framework and its based on the samples provided in the official pactnet repository.

### Make available PactBroket locally

```
docker-compose up
```

### Clone this current repository, access the root dir and perform test on a consumer side to generate the contracts

```
dotnet test .\Consumer.Tests\
```

### Publish the contract using PactBroker API


Type the following command in your terminal:

```
docker run --network="host" --rm -it -v ${PWD}/pacts:/example/pacts pactfoundation/pact-cli:latest publish /example/pacts --consumer-app-version 0.0.1 --broker-base-url http://localhost:9292

```

Note: Pay attention to the correct folder where the pact is published.

### Run provider tests to verify contracts and publish results to Local Pact Broker

```
dotnet test .\Provider.Tests\
```

### Check the pact and results in Pact Broker

http://localhost:9292/
