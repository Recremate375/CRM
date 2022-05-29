using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.SqlClient;
using System.Windows.Controls;
using Microsoft.Office.Interop.Word;

namespace CRM
{

    public partial class AllOrdersWindow : System.Windows.Window
    {
        public AllOrdersWindow()
        {
            InitializeComponent();
        }

        private void Autorization_Click(object sender, RoutedEventArgs e)
        {
            PersonalOfficeWindow personalOffice = new PersonalOfficeWindow();
            personalOffice.Show();
            this.Close();
        }

        private void Authorization_Loaded(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
        private void Add_Order_Click(object sender, RoutedEventArgs e)
        {
            AddElement addElement = new AddElement();
            addElement.Show();
            this.Close();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            EditElement changeElement = new EditElement((sender as Button).DataContext as Orders);
            changeElement.Show();
            this.Close();
        }

        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                OrdersdbEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(o => o.Reload());
                dgOrders.ItemsSource = OrdersdbEntities.GetContext().Orders.ToList();
            }
        }
        private void Products_Click(object sender, RoutedEventArgs e)
        {
            ProductsWindow productsWindow = new ProductsWindow();
            productsWindow.Show();
            this.Close();
        }

        private void Clients_Click(object sender, RoutedEventArgs e)
        {
            ClientsWindow clients = new ClientsWindow();
            clients.Show();
            this.Close();
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            return;
        }

        private void Statistics_Click(object sender, RoutedEventArgs e)
        {
            StatisticsWindow statistics = new StatisticsWindow();
            statistics.Show();
            this.Close();
        }

        private void CreateDocxFile(object sender, RoutedEventArgs e)
        {
            Microsoft.Office.Interop.Word.Application wordApplication =
                new Microsoft.Office.Interop.Word.Application();

        }
    }
}
