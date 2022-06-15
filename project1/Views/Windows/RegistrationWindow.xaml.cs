using project1.Models;
using System;
using System.Linq;
using System.Text;
using System.Windows;

namespace project1.Views.Windows
{
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
        }

        private void regButton_Click(object sender, RoutedEventArgs e)
        {
            if (SurnameTB.Text == string.Empty || FirstnameTB.Text == string.Empty || PhoneTB.Text == string.Empty || PasswordTB.Password == string.Empty)
            {
                MessageBox.Show("Заполните необходимые поля", "Обязательные поля не заполнены", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                try
                {
                    var client = (from c in ApplianceStoreEntities.Context.Clients where c.PhoneNumber == PhoneTB.Text select c).Single();
                    MessageBox.Show("Клиент с данным номером телефона уже зарегестрирован", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch
                {
                    StringBuilder errors = new StringBuilder();
                    var client = new Clients
                    {
                        ClientID = (from c in ApplianceStoreEntities.Context.Clients select c).ToList().Last().ClientID + 1,
                        Surname = SurnameTB.Text,
                        Firstname = FirstnameTB.Text,
                        Lastname = LastnameTB.Text,
                        PhoneNumber = PhoneTB.Text,
                        Password = Encryption.Encode(PasswordTB.Password)
                    };
                    if (string.IsNullOrEmpty(client.Surname))
                        errors.AppendLine("Необходимо указать фамилию");
                    if (string.IsNullOrEmpty(client.Firstname))
                        errors.AppendLine("Необходимо указать имя");
                    if (string.IsNullOrEmpty(client.PhoneNumber))
                        errors.AppendLine("Необходимо указать номер телефона");
                    if (client.PhoneNumber.Length != 11)
                        errors.AppendLine("Номер телефона имеет неверный формат");
                    foreach (var c in client.PhoneNumber)
                    {
                        if (char.IsDigit(c))
                            continue;
                        errors.AppendLine("Номер телефона имеет неверный формат");
                        break;
                    }
                    if (string.IsNullOrEmpty(client.Password))
                        errors.AppendLine("Необходимо указать пароль");
                    if (PasswordTB.Password.Length < 4)
                        errors.AppendLine("Минимальная длина пароля - 4 символа");
                    if (PasswordTB.Password.Length > 16)
                        errors.AppendLine("Максимальная длина пароля - 16 символов");
                    if (errors.Length > 0)
                    {
                        MessageBox.Show(errors.ToString(), "Возникли ошибки", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    try
                    {
                        ApplianceStoreEntities.Context.Clients.Add(client);
                        ApplianceStoreEntities.Context.SaveChanges();
                        MessageBox.Show("Аккаунт успешно зарегистрирован.\nДобро пожаловать, " + client.Firstname + ' ' + (client.Lastname ?? "") + '!', "Регистрация", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), ex.Message);
                    }
                }
            }
        }
    }
}
