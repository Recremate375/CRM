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
using System.Windows.Threading;

namespace CRM
{
    /// <summary>
    /// Логика взаимодействия для ProductsWindow.xaml
    /// </summary>
    public partial class ProductsWindow : Window
    {
        string login, password;
        public ProductsWindow(string login, string password)
        {
            InitializeComponent();
            this.login = login;
            this.password = password;
        }

        private void AllOrders_Click(object sender, RoutedEventArgs e)
        {
            AllOrdersWindow allOrdersWindow = new AllOrdersWindow(login, password);
            allOrdersWindow.Show();
            this.Close();
        }
        private void AllClients_Click(object sender, RoutedEventArgs e)
        {
            ClientsWindow clientsWindow = new ClientsWindow(login, password);
            clientsWindow.Show();
            this.Close();
        }
        private void Statistics_Click(object sender, RoutedEventArgs e)
        {
            StatisticsWindow statisticsWindow = new StatisticsWindow(login, password);
            statisticsWindow.Show();
            this.Close();
        }
        private void Authorization_Click(object sender, RoutedEventArgs e)
        {
            PersonalOfficeWindow personalOffice = new PersonalOfficeWindow(login, password);
            personalOffice.Show();
            this.Close();
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
            dgOrders.ItemsSource = OrdersdbEntities.GetContext().Products.ToList();
        }
        private void SetTimer()
        {
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 15);
            dispatcherTimer.Start();
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            return;
        }

        private void AddProducts_Click(object sender, RoutedEventArgs e)
        {
            ProductEditWindow productEditWindow = new ProductEditWindow();
            productEditWindow.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var productToShow = dgOrders.SelectedItem;
            if (productToShow != null)
            {
                DescriptionWindow descriptionWindow = new DescriptionWindow((Products)productToShow);
                descriptionWindow.Show();
            }
            else
            {
                MessageBox.Show("Выберите продукцию, которую хотите просмотреть");
            }
        }
    }
}
