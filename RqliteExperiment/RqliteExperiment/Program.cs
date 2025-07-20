using System.Diagnostics;
using RqliteDotnet;
using RqliteExperiment;

var client = new RqliteClient("http://localhost:4001");
var version = await client.Ping();

const string createTable =
    "CREATE TABLE IF NOT EXISTS ExperimentSeries (Id TEXT PRIMARY KEY, TimeStamp TEXT UNIQUE NOT NULL, Value REAL NOT NULL, DeviceId TEXT NOT NULL)";


/*await client.Execute(createTable);

var task1 = new TimeLoop().Run(10000, "Device1", client);
var task2 = new TimeLoop().Run(10000, "Device2", client);
await Task.WhenAll(task1, task2);*/

for (int i = 0; i < 5; i++)
{
    var stopWatch = Stopwatch.StartNew();
    var resultDevice1 = await client.Query("SELECT * FROM ExperimentSeries ");
    var resultDevice2 = await client.Query("SELECT * FROM ExperimentSeries ");
    stopWatch.Stop();
    Console.WriteLine($"Rows returned: {resultDevice1?.Results?.First()?.Values?.Count + resultDevice2?.Results?.First()?.Values?.Count}");
    Console.WriteLine($"Query executed in {stopWatch.ElapsedMilliseconds} ms");
    
    Thread.Sleep(1000); // Sleep for 1 second
}
