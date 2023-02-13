using System;
using System.Collections.Generic;
using System.IO;
using PactNet;
using PactNet.Infrastructure.Outputters;
using PactNet.Verifier;
using Xunit;
using Xunit.Abstractions;

namespace ReadMe.Provider.Tests
{
    public class UserApiTests : IClassFixture<UserApiFixture>
    {
        private readonly UserApiFixture fixture;
        private readonly ITestOutputHelper output;

        public UserApiTests(UserApiFixture fixture, ITestOutputHelper output)
        {
            this.fixture = fixture;
            this.output = output;
        }

            [Fact]
            public void VerifyLatestPacts()
            {
                string version = "0.0.1";

                var config = new PactVerifierConfig
                    {
                        LogLevel = PactLogLevel.Information,
                        Outputters = new List<IOutput>
                    {
                        new XUnitOutput(this.output)
                    }
                    };

                IPactVerifier verifier = new PactVerifier(config);

                verifier.ServiceProvider("provider-service01", this.fixture.ServerUri)
                        .WithPactBrokerSource(new Uri("http://localhost:9292/"), options =>
                            {
                                options.ConsumerVersionSelectors(new ConsumerVersionSelector { Latest = true })
                                    .PublishResults("0.0.1");
                            })
                        .WithProviderStateUrl(new Uri(this.fixture.ServerUri, "/provider-states"))
                        .Verify();
            }

    }
}
