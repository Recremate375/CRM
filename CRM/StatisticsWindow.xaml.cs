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

namespace CRM
{
    /// <summary>
    /// Логика взаимодействия для StatisticsWindow.xaml
    /// </summary>
    public partial class StatisticsWindow : Window
    {
        private string login;
        private string password;

        public StatisticsWindow()
        {
            InitializeComponent();
        }

        public StatisticsWindow(string login, string password)
        {
            InitializeComponent();
            this.login = login;
            this.password = password;
        }

        private void Statistic_Click(object sender, RoutedEventArgs e)
        {
            int numberOfAdded = 0;
            int numberOfWorking = 0;
            int numberOfEnded = 0;
            DateTime firstdate = Convert.ToDateTime(_firstDate.ToString());
            DateTime secondDate = Convert.ToDateTime(_secondDate.ToString());
            Guid firstStage = new Guid(), secondStage = new Guid(), thirdStage = new Guid();
            var list = OrdersdbEntities.GetContext().OrderLifeCycle.Where(o => o.Name == "Добавлен").ToList();
            foreach (var item in list)
                firstStage = item.OrderLifeCycleID;
            var list1 = OrdersdbEntities.GetContext().OrderLifeCycle.Where(o => o.Name == "Принят к исполнению").ToList();
            foreach(var item in list1)
                secondStage = item.OrderLifeCycleID;
            var list2 = OrdersdbEntities.GetContext().OrderLifeCycle.Where(o => o.Name == "Завершён").ToList();
            foreach(var item in list2)
                thirdStage = item.OrderLifeCycleID;
            var orders = OrdersdbEntities.GetContext().Orders.
                Where(o => (o.Order_date >= firstdate.Date
                && o.Order_date <= secondDate.Date)
                || (o.Date_of_completion >= firstdate.Date 
                && o.Date_of_completion <= secondDate.Date)).ToList();
            foreach (var order in orders)
            {
                if (order.OrderLifeCycleID == firstStage)
                {
                    numberOfAdded++;
                }
                if (order.OrderLifeCycleID == secondStage)
                {
                    numberOfWorking++;
                }
                if (order.OrderLifeCycleID == thirdStage)
                {
                    numberOfEnded++;
                }
            }
            _edit.Text = numberOfAdded.ToString();
            _recd.Text = numberOfWorking.ToString();
            _completed.Text = numberOfEnded.ToString();
        }

        private void AllOrders_Click(object sender, RoutedEventArgs e)
        {
            AllOrdersWindow allOrdersWindow = new AllOrdersWindow(login, password);
            allOrdersWindow.Show();
            this.Close();
        }

        private void Products_Click(object sender, RoutedEventArgs e)
        {
            ProductsWindow productsWindow = new ProductsWindow(login, password);
            productsWindow.Show();
            this.Close();
        }

        private void Clients_Click(object sender, RoutedEventArgs e)
        {
            ClientsWindow clientsWindow = new ClientsWindow(login, password);
            clientsWindow.Show();
            this.Close();
        }

        private void Autorization_Click(object sender, RoutedEventArgs e)
        {
            PersonalOfficeWindow personalOfficeWindow = new PersonalOfficeWindow(login, password);
            personalOfficeWindow.Show();
            this.Close();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            return;
        }
    }
}
