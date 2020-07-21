using System;
using System.ComponentModel;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace BadgeDemo
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {

        private int _counter = 1;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear();

            string str = _counter.ToString();

            // Create an SKPaint object to display the text
            SKPaint textPaint = new SKPaint
            {
                Color = SKColors.Red,
            };

            // Adjust TextSize property so text is 90% of screen width
            float textWidth = textPaint.MeasureText(str);
            textPaint.TextSize = 40;

            // Find the text bounds
            SKRect textBounds = new SKRect();
            textPaint.MeasureText(str, ref textBounds);

            // Calculate offsets to center the text on the screen
            float xText = info.Width / 2 - textBounds.MidX;
            float yText = info.Height / 2 - textBounds.MidY;



            // Create a new SKRect object for the frame around the text
            SKRect frameRect = textBounds;
            frameRect.Offset(xText, yText);
            frameRect.Inflate(50, 10);

            // Create an SKPaint object to display the frame
            SKPaint framePaint = new SKPaint
            {
                Style = SKPaintStyle.StrokeAndFill,
                StrokeWidth = 5,
                Color = SKColors.Blue,
                IsAntialias = true
            };

            // Draw one frame
            canvas.DrawRoundRect(frameRect, 20, 20, framePaint);

            // And draw the text
            canvas.DrawText(str, xText, yText, textPaint);


            //// Inflate the frameRect and draw another
            //frameRect.Inflate(10, 10);
            //framePaint.Color = SKColors.DarkBlue;
            //canvas.DrawRoundRect(frameRect, 30, 30, framePaint);
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            _counter++;
            CanvasView.InvalidateSurface();

            DrawToolbarItem();
        }

        private void DrawToolbarItem()
        {
            SKBitmap bitmap = new SKBitmap(80, 30, SKImageInfo.PlatformColorType, SKAlphaType.Premul);
            SKCanvas canvas = new SKCanvas(bitmap);
            SKImageInfo info = bitmap.Info;
            canvas.Clear();

            string str = _counter.ToString();

            // Create an SKPaint object to display the text
            SKPaint textPaint = new SKPaint
            {
                Color = SKColors.Red,
            };


            textPaint.TextSize = 20;

            // Find the text bounds
            SKRect textBounds = new SKRect();
            textPaint.MeasureText(str, ref textBounds);

            // Calculate offsets to center the text
            float xText = info.Width / 2 - textBounds.MidX;
            float yText = info.Height / 2 - textBounds.MidY;

            // Create a new SKRect object for the frame around the text
            SKRect frameRect = textBounds;
            frameRect.Offset(xText, yText);
            frameRect.Inflate(50, 10);

            // Create an SKPaint object to display the frame
            SKPaint framePaint = new SKPaint
            {
                Style = SKPaintStyle.StrokeAndFill,
                StrokeWidth = 5,
                Color = SKColors.Blue,
                IsAntialias = true
            };

            // Draw one frame
            canvas.DrawRoundRect(frameRect, 20, 20, framePaint);

            // And draw the text
            canvas.DrawText(str, xText, yText, textPaint);



            SKImage skImage = SKImage.FromBitmap(bitmap);
            SKData data = skImage.Encode(SKEncodedImageFormat.Png, 100);
            ToolbarItem.IconImageSource = ImageSource.FromStream(data.AsStream);
        }
    }
}
