using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.SqlClient;
using System.Windows.Controls;
using Microsoft.Office.Interop.Word;
using System.Windows.Threading;

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
        }
        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
             try
             {
                 RebindData();
                 SetTimer();
             }
             catch (Exception ex)
             {
                 MessageBox.Show(ex.ToString());
             }
        }
        protected void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            RebindData();
        }
        private void RebindData()
        {
            OrdersdbEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(o => o.Reload());
            dgOrders.ItemsSource = OrdersdbEntities.GetContext().Orders.ToList();
        }
        private void SetTimer()
        {
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 15);
            dispatcherTimer.Start();
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

        private void Delete_Order_Click(object sender, RoutedEventArgs e)
        {
            var ordersToRemove = dgOrders.SelectedItems.Cast<Orders>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следующие {ordersToRemove.Count()} элементов?", "Внимание", 
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    OrdersdbEntities.GetContext().Orders.RemoveRange(ordersToRemove);
                    OrdersdbEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные успешно удалены!");

                    dgOrders.ItemsSource = OrdersdbEntities.GetContext().Orders.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void Change_Order_Click(object sender, RoutedEventArgs e)
        {
            var orderToChange = dgOrders.SelectedItem;
            if (orderToChange != null)
            {
                EditElement changeOrder = new EditElement((Orders)orderToChange);
                changeOrder.Show();
            }
            else
            {
                MessageBox.Show("Выберите заказ, который хотите изменить");
            }
        }
    }
}
