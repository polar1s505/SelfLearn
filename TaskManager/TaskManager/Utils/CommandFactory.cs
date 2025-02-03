using TaskManager.Commands;
using TaskManager.Interfaces;
using TaskManager.Models;

namespace TaskManager.Utils
{
    public class CommandFactory
    {
        public static ICommand<T>? GetCommand<T>(string choice)
        {
            return choice switch
            {
                "1" => (ICommand<T>)new AddAssignmentCommand(),
                "2" => (ICommand<T>)new ModifyAssignmentCommand(),
                "3" => (ICommand<T>)new CloseAssignmentCommand(),
                "4" => (ICommand<T>)new DeleteAssignmentCommand(),
                "5" => (ICommand<T>)new ShowAllAssignmentsCommand(),
                "6" => (ICommand<T>)new ShowAssignmentsByPeriodCommand(),
                "7" => (ICommand<T>)new ShowActiveAssignmentsCommand(),
                "8" => (ICommand<T>)new ShowExpiredAssignmentsCommand(),
                "9" => (ICommand<T>)new ShowCompletedAssignmentsCommand(),
                "10" => (ICommand<T>)new ShowAssignmentsByDueDate(),
                _ => null
            };
        }
    }
}
