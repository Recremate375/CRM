using System;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;

namespace CRM
{
    /// <summary>
    /// Логика взаимодействия для AddElement.xaml
    /// </summary>
    public partial class AddElement : Window
    {
        DataBase _dataBase = new DataBase();
        public Orders _orders = new Orders();
        public AddElement()
        {
            InitializeComponent();
            DataContext = _orders;
            _productType.ItemsSource = OrdersdbEntities.GetContext().Products.ToList();
            _departament.ItemsSource = OrdersdbEntities.GetContext().Departments.ToList();
            _lifeCycle.ItemsSource = OrdersdbEntities.GetContext().OrderLifeCycle.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (_productType.Text.Length > 0)
            {
                string findProductString = $"Select * from [dbo].[Products] Where [Product Type] = '{_productType.Text}'";
                SqlCommand findProductCommand = new SqlCommand(findProductString, _dataBase.getConnection());
                _dataBase.openConnection();
                SqlDataReader productDataReader = findProductCommand.ExecuteReader();
                int count = 0;
                Guid productsId = Guid.Empty;
                while (productDataReader.Read())
                {
                    productsId = productDataReader.GetGuid(0);
                    count++;
                }
                _dataBase.closeConnection();
                if (_orderData != null)
                {
                    if (_departament.Text.Length > 0)
                    {
                        string departmentIdstring = $"Select * from [dbo].[Departments] Where [Name]='{_departament.Text}'";
                        SqlCommand findDepartmentCommand = new SqlCommand(departmentIdstring, _dataBase.getConnection());
                        _dataBase.openConnection();
                        SqlDataReader sqlDataReader = findDepartmentCommand.ExecuteReader();
                        int counter1 = 0;
                        Guid departmentId = new Guid();
                        while (sqlDataReader.Read())
                        {
                            departmentId = sqlDataReader.GetGuid(0);
                            counter1++;
                        }
                        _dataBase.closeConnection();
                        if (_orgName.Text.Length > 0)
                        {
                            string lifecyclestring = $"Select * from [dbo].[OrderLifeCycle] Where [Name]= '{_lifeCycle.Text}'";
                            SqlCommand findLifeCycle = new SqlCommand(lifecyclestring, _dataBase.getConnection());
                            _dataBase.openConnection();
                            SqlDataReader sqlDataReader1 = findLifeCycle.ExecuteReader();
                            int count1 = 0;
                            Guid lifeCycleID = new Guid();
                            while (sqlDataReader1.Read())
                            {
                                lifeCycleID = sqlDataReader1.GetGuid(0);
                                count1++;
                            }
                            _dataBase.closeConnection();
                            if (_contact.Text.Length > 0)
                            {
                                string insertContactInformation = $"insert into [dbo].[Clients] ([Client ID], [Client Name], [FIO], [Contact Number]," +
                                    $"[Contact Email], [Client Address]) values(NEWID(), '{_orgName.Text}', '{_contact.Text}', '{_phoneNumber.Text}', '{_address.Text}', '{_email.Text}')";
                                SqlCommand sqlCommand = new SqlCommand(insertContactInformation, _dataBase.getConnection());
                                _dataBase.openConnection();
                                if (sqlCommand.ExecuteNonQuery() == 1)
                                { }
                                _dataBase.closeConnection();
                                string findContactIdString = $"select * from [dbo].[Clients] Where [FIO] = '{_contact.Text}' and [Client Name] ='{_orgName.Text}'";
                                SqlCommand findComtactCommand = new SqlCommand(findContactIdString, _dataBase.getConnection());
                                _dataBase.openConnection();
                                SqlDataReader contactDataReader = findComtactCommand.ExecuteReader();
                                int counter = 0;
                                Guid contactId = new Guid();
                                while (contactDataReader.Read())
                                {
                                    contactId = contactDataReader.GetGuid(0);
                                    counter++;
                                }
                                _dataBase.closeConnection();
                                if (_address.Text.Length > 0)
                                {
                                    var productionAmount = Convert.ToInt32(_productionAmount.Text);
                                    string addOrdersString = $"insert into [dbo].[Orders] ([Order ID], [Product ID], [Client ID], [Departament ID], [Order date], [Date of completion], [Production amount], [OrderLifeCycleID])" +
                                        $" values(NEWID(), '{productsId}', '{contactId}', '{departmentId}', '{_orderData}', '{_completedData}', '{productionAmount}', '{lifeCycleID}')";
                                    SqlCommand command = new SqlCommand(addOrdersString, _dataBase.getConnection());
                                    _dataBase.openConnection();
                                    if (command.ExecuteNonQuery() == 1)
                                    {
                                        MessageBox.Show("Заказ успешно добавлен!");
                                        _dataBase.closeConnection();
                                        AllOrdersWindow allOrdersWindow = new AllOrdersWindow();
                                        allOrdersWindow.Show();
                                        this.Close();
                                    }
                                    else MessageBox.Show("Заказ не добавлен!");
                                    _dataBase.closeConnection();
                                }
                                else MessageBox.Show("Введите адрес!");
                            }
                            else MessageBox.Show("Введите ФИО контакта!");
                        }
                        else MessageBox.Show("Введите наименование организации!");
                    }
                    else MessageBox.Show("Выберите отдел!");
                }
                else MessageBox.Show("Введите дату заказа!");
            }
            else MessageBox.Show("Выберите тип продукции!");
        }
    }
}