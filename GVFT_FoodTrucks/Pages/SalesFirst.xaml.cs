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

namespace GVFT_FoodTrucks
{
    /// <summary>
    /// Lógica de interacción para SalesFirst.xaml
    /// </summary>
    public partial class SalesFirst : Page
    {
        public SalesFirst()
        {
            InitializeComponent();
        }

        private void BtnPendiente_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Hola primer MessageBox desde este Sistema :)");
        }

        private void BtnImp_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void BtnProcess_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void BtnInic_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void BtnCancelar_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void BtnRemove_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void BtnRest_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void BtnAgregar_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
