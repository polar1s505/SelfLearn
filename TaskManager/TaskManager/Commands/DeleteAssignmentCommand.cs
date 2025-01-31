using TaskManager.Interfaces;
using TaskManager.Models;
using TaskManager.Utils;

namespace TaskManager.Commands
{
    public class DeleteAssignmentCommand : ICommand<bool>
    {
        public bool Execute(User user)
        {
            UserInputReader reader = new UserInputReader();

            if (!user.Assignments.Any())
            {
                return false;
            }

            const string displayMessageId = "Select a task to delete:";
            Guid assignmentToDelete = reader.GetAssignmentId(user.Assignments, displayMessageId);

            Assignment? assignment = user.Assignments.FirstOrDefault(a => a.Id == assignmentToDelete);
            user.Assignments.Remove(assignment!);

            return true;
        }
    }
}
