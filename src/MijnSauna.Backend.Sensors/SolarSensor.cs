using MijnSauna.Backend.Common.Constants;
using MijnSauna.Backend.Sensors.Configuration;
using MijnSauna.Backend.Sensors.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MijnSauna.Backend.Sensors;

public class SolarSensor : ISolarSensor
{
    private readonly IConfigurationProxy _configurationProxy;

    public SolarSensor(
        IConfigurationProxy configurationProxy)
    {
        _configurationProxy = configurationProxy;
    }

    public async Task<int> GetBatteryPercentage()
    {
        var baseAddress = await _configurationProxy.GetString(ConfigurationConstants.SOLAREDGE_API_BASE_ADDRESS);
        var authToken = await _configurationProxy.GetString(ConfigurationConstants.SOLAREDGE_API_AUTH_TOKEN);
        var siteId = await _configurationProxy.GetString(ConfigurationConstants.SOLAREDGE_SITE_ID);

        var client = new HttpClient();
        client.BaseAddress = new Uri(baseAddress);

        var now = TimeProvider.System.GetLocalNow();
        var startTime = $"{now.AddMinutes(-15):yyyy-MM-dd HH:mm:00}";
        var endTime = $"{now:yyyy-MM-dd HH:mm:00}";

        try
        {
            var result = await client.GetFromJsonAsync<StorageOverview>($"site/{siteId}/storageData?api_key={authToken}&startTime={startTime}&endTime={endTime}");

            return (int)(result.Storage.Batteries.Single().Telemetries.Last().Level ?? 0M);
        }
        catch
        {
            return 0;
        }

        throw new NotImplementedException();
    }
}

internal class StorageOverview
{
    [JsonPropertyName("storageData")]
    public Storage Storage { get; init; }
}

internal class Storage
{
    [JsonPropertyName("batteries")]
    public List<Battery> Batteries { get; init; }
}

internal class Battery
{
    [JsonPropertyName("nameplate")]
    public decimal Nameplate { get; init; }

    [JsonPropertyName("telemetries")]
    public List<Telemetry> Telemetries { get; init; }
}

internal class Telemetry
{
    [JsonPropertyName("batteryPercentageState")]
    public decimal? Level { get; init; }
}