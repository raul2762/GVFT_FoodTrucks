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

namespace ItemCardOrders
{
    /// <summary>
    /// Interaction logic for CardItemOrd.xaml
    /// </summary>
    public partial class CardItemOrd : UserControl
    {
        public string Nombre { get { return lblNombre.Text; } set { lblNombre.Text = value; } }
        public string Orden { get { return lblOrden.Text; } set { lblOrden.Text = value; } }
        public Brush backGrdMyGrid { get { return MyGrid.Background; } set { MyGrid.Background = value; } }
        public Brush backFgrdlblN { get { return lblNombre.Foreground; } set { lblNombre.Foreground = value; } }
        public Brush backFgrdO { get { return lblOrden.Foreground; } set { lblOrden.Foreground = value; } }
        public Brush backFgrdC1 { get { return lblOrden_Codigo.Foreground; } set { lblOrden_Codigo.Foreground = value; } }
        public Brush backFgrdC2 { get { return lblCodigo.Foreground; } set { lblCodigo.Foreground = value; } }
        
        public CardItemOrd()
        {
            InitializeComponent();
        }
    }
}
