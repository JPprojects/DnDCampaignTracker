using DnDCampaignTracker.Models;
using Newtonsoft.Json;

namespace DnDCampaignTracker.Services;

public class PlayerService:BaseService
{
    public async Task<List<PlayersModel>> AsyncBuildPlayersModel(string campaign)
    {
        var model = await DeserializeJsonAsync<PlayersModel>("Players");
        var playerList = new List<PlayersModel>();

        foreach (var player in model)
        {
            if (player.Campaign == campaign)
            {
                var viewModel = new PlayersModel
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