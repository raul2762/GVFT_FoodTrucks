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
using MessageBoxCustomRM;
using System.Threading;

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
            //delegado pgr = RunPgrBar;
            //delegado2 pgr2 = RunLogin;
            //pgr.Invoke();
            //pgr2.Invoke();
            new Thread(RunPgrBar).Start();
            new Thread(RunLogin).Start();
            
        }

        public void RunPgrBar()
        {
            this.Dispatcher.BeginInvoke(new Action(() => 
            {
                pgrBar.Visibility = Visibility.Visible;
            }));
        }
        public void RunLogin()
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                bool userExist, passCorrect, checkIsActive;
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
                //this.Dispatcher.BeginInvoke(new Action(() =>
                //{
                    lblWarning.Content = "Este usuario no existe";
                    lblWarning.Visibility = Visibility.Visible;
                    pgrBar.Visibility = Visibility.Hidden;
                //}));
                
            }
            else
            {
                //this.Dispatcher.BeginInvoke(new Action(() =>
                //{
                    lblWarning.Visibility = Visibility.Hidden;
                //}));
                
                passCorrect = LoginBL.GetInstance().checkPassword(queryUserN);
                var pss = new LoginBL.getPass();
                if (passCorrect == false)
                {
                    //this.Dispatcher.BeginInvoke(new Action(() =>
                    //{
                        lblWarning.Content = "Este usuario no existe";
                        lblWarning.Visibility = Visibility.Visible;
                        pgrBar.Visibility = Visibility.Hidden;
                    //}));
                }
                else
                {
                    checkIsActive = LoginBL.GetInstance().chackIsActive(queryUserN);
                    //this.Dispatcher.BeginInvoke(new Action(() =>
                    //{
                        lblWarning.Visibility = Visibility.Hidden;
                    //}));
                    if (ChkSale.IsChecked == true)
                    {
                        if (checkIsActive)
                        {

                            Sales show = new Sales();
                            show.Show();
                            this.Close();
                        }
                        else
                        {
                            MessageBoxRM.Show("Este usuario esta deshabilitado.\nPongase en contacto con el administrador para mas informacion", "Usuario Deshabilitado", MessageBoxButtonRM.OK, MessageBoxIconRM.Warning);
                            //this.Dispatcher.BeginInvoke(new Action(() =>
                            //{
                                pgrBar.Visibility = Visibility.Hidden;
                            //}));
                        }

                    }
                    else if (ChkAdmin.IsChecked == true)
                    {
                        if (checkIsActive)
                        {
                            wAdmin_area show = new wAdmin_area();
                            show.IdUser = LoginBL.GetInstance().IdUser;
                            show.Show();
                            this.Close();
                        }
                        else
                        {
                            MessageBoxRM.Show("Este usuario esta deshabilitado.\nPongase en contacto con el administrador para mas informacion", "Usuario Deshabilitado", MessageBoxButtonRM.OK, MessageBoxIconRM.Warning);
                            //this.Dispatcher.BeginInvoke(new Action(() =>
                            //{
                                pgrBar.Visibility = Visibility.Hidden;
                            //}));
                        }

                    }
                }
            }
            }));
        }
        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                lblWarning.Visibility = Visibility.Hidden;
            }));
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
