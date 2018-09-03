using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using Data_GVFT.Business.BusinessEntities;
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
            cboEmployeeP.ItemsSource = LoginBL.GetInstance().GetEmployeeList();
            cboEmployeeP.DisplayMemberPath = "Name";
        }

        private void btnRegisterEmployee_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int IdDepart = cboDepart.SelectedIndex + 1;
                int IdPaysheetMode = cboPaysheetMode.SelectedIndex + 1;
                int salary = Convert.ToInt32(txtSalary.Text);
                var newEmployee = new Employees()
                {
                    Name = txtName.Text,
                    Last_name = txtLastName.Text,
                    Cedula = txtCedula.Text,
                    Department = IdDepart,
                    Salary = salary,
                    Entry_date = DateTime.Now,
                    Phone = txtPhone.Text,
                    Pay_mode = IdPaysheetMode
                };
                LoginBL.GetInstance().RegisterEmployee(newEmployee);
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

        private void btnRegisterLoan_Click(object sender, RoutedEventArgs e)
        {
            var employee = new Employees();
            int index = cboEmployeeP.SelectedIndex;
            employee = (Employees)cboEmployeeP.Items[index];
            var newLoan = new Mov_CxC_Employees()
            {
                Id_employee = employee.Id,
                Amount_charged = 0,
                Loan_Amount = Convert.ToInt32(txtMonto.Text),
                Fee = Convert.ToInt32(txtCuotas.Text),
                FeeRest = Convert.ToInt32(txtCuotas.Text),
                Fee_Amount = Convert.ToInt32(txtMCuotas.Text),
                Transaction_date = DateTime.Now,
                Amount_rest = Convert.ToInt32(txtMonto.Text),
                Transaction_type = 1
            };
            PaysheetBL.GetInstance().RegisterLoan(newLoan);
            MessageBoxRM.Show("Prestamo registrado exitosamente.", "Registro de Prestamo", MessageBoxButtonRM.OK, MessageBoxIconRM.Information);
            txtMCuotas.Clear();
            txtMonto.Clear();
            txtCuotas.Clear();
            cboEmployeeP.SelectedIndex = -1;
        }

        private void txtMonto_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
        }

        private void txtMonto_Copy_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
        }

        private void txtCuotas_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
        }

        private void txtMCuotas_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
        }

        private void txtMontoP_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
        }

        private void txtDesc_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
        }

        private void txtSalary_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
        }

        private void cboEmployee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboEmployee.SelectedIndex > -1)
            {
                txtNota.Text = "";
                var employee = new Employees();
                var loan = new Mov_CxC_Employees();
                int index = cboEmployee.SelectedIndex;
                employee = (Employees)cboEmployee.Items[index];
                txtSueldo.Text = employee.Salary.ToString();
                
                loan = PaysheetBL.GetInstance().GetMov(employee.Id);
                if (loan != null)
                {
                    if (loan.FeeRest > 0)
                    {
                        txtNota.Text = $"Descuento de cuota #{loan.FeeRest} de {loan.Fee}";
                        txtDesc.Text = PaysheetBL.GetInstance().GetFee(employee.Id).ToString();
                    }
                    else
                    {
                        txtDesc.Text = "0";
                        txtNota.Text = "";
                    }
                }
                int sueldo = Convert.ToInt32(txtSueldo.Text);
                int desc = Convert.ToInt32(txtDesc.Text);
                if (desc > 0)
                {
                    int montoPagar = sueldo - desc;
                    txtMontoP.Text = montoPagar.ToString();
                }
                else
                {
                    txtMontoP.Text = employee.Salary.ToString();
                }
            }
        }

        private void txtCuotas_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCuotas.Text))
            {
                int monto = Convert.ToInt32(txtMonto.Text);
                int NoCuota = Convert.ToInt32(txtCuotas.Text);
                if (NoCuota > 0)
                {
                    int cuotaP = monto / NoCuota;
                    txtMCuotas.Text = cuotaP.ToString();
                }
                else
                {
                    MessageBoxRM.Show("No se puede dividir por cero", "Error de calculo", MessageBoxButtonRM.OK, MessageBoxIconRM.Error);
                }
            }
            else
            {
                txtMCuotas.Text = "";
            }
            
        }

        private void btnPaidPaysheet_Click(object sender, RoutedEventArgs e)
        {
            var employee = new Employees();
            var loan = new Mov_CxC_Employees();
            int index = cboEmployee.SelectedIndex;
            employee = (Employees)cboEmployee.Items[index];
            int desc = Convert.ToInt32(txtDesc.Text);
            loan = PaysheetBL.GetInstance().GetMov(employee.Id);
            string nota;
            
            if (string.IsNullOrEmpty(txtNota.Text))
            {
                nota = "-";
            }
            else
            {
                nota = txtNota.Text;
            }
            if (desc > 0)
            {
                var newPayment = new Paysheet()
                {
                    Amount = Convert.ToInt32(txtSueldo.Text),
                    Payment_date = DateTime.Now,
                    Id_employee = employee.Id,
                    Note = nota
                };

                var updateLoan = new Mov_CxC_Employees()
                {
                    Id_employee = employee.Id,
                    Amount_charged = desc,
                    Amount_rest = loan.Amount_rest - desc,
                    FeeRest = loan.FeeRest - 1,
                    Transaction_date = DateTime.Now,
                    Transaction_type = 3
                };

                PaysheetBL.GetInstance().PayrollWithLoan(newPayment, updateLoan);
                MessageBoxRM.Show($"Pago nomina a {employee.Name} {employee.Last_name} aplicado.", "Pago nomina y cobro cuota de prestamo", MessageBoxButtonRM.OK, MessageBoxIconRM.Information);
                txtSueldo.Clear();
                txtDesc.Clear();
                txtMontoP.Clear();
                txtNota.Clear();
                cboEmployee.SelectedIndex = -1;
            }
            else
            {
                var newPayment = new Paysheet()
                {
                    Amount = Convert.ToInt32(txtSueldo.Text),
                    Payment_date = DateTime.Now,
                    Id_employee = employee.Id,
                    Note = nota
                };
                PaysheetBL.GetInstance().PaymentPayroll(newPayment);
                MessageBoxRM.Show($"Pago nomina a {employee.Name} {employee.Last_name} aplicado.", "Pago nomina", MessageBoxButtonRM.OK, MessageBoxIconRM.Information);
                txtSueldo.Clear();
                txtDesc.Clear();
                txtMontoP.Clear();
                txtNota.Clear();
                cboEmployee.SelectedIndex = -1;
            }
        }
    }
}
