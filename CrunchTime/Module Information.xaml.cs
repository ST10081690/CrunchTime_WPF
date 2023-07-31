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
using StudyTime;

namespace CrunchTime
{
    /// <summary>
    /// Interaction logic for Module_Information.xaml
    /// </summary>
    public partial class Module_Information : Window
    {
        //creating variables for calculations
        public double tempCredits; //holds module credits value for calculations before saved to list
        public double tempHours; //holds module hours value for calculations before saved to list
        public double selfStudy; //module self study hours result

        public Module_Information()
        {
            InitializeComponent();        
        }

        //button that saves details and closes window
        private void Next_Click(object sender, RoutedEventArgs e)
        {
            //decreaing number of modules left 
            ModuleNumberWindow.totalModules--;

            //checking if a box is empty
            if (moduleCode.Text == "")
            {
                //displaying "required" label
                codeRequired.Visibility = Visibility.Visible;

            }
            else //box not empty
            {
                //hiding "required" label
                codeRequired.Visibility = Visibility.Hidden;

            }

            //checking if a box is empty
            if (moduleName.Text == "")
            {
                //displaying "required" label
                nameRequired.Visibility = Visibility.Visible;

            }
            else //box not empty
            {
                //hiding "required" label
                nameRequired.Visibility = Visibility.Hidden;
            }

            //checking if a box is empty
            if (creditNum.Text == "")
            {
                //displaying "required" label
                creditRequired.Visibility = Visibility.Visible;

            }
            else //box not empty
            {
                //hiding "required" label
                creditRequired.Visibility = Visibility.Hidden;
            }

            //checking if a box is empty
            if (classHrs.Text == "")
            {
                //displaying "required" label
                classHrRequired.Visibility = Visibility.Visible;

            }
            else //box not empty
            {
                //hiding "required" label
                classHrRequired.Visibility = Visibility.Hidden;
            }

            //calling method to process data
            ProcessInfo();
    
        }

        //method that saves data to module list
        public void ProcessInfo()
        {
            //class library object reference
            StudyHour hours = new StudyHour();

            //bool to determine if there are errors
            bool noErrors = false;

            //creating window object
            Module_Information information = new Module_Information();

            DatabaseController ctrl = new DatabaseController();

            //window object
            ErrorMessage error = new ErrorMessage();

            while (noErrors == false)
            {
                try
                {
                    //saving values for calculations
                    tempCredits = int.Parse(creditNum.Text);
                    tempHours = int.Parse(classHrs.Text);

                    //calling class library method to calculate study hours
                    selfStudy = hours.SelfStudyHours(tempCredits, ModuleNumberWindow.semWeeks, tempHours);

                    //saving module details to list               
                    Module.MCode = moduleCode.Text;
                    Module.MName = moduleName.Text;
                    Module.MCredits = int.Parse(creditNum.Text);
                    Module.MHours = int.Parse(classHrs.Text);
                    Module.MSelfStudy = selfStudy;

                    ctrl.SaveModule();

                    //checking if there are no modules left
                    if (ModuleNumberWindow.totalModules == 0)
                    {
                        //closing current window
                        Close();

                        //creating window object
                        ModuleDisplayWindow displayWindow = new ModuleDisplayWindow();

                        //opening next window
                        displayWindow.Show();
                    }

                    //in case there are more modules
                    else if (ModuleNumberWindow.totalModules > 0)
                    {
                        //closing current window
                        Close();

                        //reopening window for new module
                        information.Show();

                    }

                    //exiting loop
                    noErrors = true;
                }

                catch //in case there's an error
                {
                    //adding module back into number of modules left 
                    ModuleNumberWindow.totalModules++;

                    //setting specific error message content
                    error.specificError.Content = "\t*All boxes are required. " +
                                     "\n*Only enter numbers for credits and class hours.";

                    //showing error message window
                    error.Show();

                    //exiting loop
                    noErrors = true;
                }
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            //window object
            Menu mainMenu = new Menu();

            //opening menu window
            mainMenu.Show();

            //closing curent window
            Close();
        }
    } //End of Class
}
//---------------------------------------END OF CODE-----------------------------------------------//
