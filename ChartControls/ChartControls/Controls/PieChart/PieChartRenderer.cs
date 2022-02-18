using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Media.Imaging;
using SkiaSharp;
using System.Drawing;
using System.Collections;
using System.IO;

// https://www.c-sharpcorner.com/article/boosting-up-the-reflection-performance-in-c-sharp/


namespace ChartControls.Controls
{
    public class PieChartRenderer
    {
        private SKSurface _renderSurface;
        private SKCanvas _renderCanvas;
        private SKRect _boundingRect;
        private SKPoint _center;
        private List<PieChartSlice> _slices;
        private float _totalValue;

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

            this.ConstructorBase();
        }

        public PieChartRenderer(int radius)
        {
            this.Radius = radius;
            this.Height = radius * 2;
            this.Width = radius * 2;

            this.ConstructorBase();
        }

        private void ConstructorBase()
        {
            this.CreateSurface();
            this.SetCanvas();
            this.SetBoundingRect();
            this.SetCenter();
        }

        public async Task<BitmapImage> Render(List<float> values, List<Color> colors)
        {
            this.ToSlices(values, colors);
            this.SortSlices();
            return await this.Render();
        }

        public async Task<BitmapImage> Render(List<float> values)
        {
            return await Render(values, this.defaultColors);
        }

        private async Task<BitmapImage> Render()
        {
            this.Draw();
            return await GetBitmapImageFromStream();

        }

        private void Draw()
        {
            this._renderCanvas.Clear();
            float startAngle = 0;
            foreach(PieChartSlice slice in this._slices)
            {
                startAngle += DrawSlice(slice, startAngle);
            }
        }

        private float DrawSlice(PieChartSlice slice, float startAngle)
        {
            float sweepAngle = 360f * (slice.Value / this._totalValue);
            using (SKPath path = new SKPath())
            using (SKPaint fillPaint = new SKPaint())
            {
                path.MoveTo(this._center);
                path.ArcTo(this._boundingRect, startAngle, sweepAngle, false);
                path.Close();

                fillPaint.Style = SKPaintStyle.Fill;
                fillPaint.Color = slice.FillColor;

                this._renderCanvas.DrawPath(path, fillPaint);
            }

            return sweepAngle;
        }

        private async Task<BitmapImage> GetBitmapImageFromStream()
        {
            BitmapImage renderedImage = new BitmapImage();
            using (MemoryStream stream = new MemoryStream(this._renderSurface.Snapshot().Encode().ToArray()))
            {
                await renderedImage.SetSourceAsync(stream.AsRandomAccessStream());
            }

            return renderedImage;
        }

        private void ToSlices(List<float> values, List<Color> colors)
        {
            this._slices = new List<PieChartSlice>();
            this._totalValue = 0;

            int i = 0;
            foreach (float value in values)
            {
                if(i > colors.Count - 1)
                {
                    i = 0;
                }
                this._slices.Add(new PieChartSlice(value, colors[i]));
                this._totalValue += value;
                i += 1;
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
            this._boundingRect = new SKRect(0,0, this.Width, this.Height);
        }

        private void SetCenter()
        {
            this._center = new SKPoint(this.Radius, this.Radius);
        }

    }
}
