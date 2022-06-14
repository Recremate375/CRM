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
    /// Логика взаимодействия для ChangeClientWindow.xaml
    /// </summary>
    public partial class ChangeClientWindow : Window
    {
        public Clients _client;
        public ChangeClientWindow(Clients client)
        {
            InitializeComponent();
            if (client != null)
            {
                _client = client;
            }
            DataContext = _client;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            this.Close();
        }
    }
}
