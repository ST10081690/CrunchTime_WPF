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

namespace CrunchTime
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        DatabaseController ctrl = new DatabaseController();

        public Menu()
        {
            InitializeComponent();
        }

        //button that leads to ModuleNumberWindow
        private void CalculateHours_Click(object sender, RoutedEventArgs e)
        {
            //window object
            ModuleNumberWindow moduleNumber = new ModuleNumberWindow();

            //opening next window
            moduleNumber.Show();

            //closing curent window
            Close();
        }

        //button that leads to ModuleDisplayWindow
        private void ViewHours_Click(object sender, RoutedEventArgs e)
        {

            //creating window object
            ModuleDisplayWindow displayWindow = new ModuleDisplayWindow();

            //opening window that displays study hours
            displayWindow.Show();

            //closing current window
            Close();

        }

        //button that leads to ScheduleStudyHours
        private void RecordHours_Click(object sender, RoutedEventArgs e)
        {
   
            //window object
            ScheduleStudyHours plan = new ScheduleStudyHours();

            //opening window
            plan.Show();

            //closing current window
            Close();
            
        }

        //button that leads to ViewSchedule
        private void ToViewSchedule_Click(object sender, RoutedEventArgs e)
        {

            //creating window object
            ViewSchedule schedule = new ViewSchedule();

            //opening next window
            schedule.Show();

            //closing window
            Close();

        }

        //button to exit app
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            //exiting app
            Environment.Exit(0);
        }

    }//End of class
}
//---------------------------------------END OF CODE-----------------------------------------------//
