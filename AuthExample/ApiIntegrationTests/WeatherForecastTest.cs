using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using WeatherApp;
using Xunit;

namespace ApiIntegrationTests
{
    public class WeatherForecastTest
    {
        private readonly HttpClient _client;
        private readonly ITokenManager _tokenManager;

        public WeatherForecastTest(ITokenManager tokenManager, IHttpClientFactory clientFactory)
        {
            _tokenManager = tokenManager;
            _client = clientFactory.CreateClient();
        }

        [Fact]
        public async Task TodaysWeather_AdminUser_ReturnResult()
        {
            //Arrange
            var user = new IdentityUser
            {
                UserName = "TestUser",
                Email = "TestUser@test.com"
            };
            var accessToken = _tokenManager.GenerateJWTToken(user, "Admin");
            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

            //Act
            var response = await _client.GetAsync("https://localhost:44377/api/WeatherForecast/TodaysWeather");

            //Assert
            Assert.True(response.IsSuccessStatusCode);

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<IEnumerable<WeatherForecast>>(content);
           
            Assert.True(result.Count() == 1);
            Assert.Equal(DateTime.Today.DayOfWeek.ToString(), result.First().dayOfWeek);
            Assert.Equal(DateTime.Today.ToShortDateString(), result.First().date);
        }

        [Fact]
        public async Task TodaysWeather_CommonUser_ReturnForbidden()
        {
            //Arrange
            var user = new IdentityUser
            {
                UserName = "TestUser",
                Email = "TestUser@test.com"
            };
            var accessToken = _tokenManager.GenerateJWTToken(user, "Common");
            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

            //Act
            var response = await _client.GetAsync("https://localhost:44377/api/WeatherForecast/TodaysWeather");

            //Assert
            Assert.False(response.IsSuccessStatusCode);
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.Forbidden);
        }

        [Fact]
        public async Task Forecast_CommonUser_ReturnResult()
        {
            //Arrange
            var user = new IdentityUser
            {
                UserName = "TestUser",
                Email = "TestUser@test.com"
            };
            var accessToken = _tokenManager.GenerateJWTToken(user, "Common");
            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

            //Act
            var response = await _client.GetAsync("https://localhost:44377/api/WeatherForecast/Forecast");

            //Assert
            Assert.True(response.IsSuccessStatusCode);

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<IEnumerable<WeatherForecast>>(content);

            Assert.True(result.Count() == 5);
            Assert.Equal(DateTime.Today.AddDays(1).DayOfWeek.ToString(), result.First().dayOfWeek);
            Assert.Equal(DateTime.Today.AddDays(1).ToShortDateString(), result.First().date);
        }

        [Fact]
        public async Task Forecast_AdminUser_ReturnForbidden()
        {
            //Arrange
            var user = new IdentityUser
            {
                UserName = "TestUser",
                Email = "TestUser@test.com"
            };
            var accessToken = _tokenManager.GenerateJWTToken(user, "Admin");
            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

            //Act
            var response = await _client.GetAsync("https://localhost:44377/api/WeatherForecast/Forecast");

            //Assert
            Assert.False(response.IsSuccessStatusCode);
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.Forbidden);
        }
    }
}
