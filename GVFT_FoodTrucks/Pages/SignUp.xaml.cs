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
using System.Windows.Threading;
using System.Windows.Media.Animation;

namespace GVFT_FoodTrucks.Pages
{
    /// <summary>
    /// Lógica de interacción para SignUp.xaml
    /// </summary>
    public partial class SignUp : Page
    {
        DispatcherTimer timer = new DispatcherTimer();
        Storyboard storyboard;
        bool _started;
        int idEmployeeUpdate;
        bdrValidationIcon validationIcon;
        public SignUp()
        {
            InitializeComponent();
        }
        enum bdrValidationIcon
        {
            Pass,
            Price,
            Caegory
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            RechargeAllCbo();
            bdrValidation.Visibility = Visibility.Hidden;
            storyboard = this.TryFindResource("efectValidationbdr") as Storyboard;
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (!_started)
            {
                if (storyboard != null)
                {
                    switch (validationIcon)
                    {
                        case bdrValidationIcon.Pass:
                            storyboard.Begin(bdrValidation);
                            break;
                        default:
                            break;
                    }

                    _started = true;
                }
            }
            else
            {
                _started = false;
                if (storyboard != null)
                {
                    storyboard.Stop(bdrValidation);
                }
            }
        }

        public void RechargeAllCbo()
        {
            cboEmployees.ItemsSource = LoginBL.GetInstance().GetEmployeeList();
            cboEmployees.DisplayMemberPath = "Name";

            cboRoleAcc.ItemsSource = LoginBL.GetInstance().GetRoleList();
            cboRoleAcc.DisplayMemberPath = "Roles";

            cboStatusAcc.ItemsSource = LoginBL.GetInstance().GetStatusAccList();
            cboStatusAcc.DisplayMemberPath = "StatusName";

            cboUser.ItemsSource = LoginBL.GetInstance().GetUserList();
            cboUser.DisplayMemberPath = "userName";

            cboStatusAccUpdate.ItemsSource = LoginBL.GetInstance().GetStatusAccList();
            cboStatusAccUpdate.DisplayMemberPath = "StatusName";

            cboRolesUpdate.ItemsSource = LoginBL.GetInstance().GetRoleList();
            cboRolesUpdate.DisplayMemberPath = "Roles";
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

        private void cboUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboUser.SelectedIndex > -1)
            {
                txtUserUpdate.Text = cboUser.SelectedValue.ToString();
                int idUser = cboUser.SelectedIndex + 1;
                var userLogin = new Login_FT
                {
                    Id = idUser
                };
                Login_FT login_FTs = LoginBL.GetInstance().GetLoginsDetail(userLogin);
                txtPassUpdate.Password = login_FTs.Login_pass;
                txtPass2Update.Password = login_FTs.Login_pass;
                cboStatusAccUpdate.SelectedIndex = login_FTs.IsActive - 1;
                cboRolesUpdate.SelectedIndex = login_FTs.Roles - 1;
                txtQuestUpdate.Text = login_FTs.Question;
                txtAmwserUpdate.Password = login_FTs.Answer;
                idEmployeeUpdate = login_FTs.Id_employee;
            }
            
        }
        
        private void txtPass2Update_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (txtPass2Update.BorderThickness == new Thickness(1, 1, 1, 1))
            {
                txtPass2Update.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFF8000"));
                txtPass2Update.BorderThickness = new Thickness(0, 0, 0, 1);
                timer.Stop();
                bdrValidation.Visibility = Visibility.Hidden;
            }
        }

        private void btnUpdateUser_Click(object sender, RoutedEventArgs e)
        {
            var editUser = new Login_FT
            {
                Id = cboUser.SelectedIndex + 1,
                Login_name = txtUserUpdate.Text,
                Login_pass = txtPassUpdate.Password,
                Question = txtQuestUpdate.Text,
                Answer = txtAmwserUpdate.Password,
                IsActive = cboStatusAccUpdate.SelectedIndex + 1,
                Roles = cboRolesUpdate.SelectedIndex + 1,
                Id_employee = idEmployeeUpdate
            };

            try
            {
                if (txtPassUpdate.Password == txtPass2Update.Password)
                {
                    LoginBL.GetInstance().UpdateUser(editUser);
                    cboUser.SelectedIndex = -1;
                    cboStatusAccUpdate.SelectedIndex = -1;
                    cboRolesUpdate.SelectedIndex = -1;
                    txtUserUpdate.Clear();
                    txtPassUpdate.Clear();
                    txtPass2Update.Clear();
                    txtQuestUpdate.Clear();
                    txtAmwserUpdate.Clear();
                    MessageBoxRM.Show("Usuario actualizado correctamente!", "Actualizacion Usuario", MessageBoxButtonRM.OK, MessageBoxIconRM.Information);
                }
                else
                {
                    ToolTip toolTip = new ToolTip();
                    toolTip.PlacementTarget = txtPass2Update;
                    txtPass2Update.ToolTip = toolTip;
                    toolTip.Content = "La contraseña no coinciden";
                    ToolTipService.SetShowDuration(txtPass2Update, 4000);
                    ToolTipService.SetPlacement(txtPass2Update, System.Windows.Controls.Primitives.PlacementMode.Bottom);
                    txtPass2Update.BorderBrush = Brushes.Red;
                    txtPass2Update.BorderThickness = new Thickness(1, 1, 1, 1);
                    bdrValidation.Visibility = Visibility.Visible;
                    validationIcon = bdrValidationIcon.Pass;
                    timer.Start();
                }
        }
            catch (Exception ex)
            {
                MessageBoxRM.Show(ex.InnerException.Message, "Erro al actualizar usuario", MessageBoxButtonRM.OK, MessageBoxIconRM.Error);
            }
}
    }
}
