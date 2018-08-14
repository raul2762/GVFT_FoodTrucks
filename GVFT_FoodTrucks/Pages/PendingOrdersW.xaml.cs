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
using Data_GVFT.Business.BusinessLogic;
using Serving_Table;

namespace GVFT_FoodTrucks.Pages
{
    /// <summary>
    /// Interaction logic for PendingOrdersW.xaml
    /// </summary>
    public partial class PendingOrdersW : Page
    {
        public PendingOrdersW()
        {
            InitializeComponent();
        }

        private void DtGridOrden_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void MyCbo_MouseLeave(object sender, MouseEventArgs e)
        {
            CboPopup.StaysOpen = false;
        }

        private void MyCbo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CboPopup.IsOpen = true;
            CboPopup.StaysOpen = true;
        }

        private void CboListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CboPopup.IsOpen = false;
            MyCbo.Text = CboListBox.SelectedValue.ToString();
        }

        private void btnImgBack_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new SalesFirst());
            //opaco.Radius = 7;
            //MessageBox.Show("hola nada");
            //opaco.Radius = 0;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            CboListBox.ItemsSource = GetServTables.GetInstance().Tables;
            CboListBox.SelectedIndex = 0;
        }
    }
}
