using System.Windows;

namespace MessageApp
{
    /// <summary>
    /// Uuden viestin luonti
    /// </summary>
    public partial class NewMessage : Window
    {
        DatabaseManager manager = new DatabaseManager();

        public NewMessage()
        {
            InitializeComponent();
            comUsers.ItemsSource = manager.GetUsers();
        }

        private void SendMessage(object sender, RoutedEventArgs e)
        {
            int isEncrypted = 0;
            if (chkEncrypt.IsChecked == true)
            {
                isEncrypted = 1;
            }

            if (isEncrypted == 1 && txtKey.Text.Equals(string.Empty)) 
            {
                MessageBox.Show("Salattu viesti tarvitsee salausavaimen", "Viestin salaus");
                return;
            }

            var to = ((User)comUsers.SelectedItem).Username;
            var message = txtMessage.Text;
            manager.AddMessage(to, message, isEncrypted);
            this.Close();
        }

        private void chkEncrypt_Checked(object sender, RoutedEventArgs e)
        {
            keyPanel.Visibility = Visibility.Visible;
            this.Height += 25;
        }

        private void chkEncrypt_Unchecked(object sender, RoutedEventArgs e)
        {
            keyPanel.Visibility = Visibility.Collapsed;
            this.Height -= 25;
        }
    }
}
