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
using System.Windows.Shapes;
using MahApps.Metro.Controls;

namespace GVFT_FoodTrucks
{
    /// <summary>
    /// Interaction logic for ObsMessage.xaml
    /// </summary>
    public partial class ObsMessage : MetroWindow
    {
        public bool BtnAddState { get; set; }
        public bool BtnCancelState { get; set; }
        public ObsMessage()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            SalesFirst.GetObsMessage = txtObs.Text;
            BtnAddState = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            BtnCancelState = true;
            this.Close();
        }
    }
}
