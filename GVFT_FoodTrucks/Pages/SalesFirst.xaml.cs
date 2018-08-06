using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
using ItemControl;
using Serving_Table;
using Data_GVFT.Business.BusinessLogic;
using MessageBoxCustomRM;

namespace GVFT_FoodTrucks
{
  public  class Productos
    {
        public string NombreProducto { get; set; }
        public int Cantidad { get; set; }
        public int Precio { get; set; }

        public Productos()
        { }
        public Productos(string NomP, int Cant, int Precioo)
        {
            this.NombreProducto = NomP;
            this.Cantidad = Cant;
            this.Precio = Precioo;
        }
    };
    
    /// <summary>
    /// Lógica de interacción para SalesFirst.xaml
    /// </summary>
    public partial class SalesFirst : Page
    {
        ObservableCollection<ListaNumeros> listaNumeros;
        ListBox list = new ListBox();
        ListBox listBox;
        public SalesFirst()
        {
            InitializeComponent();
            DataContext = this;
            this.listaNumeros = new ObservableCollection<ListaNumeros>();
        }

        public void llenarLista()
        {
            listaNumeros.Add(new ListaNumeros {Numeros = "1" });
        }
        private void BtnPendiente_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show(NtfIcon.Badge.ToString());
            
        }

        private void BtnImp_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void BtnProcess_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void BtnInic_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void BtnCancelar_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
            DtGridOrden.Items.Clear();
            CalculateTotal();
            //DtGridOrden.Items.Refresh();
        }

        private void BtnRemove_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (DtGridOrden.SelectedIndex >= 0)
            {
                Productos productos = (Productos)DtGridOrden.SelectedItem;
                DtGridOrden.Items.Remove(productos);
                //DataRowView productos = (DataRowView)DtGridOrden.SelectedItem;
                //productos.Row.Delete();

                CalculateTotal();
                //DtGridOrden.Items.Remove(productos);
            }
        }

        private void BtnRest_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            if (DtGridOrden.SelectedIndex >= 0)
            {
                int numB = ((Productos)DtGridOrden.SelectedItem).Cantidad;
                ((Productos)DtGridOrden.SelectedItem).Cantidad = numB - 1;
                CollectionViewSource.GetDefaultView(DtGridOrden.Items).Refresh();
                CalculateTotal();
            }
            else
            {
                //Aqui va un MessageBox
            }
        }

        private void BtnAgregar_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (DtGridOrden.SelectedIndex >= 0)
            {
                int numB = ((Productos)DtGridOrden.SelectedItem).Cantidad;
                ((Productos)DtGridOrden.SelectedItem).Cantidad = numB + 1;
                CollectionViewSource.GetDefaultView(DtGridOrden.Items).Refresh();
                var itemProduct = new Productos();
                itemProduct = (Productos)DtGridOrden.Items[DtGridOrden.SelectedIndex];
                CalculateTotal();
                
            }
            else
            {
                //Aqui va un MessageBox
            }

        }

        private void DtGridOrden_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        public void LoadMenuFood()
        {
            
            List<MenuBL.Categorias> categorias;
            categorias = MenuBL.GetInstance().GetCategorias();
            List<MenuItem_UC> menuItems;
            menuItems = MenuBL.GetInstance().GetMenuItems();
            // var menuu = new MenuItem_UC();
            //var menuu2 = new MenuItem_UC();
            //string search = categorias[1].Nombre;

            for (int i = 0; i < categorias.Count; i++)
            {
                listBox = addListBox(i);
                TabMenu.Items.Add(new TabItem() { Header = categorias[i].Nombre, Cursor = Cursors.Hand, Content = listBox });
                listBox.SelectionChanged += ListBox_SelectionChanged;
                var item = menuItems.Where(a => a.Categoria == categorias[i].Nombre);
                foreach (MenuItem_UC nombre in item)
                {
                    listBox.Items.Add(nombre);
                }
            }
        }

        class Categorias
        {
            public string Nombre { get; set; }
        }
        class ListaNumeros
        {
            public string  Numeros { get; set; }
        
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadMenuFood();
            CboListBox.ItemsSource = GetServTables.GetInstance().Tables;
            CboListBox.SelectedIndex = 0;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool exist = false;
            ListBox box = (ListBox)sender;
            if (box.SelectedIndex > -1)
            {
                var item = new MenuItem_UC();
                item = (MenuItem_UC)box.SelectedItem;
                var itemProduct = new Productos();
                for (int i = 0; i < DtGridOrden.Items.Count; i++)
                {
                    itemProduct = (Productos)DtGridOrden.Items[i];
                    if (itemProduct.NombreProducto == item.Nombre)
                    {
                        MessageBoxRM.Show("Este Item ya ha sido agregado, utilize los botones\n(Agregar Cant.) y (Restar Cant.) para aumentar o disminuir cantidad.\n\nOJO: " +
                            "debe seleccionar el producto antes de utilizar los botones indicados", "Item existente", MessageBoxButtonRM.OK, MessageBoxIconRM.Warning);
                        exist = true;
                        box.SelectedIndex = -1;
                    }
                }
                if (!exist)
                {
                    var item2 = new Productos(item.Nombre, 1, item.Precio);
                    DtGridOrden.Items.Add(item2);
                    CalculateTotal();
                    box.SelectedIndex = -1;
                }
            }
        }
        /// <summary>
        /// Metodo para crear un ListBox dinamicamente
        /// </summary>
        /// <param name="i">"Parametro de numero para generar nombres dinamicamente"</param>
        /// <returns>Retorna un ListBox con los estilos predefinidos</returns>
        ListBox addListBox(int i)
        {
            ControlTemplate controlTemplate;
            var s = new Style(typeof(ListBoxItem));
            Setter setter = new Setter();
            setter.Property = ListBoxItem.PaddingProperty;
            setter.Value = new Thickness(10);
            s.Setters.Add(setter);
            controlTemplate = (ControlTemplate)FindResource("ModeloT");
            ListBox listB = new ListBox();
            listB.Name = "listBox" + i.ToString();
            listB.Template = controlTemplate;
            listB.ItemContainerStyle = s;

            return listB;
        }
        

        public void CalculateTotal()
        {
            int Cant = 0, Precio = 0, total1 = 0, total2 = 0;
            var itemProduct = new Productos();
            for (int i = 0; i < DtGridOrden.Items.Count; i++)
            {
                itemProduct = (Productos)DtGridOrden.Items[i];
                Cant = itemProduct.Cantidad;
                Precio = itemProduct.Precio;
                total1 = Cant * Precio;
                total2 = total2 + total1;
            }
            //DataTable dt = GetTable();

            //dt = ((DataView)DtGridOrden.ItemsSource).ToTable();


            //int Cant = 0, Precio = 0, total1 = 0, total2 = 0;
            //foreach (DataRow fila in dt.Rows)
            //{
            //    Cant = Convert.ToInt32(fila["Cantidad"]);
            //    Precio = Convert.ToInt32(fila["Precio"]);
            //    total1 = Cant * Precio;
            //    total2 = total2 + total1;
            //}

            lblTotal.Text = string.Format("{0:n}", total2);

        }

        public DataTable GetTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("NombreProducto");
            table.Columns.Add("Cantidad");
            table.Columns.Add("Precio");
            //table.Rows.Add("HotDog", 2, 150);
            //table.Rows.Add("Hamburguesa", 1, 350);
            //table.Rows.Add("Yaroa", 3, 240);
            //table.Rows.Add("Refresco", 3, 50);


            return table;
        }

        private void dockP_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //var item = new MenuItem_UC();
            //item = (MenuItem_UC)dockP.SelectedItem;
            //var item2 = new Productos(item.Nombre, 1, item.Precio);

            //DtGridOrden.Items.Add(item2);
            //CalculateTotal();
        }

        private void cboMesa_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
          
        }

        private void MyCbo_MouseLeave(object sender, MouseEventArgs e)
        {
            CboPopup.StaysOpen = false;
        }

        private void MyCbo_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CboPopup.IsOpen = true;
            CboPopup.StaysOpen = true;
        }

        private void CboListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CboPopup.IsOpen = false;
            MyCbo.Text = CboListBox.SelectedValue.ToString();
        }
        
    }
}
