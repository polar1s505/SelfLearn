namespace TaskManager.Utils
{
    public class DisplayCommandInfo
    {
        public void ShowOperationResult(bool success)
        {
            if (success)
            {
                Console.WriteLine("Done!");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Oops, something went wrong:(");
                Console.ReadLine();
            }
        }
    }
}
