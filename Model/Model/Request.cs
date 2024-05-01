namespace Domain.Model;

public class Request
{
    public string ClientIp { get; set; } = string.Empty;
    public DateTime RequestTime { get; set; }
    public int Z { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
}
