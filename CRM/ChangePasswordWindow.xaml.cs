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
    /// Логика взаимодействия для ChangePasswordWindow.xaml
    /// </summary>
    public partial class ChangePasswordWindow : Window
    {
        string _login = "";
        string _password = "";
        public ChangePasswordWindow()
        {
            InitializeComponent();
        }
        public ChangePasswordWindow(string login, string password)
        {
            InitializeComponent();
            _login = login;
            _password = password;
        }

        private void ChangePass_Click(object sender, RoutedEventArgs e)
        {
            var users = OrdersdbEntities.GetContext().Users.Where(x => x.UserLogin == _login).Where(p => p.UserPassword == _password).ToList();
            foreach (var user in users)
            {
                if (oldPassword.Text != user.UserPassword)
                {
                    MessageBox.Show("Неверный текущий пароль!");
                }
                else if(confirmedPassword.Password == newPassword.Password)
                {
                    user.UserPassword = newPassword.Password;
                    try
                    {
                        OrdersdbEntities.GetContext().SaveChanges();
                        MessageBox.Show("Пароль успешно изменён!");
                        PersonalOfficeWindow personalOfficeWindow = new PersonalOfficeWindow(_login, newPassword.Password);
                        personalOfficeWindow.Show();
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }

        }
    }
}
