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
    /// Логика взаимодействия для ClientAddWindow.xaml
    /// </summary>
    public partial class ClientAddWindow : Window
    {
        DataBase _dataBase = new DataBase();
        public ClientAddWindow()
        {
            InitializeComponent();
        }

        private void AddContact_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_clientName.Text))
                errors.AppendLine("Введите название организции");
            if (string.IsNullOrWhiteSpace(_clientFIO.Text))
                errors.AppendLine("Введите ФИО контакта");
            if (string.IsNullOrWhiteSpace(_clientNumber.Text))
                errors.AppendLine("Укажите номер телефона клиента");
            
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            string insertClient = $"insert into [dbo].[Clients] ([Client ID],[Client Name],[FIO],[Contact position],[Contact Number],[Contact Email],[Client Address])" +
                $"values(NEWID(), '{_clientName.Text}', '{_clientFIO.Text}', '{_clientPosition.Text}', '{_clientNumber.Text}', '{_clientEmail.Text}', '{_clientAddress.Text}')";
            SqlCommand sqlCommand = new SqlCommand(insertClient, _dataBase.getConnection());
            _dataBase.openConnection();
            if (sqlCommand.ExecuteNonQuery() == 1)
            {
                _dataBase.closeConnection();
                MessageBox.Show("Клиент успешно добавлен!");
                ClientsWindow clientsWindow = new ClientsWindow();
                clientsWindow.Show();
                this.Close();
            }
            else
                _dataBase.closeConnection();
        }
    }
}
