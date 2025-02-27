using Microsoft.EntityFrameworkCore;
using PromoManagementPlatform.Domain.Abstract;
using PromoManagementPlatform.Domain.Constants;
using PromoManagementPlatform.Domain.Entities;
using PromoManagementPlatform.Infrastructure.Persistence;

namespace PromoManagementPlatform.Infrastructure.Implementations
{
    public class CampaignRepository : ICampaignRepository
    {
        private readonly ApplicationDbContext _context;

        public CampaignRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateCampaignAsync(Campaign campaign)
        {
            await _context.Campaigns.AddAsync(campaign);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCampaignAsync(Guid campaignId)
        {
            var campaign = await _context.Campaigns.FindAsync(campaignId);

            if (campaign != null)
            {
                _context.Campaigns.Remove(campaign);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Campaign>> GetManagerCampaignsAsync(string managerId)
        {
            return await _context.Campaigns.Where(c => c.UserId == managerId)
                .OrderBy(c => c.StartDate)
                .Select(c => new Campaign
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate,
                    Type = c.Type,
                    Status = c.Status,
                }).ToListAsync();
        }

        public async Task<Campaign?> GetCampaignByIdAsync(Guid campaignId)
        {
            
            return await _context.Campaigns.FindAsync(campaignId);
        }

        public async Task UpdateCampaignStatusAsync(Guid campaignId, CampaignStatusEnum status)
        {
            var campaign = await _context.Campaigns.FindAsync(campaignId);

            if (campaign != null)
            {
                campaign.Status = status;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Campaign>> GetCampaignsAsync()
            => await _context.Campaigns.ToListAsync();
    }
}
