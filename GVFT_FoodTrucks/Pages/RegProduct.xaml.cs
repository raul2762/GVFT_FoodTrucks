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
    /// Lógica de interacción para RegProduct.xaml
    /// </summary>
    public partial class RegProduct : Page
    {
        DispatcherTimer timer = new DispatcherTimer();
        Storyboard storyboard;
        bool _started;
        bdrValidationIcon validationIcon;
        public string Mensajito { get; set; }
        public RegProduct()
        {
            InitializeComponent();
            
        }

        enum bdrValidationIcon
        {
            User,
            Price,
            Caegory
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            RechargeAllCbo();
            bdrValidation.Visibility = Visibility.Hidden;
            bdrValidation_Copy.Visibility = Visibility.Hidden;
            bdrValidation_Copy1.Visibility = Visibility.Hidden;
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
                        case bdrValidationIcon.User:
                            storyboard.Begin(bdrValidation);
                            break;
                        case bdrValidationIcon.Price:
                            storyboard.Begin(bdrValidation_Copy);
                            break;
                        case bdrValidationIcon.Caegory:
                            storyboard.Begin(bdrValidation_Copy1);
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
            cboCategory.ItemsSource = MenuBL.GetInstance().GetCategoryList();
            cboCategory.DisplayMemberPath = "Name";

            cboCategoryUpdate.ItemsSource = MenuBL.GetInstance().GetCategoryList();
            cboCategoryUpdate.DisplayMemberPath = "Name";

            cboProduct.ItemsSource = MenuBL.GetInstance().GetProductList();
            cboProduct.DisplayMemberPath = "Name";
        }

        private void btnRegProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                if (!string.IsNullOrEmpty(txtProduct.Text))
                {
                    if (!string.IsNullOrEmpty(txtPrice.Text))
                    {
                        if (cboCategory.SelectedIndex > -1)
                        {
                            int idCategory = cboCategory.SelectedIndex + 1;
                            int price = Convert.ToInt32(txtPrice.Text);
                            MenuBL.GetInstance().RegisterProduct(txtProduct.Text, price, idCategory);
                            MessageBoxRM.Show("Producto registrado correctamente!", "Registro de Producto", MessageBoxButtonRM.OK, MessageBoxIconRM.Information);
                            txtProduct.Clear();
                            txtPrice.Clear();
                            cboCategory.SelectedIndex = -1;
                            RechargeAllCbo();
                        }
                        else
                        {
                            ToolTip toolTip = new ToolTip();
                            toolTip.PlacementTarget = cboCategory;
                            cboCategory.ToolTip = toolTip;
                            toolTip.Content = "Esta opcion es requerida";
                            ToolTipService.SetShowDuration(cboCategory, 4000);
                            ToolTipService.SetPlacement(cboCategory, System.Windows.Controls.Primitives.PlacementMode.Bottom);
                            cboCategory.BorderBrush = Brushes.Red;
                            bdrValidation_Copy1.Visibility = Visibility.Visible;
                            validationIcon = bdrValidationIcon.Caegory;
                            timer.Start();
                        }
                    }
                    else
                    {
                        ToolTip toolTip = new ToolTip();
                        toolTip.PlacementTarget = txtPrice;
                        txtPrice.ToolTip = toolTip;
                        toolTip.Content = "Este campo es requerido";
                        ToolTipService.SetShowDuration(txtPrice, 4000);
                        ToolTipService.SetPlacement(txtPrice, System.Windows.Controls.Primitives.PlacementMode.Bottom);
                        txtPrice.BorderBrush = Brushes.Red;
                        txtPrice.BorderThickness = new Thickness(1, 1, 1, 1);
                        bdrValidation_Copy.Visibility = Visibility.Visible;
                        validationIcon = bdrValidationIcon.Price;
                        timer.Start();
                    }
                }
                else
                {
                    
                    ToolTip toolTip = new ToolTip();
                    toolTip.PlacementTarget = txtProduct;
                    txtProduct.ToolTip = toolTip;
                    toolTip.Content = "Este campo es requerido";
                    ToolTipService.SetShowDuration(txtProduct, 4000);
                    ToolTipService.SetPlacement(txtProduct, System.Windows.Controls.Primitives.PlacementMode.Bottom);
                    txtProduct.BorderBrush = Brushes.Red;
                    txtProduct.BorderThickness = new Thickness(1, 1, 1, 1);
                    bdrValidation.Visibility = Visibility.Visible;
                    validationIcon = bdrValidationIcon.User;
                    timer.Start();
                }
                
            }
            catch (Exception ex)
            {
                MessageBoxRM.Show(ex.InnerException.Message, "Ha ocurrido un error :(", MessageBoxButtonRM.OK, MessageBoxIconRM.Error);
            }


        }

        private void txtProduct_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtProduct.BorderThickness == new Thickness(1,1,1,1))
            {
                txtProduct.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFF8000"));
                txtProduct.BorderThickness = new Thickness(0, 0, 0, 1);
                timer.Stop();
                bdrValidation.Visibility = Visibility.Hidden;
            }
        }

        private void txtPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtPrice.BorderThickness == new Thickness(1,1,1,1))
            {
                txtPrice.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFF8000"));
                txtPrice.BorderThickness = new Thickness(0, 0, 0, 1);
                timer.Stop();
                bdrValidation_Copy.Visibility = Visibility.Hidden;
            }
        }

        private void cboCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboCategory.BorderBrush == Brushes.Red)
            {
                cboCategory.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFCCCCCC"));
                timer.Stop();
                bdrValidation_Copy1.Visibility = Visibility.Hidden;
            }
        }

        private void cboProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboProduct.SelectedIndex != -1)
            {
                txtProductUpdate.Text = cboProduct.SelectedValue.ToString();
                int idProducto = cboProduct.SelectedIndex + 1;
                Product product;
                var producto = new Product
                {
                    Id = idProducto
                };
                product = MenuBL.GetInstance().GetProductDetail(producto);

                txtPriceUpdate.Text = product.unitPrice.ToString();
                cboCategoryUpdate.SelectedIndex = product.Id_category - 1;
            }
        }

        private void btnUpdatProduct_Click(object sender, RoutedEventArgs e)
        {
            if (cboProduct.SelectedIndex > -1)
            {
                int idCategory = cboCategoryUpdate.SelectedIndex + 1;
                int idProduct = cboProduct.SelectedIndex + 1;
                int price = Convert.ToInt32(txtPriceUpdate.Text);
                var producto = new Product
                {
                    Id = idProduct,
                    Name_product = txtProductUpdate.Text,
                    unitPrice = price,
                    Id_category = idCategory
                };
                try
                {
                    MenuBL.GetInstance().UpdateProduct(producto);
                    MessageBoxRM.Show("Producto actualizado correctamente!", "Actualizacion de Producto", MessageBoxButtonRM.OK, MessageBoxIconRM.Information);
                    cboProduct.SelectedIndex = -1;
                    cboCategoryUpdate.SelectedIndex = -1;
                    txtProductUpdate.Clear();
                    txtPriceUpdate.Clear();
                    RechargeAllCbo();
                }
                catch (Exception ex)
                {
                    MessageBoxRM.Show(ex.InnerException.Message, "Ha ocurrido un error :(", MessageBoxButtonRM.OK, MessageBoxIconRM.Error);
                }
                
            }
            
        }
    }
}
