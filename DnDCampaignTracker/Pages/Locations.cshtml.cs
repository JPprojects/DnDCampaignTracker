using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DnDCampaignTracker.Models;
using Newtonsoft.Json;
namespace DnDCampaignTracker.Pages;

public class LocationsModel : PageModel
{
    public List<LocationModel> LocationsList { get; set; }

    public async Task OnGet()
    {
        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "locations.json");

        if (!System.IO.File.Exists(filePath))
        {
            // Handle the case where the file is not found
            ModelState.AddModelError("", "Location data not found.");
        }
        else
        {
            var jsonData = await System.IO.File.ReadAllTextAsync(filePath);

            var locationData = JsonConvert.DeserializeObject<List<LocationModel>>(jsonData);

            LocationsList = new List<LocationModel>();

            if (locationData != null)
            {
                foreach (var location in locationData)
                {
                    LocationsList.Add(new LocationModel
                    {
                        // Assign the deserialized values to the model properties
                        Name = location.Name,
                        Country = location.Country,
                        Description = location.Description,
                        Shops = location.Shops,
                    });
                }
            }
        }
    }

    // Define the LocationModel class (one single class for the location data)
    public class LocationModel
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
        public List<ShopModel> Shops { get; set; } // List of shops within the location
    }

    // Define the ShopModel class to represent individual shops
    public class ShopModel
    {
        public string Name { get; set; }
        public string Owner { get; set; }

        public List<string> Items { get; set;}

    }
}


//[Location.cshtml] <-- (View)
//    |
//    v
//[LocationsModel.cs] <-- (PageModel - Handles logic, loads data)
//    |
//    v
//[LocationModel.cs] <-- (Data Model - Holds structured data)
