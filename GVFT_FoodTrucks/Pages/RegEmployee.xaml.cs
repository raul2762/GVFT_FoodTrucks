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
using MessageBoxCustomRM;

namespace GVFT_FoodTrucks.Pages
{
    /// <summary>
    /// Lógica de interacción para RegEmployee.xaml
    /// </summary>
    public partial class RegEmployee : Page
    {
        public RegEmployee()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            cboDepart.ItemsSource = LoginBL.GetInstance().GetDepartmentsList();
            cboDepart.DisplayMemberPath = "DepartName";
            cboPaysheetMode.ItemsSource = LoginBL.GetInstance().GetPaysheetModeList();
            cboPaysheetMode.DisplayMemberPath = "PaysheetModeName";
            cboEmployee.ItemsSource = LoginBL.GetInstance().GetEmployeeList();
            cboEmployee.DisplayMemberPath = "Name";
        }

        private void btnRegisterEmployee_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int IdDepart = cboDepart.SelectedIndex + 1;
                int IdPaysheetMode = cboPaysheetMode.SelectedIndex + 1;
                int salary = Convert.ToInt32(txtSalary.Text);
                LoginBL.GetInstance().RegisterEmployee(txtName.Text, txtLastName.Text, txtCedula.Text, IdDepart, salary, DateTime.Now.Date, txtPhone.Text, IdPaysheetMode);
                MessageBoxRM.Show("Empleado Registrado correctamente!", "Registrado!", MessageBoxButtonRM.OK, MessageBoxIconRM.Information);
                txtName.Clear();
                txtLastName.Clear();
                txtCedula.Clear();
                txtSalary.Clear();
                txtPhone.Clear();
                cboDepart.SelectedIndex = -1;
                cboPaysheetMode.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBoxRM.Show(ex.InnerException.Message, "Ha ocurrido un error :(", MessageBoxButtonRM.OK, MessageBoxIconRM.Error);
            }
        }
    }
}
