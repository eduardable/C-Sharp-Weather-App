public class Place
{
    public string name { get; set; }
	public string adm_area1 {get; set;}
	public string place_id {get; set;}
}

public class CurrentWeather
{
    public string Icon { get; set; }
    public int Icon_num { get; set; }
    public string Summary { get; set; }
    public string Temperature {get; set;}
}

public class WeatherData
{
    public string Units { get; set; }
    public CurrentWeather Current { get; set; }
}