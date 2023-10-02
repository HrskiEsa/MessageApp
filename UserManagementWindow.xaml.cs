using System.Windows;

namespace MessageApp
{
    /// <summary>
    /// Käyttähallinta
    /// </summary>
    public partial class UserManagementWindow : Window
    {
        private DatabaseManager manager;
        private User currentUser;

        /// <summary>
        /// Luokan alustukset
        /// </summary>
        public UserManagementWindow()
        {
            InitializeComponent();
            currentUser = new User();
            manager = new DatabaseManager();
        }

        /// <summary>
        /// Luokan alustukset (käyttäjätietojen päivitys)
        /// </summary>
        /// <param name="user">Päivitettävä käyttäjä</param>
        public UserManagementWindow(User user) : this()
        {
            currentUser = user;
            txtUserName.Text = currentUser.Username;
            txtUserName.IsEnabled = false;
        }

        private void OKClicked(object sender, RoutedEventArgs e)
        {
            if (txtPassword.Text == txtRetypedPassword.Text)
            {
                if (!txtUserName.IsEnabled)
                {
                    // Salasanan vaihtaminen
                    currentUser.Password = txtPassword.Text;
                    manager.UpdateUser(currentUser);

                    MessageBox.Show("Salasana vaihdettu onnistuneesti", "Käyttäjähallinta");
                }
                else
                {
                    // uuden käyttäjä lisäys
                    var user = new User() { Username = txtUserName.Text, Password = txtPassword.Text };
                    var result = manager.AddUser(user);

                    var message = "";
                    if (result)
                    {
                        message = $"Käyttäjä {user.Username}  lisätty onnistuneesti";
                    }
                    else
                    {
                        message = $"Käyttäjän { user.Username } lisääminen ei onnistunut!";
                    }

                    MessageBox.Show(message, "Käyttäjähallinta");
                }

                Close();
            }
            else
            {
                lblNotMatch.Visibility = Visibility.Visible;
            }
        }
    }
}
