// See https://aka.ms/new-console-template for more information
using TimeStoreExperiment;

using var client = new HttpClient();

/*var task1 = new TimeLoop().Run(10000, "Device1", client);
var task2 = new TimeLoop().Run(10000, "Device2", client);

await Task.WhenAll(task1, task2);*/

var task1 = new TimeLoop().Query(client, "T1");
var task2 = new TimeLoop().Query(client, "T2");

await Task.WhenAll(task1, task2);