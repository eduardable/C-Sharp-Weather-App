using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
public class WeatherService
{
    private string getKey(){
        var config = new ConfigurationBuilder()
            .AddUserSecrets<Program>()
            .Build();
        string key = config["key"];
        return key;
    }
    public async Task<String> sendRequest(string url)
    {
        var client = new HttpClient();
		var request = new HttpRequestMessage
		{
			Method = HttpMethod.Get,
			RequestUri = new Uri(url)
		};
		using (var response = await client.SendAsync(request))
		{
			response.EnsureSuccessStatusCode();
			var body = await response.Content.ReadAsStringAsync();
            return body;
        }
    }
    public async Task<List<Place>> getLocation(string place)
	{
        List<String> placeList = new List<String>();
		var uri = "https://www.meteosource.com/api/v1/free/find_places_prefix?text="
                +place+"&language=en&key="
                +getKey();
		string body = sendRequest(uri).Result;
        List<Place> places = JsonConvert.DeserializeObject<List<Place>>(body);
        return places;
	}

    public async Task<WeatherData> getWeather(string place_id)
	{
		var uri = "https://www.meteosource.com/api/v1/free/point?place_id="
                +place_id+"&sections=current%2Chourly&language=en&units=auto&key="
                +getKey();
        string body = sendRequest(uri).Result;
        WeatherData weatherData = JsonConvert.DeserializeObject<WeatherData>(body);
        return weatherData;
	}
}