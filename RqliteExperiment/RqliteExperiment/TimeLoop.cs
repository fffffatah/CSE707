using System.Diagnostics;
using System.Text.Json;
using RqliteDotnet;
using RqliteExperiment.Models;

namespace RqliteExperiment;

public class TimeLoop
{
    public async Task Run(int iterations, string deviceId, RqliteClient client)
    {
        var httpClient = new HttpClient();

        for (var i = 0; i < iterations; i++)
        {
            using var stream =
                await httpClient.GetAsync("http://localhost:4001/status?pretty");
            var response = stream.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var status = JsonSerializer.Deserialize<Status>(responseBody);

            var id = Guid.NewGuid().ToString();
            var timeStamp = DateTime.UtcNow.ToString("o");
            var value = Math.Sin(i * 0.1);
            var insertQuery = $"INSERT INTO ExperimentSeries (Id, TimeStamp, Value, DeviceId) VALUES ('{id}', '{timeStamp}', {value}, '{deviceId}')";

            try
            {
                var stopWatch = Stopwatch.StartNew();
                var executeResults = await client.Execute(insertQuery);
                stopWatch.Stop();
                Console.WriteLine($"Inserted: {id}, DeviceId: {deviceId}, Time: {timeStamp}, Value: {value}, LeaderAddr: {status?.Store.Leader.Addr},LeaderAddr: {status?.Store.Leader.NodeId}, ExecutionTime: {stopWatch.ElapsedMilliseconds} ms");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting data: {ex.Message}");
            }

            Thread.Sleep(1000); // Sleep for 1 second
        }
    }
}