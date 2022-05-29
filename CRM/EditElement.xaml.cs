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
    /// Логика взаимодействия для AddElement.xaml
    /// </summary>
    public partial class EditElement : Window
    {
        public Orders _orders = new Orders();
        DataBase _dataBase = new DataBase();
        Guid OrderID = new Guid();
        public EditElement(Orders orders)
        {
            InitializeComponent();
            if (orders != null)
            {
                _orders = orders;
                OrderID = orders.Order_ID;
            }
            DataContext = orders;
            _productType.ItemsSource = OrdersdbEntities.GetContext().Products.ToList();
            _departament.ItemsSource = OrdersdbEntities.GetContext().Departments.ToList();
            _lifecycle.ItemsSource = OrdersdbEntities.GetContext().OrderLifeCycle.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (_productType.Text.Length == 0)
                errors.AppendLine("Выберите тип продукции!");
            if (_orderData == null)
                errors.AppendLine("Введите дату заказа!");
            if (_departament.Text.Length == 0)
                errors = errors.AppendLine("Выберите отдел!");
            if (_orgName.Text.Length == 0)
                errors = errors.AppendLine("Введите наименование организации!");
            if (_contact.Text.Length == 0)
                errors = errors.AppendLine("Введите ФИО контакта!");
            if (_address.Text.Length == 0)
                errors = errors.AppendLine("Введите адрес!");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            Guid orderLifeGuid = new Guid();
            var lifeCycle = OrdersdbEntities.GetContext().OrderLifeCycle.Where(o => o.Name == _lifecycle.Text.ToString()).ToList();
            foreach (var cycle in lifeCycle)
            {
                orderLifeGuid = cycle.OrderLifeCycleID;
            }

            int productionAmount = Convert.ToInt32(_productionAmount.Text.ToString());
            string updateOrders = $"Update [dbo].[Orders] set [OrderLifeCycleID] = '{orderLifeGuid}' Where [Order ID] = '{OrderID}'";
            SqlCommand sqlCommand = new SqlCommand(updateOrders, _dataBase.getConnection());
            _dataBase.openConnection();
            if(sqlCommand.ExecuteNonQuery() == 1) { _dataBase.closeConnection(); }
            _dataBase.closeConnection();
            try
            {
                OrdersdbEntities.GetContext().SaveChanges();
                MessageBox.Show("Заказ успешно изменён!");
                AllOrdersWindow allOrdersWindow = new AllOrdersWindow();
                allOrdersWindow.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
