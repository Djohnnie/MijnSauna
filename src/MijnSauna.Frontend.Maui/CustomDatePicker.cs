using MudBlazor;

namespace MijnSauna.Frontend.Maui;

internal class CustomDatePicker : MudDatePicker
{

    protected override string GetDayClasses(int month, DateTime day)
    {
        return base.GetDayClasses(month, day);
    }
}