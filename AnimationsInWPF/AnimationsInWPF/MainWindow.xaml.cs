using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AnimationsInWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnStartAnimation_Click(object sender, RoutedEventArgs e)
        {
            Storyboard sb = (Storyboard)this.Resources["rctAnimation"];
            sb.Begin();
        }

        private void btnHoverAnimation_Click(object sender, RoutedEventArgs e)
        {
            Storyboard sb = (Storyboard)this.Resources["HoverAnimation"];
            sb.Begin();
        }

        private void imgRobot_MouseEnter(object sender, MouseEventArgs e)
        {
            Storyboard sb = (Storyboard)this.Resources["RobotHover"];
            sb.Begin();
        }

        private void imgRobot_MouseLeave(object sender, MouseEventArgs e)
        {
            Storyboard sb = (Storyboard)this.Resources["RobotHover"];
            sb.Stop();
        }
    }
}