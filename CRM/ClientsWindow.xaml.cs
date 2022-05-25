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

namespace CRM
{
    /// <summary>
    /// Логика взаимодействия для ClientsWindow.xaml
    /// </summary>
    public partial class ClientsWindow : Window
    {
        public ClientsWindow()
        {
            InitializeComponent();
        }
        private void AllOrders_Click(object sender, RoutedEventArgs e)
        {
            AllOrdersWindow allOrdersWindow = new AllOrdersWindow();
            allOrdersWindow.Show();
            this.Close();
        }
        private void Authorization_Click(object sender, RoutedEventArgs e)
        {
            PersonalOfficeWindow personalOffice = new PersonalOfficeWindow();
            personalOffice.Show();
            this.Close();
        }
        private void Products_Click(object sender, RoutedEventArgs e)
        {
            ProductsWindow productsWindow = new ProductsWindow();
            productsWindow.Show();
            this.Close();
        }
        private void Statistics_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                OrdersdbEntities.GetContext().ChangeTracker.Entries().ToList().
                    ForEach(p => p.Reload());
                dgOrders.ItemsSource = OrdersdbEntities.GetContext().Clients.ToList();
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            return;
        }
    }
}
