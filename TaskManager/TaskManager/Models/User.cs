namespace TaskManager.Models
{
    public class User
    {
        public List<Assignment> Assignments { get; private set; }

        public User(List<Assignment> assignments)
        {
            Assignments = assignments;
        }

        public void UpdateAssignment(Assignment assignment)
        {
            Assignments.Add(assignment);
        }
    }
}
