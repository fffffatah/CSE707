namespace TimeStoreExperiment.Dtos;

public class Status
{
    public string NodeId { get; set; }
    
    public string State { get; set; }
    
    public string? LeaderId { get; set; }
}