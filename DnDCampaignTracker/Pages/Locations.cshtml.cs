using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace DnDCampaignTracker.Pages;

public class LocationsModel : PageModel
{
    public List<ContinentModel> ContinentList { get; set; } = new List<ContinentModel>();
    public List<LocationModel> SelectedLocations { get; set; } = new List<LocationModel>();

    [BindProperty]
    public string SelectedContinent { get; set; }

    public SelectList ContinentsSelectList { get; set; }

    public async Task OnGetAsync()
    {
        // Load continents and locations (this could be from a database in a real-world app)
        await PopulateContinentsAsync();

        ContinentsSelectList = new SelectList(ContinentList, "Continent", "Continent");
    }

    public async Task OnPostAsync()
    {
        // Reload the list of continents
       await PopulateContinentsAsync();

        // If a continent is selected, find its locations
        if (!string.IsNullOrEmpty(SelectedContinent))
        {
            var selectedContinent = ContinentList.FirstOrDefault(c => c.continent == SelectedContinent);
            SelectedLocations = selectedContinent?.locations ?? new List<LocationModel>();
        }

        // Reload the select list after post
        ContinentsSelectList = new SelectList(ContinentList, "Continent", "Continent");
    }

    private async Task PopulateContinentsAsync()
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

            var locationData = JsonConvert.DeserializeObject<List<ContinentModel>>(jsonData);

            ContinentList = new List<ContinentModel>();

            if (locationData != null)
            {
                foreach (var location in locationData)
                {
                    ContinentList.Add(new ContinentModel
                    {
                        // Assign the deserialized values to the model properties
                        continent = location.continent,
                        locations = location.locations
                    });
                }
            }
        }

    }

    public class ContinentModel
    {
        public string continent { get; set; }
        public List<LocationModel> locations { get; set; }
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
