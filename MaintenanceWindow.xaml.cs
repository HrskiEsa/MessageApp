using System.Windows;

namespace MessageApp
{
    /// <summary>
    /// Interaction logic for MaintenanceWindow.xaml
    /// </summary>
    public partial class MaintenanceWindow : Window
    {
        public MaintenanceWindow()
        {
            InitializeComponent();
        }

        private void EncryptPasswordsClick(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Haluatko varmasti jatkaa?", "Varmistus", MessageBoxButton.OKCancel, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                // salasanojen salaus koodi
            }
        }
    }
}
