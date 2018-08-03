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
using MahApps.Metro.Controls;
using GVFT_FoodTrucks.Pages;
namespace GVFT_FoodTrucks
{
    /// <summary>
    /// Lógica de interacción para wAdmin_area.xaml
    /// </summary>
    public partial class wAdmin_area : MetroWindow
    {
        public int IdUser { get; set; }
        public wAdmin_area()
        {
            InitializeComponent();
        }

        private void btnAdminUser_Click(object sender, RoutedEventArgs e)
        {
            frameView.Navigate(new SignUp());
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            frameView.Navigate(new Home());
        }

        private void btnRegProduct_Click(object sender, RoutedEventArgs e)
        {
            frameView.Navigate(new RegProduct());
        }

        private void btnRegMerchandise_Click(object sender, RoutedEventArgs e)
        {
            frameView.Navigate(new RegMerchandise());
        }

        private void btnRegEmployee_Click(object sender, RoutedEventArgs e)
        {
            frameView.Navigate(new RegEmployee());
        }

        private void btnGenCredit_Click(object sender, RoutedEventArgs e)
        {
            frameView.Navigate(new RegCredits());
        }
    }
}
