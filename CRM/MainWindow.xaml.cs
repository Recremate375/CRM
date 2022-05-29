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
        DataBase _dataBase = new DataBase();
        public MainWindow()
        {
            InitializeComponent();
        }
        private void SingInClick(object sender, RoutedEventArgs e)
        {
            if (textBox_login.Text.Length > 0)
            {
                if (password.Password.Length > 0)
                {
                    string findUserString = $"Select * from [dbo].[Users] Where [UserLogin] = '{textBox_login.Text}' and [UserPassword] = '{password.Password}'";
                    SqlCommand sqlCommand = new SqlCommand(findUserString, _dataBase.getConnection());
                    _dataBase.openConnection();
                    SqlDataReader usersDataReader = sqlCommand.ExecuteReader();
                    int count = 0;
                    string login = "";
                    string userPassword = "";
                    while (usersDataReader.Read())
                    {
                        login = usersDataReader.GetString(0);
                        userPassword = usersDataReader.GetString(1);
                        count++;
                    }
                    if (count != 0)
                    {
                        PersonalOfficeWindow personalOfficeWindow = new PersonalOfficeWindow(login, userPassword);
                        _dataBase.closeConnection();
                        personalOfficeWindow.Show();
                        this.Close();
                    }
                    else MessageBox.Show("Пользователь не найден!");
                    _dataBase.closeConnection();

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
