using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    /// Логика взаимодействия для PersonalOfficeWindow.xaml
    /// </summary>
    public partial class PersonalOfficeWindow : Window
    {
        string login, password;
        public PersonalOfficeWindow()
        { 
            InitializeComponent();
        }
        public PersonalOfficeWindow(string Login, string Password)
        {
            InitializeComponent();
            login = Login;
            password = Password;
        }
        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Guid deparment = new Guid();
            var user = OrdersdbEntities.GetContext().Users.Where(l => l.UserLogin == login).Where(x => x.UserPassword == password).ToList();
            foreach (var u in user)
            {
                userLogin.Text = u.UserLogin.ToString();
                userPassword.Password = u.UserPassword.ToString();
                userFIO.Text = u.UserName.ToString();
                deparment = (Guid)u.Userdepartament;
                var departments = OrdersdbEntities.GetContext().Departments.Where(d => d.DepartmentId == deparment).ToList();
                foreach (var department in departments)
                {
                    userDepartment.Text = department.Name.ToString();
                }
                userPosition.Text = u.Userposition.ToString();
            }
        }

        private void ExitAcc_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
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

        private void Statistics_Click(object sender, RoutedEventArgs e)
        {
            StatisticsWindow statisticsWindow = new StatisticsWindow(login, password);
            statisticsWindow.Show();
            this.Close();
        }

        private void ChangePass_Click(object sender, RoutedEventArgs e)
        {
            ChangePasswordWindow changePasswordWindow = new ChangePasswordWindow(userLogin.Text,userPassword.Password);
            changePasswordWindow.Show();
            this.Close();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            return;
        }
    }
}
