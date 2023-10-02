using System.Windows;

namespace MessageApp
{
    /// <summary>
    /// Ikkuna SQL kyselyiden tekemiseen
    /// </summary>
    public partial class SQLProWindow : Window
    {
        public SQLProWindow()
        {
            InitializeComponent();
        }

        private void ExecuteQuery(object sender, RoutedEventArgs e)
        {

            DatabaseManager mgr = new DatabaseManager();
            var result = mgr.ExecuteCmd(txtQuery.Text);

            grdResult.ItemsSource = result.DefaultView;
        }
    }
}
