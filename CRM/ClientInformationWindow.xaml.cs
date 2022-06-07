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
    /// Логика взаимодействия для ClientInformationWindow.xaml
    /// </summary>
    public partial class ClientInformationWindow : Window
    {
        public Clients _client;
        public ClientInformationWindow(Clients client)
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
