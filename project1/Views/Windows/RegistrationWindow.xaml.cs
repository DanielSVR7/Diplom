using project1.Models;
using System;
using System.Linq;
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
                MessageBox.Show("Заполните необходимые поля", "Обязательные поля не заполнены", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                try
                {
                    var client = new Clients
                    {
                        ClientID = (from c in ApplianceStoreEntities.Context.Clients select c).ToList().Last().ClientID + 1,
                        Surname = SurnameTB.Text,
                        Firstname = FirstnameTB.Text,
                        Lastname = LastnameTB.Text,
                        PhoneNumber = PhoneTB.Text,
                        Password = PasswordTB.Password
                    };
                    ApplianceStoreEntities.Context.Clients.Add(client);
                    ApplianceStoreEntities.Context.SaveChanges();
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
