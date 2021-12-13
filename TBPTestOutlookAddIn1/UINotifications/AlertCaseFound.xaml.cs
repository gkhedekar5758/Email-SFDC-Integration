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
using System.Windows.Threading;


namespace UINotifications
{
    /// <summary>
    /// Interaction logic for AlertCaseFound.xaml
    /// </summary>
    public partial class AlertCaseFound : Window
    {
        public AlertCaseFound()
        {
            InitializeComponent();
            //MessageBox.Show("Herer");
             DispatcherTimer timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(2d);
                timer.Tick += TimerTick;
                timer.Start();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            DispatcherTimer timer = (DispatcherTimer)sender;
            timer.Stop();
            timer.Tick -= TimerTick;
            Close();
            
            
        }

        private void lblcasefounda_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var tlocation = System.Windows.SystemParameters.WorkArea;
            this.Left = tlocation.Right - this.Width;
            this.Top = tlocation.Bottom - this.Height;
        }
            //CloseWindow();
            
        
        
    }
}
