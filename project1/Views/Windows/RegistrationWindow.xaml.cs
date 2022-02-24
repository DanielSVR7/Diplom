using project1.Models;
using System;
using System.Linq;
using System.Windows;

namespace project1.Views.Windows
{
    public partial class RegistrationWindow : Window
    {
        ApplianceStoreEntities db;
        public RegistrationWindow(ApplianceStoreEntities context)
        {
            InitializeComponent();
            db = context;
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
                        ClientID = (from c in db.Clients select c).ToList().Last().ClientID + 1,
                        Surname = SurnameTB.Text,
                        Firstname = FirstnameTB.Text,
                        Lastname = LastnameTB.Text,
                        PhoneNumber = PhoneTB.Text,
                        Password = Encryption.Encode(PasswordTB.Password)
                };
                    db.Clients.Add(client);
                    db.SaveChanges();
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
