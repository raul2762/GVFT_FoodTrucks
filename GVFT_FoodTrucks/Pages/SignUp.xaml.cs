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
using Data_GVFT.Business.BusinessEntities;
using MessageBoxCustomRM;

namespace GVFT_FoodTrucks.Pages
{
    /// <summary>
    /// Lógica de interacción para SignUp.xaml
    /// </summary>
    public partial class SignUp : Page
    {
        public SignUp()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            cboEmployees.ItemsSource = LoginBL.GetInstance().GetEmployeeList();
            cboEmployees.DisplayMemberPath = "Name";

            cboRoleAcc.ItemsSource = LoginBL.GetInstance().GetRoleList();
            cboRoleAcc.DisplayMemberPath = "Roles";

            cboStatusAcc.ItemsSource = LoginBL.GetInstance().GetStatusAccList();
            cboStatusAcc.DisplayMemberPath = "StatusName";
        }

        private void btnRegUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var IdEmployee = cboEmployees.SelectedIndex + 1;
                var IdRoles = cboRoleAcc.SelectedIndex + 1;
                var IdStatus = cboStatusAcc.SelectedIndex + 1;
                var newUser = new Login_FT
                {
                    Login_name = txtUser.Text,
                    Login_pass = txtPass.Password,
                    Roles = IdRoles,
                    IsActive = IdStatus,
                    Registration_date = DateTime.Now.Date,
                    Id_employee = IdEmployee,
                    Question = txtQuest.Text,
                    Answer = txtAmwser.Password
                };
                LoginBL.GetInstance().RegisterUserLogin(newUser);
                MessageBoxRM.Show("Empleado Registrado correctamente!", "Registrado!", MessageBoxButtonRM.OK, MessageBoxIconRM.Information);

                txtUser.Clear();
                txtPass.Clear();
                txtPass2.Clear();
                txtQuest.Clear();
                txtAmwser.Clear();
                cboEmployees.SelectedIndex = -1;
                cboRoleAcc.SelectedIndex = -1;
                cboStatusAcc.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBoxRM.Show(ex.InnerException.Message, "Ha ocurrido un error :(", MessageBoxButtonRM.OK, MessageBoxIconRM.Error);
            }
        }
    }
}
