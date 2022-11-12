using System.Text.Json.Serialization;

public struct ServerConfig
{
    [JsonInclude]
    [JsonPropertyName("uri")]
    public string Uri { get; set; }
}
