using BDMS.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BDMS.Infrastructure.Services
{
    public class GoogleMapsService : IGoogleMapsService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        public GoogleMapsService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _apiKey = config["GoogleMaps:ApiKey"];
        }

        public async Task<double> GetDistanceInKm(double originLat, double originLng, double destLat, double destLng)
        {
            var url = $"https://maps.googleapis.com/maps/api/distancematrix/json" + $"?origins={originLat},{originLng}" + $"&destinations={destLat},{destLng}" + $"&key={_apiKey}";
            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            var json = JsonDocument.Parse(content);
            var distanceMeters = json.RootElement.GetProperty("rows")[0]
                                                 .GetProperty("elements")[0]
                                                 .GetProperty("distance")
                                                 .GetProperty("value")
                                                 .GetDouble();

            return distanceMeters / 1000.0; // km
        }
    }
}
