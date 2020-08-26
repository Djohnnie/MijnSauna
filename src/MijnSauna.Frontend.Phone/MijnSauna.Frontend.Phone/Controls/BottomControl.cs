using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace MijnSauna.Frontend.Phone.Controls
{
    public class BottomControl : SKGLView
    {
        #region <| Color |>

        public Color Color
        {
            get => (Color)GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }

        public static readonly BindableProperty ColorProperty = BindableProperty.Create(
            propertyName: nameof(Color),
            returnType: typeof(Color),
            declaringType: typeof(BottomControl),
            defaultValue: Color.Black);

        #endregion

        #region <| Padding |>

        public Thickness Padding
        {
            get => (Thickness)GetValue(PaddingProperty);
            set => SetValue(PaddingProperty, value);
        }

        public static readonly BindableProperty PaddingProperty = BindableProperty.Create(
            propertyName: nameof(Padding),
            returnType: typeof(Thickness),
            declaringType: typeof(BottomControl),
            defaultValue: new Thickness(0));

        #endregion

        protected override void OnPaintSurface(SKPaintGLSurfaceEventArgs e)
        {
            base.OnPaintSurface(e);

            var paint = new SKPaint
            {
                Color = Color.ToSKColor(),
                Style = SKPaintStyle.Fill,
                IsAntialias = true
            };

            var bounds = e.Surface.Canvas.LocalClipBounds;
            var canvas = e.Surface.Canvas;

            var rect = new SKRect(-bounds.Width / 2, (float)Padding.Top, bounds.Width * 2 - bounds.Width / 2, bounds.Height * 8);
            canvas.DrawOval(rect, paint);
        }
    }
}