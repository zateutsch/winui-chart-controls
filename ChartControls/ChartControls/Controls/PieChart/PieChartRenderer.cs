using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Media.Imaging;
using SkiaSharp;
using System.Drawing;

namespace ChartControls.Controls.PieChart
{
    public class PieChartRenderer
    {
        private SKSurface _renderSurface;
        private SKCanvas _renderCanvas;
        private SKRect _boundingRect;

        private int height;
        private int width;

        private Color[] defaultColors = new Color[]
        {
            Color.Red,
            Color.Blue,
            Color.Green,
            Color.Yellow,
            Color.Pink,
            Color.Silver,
            Color.LightBlue,
            Color.GreenYellow,
            Color.HotPink,
            Color.DarkOrange
        };

        public int Height { get; set; }
        public int Width { get; set; }
        public int Radius { get; set; }

        public PieChartRenderer(int height, int width)
        {
            this.Height = height;
            this.Width = width;
            this.Radius = width/2;

            this.CreateSurface();
            this.SetCanvas();
            this.SetBoundingRect();
        }

        public PieChartRenderer(int radius)
        {
            this.Radius = radius;
            this.Height = radius * 2;
            this.Width = radius * 2;

            this.CreateSurface();
            this.SetCanvas();
            this.SetBoundingRect();
        }

        public async static Task<BitmapImage> Render(float[] values, Color[] colors)
        {
            //TODO
            return new BitmapImage();
        }

        private void CreateSurface()
        {
            SKImageInfo imageInfo = new SKImageInfo
            (
                height: this.Height,
                width: this.Width
            );

            this._renderSurface = SKSurface.Create(imageInfo);
        }

        private void SetCanvas()
        {
            this._renderCanvas = _renderSurface.Canvas;
        }

        private void SetBoundingRect()
        {
            this._boundingRect = new SKRect(this.Width, this.Height, 0, 0);
        }

        private float[] GetAngles(float[] values)
        {

            return new float[]
        }

        

        

    }
}
