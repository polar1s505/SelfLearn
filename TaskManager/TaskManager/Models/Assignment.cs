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
    }
}
