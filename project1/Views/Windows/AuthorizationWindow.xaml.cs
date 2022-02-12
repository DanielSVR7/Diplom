using project1.Models;
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

namespace project1.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        ApplianceStoreEntities db = new ApplianceStoreEntities();
        public AuthorizationWindow()
        {
            InitializeComponent();
            WelcomeMessage.Visibility = Visibility.Hidden;
        }
        private async void LoginButton_Click(object sender, RoutedEventArgs e)      //Обработчик события нажатия на конпку войти 
        {
            try         //Попытка авторизоваться
            {
                string _login = loginTextBox.Text;
                string _password = PasswordBox.Password;
                var name = (from client in db.Clients where _login == client.PhoneNumber && _password == client.Password select client.Firstname + " " + client.Lastname).Single();
                WelcomeMessage.Text += "\n" + name + '!';
                WelcomeMessage.Visibility = Visibility.Visible;
                LoginButton.IsEnabled = false;
                await Task.Delay(2000);
                Catalog c = new Catalog();
                c.Show();
                this.Close();
            }
            catch       //Если пара логина и пароля не найдена в базе данных
            {
                LoginLabel1.Foreground = Brushes.Red;
                LoginLabel2.Foreground = Brushes.Red;
                await Task.Delay(1000);
                LoginLabel1.Foreground = (Brush)new BrushConverter().ConvertFrom("#BF000000");
                LoginLabel2.Foreground = (Brush)new BrushConverter().ConvertFrom("#BF000000");
            }
        }
    }
}
