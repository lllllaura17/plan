public class Activity
{
    public string Name { get; set; } = string.Empty;

    public double MinTemperature { get; set; }
    public double MaxTemperature { get; set; }

    public bool AllowRain { get; set; }
    public bool AllowLightRain { get; set; }

    public double MaxWindSpeed { get; set; }
}
