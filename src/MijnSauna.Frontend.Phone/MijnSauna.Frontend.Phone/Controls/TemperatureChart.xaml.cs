using System.Collections.Generic;
using Microcharts;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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

        #region <| ChartBackgroundColor |>

        public Color ChartBackgroundColor
        {
            get => (Color)GetValue(ChartBackgroundColorProperty);
            set => SetValue(ChartBackgroundColorProperty, value);
        }

        public static readonly BindableProperty ChartBackgroundColorProperty = BindableProperty.Create(
            propertyName: nameof(ChartBackgroundColor),
            returnType: typeof(Color),
            declaringType: typeof(TemperatureChart),
            propertyChanged: OnPropertyChanged);

        #endregion

        #region <| ChartColdColor |>

        public Color ChartColdColor
        {
            get => (Color)GetValue(ChartColdColorProperty);
            set => SetValue(ChartColdColorProperty, value);
        }

        public static readonly BindableProperty ChartColdColorProperty = BindableProperty.Create(
            propertyName: nameof(ChartColdColor),
            returnType: typeof(Color),
            declaringType: typeof(TemperatureChart),
            propertyChanged: OnPropertyChanged);

        #endregion

        #region <| ChartColdColor |>

        public Color ChartHotColor
        {
            get => (Color)GetValue(ChartHotColorProperty);
            set => SetValue(ChartHotColorProperty, value);
        }

        public static readonly BindableProperty ChartHotColorProperty = BindableProperty.Create(
            propertyName: nameof(ChartHotColor),
            returnType: typeof(Color),
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
                Entries = GenerateChartEntries(),
                BackgroundColor = ChartBackgroundColor.ToSKColor(),
                Margin = 0f,
                LineSize = 5f,
                LineMode = LineMode.Spline,
                MinValue = MinimumTemperature - 5,
                MaxValue = MaximumTemperature + 5,
                PointMode = PointMode.None,
                LabelTextSize = 0f
            };
        }

        private IEnumerable<ChartEntry> GenerateChartEntries()
        {
            var entries = new List<ChartEntry>();

            if (Temperatures != null)
            {
                foreach (var temperature in Temperatures)
                {
                    var value = temperature;
                    if (value < MinimumTemperature) value = MinimumTemperature;
                    if (value > MaximumTemperature) value = MaximumTemperature;

                    entries.Add(GenerateChartEntry(value));
                }
            }

            return entries;
        }

        private ChartEntry GenerateChartEntry(int value)
        {
            var r = value / (double)MaximumTemperature * 255f;
            var b = 255 - value / (double)MaximumTemperature * 255f;

            return new ChartEntry(value)
            {
                Label = "Temperature",
                ValueLabel = $"{value}",
                //Color = new SKColor((byte)r, 0, (byte)b)
                Color = ChartHotColor.ToSKColor()
            };
        }

        #endregion
    }
}