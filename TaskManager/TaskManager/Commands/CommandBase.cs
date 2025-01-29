using TaskManager.Enums;
using TaskManager.Interfaces;
using TaskManager.Models;
using TaskManager.Utils;

namespace TaskManager.Commands
{
    public abstract class CommandBase : ICommand<bool>
    {
        protected AssignmentStatus _assignmentStatus;

        protected CommandBase(AssignmentStatus assignmentStatus)
        {
            _assignmentStatus = assignmentStatus;
        }

        public abstract bool Execute(User user);

    }
}
