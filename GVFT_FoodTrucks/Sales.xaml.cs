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
using System.Windows.Shapes;
using System.Windows.Threading;
using MahApps.Metro.Controls;
using MessageBoxCustomRM;

namespace GVFT_FoodTrucks
{
    /// <summary>
    /// Lógica de interacción para Sales.xaml
    /// </summary>
    public partial class Sales : MetroWindow
    {
        DispatcherTimer timer = new DispatcherTimer();
        public static bool CloseSalesW = false;
        public static bool isActive = false;
        public Sales()
        {
            InitializeComponent();
        }
        
        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Tick += Timer_Tick;
            timer.Start();
            isActive = true;
            CloseSalesW = false;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (CloseSalesW)
            {
                this.Close();
            }
        }
        

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (OrdersProduction._open)
            {
                MessageBoxRM.Show("La ventana Orden en Produccion esta abierta cierrela primero antes de cerrar esta", "Ventana OrdenProduccion Abierta", MessageBoxButtonRM.OK, MessageBoxIconRM.Error);
                e.Cancel = true;
            }
            else
            {
                //Application.Current.Shutdown();
                CloseSalesW = true;
            }
        }
    }
}
