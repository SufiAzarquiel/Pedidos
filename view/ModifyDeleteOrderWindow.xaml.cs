using System;
using System.Windows;

namespace Pedidos.view
{
    /// <summary>
    /// Interaction logic for ModifyDeleteOrderWindow.xaml
    /// </summary>
    public partial class ModifyDeleteOrderWindow : Window
    {
        private MainWindow mainWindow;

        public ModifyDeleteOrderWindow(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            InitializeComponent();
        }

        override protected void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            mainWindow.WindowOpened = false;
        }
    }
}
