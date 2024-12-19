using ErgastF1ApiServiceCache.Application.Interfaces;
using ErgastF1ApiServiceCache.Application.Services;
using ErgastF1ApiServiceCache.Domain.Interfaces.Repositories;
using ErgastF1ApiServiceCache.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using StackExchange.Redis;
using ErgastF1ApiServiceCache.Infrastructure.Cache;
using ErgastF1ApiServiceCache.Infrastructure.Repositories;


namespace ErgastF1ApiServiceCache.Tests
{
    public class SeasonServiceTests
    {
        private readonly Mock<IDatabase> _mockDatabase;
        private readonly Mock<IHttpClientFactory> _mockHttpClientFactory;
        private readonly ISeasonService _seasonService;      


        public SeasonServiceTests()
        {
            var service = new ServiceCollection();

            service.AddScoped<IConnectionMultiplexer>(_ =>
            {               
                return ConnectionMultiplexer.Connect("localhost:32768");
            });

            // HttpClient
            service.AddHttpClient();

            // Serviços
            service.AddSingleton<ICacheService, RedisCacheService>();
            service.AddScoped<ISeasonService, SeasonService>();
            service.AddScoped<ISeasonRepository, SeasonRepository>();

            var provider = service.BuildServiceProvider();
            _seasonService = provider.GetService<ISeasonService?>();
        }
           

        [Fact]
        public async Task GetSeasons_ShouldReturnSeasons_WhenCacheMiss2()
        {
            // Arrange
            string cacheKey = "seasons";
            _mockDatabase.Setup(db => db.StringGetAsync(cacheKey, It.IsAny<CommandFlags>()))
                .ReturnsAsync(RedisValue.Null);

            var mockHttpResponse = new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StringContent("{ \"MRData\": { \"SeasonTable\": { \"Seasons\": [{ \"season\": \"1950\", \"url\": \"http://example.com\" }] } } }")
            };

            var mockHttpMessageHandler = new MockHttpMessageHandler(mockHttpResponse);
            var httpClient = new HttpClient(mockHttpMessageHandler)
            {
                BaseAddress = new Uri("http://api.jolpi.ca/ergast/f1")
            };
            _mockHttpClientFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);

            // Act
            var result = await _seasonService.GetSeasonsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("1950", result[0].season);
            _mockDatabase.Verify(db => db.StringSetAsync(
                cacheKey, It.IsAny<string>(), It.IsAny<TimeSpan>(), false, When.Always, CommandFlags.None), Times.Once);
        }
    }

    public class MockHttpMessageHandler : HttpMessageHandler
    {
        private readonly HttpResponseMessage _mockResponse;

        public MockHttpMessageHandler(HttpResponseMessage mockResponse)
        {
            _mockResponse = mockResponse;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_mockResponse);
        }
    }
}
