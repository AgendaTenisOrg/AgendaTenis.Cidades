using System.Text.Json.Serialization;

namespace AgendaTenis.Cidades.WebApi.DTOs;

public class CoordenadasDto
{
    [JsonPropertyName("lon")]
    public double Longitude { get; set; }

    [JsonPropertyName("lat")]
    public double Latitude { get; set; }
}
