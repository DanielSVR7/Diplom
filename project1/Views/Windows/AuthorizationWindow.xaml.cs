using project1.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace project1.Views.Windows
{
    public partial class AuthorizationWindow : Window
    {
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
                string _password = Encryption.Encode(PasswordBox.Password);
                try
                {
                    var _client = (from client in ApplianceStoreEntities.Context.Clients
                                   where _login == client.PhoneNumber && _password == client.Password
                                   select client).Single();
                    WelcomeMessage.Text += "\n" + _client.Firstname + ' ' + _client.Lastname + '!';
                    WelcomeMessage.Visibility = Visibility.Visible;
                    LoginButton.IsEnabled = false;

                    await Task.Delay(2000);
                    CatalogWindow c = new CatalogWindow();
                    c.Client = _client;
                    c.Show();
                    this.Close();
                }
                catch
                {
                    _password = Encryption.Decode(_password);
                    var _manager = (from manager in ApplianceStoreEntities.Context.Managers
                                    where _login == manager.Login && _password == manager.Password
                                    select manager).Single();
                    WelcomeMessage.Text = _manager.FullName;
                    WelcomeMessage.Visibility = Visibility.Visible;
                    LoginButton.IsEnabled = false;

                    await Task.Delay(2000);
                    CatalogWindow c = new CatalogWindow();
                    c.IsAdmin = true;
                    c.AddProductButton.Visibility = Visibility.Visible;
                    c.Client = new Clients { Surname = _manager.FullName, DiscountLevels = new DiscountLevels { Name = " [Менеджер]" } };
                    c.Show();
                    this.Close();
                }

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

        private void RegisterLink_MouseUp(object sender, MouseButtonEventArgs e)
        {
            RegistrationWindow r = new RegistrationWindow();
            r.ShowDialog();
        }
    }
}
