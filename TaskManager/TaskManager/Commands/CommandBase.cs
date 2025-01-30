using TaskManager.Interfaces;
using TaskManager.Models;

namespace TaskManager.Commands
{
    public abstract class CommandBase : ICommand<bool>
    {
        public abstract bool Execute(User user);

    }
}
