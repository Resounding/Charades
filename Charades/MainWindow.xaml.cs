using System.Windows;

namespace Charades
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            
            DataContext = new ViewModel();
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            var remote = new RemoteControl(DataContext as ViewModel) { Owner = this };
            remote.Show();
        }
    }
}
