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
    /// Логика взаимодействия для EditingWindow.xaml
    /// </summary>
    public partial class EditingWindow : Window
    {
        public EditingWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Информация о заказе
            _prodName.Text = "1";
            _productType.Text = "2";
            _orderData.Text = "3";
            _departament.Text = "4";
            _completedData.Text = "5";
            //Информация о заказчике
            _orgName.Text = "6";
            _contact.Text = "7";
            _address.Text = "8";
            _phoneNumber.Text = "9";
            _email.Text = "10";
        }
    }
}
