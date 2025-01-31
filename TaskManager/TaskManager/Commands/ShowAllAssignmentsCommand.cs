using TaskManager.Interfaces;
using TaskManager.Models;

namespace TaskManager.Commands
{
    public class ShowAllAssignmentsCommand : ICommand<List<Assignment>>
    {
        public List<Assignment> Execute(User user)
        {
            return user.Assignments;
        }
    }
}
