using MijnSauna.Backend.Logic.Interfaces;
using ModelContextProtocol.Server;
using System.ComponentModel;
using System.Threading.Tasks;

namespace MijnSauna.Backend.Api.Tools;

[McpServerToolType]
public class MijnSaunaTools
{
    private readonly ISensorLogic _sensorLogic;

    public MijnSaunaTools(ISensorLogic sensorLogic)
    {
        _sensorLogic = sensorLogic;
    }

    [McpServerTool]
    [Description("Gets the state of the sauna: Off, Infrared heating or Finnish sauna heating.")]
    public async Task<string> GetSaunaState()
    {
        var state = await _sensorLogic.GetSaunaState();

        if (state.IsSaunaOn)
        {
            return "Finnish sauna heating";
        }

        if (state.IsInfraredOn)
        {
            return "Infrared heating";
        }

        return "Off";
    }
}