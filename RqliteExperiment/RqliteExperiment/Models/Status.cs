using System.Text.Json.Serialization;

namespace RqliteExperiment.Models;

public class Status
{
    [JsonPropertyName("store")]
    public Store Store { get; set; }
}