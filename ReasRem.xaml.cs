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
using System.Windows.Shapes;

namespace modulnik
{
    /// <summary>
    /// Логика взаимодействия для ReasRem.xaml
    /// </summary>
    public partial class ReasRem : Window
    {
        public ReasRem()
        {
            InitializeComponent();
        }

        private void Button_RemoteOrder(object sender, RoutedEventArgs e)
        {
            Options.resremote = reason_remote.Text;
            this.Close();
        }
    }
}
