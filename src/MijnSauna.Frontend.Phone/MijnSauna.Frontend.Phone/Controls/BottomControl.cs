using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace MijnSauna.Frontend.Phone.Controls
{
    public class BottomControl : SKCanvasView
    {
        public Color Color1 { get; 
            set; }

        public static readonly BindableProperty Color1Property = BindableProperty.Create(
            propertyName: nameof(Color1),
            returnType: typeof(Color),
            declaringType: typeof(SwirlControl),
            defaultValue: Color.White);

        public Color Color2 { get; set; }

        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            var paint = new SKPaint
            {
                Color = Color2.ToSKColor(),
                Style = SKPaintStyle.Fill,
                IsAntialias = true
            };

            var bounds = e.Surface.Canvas.LocalClipBounds;
            var canvas = e.Surface.Canvas;

            canvas.Clear(Color1.ToSKColor());

            var rect = new SKRect(-bounds.Width / 2, 0, bounds.Width * 2 - bounds.Width / 2, bounds.Height * 2);
            canvas.DrawOval(rect, paint);
        }
    }
}