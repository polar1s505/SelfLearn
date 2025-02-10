int factoryStock = 0;
object lockObj = new object();
const int truckCount = 100;

Task[] tasks = new Task[truckCount * 2];

for (int i = 0; i < truckCount; i++)
{
    tasks[i] = Task.Run(() =>
    {
        lock (lockObj)
        {
            factoryStock++;
        }
    });

    tasks[i + truckCount] = Task.Run(() =>
    {
        lock (lockObj)
        {
            factoryStock--;
        }
    });
}

Task.WaitAll(tasks);

Console.WriteLine("Final factory stock: " + factoryStock);