using TaskManager.Enums;

namespace TaskManager.Commands
{
    public class ShowExpiredAssignmentsCommand : ShowAssignmentsByStatusCommandBase
    {
        public ShowExpiredAssignmentsCommand() : base(AssignmentStatus.Expired) { }
    }
}
