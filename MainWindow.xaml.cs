using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MessageApp
{
    /// <summary>
    /// Sovelluksen pääikkuna
    /// </summary>
    public partial class MainWindow : Window
    {

        private User currentUser;
        private DatabaseManager manager = new DatabaseManager();

        public MainWindow(User user)
        {
            InitializeComponent();

            comFrom.ItemsSource = manager.GetUsers();

            currentUser = user;
            comFrom.IsEnabled = currentUser.IsAdmin;
            comFrom.SelectedValue = currentUser.Username;


            mnuMaintenanceItem.IsEnabled = currentUser.IsAdmin;

        }

        private void Refresh(object sender, MouseButtonEventArgs e)
        {
            GetMessages();
        }

        private void SelecteItemChanged(object sender, SelectionChangedEventArgs e)
        {
            GetMessages();
        }

        private void SearchClicked(object sender, RoutedEventArgs e)
        {
            GetMessages();
        }

        private void GetMessages()
        {
            if (comFrom.SelectedItem != null)
            {
                var reseiver = ((User)comFrom.SelectedItem).Username;
                var filterText = txtSearch.Text;
                if (dataGrid != null)
                {

                    try
                    {
                        dataGrid.ItemsSource = manager.GetMessages(reseiver, filterText);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "SQL VIRHE!");
                    }
                }
            }
        }

        private void RowSelected(object sender, SelectionChangedEventArgs e)
        {
            var message = (MailMessage)dataGrid.SelectedItem;

            if (message != null)
            {
                txtMessage.Text = message.Message;

                if(message.Encrypted == 1)
                {
                    pnlDecrypt.Visibility = Visibility.Visible;
                }
                else
                {
                    pnlDecrypt.Visibility = Visibility.Hidden;
                }
                
            }
        }

        private void NewEmailClicked(object sender, RoutedEventArgs e)
        {
            var newMessage = new NewMessage();
            newMessage.ShowDialog();
            GetMessages();
        }

        private void SQLProClicked(object sender, RoutedEventArgs e)
        {
            var sqlView = new SQLProWindow();
            sqlView.ShowDialog();
        }

        private void UserManagementClicked(object sender, RoutedEventArgs e)
        {
            var usersView = new UserManagementWindow(currentUser);
            usersView.ShowDialog();
        }

        private void CloseClicked(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DecryptClicked(object sender, RoutedEventArgs e)
        {
            // Lähdin lomille ja tämä jäi kesken... toivottavasti muistan tehdä loppuun ;)
            MessageBox.Show("Toiminnallisuus kesken...", "Todo");

        }

        private void ImportMenuItemClicked(object sender, RoutedEventArgs e)
        {
            var importWin = new ImportMessagesWindow();
            importWin.ShowDialog();
        }

        private void MaintenanceClicked(object sender, RoutedEventArgs e)
        {
            var maintenance = new MaintenanceWindow();
            maintenance.ShowDialog();
        }
    }
}
