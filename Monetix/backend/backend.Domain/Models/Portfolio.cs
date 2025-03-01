using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Domain.Models
{
    [Table("Portfolios")]
    public class Portfolio
    {
        public string ApplicationUserId { get; set; }
        public Guid StockId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public Stock Stock { get; set; }
    }
}
