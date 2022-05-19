using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CRM
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow mainWindow;
        public MainWindow()
        {
            InitializeComponent();
        }

        public DataTable Select(string selectSQL)
        {
            DataTable dataTable = new DataTable("dataBase");
            //server - сервер самой базы данных, DataBase - имя БД, TrustedConnection - безопасное подключение
            SqlConnection sqlConnection = new SqlConnection("server=DESKTOP-MKHVCVP;Trusted_Connection=Yes;DataBase=Ordersdb;");
            sqlConnection.Open();
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = selectSQL;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            sqlDataAdapter.Fill(dataTable);
            sqlConnection.Close();
            return dataTable;
        }
        private void SingInClick(object sender, RoutedEventArgs e)
        {
            if (textBox_login.Text.Length > 0)
            {
                if (password.Password.Length > 0)
                {
                    DataTable dt_user = Select("SELECT * FROM [dbo].[Users] WHERE [UserLogin] = '" +
                        textBox_login.Text + "' AND [UserPassword] = '" + password.Password + "'");
                    if (dt_user.Rows.Count > 0)
                    {
                        AllOrdersWindow allOrdersWindow = new AllOrdersWindow();
                        allOrdersWindow.Show();
                        this.Close();
                    }
                    else MessageBox.Show("Пользователь не найден");
                }
                else MessageBox.Show("Введите пароль");
            }
            else MessageBox.Show("Введите логин");
        }

        private void RegistrationClick(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registrationWindow = new RegistrationWindow();
            registrationWindow.Show();
            this.Close();
        }
    }
}
