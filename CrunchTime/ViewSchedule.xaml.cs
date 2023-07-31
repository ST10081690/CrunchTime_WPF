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
    /// Interaction logic for ViewSchedule.xaml
    /// </summary>
    public partial class ViewSchedule : Window
    {
        ModuleDisplayWindow moduleDisplay = new ModuleDisplayWindow();

        public ViewSchedule()
        {
            InitializeComponent();

            DisplaySchedule();

            DisplayRemaining();
        }

        public void DisplaySchedule()
        {
            DatabaseController ctrl = new DatabaseController();

            ctrl.FetchScheduledHours();

            scheduledHours.ItemsSource = ctrl.ScheduleHrsTbl.DefaultView;

        }

        public void DisplayRemaining()
        {
            DatabaseController ctrl = new DatabaseController();

            ctrl.FetchRemainingHours();

            remainingHours.ItemsSource = ctrl.RemainingHrsTbl.DefaultView;

        }
        
        //button to return to main menu
        private void BackToMenu_Click(object sender, RoutedEventArgs e)
        {
            //window object
            Menu mainMenu = new Menu();

            //opening menu window
            mainMenu.Show();

            //closing curent window
            Close();
        }

        //button to return to ScheduleStudyHours
        private void BackToSchedule_Click(object sender, RoutedEventArgs e)
        {
            //window object
            ScheduleStudyHours plan = new ScheduleStudyHours();

            //opening window
            plan.Show();

            //closing current window
            Close();
        }

        //button to exit application
        private void Done_Click(object sender, RoutedEventArgs e)
        {
            //exiting app
            Environment.Exit(0);
        }
    }
}
