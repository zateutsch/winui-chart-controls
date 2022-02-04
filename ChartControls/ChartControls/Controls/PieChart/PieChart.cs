using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Documents;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ChartControls.Controls
{
    public sealed partial class PieChart : ItemsControl
    {
        // Control Names
        private const string tempList = "tempList";

        public PieChart()
        {
            this.DefaultStyleKey = typeof(PieChart);
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            (this.ItemsSource as ObservableCollection<object>).CollectionChanged += PieChart_CollectionChanged;
        }

        private void PieChart_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
