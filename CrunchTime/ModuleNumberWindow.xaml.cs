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
    /// Interaction logic for ModuleNumberWindow.xaml
    /// </summary>
    public partial class ModuleNumberWindow : Window
    {
        //creating variables
        public static int totalModules; //number of modules user is taking
        public static int semWeeks; //number of weeks in the semester
        public static string startDt; //semester start date

        public ModuleNumberWindow()
        {
            InitializeComponent();

        }

        //button that checks data before it is saved
        private void ToModuleInfo_Click(object sender, RoutedEventArgs e)
        {
            //window object
            ErrorMessage error = new ErrorMessage();

            //checking if a box is empty
            if (numOfModules.Text == "")
            {
                //displaying "required" label
                modNumRequired.Visibility = Visibility.Visible;

            }
            else //box not empty
            {
                //hiding "required" label
                modNumRequired.Visibility = Visibility.Hidden;

            }

            //checking if a box is empty
            if (weeks.Text == "")
            {
                //displaying "required" label
                weekNumRequired.Visibility = Visibility.Visible;

            }
            else //box not empty
            {
                //hiding "required" label
                weekNumRequired.Visibility = Visibility.Hidden;

            }

            //checking if a date was picked
            if (semesterStartDate.SelectedDate != null)
            {
                //hiding "required" label
                startDtRequired.Visibility = Visibility.Hidden;

                //calling method to process details
                ProcessInfo();
                
            }
            else
            {
                //hiding "required" label
                startDtRequired.Visibility = Visibility.Visible;

                //setting specific error message content
                error.specificError.Content = "\t*Please select a start date.";

                //showing error message window
                error.Show();
            }
        }

        //method to process and save data
        public void ProcessInfo()
        {
            //window object
            ErrorMessage error = new ErrorMessage();

            //creating window object
            Module_Information information = new Module_Information();

            //bool to determine if there's an error
            bool noErrors = false;

            //error handling loop
            while (noErrors == false)
            {
                try
                {                 
                    //checking if module number is bigger than zero
                    if (int.Parse(numOfModules.Text) > 0)
                    {
                        //saving textbox content into variables
                        totalModules = int.Parse(numOfModules.Text);
                        semWeeks = int.Parse(weeks.Text);
                        startDt = semesterStartDate.Text;

                        //exiting while loop
                        noErrors = true;

                        //opening next window
                        information.Show();

                        //closing current window
                        Close();
                    }                                                          
                }

                catch //in case there's an error
                {
                    //setting specific error message content
                    error.specificError.Content = "*You need to be taking at least one (1) module." +
                                                  "\n*Only enter numbers. All boxes are required.";

                    //showing error message window
                    error.Show();

                    //exiting loop
                    noErrors = true;
                }
            }
        }

        //button to return to main menu
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            //window object
            Menu mainMenu = new Menu();

            //opening menu window
            mainMenu.Show();

            //closing curent window
            Close();
        }
    }//End of class
}
//---------------------------------------END OF CODE-----------------------------------------------//
