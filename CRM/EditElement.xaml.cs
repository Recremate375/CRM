﻿using System;
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

            DataContext = _orders;
            try
            {
                var productList = new List<Products>();
                var departmentList = new List<Departments>();
                var orderLifeCycleList = new List<OrderLifeCycle>();
                productList = OrdersdbEntities.GetContext().Products.ToList();
                departmentList = OrdersdbEntities.GetContext().Departments.ToList();
                orderLifeCycleList = OrdersdbEntities.GetContext().OrderLifeCycle.ToList();
                int productNumber = 0;
                int departmentNumber = 0;
                int orderLifeCycleNumber = 0;
                for (int i = 0; i < productList.Count; i++)
                {
                    if (productList[i].Product_ID == _orders.Product_ID)
                    {
                        productNumber = i;
                        break;
                    }
                }
                for (int i = 0; i < departmentList.Count; i++)
                {
                    if (departmentList[i].DepartmentId == _orders.Departament_ID)
                    {
                        departmentNumber = i;
                        break;
                    }
                }
                for (int i = 0; i < orderLifeCycleList.Count; i++)
                {
                    if (orderLifeCycleList[i].OrderLifeCycleID == _orders.OrderLifeCycleID)
                    {
                        orderLifeCycleNumber = i;
                        break;
                    }
                }
                _productType.ItemsSource = productList;
                _departament.ItemsSource = departmentList;
                _lifecycle.ItemsSource = orderLifeCycleList;

                _productType.SelectedIndex = productNumber;
                _departament.SelectedIndex = departmentNumber;
                _lifecycle.SelectedIndex = orderLifeCycleNumber;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
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
                Guid productId = new Guid();
                Guid departmentId = new Guid();
                var lifeCycle = OrdersdbEntities.GetContext().OrderLifeCycle.Where(o => o.Name == _lifecycle.Text.ToString()).ToList();
                foreach (var cycle in lifeCycle)
                {
                    orderLifeGuid = cycle.OrderLifeCycleID;
                }
                var product = OrdersdbEntities.GetContext().Products.Where(p => p.Product_Type == _productType.Text.ToString()).ToList();
                foreach (var p in product)
                {
                    productId = p.Product_ID;
                }
                var department = OrdersdbEntities.GetContext().Departments.Where(d => d.Name == _departament.Text.ToString()).ToList();
                foreach (var dep in department)
                {
                    departmentId = dep.DepartmentId;
                }

                int productionAmount = Convert.ToInt32(_productionAmount.Text.ToString());
                string updateOrders = $"Update [dbo].[Orders] set [OrderLifeCycleID] = '{orderLifeGuid}'," +
                    $" [Product ID] = '{productId}', [Departament ID] = '{departmentId}' Where [Order ID] = '{OrderID}'";
                SqlCommand sqlCommand = new SqlCommand(updateOrders, _dataBase.getConnection());
                _dataBase.openConnection();
                if (sqlCommand.ExecuteNonQuery() == 1) { _dataBase.closeConnection(); }
                _dataBase.closeConnection();

                OrdersdbEntities.GetContext().SaveChanges();
                MessageBox.Show("Заказ успешно изменён!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


    }
}
