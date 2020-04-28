using System.Collections.Generic;
using Microcharts;
using SkiaSharp;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Entry = Microcharts.Entry;

namespace MijnSauna.Frontend.Phone.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TemperatureChart : Grid
    {
        #region <| Construction |>

        public TemperatureChart()
        {
            InitializeComponent();
        }

        #endregion

        #region <| MinimumTemperature |>

        public int MinimumTemperature
        {
            get => (int)GetValue(MinimumTemperatureProperty);
            set => SetValue(MinimumTemperatureProperty, value);
        }

        public static readonly BindableProperty MinimumTemperatureProperty = BindableProperty.Create(
            propertyName: nameof(MinimumTemperature),
            returnType: typeof(int),
            declaringType: typeof(TemperatureChart),
            propertyChanged: OnPropertyChanged);

        #endregion

        #region <| MaximumTemperature |>

        public int MaximumTemperature
        {
            get => (int)GetValue(MaximumTemperatureProperty);
            set => SetValue(MaximumTemperatureProperty, value);
        }

        public static readonly BindableProperty MaximumTemperatureProperty = BindableProperty.Create(
            propertyName: nameof(MaximumTemperature),
            returnType: typeof(int),
            declaringType: typeof(TemperatureChart),
            propertyChanged: OnPropertyChanged);

        #endregion

        #region <| Temperatures |>

        public List<int> Temperatures
        {
            get => (List<int>)GetValue(TemperaturesProperty);
            set => SetValue(TemperaturesProperty, value);
        }

        public static readonly BindableProperty TemperaturesProperty = BindableProperty.Create(
            propertyName: nameof(Temperatures),
            returnType: typeof(List<int>),
            declaringType: typeof(TemperatureChart),
            propertyChanged: OnPropertyChanged);

        #endregion

        #region <| Event Handlers |>

        private static void OnPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var temperatureChart = bindable as TemperatureChart;
            temperatureChart?.RefreshChart();
        }

        #endregion

        #region <| Helper Methods |>

        private void RefreshChart()
        {
            ChartView.Chart = new LineChart
            {
                Entries = GenerateEntries(),
                BackgroundColor = SKColor.Parse("#DDDDDD"),
                Margin = 0f,
                LineSize = 5f,
                LineMode = LineMode.Spline,
                MinValue = MinimumTemperature - 5,
                MaxValue = MaximumTemperature + 5,
                PointMode = PointMode.None,
                LabelTextSize = 0f
            };
        }

        private IEnumerable<Entry> GenerateEntries()
        {
            var entries = new List<Entry>();

            if (Temperatures != null)
            {
                foreach (var temperature in Temperatures)
                {
                    var value = temperature;
                    if (value < MinimumTemperature) value = MinimumTemperature;
                    if (value > MaximumTemperature) value = MaximumTemperature;

                    entries.Add(GenerateEntry(value));
                }
            }

            return entries;
        }

        private Entry GenerateEntry(int value)
        {
            var r = value / (double)MaximumTemperature * 255f;
            var b = 255 - value / (double)MaximumTemperature * 255f;

            return new Entry(value)
            {
                Label = "Temperature",
                ValueLabel = $"{value}",
                Color = new SKColor((byte)r, 0, (byte)b)
            };
        }

        #endregion
    }
}