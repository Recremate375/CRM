using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace CRM
{
    public partial class RegistrationWindow : Window
    {
        DataBase _dataBase = new DataBase();
        public RegistrationWindow()
        {
            InitializeComponent();
            string queryString = $"Select * from [dbo].[Departments]";
            SqlCommand command = new SqlCommand(queryString, _dataBase.getConnection());
            _dataBase.openConnection();
            SqlDataReader sqlDataReader = command.ExecuteReader();
            int counter = 0;
            while (sqlDataReader.Read())
            {
                departamet.Items.Add(sqlDataReader.GetString(1));
                counter++;
            }
            _dataBase.closeConnection();
        }
        private void Authorization_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
        private void Registrarion_Click(object sender, RoutedEventArgs e)
        {
            if (textBox_login.Text.Length > 0)
            {
                if (password.Password.Length > 0)
                {
                    if (confirmed_password.Password == password.Password)
                    {
                        if (fio.Text.Length > 0)
                        {
                            if (departamet.Text.Length > 0)
                            {
                                string departmentsName = departamet.Text.ToString();
                                string departmentIdstring = $"Select * from [dbo].[Departments] Where [Name]='{departmentsName}'";
                                SqlCommand sqlCommand = new SqlCommand(departmentIdstring, _dataBase.getConnection());
                                _dataBase.openConnection();
                                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                                //string departmentId = sqlDataReader.GetString(0);
                                int counter = 0;
                                Guid departmentId = new Guid();
                                while (sqlDataReader.Read())
                                {
                                    departmentId = sqlDataReader.GetGuid(0);
                                    counter++;
                                }
                                _dataBase.closeConnection();
                                if (position.Text.Length > 0)
                                {
                                    string querystring = $"INSERT INTO [dbo].[Users] (UserId,UserLogin, UserPassword, UserName, Userdepartament, Userposition) values(NEWID(),'{textBox_login.Text}', '{password.Password}'," +
                                        $" '{fio.Text}', '{departmentId}', '{position.Text}')";
                                    SqlCommand command = new SqlCommand(querystring, _dataBase.getConnection());
                                    _dataBase.openConnection();
                                    if (command.ExecuteNonQuery() == 1)
                                    {
                                        MessageBox.Show("Вы успешно зарегистрировались!");
                                        MainWindow mainWindow = new MainWindow();
                                        this.Close();
                                        mainWindow.Show();
                                    }
                                    else
                                    {
                                        MessageBox.Show("Аккаунт не был создан!");
                                    }
                                    _dataBase.closeConnection();
                                }
                                else MessageBox.Show("Введите должность сотрудника");
                            }   
                            else MessageBox.Show("Введите отдел сотрудника");
                        }
                        else MessageBox.Show("Введите ФИО сотрудника");
                    }
                    else MessageBox.Show("Пароли не совпадают");
                }
                else MessageBox.Show("Введите пароль");
            }
            else MessageBox.Show("Введите логин");
        }
        private Boolean checkuser()
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dataTable = new DataTable();
            string querystring = $"SELECT * from Users where UserLogin = '{textBox_login.Text}' and UserPassword = '{password.Password}'";
            SqlCommand command = new SqlCommand(querystring, _dataBase.getConnection());
            adapter.SelectCommand = command;
            adapter.Fill(dataTable);

            if (dataTable.Rows.Count > 0)
            {
                MessageBox.Show("Данный аккаунт уже зарегестрирован в системе!");
                return true;
            }
            else return false;
        }
    }
}

