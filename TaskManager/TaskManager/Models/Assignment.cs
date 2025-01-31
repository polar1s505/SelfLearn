using TaskManager.Enums;

namespace TaskManager.Models
{
    public class Assignment
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public AssignmentStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public DateTime DueDate { get; set; }

        public Assignment(Guid id, string title, string description,
            AssignmentStatus status, DateTime createdAt, DateTime lastUpdatedAt, DateTime dueDate)
        {
            Id = id;
            Title = title;
            Description = description;
            Status = status;
            CreatedAt = createdAt;
            LastUpdatedAt = lastUpdatedAt;
            DueDate = dueDate;
        }

        public void CheckExpiration()
        {
            if (Status == AssignmentStatus.Active && DueDate < DateTime.UtcNow)
            {
                Status = AssignmentStatus.Expired;
            }
        }

        public override string ToString()
        {
            return $"Title: {Title}\nDescription: {Description}\nCreated: {CreatedAt:HH:mm, dd/MM/yyyy}\n" +
                $"Last Updated: {LastUpdatedAt:HH:mm, dd/MM/yyyy}\nDeadline: {DueDate:HH:mm, dd/MM/yyyy}\nStatus: {Status}\n";
        }
    }
}
