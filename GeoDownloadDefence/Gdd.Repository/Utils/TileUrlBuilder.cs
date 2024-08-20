using Gdd.Domain.Model;
using System.Text;

namespace Gdd.Repository.Utils;

public static class TileUrlBuilder
{
    public static string Build(string baseUrl, TileCoordinates coordinates)
    {
        var builder = new StringBuilder();

        builder.Append(baseUrl);
        builder.Append("/tiles/");
        builder.Append(coordinates.Z);
        builder.Append('/');
        builder.Append(coordinates.X);
        builder.Append('/');
        builder.Append(coordinates.Y);

        return builder.ToString();
    }
}
