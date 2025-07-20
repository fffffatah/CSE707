using System.Text.Json.Serialization;

namespace RqliteExperiment.Models;

public class Store
{
    [JsonPropertyName("leader")]
    public Leader Leader { get; set; }
}