using TaskManager.Enums;

namespace TaskManager.Commands
{
    public class ShowActiveAssignmentsCommand : ShowAssignmentsByStatusCommandBase
    {
        public ShowActiveAssignmentsCommand() : base(AssignmentStatus.Active) { }
    }
}
