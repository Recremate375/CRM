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
    /// Логика взаимодействия для AllOrdersWindow.xaml
    /// </summary>
    public partial class AllOrdersWindow : Window
    {
        public AllOrdersWindow()
        {
            InitializeComponent();
        }

        private void Autorization_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registrationWindow = new RegistrationWindow();
            registrationWindow.Show();
            //Authorization authorizationWindow = new Authorization();
            //authorizationWindow.Show();
        }
        private void Authorization_Loaded(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
        private void Add_Order_Click(object sender, RoutedEventArgs e)
        {
            AddElement addElement = new AddElement();
            addElement.Show();
        }
        private void Editing_Order_click(object sender, RoutedEventArgs e)
        {
            EditingWindow editingWindow = new EditingWindow();
            editingWindow.Show();
        }
    }
}
