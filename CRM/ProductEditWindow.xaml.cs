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
    /// Логика взаимодействия для ProductEditWindow.xaml
    /// </summary>
    public partial class ProductEditWindow : Window
    {
        public Products products = new Products();
        DataBase _dataBase = new DataBase();
        public ProductEditWindow()
        {
            InitializeComponent();
        }

        private void AddNewProduct_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_productType.ToString()))
            {
                errors.AppendLine("Укажите название продукта");
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            string insertProduct = $"insert into [dbo].[Products] ([Product ID], [Product Type], [Description]) " +
                $"values(NEWID(), '{_productType.Text}', '{_productDescription.Text}')";
            SqlCommand sqlCommand = new SqlCommand(insertProduct, _dataBase.getConnection());
            _dataBase.openConnection();
            if(sqlCommand.ExecuteNonQuery() == 1)
            {
                _dataBase.closeConnection();
                MessageBox.Show("Продукт успешно добавлен!");
                this.Close();
            }
            else
            {
                _dataBase.closeConnection();
            }
        }
    }
}
