using System.Text.Json.Serialization;

namespace AgendaTenis.Cidades.WebApi.DTOs;

public class CidadeDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Nome { get; set; }

    [JsonPropertyName("state")]
    public string Estado { get; set; }

    [JsonPropertyName("country")]
    public string Pais { get; set; }

    [JsonPropertyName("coord")]
    public CoordenadasDto Coordenadas { get; set; }
}
