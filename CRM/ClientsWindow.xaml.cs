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
    /// Логика взаимодействия для ClientsWindow.xaml
    /// </summary>
    public partial class ClientsWindow : Window
    {
        private string login;
        private string password;

        public ClientsWindow()
        {
            InitializeComponent();
        }

        public ClientsWindow(string login, string password)
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
        private void Authorization_Click(object sender, RoutedEventArgs e)
        {
            PersonalOfficeWindow personalOffice = new PersonalOfficeWindow(login, password);
            personalOffice.Show();
            this.Close();
        }
        private void Products_Click(object sender, RoutedEventArgs e)
        {
            ProductsWindow productsWindow = new ProductsWindow(login, password);
            productsWindow.Show();
            this.Close();
        }
        private void Statistics_Click(object sender, RoutedEventArgs e)
        {
            StatisticsWindow statisticsWindow = new StatisticsWindow(login, password);
            statisticsWindow.Show();
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
            OrdersdbEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(c => c.Reload());
            dgOrders.ItemsSource = OrdersdbEntities.GetContext().Clients.ToList();
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

        private void EditClient_Click(object sender, RoutedEventArgs e)
        {
            ClientAddWindow clientAddWindow = new ClientAddWindow();
            clientAddWindow.Show();
            this.Close();
        }

        private void ShowClientInformation_Click(object sender, RoutedEventArgs e)
        {
            var clientToShow = dgOrders.SelectedItem;
            if (clientToShow != null)
            {
                ClientInformationWindow informationWindow = new ClientInformationWindow((Clients)clientToShow);
                informationWindow.Show();
            }
            else
            {
                MessageBox.Show("Выберите клиента, о котором хотите увидеть информацию");
            }
        }

        private void ChangeClient_Click(object sender, RoutedEventArgs e)
        {
            var clientToChange = dgOrders.SelectedItem;
            if (clientToChange != null)
            {
                ChangeClientWindow changeWindow = new ChangeClientWindow((Clients)clientToChange);
                changeWindow.Show();
            }
            else
            {
                MessageBox.Show("Выберите клиента, информацию о котором вы хотите изменить");
            }
        }
    }
}
