using System.Text.Json.Serialization;

namespace Gdd.Domain.Model;

public class TileCoordinates
{
    [JsonPropertyName("x")]
    public int X { get; set; }

    [JsonPropertyName("y")]
    public int Y { get; set; }

    [JsonPropertyName("z")]
    public int Z { get; set; }
}
