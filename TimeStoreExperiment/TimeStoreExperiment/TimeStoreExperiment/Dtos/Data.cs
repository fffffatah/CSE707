namespace TimeStoreExperiment.Dtos;

public class Data
{
    public Guid Id { get; set; }
    
    public string DeviceId { get; set; }
    
    public DateTime Timestamp { get; set; }
    
    public double Value { get; set; }
}