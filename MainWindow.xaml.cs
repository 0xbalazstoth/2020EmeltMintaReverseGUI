using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _2020EmeltReverseGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Tabla tabla;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Mw_Loaded(object sender, RoutedEventArgs e)
        {
            tabla = new Tabla(@"allas.txt", Mw, Can);
            tabla.Megjelenit();
        }
    }
}
