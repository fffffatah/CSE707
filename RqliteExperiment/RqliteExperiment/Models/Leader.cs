using System.Text.Json.Serialization;

namespace RqliteExperiment.Models;

public class Leader
{
    [JsonPropertyName("addr")]
    public string Addr { get; set; }

    [JsonPropertyName("node_id")]
    public string NodeId { get; set; }
}