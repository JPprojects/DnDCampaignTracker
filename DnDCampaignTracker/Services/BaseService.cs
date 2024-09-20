using Newtonsoft.Json;

namespace DnDCampaignTracker.Services;
public class BaseService
{
    public async Task<T[]> DeserializeJsonAsync<T>(string file)
    {
        var jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "data", file + ".json");

        if (!File.Exists(jsonFilePath))
        {
            throw new FileNotFoundException("The specified file was not found", jsonFilePath);
        }

        var jsonData = await File.ReadAllTextAsync(jsonFilePath);

        return JsonConvert.DeserializeObject<T[]>(jsonData);
    }
}