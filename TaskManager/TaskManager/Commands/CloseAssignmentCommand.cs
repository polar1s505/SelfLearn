using System.Reflection.PortableExecutable;
using TaskManager.Enums;
using TaskManager.Models;
using TaskManager.Utils;

namespace TaskManager.Commands
{
    public class CloseAssignmentCommand : CommandBase
    {
        public override bool Execute(User user)
        {
            UserInputReader reader = new UserInputReader();

            var activeAssignments = user.Assignments.Where(a => a.Status == AssignmentStatus.Active).ToList();
            if (!activeAssignments.Any())
            {
                return false;
            }

            const string displayMessageId = "Select a task to close:";
            Guid assignmentToClose = reader.GetAssignmentId(activeAssignments, displayMessageId);

            Assignment? assignment = user.Assignments.FirstOrDefault(a => a.Id == assignmentToClose);
            assignment!.Status = AssignmentStatus.Completed;
            assignment.LastUpdatedAt = DateTime.UtcNow;

            Console.WriteLine("Task successfully closed.");
            return true;
        }
    }
}
