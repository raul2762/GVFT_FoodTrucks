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

namespace ItemControl
{
    /// <summary>
    /// Lógica de interacción para MenuItem_UC.xaml
    /// </summary>
    public partial class MenuItem_UC : UserControl
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Precio { get; set; }
        public string Categoria { get; set; }
        public MenuItem_UC()
        {
            InitializeComponent();
            
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            lblName.Text = Nombre;
            lblPrice.Text = Precio.ToString();
        }
    }
}
