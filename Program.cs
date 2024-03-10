public class Program 
{
	
	static void Main(string[] args)
	{
		Console.WriteLine("Hello this is a Weather App");
		Console.WriteLine("Type in the location:");
		var placeName = Console.ReadLine();
		var service = new WeatherService();
		var places = service.getLocation(placeName).Result;
		Console.WriteLine("We found the following locations, which one would you like to choose?");
		for(int i=0; i<places.Count; i++)
			{
				Console.WriteLine(i+1 +". "+ places[i].name + "," + places[i].adm_area1);
			}
		
		int placeNr = Int16.Parse(Console.ReadLine());
		string place_id = places[placeNr-1].place_id;
	    var weather = service.getWeather(place_id).Result;
		string degrees;
		if(weather.Units == "metric"){
			degrees = " Celsius";
		}
		else{
			degrees = " Fahrenheit";
		}
		Console.WriteLine("Currently it's " + weather.Current.Summary + " at " + weather.Current.Temperature + degrees);
		Console.ReadLine();
	} 
}

