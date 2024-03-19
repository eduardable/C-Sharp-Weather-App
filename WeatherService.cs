using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
public class WeatherService
{
    //gets API key from user secrets
    private string getKey(){
        var config = new ConfigurationBuilder()
            .AddUserSecrets<Program>()
            .Build();
        string key = config["key"];
        return key;
    }
    //sends API request to meteosource with a given URI
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
    //expects either a name or a zip code to a location
    //e.g London or 10019 (Manhattan, NYC)
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

    //gets weather data from a place_id 
    public async Task<WeatherData> getWeather(string place_id)
	{
		var uri = "https://www.meteosource.com/api/v1/free/point?place_id="
                +place_id+"&sections=current%2Chourly&language=en&units=auto&key="
                +getKey();
        string body = sendRequest(uri).Result;
        WeatherData weatherData = JsonConvert.DeserializeObject<WeatherData>(body);
        return weatherData;
	}

    //converts yyyy-MM-ddTHH:mm:ss time format to HH:mm
    public string dateToTime(string dateString){
		// Parse the string into a DateTime object
		DateTime dateTime;
		if (DateTime.TryParseExact(dateString, "yyyy-MM-ddTHH:mm:ss",
			System.Globalization.CultureInfo.InvariantCulture,
			System.Globalization.DateTimeStyles.None, out dateTime))
		{
			// Extract the time part (HH:mm) from the DateTime object
			string timePart = dateTime.ToString("HH:mm");
			return timePart;
		}
		else
		{
			return "Invalid date and time format.";
		}
	}
}