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
using Data_GVFT.Business.BusinessEntities;
using Data_GVFT.Business.BusinessLogic;
using MessageBoxCustomRM;

namespace GVFT_FoodTrucks.Pages
{
    /// <summary>
    /// Lógica de interacción para RegCredits.xaml
    /// </summary>
    public partial class RegCredits : Page
    {
        public RegCredits()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            RechargeAllCbo();
        }

        public void RechargeAllCbo()
        {
            cboModeExp.ItemsSource = CreditBL.GetInstance().GetTypeCredit();
            cboModeExp.DisplayMemberPath = "ModeName";
        }

        private void cboModeExp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboModeExp.SelectedIndex > -1)
            {
                string expMode = cboModeExp.SelectedValue.ToString();
                if (expMode == "Cantidad")
                {
                    txtCant.IsEnabled = true;
                    DateT.IsEnabled = false;
                }
                else if (expMode == "Fecha")
                {
                    DateT.IsEnabled = true;
                    txtCant.IsEnabled = false;
                }
            }
            
        }

        private void btnRegCredit_Click(object sender, RoutedEventArgs e)
        {
            int idTrnsT;
            var idTransType = new Expiry_of_mode()
            {
                 ModeName = cboModeExp.SelectedValue.ToString()
            };
            idTrnsT = CreditBL.GetInstance().GetIdTypeCredit(idTransType);
            string expMode = cboModeExp.SelectedValue.ToString();
            if (txtCode.Text != "" && txtMonto.Text != "" && cboModeExp.SelectedIndex > -1)
            {
                if (expMode == "Cantidad")
                {
                    if (txtCant.Text != "")
                    {
                        var newCredit = new Credits()
                        {
                            Code_credits = txtCode.Text,
                            Amount = Convert.ToInt32(txtMonto.Text),
                            Type_credit = 1,
                            Cant = Convert.ToInt32(txtCant.Text),
                            Expiry_mode = idTrnsT,
                            Qty_sold = 0,
                            Creation_date = DateTime.Now
                        };
                        CreditBL.GetInstance().RegisterCreditCode(newCredit);
                        MessageBoxRM.Show("Codigo registrado exitosamente", "Registro de codigo credito", MessageBoxButtonRM.OK, MessageBoxIconRM.Information);
                        txtCode.Clear();
                        txtMonto.Clear();
                        txtCant.Clear();
                        cboModeExp.SelectedIndex = -1;
                        txtCant.IsEnabled = false;
                        DateT.Text = "";
                    }
                    else
                    {
                        MessageBoxRM.Show("El campo cantidad se encuentra vacio", "Campos sin llenar", MessageBoxButtonRM.OK, MessageBoxIconRM.Warning);
                    }
                }
                else if (expMode == "Fecha")
                {
                    if (DateT.Text != "")
                    {
                        var newCredit = new Credits()
                        {
                            Code_credits = txtCode.Text,
                            Amount = Convert.ToInt32(txtMonto.Text),
                            Type_credit = 1,
                            Expire_date = DateT.SelectedDate,
                            Cant = 0,
                            Expiry_mode = idTrnsT,
                            Qty_sold = 0,
                            Creation_date = DateTime.Now
                        };
                        CreditBL.GetInstance().RegisterCreditCode(newCredit);
                        MessageBoxRM.Show("Codigo registrado exitosamente", "Registro de codigo credito", MessageBoxButtonRM.OK, MessageBoxIconRM.Information);
                        txtCode.Clear();
                        txtMonto.Clear();
                        txtCant.Clear();
                        cboModeExp.SelectedIndex = -1;
                        DateT.IsEnabled = false;
                        DateT.Text = "";
                    }
                    else
                    {
                        MessageBoxRM.Show("No se ha seleccionado una fecha", "Campos sin llenar", MessageBoxButtonRM.OK, MessageBoxIconRM.Warning);
                    }
                }   
            }
            else
            {
                MessageBoxRM.Show("Algun campo se encuentra vacio", "Campos sin llenar", MessageBoxButtonRM.OK, MessageBoxIconRM.Warning);
            }
        }
    }
}
