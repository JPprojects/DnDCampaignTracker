using DnDCampaignTracker.Models;
using Newtonsoft.Json;

namespace DnDCampaignTracker.Services;

public class PlayerService:BaseService
{
    public async Task<PlayersModel[]> AsyncBuildPlayersModel()
    {
        var model = await DeserializeJsonAsync<PlayersModel>("Players");
        PlayersModel[] viewModels = new PlayersModel[] { };

        foreach (var player in model)
        {
            if (player.Campaign == "The Diving Titans")
            {
                new PlayersModel { Name = player.Name };
            }
        }

        return viewModels;
    }
}