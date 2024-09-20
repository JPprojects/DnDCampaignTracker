using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DnDCampaignTracker.Models;
public class PlayerModel : PageModel
{
    public string Name {get; set;}

    public string Campaign {get; set;}

    public string PlayerClass {get; set;}
}