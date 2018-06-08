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

namespace GVFT_FoodTrucks
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : MetroWindow
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSignIn_MouseEnter(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            btn.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFDE7000"));
        }

        private void btnSignIn_MouseLeave(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            btn.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFF8000"));
        }

        private void btnSignIn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Button btn = sender as Button;
            btn.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFF9224"));
        }

        private void btnSignIn_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Button btn = sender as Button;
            btn.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFDE7000"));
        }
    }
}
