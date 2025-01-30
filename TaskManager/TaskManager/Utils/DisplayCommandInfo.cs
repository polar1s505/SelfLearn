namespace TaskManager.Utils
{
    public class DisplayCommandInfo
    {
        public void ShowAddAssignmentResult(bool success)
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

        public void ShowModifyAssignmentResult(bool success)
        {
            if (success)
            {
                Console.WriteLine("Your assignment was successfuly modified!");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("No active tasks available for modification.");
                Console.ReadLine();
            }
        }

        public void ShowDeleteAssignmentResult(bool success)
        {
            if (success)
            {
                Console.WriteLine("Your assignment was successfuly deleted!");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("No tasks available for deletion.");
                Console.ReadLine();
            }
        }

        public void ShowCloseAssignmentResult(bool success)
        {
            if (success)
            {
                Console.WriteLine("Your assignment was successfuly closed!");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("No active tasks available.");
                Console.ReadLine();
            }
        }
    }
}
