using TaskManager.Models;
using TaskManager.Utils;

namespace TaskManager.Commands
{
    public class DeleteAssignmentCommand : CommandBase
    {
        public override bool Execute(User user)
        {
            UserInputReader reader = new UserInputReader();

            var deletableAssignments = user.Assignments.Where(a => a.Status == Enums.AssignmentStatus.Active || a.Status == Enums.AssignmentStatus.Completed).ToList();
            if (!deletableAssignments.Any())
            {
                return false;
            }

            const string displayMessageId = "Select a task to delete:";
            Guid assignmentToDelete = reader.GetAssignmentId(deletableAssignments, displayMessageId);

            Assignment? assignment = user.Assignments.FirstOrDefault(a => a.Id == assignmentToDelete);
            user.Assignments.Remove(assignment!);

            return true;
        }
    }
}
