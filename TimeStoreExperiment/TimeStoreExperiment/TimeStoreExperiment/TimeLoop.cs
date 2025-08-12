using System.Diagnostics;
using TimeStoreExperiment.Dtos;

namespace TimeStoreExperiment;

public class TimeLoop
{
    private const string apiUrl = "http://localhost:5002/api/TimeSeries";
    private const string statusUrl = "http://localhost:5002/raft/status";
    
    public async Task Run(int iterations, string deviceId, HttpClient client)
    {
        var totalTime = new List<float>();
        
        for (var i = 0; i < iterations; i++)
        {
            var data = new Data()
            {
                Id = Guid.NewGuid(),
                DeviceId = deviceId,
                Timestamp = DateTime.UtcNow,
                Value = Math.Sin(i * 0.1)
            };
            
            var jsonContent = System.Text.Json.JsonSerializer.Serialize(data);
            var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
            
            try
            {
                var statusResponse = await client.GetAsync(statusUrl);
                statusResponse.EnsureSuccessStatusCode();
                
                var status = await statusResponse.Content.ReadAsStringAsync();
                var statusData = System.Text.Json.JsonSerializer.Deserialize<Status>(status);
                
                var stopWatch = Stopwatch.StartNew();
                var response = await client.PostAsync(apiUrl, content);
                stopWatch.Stop();
                
                response.EnsureSuccessStatusCode();
                
                Console.WriteLine($"Inserted: {data.Id}, DeviceId: {data.DeviceId}, Time: {data.Timestamp}, LeaderId: {statusData?.LeaderId}, ExecutionTime: {stopWatch.ElapsedMilliseconds} ms");
                totalTime.Add(stopWatch.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting data: {ex.Message}");
            }
            
            await Task.Delay(1000); // Sleep for 1 second
        }
        
        if (totalTime.Count > 0)
        {
            Console.WriteLine($"Average execution time over {iterations} iterations: {totalTime.Average()} ms");
        }
        else
        {
            Console.WriteLine("No data was inserted.");
        }
    }

    public async Task Query(HttpClient client, string task)
    {
        for (int i = 0; i < 5; i++)
        {
            try
            {
                var stopWatch = Stopwatch.StartNew();
                await client.GetAsync(apiUrl);
                stopWatch.Stop();
                Console.WriteLine($"{task}: Query executed in {stopWatch.ElapsedMilliseconds} ms");

                Thread.Sleep(1000); // Sleep for 1 second
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}