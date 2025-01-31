using TaskManager.Interfaces;
using TaskManager.Models;
using TaskManager.Enums;

namespace TaskManager.Commands
{
    public class ShowAssignmentsByDueDate : ICommand<List<Assignment>>
    {
        public List<Assignment> Execute(User user)
        {
            var orderedList = user.Assignments
                .Where(a => a.Status != AssignmentStatus.Completed)
                .OrderBy(a => a.DueDate).ToList();
            
            return orderedList;
        }
    }
}
