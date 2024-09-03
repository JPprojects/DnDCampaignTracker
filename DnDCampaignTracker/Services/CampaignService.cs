using DnDCampaignTracker.Data;
using DnDCampaignTracker.Models;
using Newtonsoft.Json;

namespace DnDCampaignTracker.Services;

public class CampaignService
{
    public async Task<CampaignModel[]> BuildCampaignModelAsync()
    {
        var jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "data", "campaigns.json");

        if (!File.Exists(jsonFilePath))
        {
            throw new FileNotFoundException("The specified file was not found", jsonFilePath);
        }

        var jsonData = await File.ReadAllTextAsync(jsonFilePath);
        var campaigns = JsonConvert.DeserializeObject<CampaignModel[]>(jsonData);

        return campaigns;
    }
}

