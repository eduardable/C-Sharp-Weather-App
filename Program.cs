public class Program 
{
	static void Main(string[] args)
	{
		Console.WriteLine("Hello this is a Weather App");
		//sets location parameter
		Console.WriteLine("Type in the location:");
		var placeName = Console.ReadLine();
		var service = new WeatherService(); //create service object
		// gets several places for the user to choose from, prefix search
		var places = service.getLocation(placeName).Result; 
		Console.WriteLine("We found the following locations, which one would you like to choose?");
		for(int i=0; i<places.Count; i++)
			{
				Console.WriteLine(i+1 +". "+ places[i].name + "," + places[i].adm_area1);
			}
		
		// stores the place_id of the chosen location
		int placeNr = Int16.Parse(Console.ReadLine());
		string place_id = places[placeNr-1].place_id; 
		//retrieve all weather data
	    var weather = service.getWeather(place_id).Result;
		string degrees;
		if(weather.Units == "us"){
			degrees = " Fahrenheit";
		}
		else{
			degrees = " Celsius";
		}
		Console.WriteLine("Currently it's " + weather.Current.Summary + " at "
		 + weather.Current.Temperature + degrees);
		Console.WriteLine("Precipitation " + weather.Current.Precipitation.Total + "%"
		 + " " + weather.Current.Precipitation.Type);
		Console.WriteLine("Would you like an hourly breakdown of today's weather? y/n");
		
		if(String.Equals(Console.ReadLine(), "y", StringComparison.CurrentCultureIgnoreCase)){
			foreach (Forecast forecast in weather.Hourly.Data)
			{
				var time = service.dateToTime(forecast.Date);
				Console.WriteLine(time+": " + forecast.Summary + " at " + forecast.Temperature + degrees);
				Console.WriteLine("Precipitation " + forecast.Precipitation.Total + "%"
				 + " " + forecast.Precipitation.Type);
				Console.WriteLine();
			}
			Console.WriteLine("Thank you for using my Weather App!"); 
		}
		else{
			Console.WriteLine("Thank you for using my Weather App!"); 
		}
		Console.ReadLine();
	} 
}

