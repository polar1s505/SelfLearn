using TaskManager.Enums;

namespace TaskManager.Commands
{
    public class ShowCompletedAssignmentsCommand : ShowAssignmentsByStatusCommandBase
    {
        public ShowCompletedAssignmentsCommand() : base(AssignmentStatus.Completed) { }
    }
}
