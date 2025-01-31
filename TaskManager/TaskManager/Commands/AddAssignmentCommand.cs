using TaskManager.Enums;
using TaskManager.Interfaces;
using TaskManager.Models;
using TaskManager.Utils;

namespace TaskManager.Commands
{
    public class AddAssignmentCommand : ICommand<bool>
    {
        private readonly AssignmentStatus _assignmentStatus;
        public AddAssignmentCommand()
        {
            _assignmentStatus = AssignmentStatus.Active;
        }

        public bool Execute(User user)
        {
            UserInputReader reader = new UserInputReader();
            Guid id = Guid.NewGuid();
            string title = reader.GetTitle(id);
            string description = reader.GetDescription();
            DateTime createdAt = DateTime.UtcNow;
            DateTime updatedAt = DateTime.UtcNow;
            DateTime dueDate = reader.GetDueDate();
            Assignment assignment = new Assignment(id, title, description, _assignmentStatus, createdAt, updatedAt, dueDate);
            user.UpdateAssignment(assignment);

            return (assignment != null) ? true : false;
        }
    }
}
