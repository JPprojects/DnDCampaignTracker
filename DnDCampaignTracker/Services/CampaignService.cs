using DnDCampaignTracker.Data;
using DnDCampaignTracker.Models;
using Newtonsoft.Json;

namespace DnDCampaignTracker.Services;

public class CampaignService:BaseService
{
    public async Task<CampaignModel[]> BuildCampaignModelAsync()
    {
        var campaigns = await DeserializeJsonAsync<CampaignModel>("Campaigns");

        return campaigns;
    }
}

