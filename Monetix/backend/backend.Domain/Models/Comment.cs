using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Domain.Models
{
    [Table("Comments")]
    public class Comment
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        [ForeignKey(nameof(StockID))]
        public Guid? StockID { get; set; }
        public Stock? Stock { get; set; }

        [ForeignKey(nameof(ApplicationUserId))]
        public string? ApplicationUserId { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }
    }
}
