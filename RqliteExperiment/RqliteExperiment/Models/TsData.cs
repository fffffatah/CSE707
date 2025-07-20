using System.Text.Json.Serialization;

namespace RqliteExperiment.Models;

public class TsData
{
    [JsonPropertyName("id")]
    public string Id { get; set; }
    
    [JsonPropertyName("timestamp")]
    public string TimeStamp { get; set; }
    
    [JsonPropertyName("value")]
    public float Value { get; set; }
    
    [JsonPropertyName("deviceId")]
    public string DeviceId { get; set; }
}