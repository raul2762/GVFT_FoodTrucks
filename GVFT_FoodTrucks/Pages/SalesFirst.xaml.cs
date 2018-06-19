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
        ObservableCollection<Productos> col;
        ObservableCollection<DataRowView> col2;
        public SalesFirst()
        {
            InitializeComponent();
        }

        private void BtnPendiente_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Hola primer MessageBox desde este Sistema :)");
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
                //DataRowView data = (DataRowView)DtGridOrden.SelectedItem;
                //int numB = Convert.ToInt32(data["Cantidad"]);
                //data["Cantidad"] = numB + 1;
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
                //DataRowView data = (DataRowView)DtGridOrden.SelectedItem;
                //int numB = Convert.ToInt32(data["Cantidad"]);
                //data["Cantidad"] = numB + 1;
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
            //DataTable dt = GetTable();

            //dt.Rows.Add(1, 2, 3);
            
            //DtGridOrden.ItemsSource = dt.AsDataView();
            //CalculateTotal();
        }

        public void LoadMenuFood()
        {
            List<MenuItem_UC> menuItems = new List<MenuItem_UC>();
            menuItems.Add(new MenuItem_UC() { Nombre = "HotDog", Precio = 150 });
            menuItems.Add(new MenuItem_UC() { Nombre = "Hamburguesa", Precio = 250 });
            menuItems.Add(new MenuItem_UC() { Nombre = "Yaroa", Precio = 190 });
            menuItems.Add(new MenuItem_UC() { Nombre = "Refresco", Precio = 60 });
            //MessageBox.Show(menuItems[0].Nombre);
            for (int i = 0; i < menuItems.Count; i++)
            {
                var itemFood = new MenuItem_UC();
                itemFood.Nombre = menuItems[i].Nombre;
                itemFood.Precio = menuItems[i].Precio;
                dockP.Items.Add(itemFood);
            }

        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            TabMenu.Items.Add(new TabItem() { Header = "Page 3" , Cursor = Cursors.Hand});
            TabMenu.Items.Add(new TabItem() { Header = "Page 4", Cursor = Cursors.Hand });
            LoadMenuFood();
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
            var item = new MenuItem_UC();
            item = (MenuItem_UC)dockP.SelectedItem;
            var item2 = new Productos(item.Nombre, 1, item.Precio);

            DtGridOrden.Items.Add(item2);
            CalculateTotal();
        }

        
    }
}
