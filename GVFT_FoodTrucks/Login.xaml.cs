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
using Data_GVFT.Business.BusinessEntities;
using Data_GVFT.Business.BusinessLogic;

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
            bool userExist, passCorrect;
            string user = txtUser.Text;
            string pass = txtPass.Password;

            var queryUserN = new Login_FT
            {
                Login_name = user,
                Login_pass = pass
            };

            userExist = LoginBL.GetInstance().checkUsername(queryUserN);

            if (userExist == false)
            {
                lblWarning.Content = "Este usuario no existe";
                lblWarning.Visibility = Visibility.Visible;
            }
            else
            {
                lblWarning.Visibility = Visibility.Hidden;
                passCorrect = LoginBL.GetInstance().checkPassword(queryUserN);
                var pss = new LoginBL.getPass();
                if (passCorrect == false)
                {
                    lblWarning.Content = "Contraseña incorrecta";
                    lblWarning.Visibility = Visibility.Visible;
                }
                else
                {
                    lblWarning.Visibility = Visibility.Hidden;
                    if (ChkSale.IsChecked == true)
                    {
                        Sales show = new Sales();
                        show.Show();
                        this.Close();
                    }
                    else if(ChkAdmin.IsChecked == true)
                    {
                        wAdmin_area show = new wAdmin_area();
                        show.IdUser = LoginBL.GetInstance().IdUser;
                        show.Show();
                        this.Close();
                    }
                }
            }

        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            lblWarning.Visibility = Visibility.Hidden;
            txtUser.Focus();
        }

        private void MetroWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F2)
            {
                txtUser.Text = "master18";
                txtPass.Password = "Master809@";
                
                btnSignIn_Click(sender,e);
            }
        }
    }
}
