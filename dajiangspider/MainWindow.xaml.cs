using GMap.NET.MapProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace dajiangspider
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            map.Position = new GMap.NET.PointLatLng(39, 112);
            map.Zoom = 10;
            map.DragButton = MouseButton.Left;
            map.MapProvider = GMapProviders.BingMap;
        }

        private void map_OnPositionChanged(GMap.NET.PointLatLng point)
        {
            (DataContext as MainWindowViewModel).Lat = point.Lat;
            (DataContext as MainWindowViewModel).Lng = point.Lng;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
          await  (DataContext as MainWindowViewModel).RequestAsync();
        }
    }
}
