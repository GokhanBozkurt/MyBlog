using System.Diagnostics;

Console.WriteLine("Stopwatch starting ");

var stopwatch = new Stopwatch();

stopwatch.Start();

Thread.Sleep(10000);

stopwatch.Stop();

TimeSpan elapsedTime = stopwatch.Elapsed;

Console.WriteLine("Stopwatch elapsedTime " + elapsedTime.ToString(@"m\:ss\.fff"));

Console.ReadLine();

