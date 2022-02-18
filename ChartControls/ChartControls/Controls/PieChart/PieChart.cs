using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Documents;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
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
        private PieChartRenderer _renderer;

        public PieChart()
        {
            this._renderer = new PieChartRenderer(500, 500);
            this.ItemsSource = new ObservableCollection<Object>();
            this.DefaultStyleKey = typeof(PieChart);
            
        }

        public async void ForceRender()
        {
            Image image = (Image)GetTemplateChild("image");
            image.Source = await this._renderer.Render(this.GetValuesFromItems());
        }

        protected override void OnApplyTemplate()
        {

            base.OnApplyTemplate();
        }

        protected override void OnItemsChanged(object e)
        {
            base.OnItemsChanged(e);
            ItemsChanged();
        }
        private void ItemsChanged()
        {
            (ItemsSource as INotifyCollectionChanged).CollectionChanged += PieChart_CollectionChanged;
            CallForRender();

        }
        
        private async void PieChart_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            CallForRender();
        }

        private async void CallForRender()
        {
            Image image = (Image)GetTemplateChild("image");
            if(image != null)
            {
                image.Source = await this._renderer.Render(this.GetValuesFromItems());
            }
            
        }

        private List<float> GetValuesFromItems()
        {
            List<float> values = new List<float>(this.Items.Count);
            //Func<Object, float> getValue = (Func<Object, float>)Delegate.CreateDelegate(typeof(Func<Object, float>), null, typeof(Object).GetProperty(this.ValuePath).GetGetMethod());
            foreach (Object item in this.Items)
            {
                values.Add((item as dynamic).Val);
            }

            return values;
        }
    }
}
