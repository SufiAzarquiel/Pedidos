using System;
using System.Windows;

namespace Pedidos.view
{
    /// <summary>
    /// Interaction logic for NewOrderWindow.xaml
    /// </summary>
    public partial class NewOrderWindow : Window
    {
        private MainWindow mainWindow;

        public NewOrderWindow(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            InitializeComponent();
        }

        override protected void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            mainWindow.WindowOpened = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
