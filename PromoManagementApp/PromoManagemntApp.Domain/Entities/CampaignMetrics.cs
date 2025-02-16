using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PromoManagemntApp.Domain.Entities
{
    public class CampaignMetrics
    {
        [Key]
        public Guid Id { get; set; }

        public int Impressions { get; set; }

        public int Clicks { get; set; }

        public int Conversions { get; set; }

        [NotMapped]
        public double ConversionRate => Clicks == 0 ? 0 : Math.Round((double)Conversions / Clicks);

        public Guid CampaignId { get; set; }
        [ForeignKey(nameof(CampaignId))]
        public Campaign? Campaign { get; set; }
    }
}
