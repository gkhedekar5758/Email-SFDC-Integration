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

namespace UINotifications
{
    /// <summary>
    /// Interaction logic for AlertMessageWin.xaml
    /// </summary>
    public partial class AlertMessageWin : Window
    {
        public AlertMessageWin()
        {
            InitializeComponent();
        }
        public AlertMessageWin(String lablemsg)
        {
            InitializeComponent();
            this.lblcasefound.Content = lablemsg;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
           
        }
    }
}
