using TaskManager.Models;
using TaskManager.Utils;

namespace TaskManager.Commands
{
    public class ModifyAssignmentCommand : CommandBase
    {
        public override bool Execute(User user)
        {
            UserInputReader reader = new UserInputReader();

            var activeAssignments = user.Assignments.Where(a => a.Status == Enums.AssignmentStatus.Active).ToList();
            if (!activeAssignments.Any())
            {
                return false;
            }

            Guid assignmentToModify = reader.GetAssignmentId(activeAssignments);
            Assignment? assignment = user.Assignments.FirstOrDefault(a => a.Id == assignmentToModify);
            

            

            string newTitle = reader.GetTitle();
            if (!string.IsNullOrWhiteSpace(newTitle))
            {
                assignment!.Title = newTitle;
            }

            string displayMessageDescr = "Enter a new description (leave empty to keep the same):";
            string newDescription = reader.GetDescription(displayMessageDescr);
            if (!string.IsNullOrWhiteSpace(newDescription))
            {
                assignment!.Description = newDescription;
            }

            assignment!.DueDate = reader.GetDueDate();

            assignment!.LastUpdatedAt = DateTime.UtcNow;
            return true;
        }
    }
}
