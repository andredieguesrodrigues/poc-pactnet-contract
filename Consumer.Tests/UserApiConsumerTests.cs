using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using PactNet;
using PactNet.Matchers;
using Xunit;

namespace ReadMe.Consumer.Tests
{
    public class UserApiConsumerTests
    {
        private readonly IPactBuilderV3 pactBuilder;

        public UserApiConsumerTests()
        {
            // specify custom log and pact directories
            var pact = Pact.V3("consumer-service01", "provider-service01", new PactConfig
            {
                PactDir = $"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName}{Path.DirectorySeparatorChar}pacts"
            });

            // Initialize Rust backend
            this.pactBuilder = pact.WithHttpInteractions();
        }

        [Fact]
        public async Task GetUser_WhenTheUserIdExists_ReturnsDatUser()
        {
            // Arrange
            this.pactBuilder
                .UponReceiving("A GET request to retrieve the user")
                    .Given("There is a user with id '001'")
                    .WithRequest(HttpMethod.Get, "/users/001")
                    .WithHeader("Accept", "application/json")
                .WillRespond()
                    .WithStatus(HttpStatusCode.OK)
                    .WithHeader("Content-Type", "application/json; charset=utf-8")
                    .WithJsonBody(new
                    {
                        id = Match.Type("001"),
                        firstName = Match.Type("Ozzy"),
                        lastName = Match.Type("Osbourne")
                    });

            await this.pactBuilder.VerifyAsync(async ctx =>
            {
                // Act
                var client = new UserApiClient(ctx.MockServerUri);
                var user = await client.GetUser("001");

                // Assert
                Assert.Equal("001", user.Id);
            });
        }
    }
}
