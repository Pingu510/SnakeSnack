using System.Windows;

namespace SnakeSnack
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GameHandler handler;

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new ViewModels.MainViewModel();            
        }
    }
}
