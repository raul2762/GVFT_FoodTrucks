﻿using System;
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
using System.Windows.Threading;
using ItemCardOrders;
using MessageBoxCustomRM;

namespace GVFT_FoodTrucks
{
    /// <summary>
    /// Interaction logic for OrdersProduction.xaml
    /// </summary>
    public partial class OrdersProduction : MetroWindow
    {
        DispatcherTimer timer = new DispatcherTimer();
        DispatcherTimer timer2 = new DispatcherTimer();
        DispatcherTimer timer3 = new DispatcherTimer();
        DispatcherTimer timer4 = new DispatcherTimer();
        DispatcherTimer timer5 = new DispatcherTimer();
        DispatcherTimer timer6 = new DispatcherTimer();
        int count1 = 0,count2 = 0;
        bool _initiated = false;
        public static bool _open;
        public static bool CloseWOrdr;
        public static string MessageClose { get; set; }
        public static int OrdrIncomplete { get; set; }
        public static bool RemoveItem;
        public OrdersProduction()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;

            timer2.Interval = TimeSpan.FromSeconds(1);
            timer2.Tick += Timer2_Tick;

            timer3.Interval = TimeSpan.FromMilliseconds(100);
            timer3.Tick += Timer3_Tick;
            timer3.Start();

            timer4.Interval = TimeSpan.FromMilliseconds(100);
            timer4.Tick += Timer4_Tick;
            timer4.Start();

            timer5.Interval = TimeSpan.FromMilliseconds(100);
            timer5.Tick += Timer5_Tick;
            timer5.Start();

            timer6.Interval = TimeSpan.FromMilliseconds(100);
            timer6.Tick += Timer6_Tick;
            timer6.Start();

            _open = true;
            CloseWOrdr = false;
            RemoveItem = false;
            MessageClose = "";
            this.WindowState = WindowState.Maximized;
            this.WindowStyle = WindowStyle.ToolWindow;
        }

        private void Timer6_Tick(object sender, EventArgs e)
        {
            OrdrIncomplete = dockP.Items.Count;
        }

        private void Timer5_Tick(object sender, EventArgs e)
        {
            if (RemoveItem)
            {
                dockP.Items.Clear();
            }
        }

        private void Timer4_Tick(object sender, EventArgs e)
        {
            if (CloseWOrdr)
            {
                if (dockP.Items.Count > 0)
                {
                    MessageClose = "No se puede cerrar esta ventana, hay ordenes sin completar";
                }
                else
                {
                    this.Close();
                }
            }
        }

        private void Timer3_Tick(object sender, EventArgs e)
        {
            if (SalesFirst.sendOrder == true)
            {
                
                var Ordens = new CardItemOrd();
                Ordens = SalesFirst.GetOrdens;
                var itemLbx = (ListBoxItem)dockP.ItemContainerGenerator.ContainerFromItem(Ordens);
                dockP.Items.Add(Ordens);

                int index = dockP.Items.Count;
                //index--;
                //itemLbx = (ListBoxItem)dockP.Items[index];
                //MessageBox.Show(itemLbx.Height.ToString());
                if (_initiated == false)
                {
                    timer.Start();
                }
                SalesFirst.sendOrder = false;
            }
        }
        private void Timer2_Tick(object sender, EventArgs e)
        {
            _initiated = true;
            if (count2 == 30)
            {
                count2 = 0;
                for (int i = 0; i < dockP.Items.Count; i++)
                {
                    var bc = new BrushConverter();
                    var card = new CardItemOrd();
                    card = (CardItemOrd)dockP.Items[i];
                    if (card.backGrdMyGrid == Brushes.LimeGreen)
                    {
                        SalesFirst.tableN = card.Nombre;
                        SalesFirst.ordrDone = true;
                        dockP.Items.RemoveAt(i);
                        timer2.Stop();
                    }
                }
            }
            count2++;
        }

        private void dockP_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dockP.SelectedIndex > -1)
            {
                timer.Stop();
                timer2.Stop();
                _initiated = true;
                var bc = new BrushConverter();
                int index = dockP.SelectedIndex;
                var card = new CardItemOrd();
                card = (CardItemOrd)dockP.SelectedValue;
                if (card.backGrdMyGrid == Brushes.LimeGreen)
                {
                    SalesFirst.tableN = card.Nombre;
                    SalesFirst.ordrDone = true;
                    dockP.Items.RemoveAt(index);
                }
                else
                {
                    SalesFirst.tableN = card.Nombre;
                    SalesFirst.ordrDone = true;
                    dockP.Items.RemoveAt(index);
                    card.backGrdMyGrid = Brushes.LimeGreen;
                    card.backFgrdO = (Brush)bc.ConvertFrom("#000000");
                    card.backFgrdC2 = (Brush)bc.ConvertFrom("#000000");
                    card.backFgrdC1 = (Brush)bc.ConvertFrom("#000000");
                    dockP.Items.Insert(index, card);
                }
                timer.Start();
                timer2.Start();
            }
        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _open = false;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            _initiated = true;
            switch (count1)
            {
                case 1:
                    count1 = 0;
                    break;
                case 0:
                    count1 = 1;
                    break;
                default:
                    break;
            }
            if (count1 == 1)
            {
                for (int i = 0; i < dockP.Items.Count; i++)
                {
                    var bc = new BrushConverter();
                    int index = dockP.SelectedIndex;
                    var card = new CardItemOrd();
                    card = (CardItemOrd)dockP.Items[i];
                    
                    if (card.backGrdMyGrid != Brushes.LimeGreen)
                    {
                        dockP.Items.RemoveAt(i);
                        card.backGrdMyGrid = Brushes.Orange;
                        dockP.Items.Insert(i, card);
                    }
                }
            }
            else
            {
                for (int i = 0; i < dockP.Items.Count; i++)
                {
                    var bc = new BrushConverter();
                    int index = dockP.SelectedIndex;
                    var card = new CardItemOrd();
                    card = (CardItemOrd)dockP.Items[i];

                    if (card.backGrdMyGrid != Brushes.LimeGreen)
                    {
                        dockP.Items.RemoveAt(i);
                        card.backGrdMyGrid = Brushes.Gray;
                        dockP.Items.Insert(i, card);
                    }

                }
            }
        }
    }
}
