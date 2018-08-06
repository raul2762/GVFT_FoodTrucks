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
    /// Lógica de interacción para RegMerchandise.xaml
    /// </summary>
    public partial class RegMerchandise : Page
    {
        
        public RegMerchandise()
        {
            InitializeComponent();
        }

        private void btnRegMerch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MerLogic.GetInstance().RegisterMerch(txtMerch.Text, 0);
                MessageBoxRM.Show("Mercancia registrada correctamente!", "Registro de Mercancia", MessageBoxButtonRM.OK, MessageBoxIconRM.Information);
                txtMerch.Clear();
                RechargeAllCboS();
            }
            catch (Exception ex)
            {
                MessageBoxRM.Show(ex.InnerException.Message, "Ha ocurrido un error :(", MessageBoxButtonRM.OK, MessageBoxIconRM.Error);
            }
        }

        private void btnPurchase_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int IdMerch = cboMerch.SelectedIndex + 1;
                int IdSuppl = cboSuppl.SelectedIndex + 1;
                int Amount = Convert.ToInt32(txtCostPur.Text);
                int Qty = Convert.ToInt32(txtCant.Text);
                int IdUser = LoginBL.GetInstance().IdUser;
                MerLogic.GetInstance().RegisterPofMerch(txtDetail.Text, Amount, DateTime.Now.Date, Qty, IdMerch, IdSuppl, IdUser);
                MessageBoxRM.Show("Compra registrada correctamente!", "Registro de compras", MessageBoxButtonRM.OK, MessageBoxIconRM.Information);
                txtDetail.Clear();
                txtCant.Clear();
                txtCostPur.Clear();
                cboMerch.SelectedIndex = -1;
                cboSuppl.SelectedIndex = -1;
                RechargeAllCboS();
            }
            catch (Exception ex)
            {
                MessageBoxRM.Show(ex.InnerException.Message, "Ha ocurrido un error :(", MessageBoxButtonRM.OK, MessageBoxIconRM.Error);
            }
        }

        private void btnMerchUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var updateMerch = new Merchandise
                {
                    Id = cboMerchUpdate.SelectedIndex + 1,
                    Name = txtMerchUpdate.Text
                };
                MerLogic.GetInstance().UpdateMerch(updateMerch);
                MessageBoxRM.Show("Mercancia actualizada correctamente!", "Actualizacion de Mercancia", MessageBoxButtonRM.OK, MessageBoxIconRM.Information);
                cboMerchUpdate.SelectedIndex = -1;
                txtMerchUpdate.Clear();
                RechargeAllCboS();
            }
            catch (Exception ex)
            {
                MessageBoxRM.Show(ex.InnerException.Message, "Ha ocurrido un error :(", MessageBoxButtonRM.OK, MessageBoxIconRM.Error);
            }
        }

        private void cboMerchUpdate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboMerchUpdate.SelectedIndex > -1)
            {
                txtMerchUpdate.Text = cboMerchUpdate.SelectedValue.ToString();
            }
            
        }

        private void btnRegSuppl_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string city = cboLocation.SelectedValue.ToString();
                MerLogic.GetInstance().RegisterSupplier(txtNameSuppl.Text, txtAddrSuppl.Text, city, txtPhoneSuppl.Text);
                MessageBoxRM.Show("Suplidor registrado correctamente!", "Registro de Suplidor", MessageBoxButtonRM.OK, MessageBoxIconRM.Information);
                txtNameSuppl.Clear();
                txtAddrSuppl.Clear();
                txtPhoneSuppl.Clear();
                cboLocation.SelectedIndex = -1;
                RechargeAllCboS();
            }
            catch (Exception ex)
            {
                MessageBoxRM.Show(ex.InnerException.Message, "Ha ocurrido un error :(", MessageBoxButtonRM.OK, MessageBoxIconRM.Error);
            }
        }

        public void RechargeAllCboS()
        {
            cboMerch.ItemsSource = MerLogic.GetInstance().GetMerchandiseList();
            cboMerch.DisplayMemberPath = "Name";

            cboSuppl.ItemsSource = MerLogic.GetInstance().GetSupplierList();
            cboSuppl.DisplayMemberPath = "Name";

            cboMerchUpdate.ItemsSource = MerLogic.GetInstance().GetMerchandiseList();
            cboMerchUpdate.DisplayMemberPath = "Name";

            DtGridMerch.ItemsSource = MerLogic.GetInstance().GerMerchList();
            //DtGridMerch.DisplayMemberPath = "Name";
            

            
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            cboLocation.ItemsSource = MerLogic.GetInstance().GetLocationsList();
            cboLocation.DisplayMemberPath = "Name";

            RechargeAllCboS();
        }
        private void itemMenuBuy_Click(object sender, RoutedEventArgs e)
        {
            tabControl1.SelectedIndex = 1;
            object item = DtGridMerch.SelectedItem;
            string Id = (DtGridMerch.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
            int Id2 = Convert.ToInt32(Id) - 1;
            cboMerch.SelectedIndex = Id2;
            //MessageBox.Show(Id);
        }

        private void itemMenuEdit_Click(object sender, RoutedEventArgs e)
        {
            tabControl1.SelectedIndex = 2;
            object item = DtGridMerch.SelectedItem;
            string Id = (DtGridMerch.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
            int Id2 = Convert.ToInt32(Id) - 1;
            cboMerchUpdate.SelectedIndex = Id2;
        }
    }
}
