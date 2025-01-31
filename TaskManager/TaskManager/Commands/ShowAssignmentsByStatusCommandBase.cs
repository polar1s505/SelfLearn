using TaskManager.Enums;
using TaskManager.Interfaces;
using TaskManager.Models;

namespace TaskManager.Commands
{
    public abstract class ShowAssignmentsByStatusCommandBase : ICommand<List<Assignment>>
    {
        protected AssignmentStatus _assignmentStatus;

        public ShowAssignmentsByStatusCommandBase(AssignmentStatus assignmentStatus)
        {
            _assignmentStatus = assignmentStatus;
        }

        public List<Assignment> Execute(User user)
        {
            var assignmentsByStatus = user.Assignments
                .Where(a => a.Status == _assignmentStatus)
                .ToList();

            return assignmentsByStatus;
        }
    }
}
