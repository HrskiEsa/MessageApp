using System.Configuration;
using System.Windows;
using System.Windows.Media;

namespace MessageApp
{
    /// <summary>
    /// Kirjautuminen järjestelmään
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void LogIn(object sender, RoutedEventArgs e)
        {
            DatabaseManager manager = new DatabaseManager();

            var userName = txtUsername.Text;
            var passWord = txtPassword.Password;

            var user = manager.LogIn(userName, passWord);

            if (user != null)
            {
                var main = new MainWindow(user);
                main.Show();
                this.Close();
            }
            else
            {
                lblPassword.Foreground = Brushes.Red;
            }
        }

        private void AddNewUser(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var newUser = new UserManagementWindow();
            newUser.ShowDialog();
        }
    }
}
