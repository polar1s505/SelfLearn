using TaskManager.Models;

namespace TaskManager.Utils
{
    public class DisplayCommandInfo
    {
        public void ShowAssignmentResult(bool success)
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

        public void ShowAssignments(List<Assignment> assignments)
        {
            if (!assignments.Any())
            {
                Console.WriteLine("No tasks available.");
            }

            foreach (var assignment in assignments)
            {
                Console.WriteLine(assignment);
            }

            Console.ReadLine();
        }
    }
}
