using System.Drawing;
using System.Collections.Generic;

// https://docs.microsoft.com/en-us/troubleshoot/developer/visualstudio/csharp/general/use-icomparable-icomparer

namespace ChartControls.Controls
{
    public class PieChartSlice
    {
        public float StartValue { get; set; }
        public float EndValue { get; set; }
        public float Value { get; set; }
        public Color FillColor { get; set; }

        public PieChartSlice(float value, Color fillColor)
        {
            this.Value = Value;
            this.FillColor = fillColor;
        }

        public static IComparer<PieChartSlice> SortValueAscending()
        {
            return new SortValueAscendingHelper();
        }

        public static IComparer<PieChartSlice> SortValueDescending()
        {
            return new SortValueDescendingHelper();
        }

        private class SortValueAscendingHelper : IComparer<PieChartSlice>
        {
            int IComparer<PieChartSlice>.Compare(PieChartSlice slice1, PieChartSlice slice2)
            {

                if (slice1.Value < slice2.Value)
                    return 1;

                if (slice1.Value > slice2.Value)
                    return -1;

                else
                    return 0;
            }
        }

        private class SortValueDescendingHelper : IComparer<PieChartSlice>
        {
            int IComparer<PieChartSlice>.Compare(PieChartSlice slice1, PieChartSlice slice2)
            {

                if (slice1.Value > slice2.Value)
                    return 1;

                if (slice1.Value < slice2.Value)
                    return -1;

                else
                    return 0;
            }
        }
    }
}
