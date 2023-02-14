# PactNet

This repository is a simple example to create contract tests in .Net services with Pact library and broker.

### Make available PactBroket locally

```
docker-compose up
```

### Clone this current repository, access the root dir and perform test on a consumer side to generate the contracts

```
dotnet test .\Consumer.Tests\
```

### Publish the contract using PactBroker API


Fill in correctly the fields "pacticipantName", "consumerName", "providerName" with the correct names of the pacticpant and the field "content" refers to the PACT but needs be populated in Base64, so convert it to make this request:

```
curl --location --request POST 'http://localhost:9292/contracts/publish' \
--header 'Content-Type: application/json' \
--data-raw '{
  "pacticipantName": "consumer-service01",
  "pacticipantVersionNumber": "dc5eb529230038a4673b8c971395bd2922db240",
  "branch": "main",
  "tags": ["main"],
  "buildUrl": "https://ci/builds/1234",
  "contracts": [
    {
      "consumerName": "consumer-service01",
      "providerName": "provider-service01",
      "specification": "pact",
      "contentType": "application/json",
      "content": "ewogICJjb25zdW1lciI6IHsKICAgICJuYW1lIjogImNvbnN1bWVyLXNlcnZpY2UwMSIKICB9LAogICJpbnRlcmFjdGlvbnMiOiBbCiAgICB7CiAgICAgICJkZXNjcmlwdGlvbiI6ICJBIEdFVCByZXF1ZXN0IHRvIHJldHJpZXZlIHRoZSB1c2VyIiwKICAgICAgInByb3ZpZGVyU3RhdGVzIjogWwogICAgICAgIHsKICAgICAgICAgICJuYW1lIjogIlRoZXJlIGlzIGEgdXNlciB3aXRoIGlkICcwMDEnIgogICAgICAgIH0KICAgICAgXSwKICAgICAgInJlcXVlc3QiOiB7CiAgICAgICAgImhlYWRlcnMiOiB7CiAgICAgICAgICAiQWNjZXB0IjogImFwcGxpY2F0aW9uL2pzb24iCiAgICAgICAgfSwKICAgICAgICAibWV0aG9kIjogIkdFVCIsCiAgICAgICAgInBhdGgiOiAiL3VzZXJzLzAwMSIKICAgICAgfSwKICAgICAgInJlc3BvbnNlIjogewogICAgICAgICJib2R5IjogewogICAgICAgICAgImZpcnN0TmFtZSI6ICJKb2FvIiwKICAgICAgICAgICJpZCI6ICIwMDEiLAogICAgICAgICAgImxhc3ROYW1lIjogIlNpbHZhIgogICAgICAgIH0sCiAgICAgICAgImhlYWRlcnMiOiB7CiAgICAgICAgICAiQ29udGVudC1UeXBlIjogImFwcGxpY2F0aW9uL2pzb247IGNoYXJzZXQ9dXRmLTgiCiAgICAgICAgfSwKICAgICAgICAibWF0Y2hpbmdSdWxlcyI6IHsKICAgICAgICAgICJib2R5IjogewogICAgICAgICAgICAiJC5maXJzdE5hbWUiOiB7CiAgICAgICAgICAgICAgImNvbWJpbmUiOiAiQU5EIiwKICAgICAgICAgICAgICAibWF0Y2hlcnMiOiBbCiAgICAgICAgICAgICAgICB7CiAgICAgICAgICAgICAgICAgICJtYXRjaCI6ICJ0eXBlIgogICAgICAgICAgICAgICAgfQogICAgICAgICAgICAgIF0KICAgICAgICAgICAgfSwKICAgICAgICAgICAgIiQuaWQiOiB7CiAgICAgICAgICAgICAgImNvbWJpbmUiOiAiQU5EIiwKICAgICAgICAgICAgICAibWF0Y2hlcnMiOiBbCiAgICAgICAgICAgICAgICB7CiAgICAgICAgICAgICAgICAgICJtYXRjaCI6ICJ0eXBlIgogICAgICAgICAgICAgICAgfQogICAgICAgICAgICAgIF0KICAgICAgICAgICAgfSwKICAgICAgICAgICAgIiQubGFzdE5hbWUiOiB7CiAgICAgICAgICAgICAgImNvbWJpbmUiOiAiQU5EIiwKICAgICAgICAgICAgICAibWF0Y2hlcnMiOiBbCiAgICAgICAgICAgICAgICB7CiAgICAgICAgICAgICAgICAgICJtYXRjaCI6ICJ0eXBlIgogICAgICAgICAgICAgICAgfQogICAgICAgICAgICAgIF0KICAgICAgICAgICAgfQogICAgICAgICAgfSwKICAgICAgICAgICJoZWFkZXIiOiB7fQogICAgICAgIH0sCiAgICAgICAgInN0YXR1cyI6IDIwMAogICAgICB9CiAgICB9CiAgXSwKICAibWV0YWRhdGEiOiB7CiAgICAicGFjdFJ1c3QiOiB7CiAgICAgICJmZmkiOiAiMC4zLjgiLAogICAgICAibW9kZWxzIjogIjAuNC40IgogICAgfSwKICAgICJwYWN0U3BlY2lmaWNhdGlvbiI6IHsKICAgICAgInZlcnNpb24iOiAiMy4wLjAiCiAgICB9CiAgfSwKICAicHJvdmlkZXIiOiB7CiAgICAibmFtZSI6ICJwcm92aWRlci1zZXJ2aWNlMDEiCiAgfQp9"
    }
  ]
}'

```

### Run provider tests to verify contracts and publish results to Local Pact Broker

```
dotnet test .\Provider.Tests\
```

### Check the pact and results in Pact Broker

http://localhost:9292/
