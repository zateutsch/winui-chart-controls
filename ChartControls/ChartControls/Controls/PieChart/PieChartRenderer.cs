using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Media.Imaging;
using SkiaSharp;
using System.Drawing;
using System.Collections;

// https://www.c-sharpcorner.com/article/boosting-up-the-reflection-performance-in-c-sharp/


namespace ChartControls.Controls
{
    public class PieChartRenderer
    {
        private SKSurface _renderSurface;
        private SKCanvas _renderCanvas;
        private SKRect _boundingRect;
        private List<PieChartSlice> _slices;

        private List<Color> defaultColors = new List<Color>
        (
            new Color[]
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
            }
        );

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

        public async Task<BitmapImage> Render(List<float> values, List<Color> colors)
        {
            this.ToSlices(values, colors);
            this.SortSlices();
            return new BitmapImage();
        }

        private void ToSlices(List<float> values, List<Color> colors)
        {
            this._slices = new List<PieChartSlice>();

            int i = 0;
            foreach (float value in values)
            {
                this._slices.Add(new PieChartSlice(value, colors[i]));
                i++;
            }

        }

        private void SortSlices(bool ascending = false)
        {
            IComparer<PieChartSlice> comparer = ascending ? PieChartSlice.SortValueAscending() : PieChartSlice.SortValueDescending();
            this._slices.Sort(comparer);
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
            this._boundingRect = new SKRect(0, 0, this.Width, this.Height);
        }

        

        

        

    }
}
