//This whole code is just to navigate the JSON data from the API response
public class Place
{
    public string name { get; set; }
	public string adm_area1 {get; set;}
	public string place_id {get; set;}
}

public class Precipitation{
    public string Total {get; set;}
    public string Type {get; set;}
}

public class CurrentWeather
{
    public string Icon { get; set; }
    public int Icon_num { get; set; }
    public string Summary { get; set; }
    public string Temperature {get; set;}
    public Precipitation Precipitation {get; set;}
}

public class Forecast{
    public string Date {get; set;}
    public string Summary {get; set;}
    public string Temperature {get; set;}
    public Precipitation Precipitation {get; set;}

}

public class HourlyData{
    public Forecast[] Data { get; set; }
}

public class WeatherData
{
    public string Units { get; set; }
    public CurrentWeather Current { get; set; }
    public HourlyData Hourly{get; set;}
}

