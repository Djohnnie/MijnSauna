using MijnSauna.Backend.Logic.Interfaces;
using MijnSauna.Common.DataTransferObjects.Sessions;
using ModelContextProtocol.Server;
using System.ComponentModel;
using System.Threading.Tasks;

namespace MijnSauna.Backend.Api.Tools;

[McpServerToolType]
public class MijnSaunaTools
{
    private readonly ISensorLogic _sensorLogic;
    private readonly ISessionLogic _sessionLogic;

    public MijnSaunaTools(ISensorLogic sensorLogic)
    {
        _sensorLogic = sensorLogic;
    }

    [McpServerTool(Name = $"mijnsauna_{nameof(GetSaunaState)}", ReadOnly = true)]
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

    [McpServerTool(Name = $"mijnsauna_{nameof(GetSaunaTemperature)}", ReadOnly = true)]
    [Description("Gets the temperature, in degrees Celcius, inside the sauna cabin.")]
    public async Task<string> GetSaunaTemperature()
    {
        var state = await _sensorLogic.GetSaunaTemperature();

        return $"{state.Temperature} °C";
    }

    [McpServerTool(Name = $"mijnsauna_{nameof(StartSaunaSession)}", Idempotent = true)]
    [Description("Start a sauna session.")]
    public async Task<string> StartSaunaSession()
    {
        var currentSession = await _sessionLogic.GetActiveSession();

        if (currentSession is not null)
        {
            return $"There is {(currentSession.IsSauna ? "a sauna" : "an infrared")} session already active.";
        }

        await _sessionLogic.QuickStartSession(new QuickStartSessionRequest { IsSauna = true });

        return "A sauna session was started.";
    }

    [McpServerTool(Name = $"mijnsauna_{nameof(StartInfraredSession)}", Idempotent = true)]
    [Description("Start an infrared session.")]
    public async Task<string> StartInfraredSession()
    {
        var currentSession = await _sessionLogic.GetActiveSession();

        if (currentSession is not null)
        {
            return $"There is {(currentSession.IsSauna ? "a sauna" : "an infrared")} session already active.";
        }

        await _sessionLogic.QuickStartSession(new QuickStartSessionRequest { IsInfrared = true });

        return "An infrared session was started.";
    }

    [McpServerTool(Name = $"mijnsauna_{nameof(SwitchSaunaOff)}", Idempotent = true)]
    [Description("Switch off the sauna.")]
    public async Task<string> SwitchSaunaOff()
    {
        var currentSession = await _sessionLogic.GetActiveSession();

        if (currentSession is null)
        {
            return "There is no active session to switch off.";
        }

        await _sessionLogic.CancelSession(currentSession.SessionId);

        return $"The current {(currentSession.IsSauna ? "sauna" : "infrared")} session was stopped.";
    }
}