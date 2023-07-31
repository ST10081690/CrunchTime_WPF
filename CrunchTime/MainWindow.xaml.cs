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

namespace CrunchTime
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

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            //window object
            LoginWindow login = new LoginWindow();

            //closing curent window
            Close();

            //opening next window
            login.Show();

        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            //window object
            RegisterWindow register = new RegisterWindow();

            //closing curent window
            Close();

            //opening next window
            register.Show();

        }
    }//End of class
}
//---------------------------------------END OF CODE-----------------------------------------------//
