using DnDCampaignTracker.Models;
using Newtonsoft.Json;

namespace DnDCampaignTracker.Services;

public class PlayerService:BaseService
{
    public async Task<List<PlayerModel>> AsyncBuildPlayerModel(string campaign)
    {
        var model = await DeserializeJsonAsync<PlayerModel>("Players");
        var playerList = new List<PlayerModel>();

        foreach (var player in model)
        {
            if (player.Campaign == campaign)
            {
                var viewModel = new PlayerModel
                {
                    Campaign = player.Campaign,
                    Name = player.Name,
                    PlayerClass = player.PlayerClass,
                };

                playerList.Add(viewModel);
            }
        }

        return playerList;
    }
}