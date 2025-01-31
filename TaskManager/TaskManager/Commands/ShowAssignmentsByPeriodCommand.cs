using TaskManager.Interfaces;
using TaskManager.Models;
using TaskManager.Utils;

namespace TaskManager.Commands
{
    public class ShowAssignmentsByPeriodCommand : ICommand<List<Assignment>>
    {
        public List<Assignment> Execute(User user)
        {
            UserInputReader reader = new UserInputReader();

            DateTime startDate = reader.GetStartDate();
            DateTime endDate = reader.GetEndDate();

            var assignmentsInRange = user.Assignments
                .Where(a => a.CreatedAt >= startDate && a.CreatedAt <= endDate)
                .ToList();

            return assignmentsInRange;
        }
    }
}
