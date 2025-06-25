using System.Text.Json;

namespace KRD.Service;

public class AddressValidationService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey="c9c86212-f6c3-4eb0-b8e5-deba0ca4125c";

    public AddressValidationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<bool> IsAddressValidAsync(string address)
    {
        var url = $"https://geocode-maps.yandex.ru/1.x/?apikey={_apiKey}&geocode={Uri.EscapeDataString(address)}&format=json";

        var response = await _httpClient.GetAsync(url);
        if (!response.IsSuccessStatusCode) return false;

        var content = await response.Content.ReadAsStringAsync();
        var json = JsonDocument.Parse(content);

        var found = json.RootElement
            .GetProperty("response")
            .GetProperty("GeoObjectCollection")
            .GetProperty("metaDataProperty")
            .GetProperty("GeocoderResponseMetaData")
            .GetProperty("found")
            .GetString();

        return found != "0";
    }
}