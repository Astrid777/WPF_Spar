using System;
using System.Windows;
using System.Windows.Navigation;

namespace WpfApp20
{


    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int raz;
        public MainWindow()
        {
            InitializeComponent();

            //this.Width = 750;
        }




        private void Table_Selected(object sender, RoutedEventArgs e)
        {

            fr.Navigate(new Uri("/Page1.xaml", UriKind.Relative));
            this.Width = 1200;

        }

        private void Chart_Selected(object sender, RoutedEventArgs e)
        {
            fr.Navigate(new Uri("/Page2.xaml", UriKind.Relative));
            this.Width = 1000;
        }

        private void fr_Navigated(object sender, NavigationEventArgs e)
        {




        }
    }
}
