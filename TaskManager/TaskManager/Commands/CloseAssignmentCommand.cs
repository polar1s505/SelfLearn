using TaskManager.Enums;
using TaskManager.Interfaces;
using TaskManager.Models;
using TaskManager.Utils;

namespace TaskManager.Commands
{
    public class CloseAssignmentCommand : ICommand<bool>
    {
        public bool Execute(User user)
        {
            UserInputReader reader = new UserInputReader();

            var activeAssignments = user.Assignments.Where(a => a.Status == AssignmentStatus.Active || a.Status == AssignmentStatus.Expired).ToList();
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
