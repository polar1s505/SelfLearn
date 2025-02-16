using Microsoft.EntityFrameworkCore;
using PromoManagemntApp.Domain.Constants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PromoManagemntApp.Domain.Entities
{
    [Table("Campaigns")]
    [Index(nameof(Status))]
    public class Campaign
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public CampaignTypeEnum Type { get; set; }

        [Required]
        public CampaignStatusEnum Status { get; set; }

  
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public ApplicationUser? User { get; set; }

        public CampaignMetrics? Metrics { get; set; }
    }
}
