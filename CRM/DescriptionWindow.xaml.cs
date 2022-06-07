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
    /// Логика взаимодействия для DescriptionWindow.xaml
    /// </summary>
    public partial class DescriptionWindow : Window
    {
        public Products _product = new Products();
        public DescriptionWindow(Products product)
        {
            InitializeComponent();
            if (product != null)
            {
                _product = product;
            }
            DataContext = _product;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
