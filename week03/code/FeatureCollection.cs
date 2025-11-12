public class FeatureCollection
{
    public List<Feature> Features { get; set; } = new List<Feature>();
}

public class Feature
{
    public Properties Properties { get; set; } = new Properties();
}

public class Properties
{
    public string Place { get; set; } = string.Empty;
    public double? Mag { get; set; }
}