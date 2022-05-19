using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.SqlClient;
namespace CRM
{
    enum RowState
    {
        Existed,
        New,
        Modified,
        ModifiedNew,
        Deleted
    }

    public partial class AllOrdersWindow : Window
    {
        DataBase _dataBase = new DataBase();
        int selectedRow;
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
        private void CreateColumns()
        {

        }
        private void Authorization_Loaded(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
        private void Add_Order_Click(object sender, RoutedEventArgs e)
        {
            AddElement addElement = new AddElement();
            addElement.Show();
            this.Close();
        }
        private void Editing_Order_click(object sender, RoutedEventArgs e)
        {
            EditingWindow editingWindow = new EditingWindow();
            editingWindow.Show();
            this.Close();
        }
    }
}
