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
using System.Data;
using StudyTime;


namespace CrunchTime
{
    /// <summary>
    /// Interaction logic for ScheduleStudyHours.xaml
    /// </summary>
    public partial class ScheduleStudyHours : Window
    {
        //window object reference
        DatabaseController ctrl = new DatabaseController();


        public ScheduleStudyHours()
        {
            InitializeComponent();

            //calling method
            ShowModules();

        }

        //method that populates comboBox with saved module names
        public void ShowModules()
        {
            ctrl.ModuleForScheduling();

            //populating module selection (combo box)
            foreach (DataRow row in ctrl.ModuleNameTbl.Rows)
            {
                recordedModules.Items.Add(row["m_name"].ToString());

            }

        }

        //method that processes and saves data
        public void ProcessInfo()
        {
            //class library object reference
            StudyHour hours = new StudyHour();

            //window object reference
            Module_Information info = new Module_Information();

            //window object
            ErrorMessage error = new ErrorMessage();

            //bool to determine if there's an error
            bool noErrors = false;

            while (noErrors == false)
            {
                try
                {
                    //saving details to variable
                    Module.MScheduledHrs = double.Parse(dedicatedHrs.Text);

                    //getting the selected combobox item
                    Module.SelectedModule = recordedModules.Text;

                    ctrl.FindModuleId();

                    ctrl.FetchStudyHours();

                    //saving values
                    Module.DayToStudy = dayOfStudy.Text;

                    //calling class library method to calculate study hours
                    Module.MRemainingHrs = hours.RemainingStudyHours(Module.SavedStudyHours, Module.MScheduledHrs);

                    ctrl.SaveSchedule();
                    
                    //exiting loop
                    noErrors = true;
                }

                catch //in case there's an error
                {
                    error.specificError.Content = "\t*Please select a module" +
                                                  "\n*Please enter a number of study hours." +
                                                  "\n\t*All boxes are required.";

                    //showing error message window
                    error.Show();

                    //exiting loop
                    noErrors = true;
                }
            }
        }

        //button to store data and show next window
        private void ShowSchedule_Click(object sender, RoutedEventArgs e)
        {
            //checking if module was selected
            if (recordedModules.Text == "")
            {
                //displaying "required" label
                modRequired.Visibility = Visibility.Visible;

            }
            else //box not empty
            {
                //hiding "required" label
                modRequired.Visibility = Visibility.Hidden;

            }

            //checking if a box is empty
            if (dedicatedHrs.Text == "")
            {
                //displaying "required" label
                studyHrRequired.Visibility = Visibility.Visible;

            }
            else //box not empty
            {
                //hiding "required" label
                studyHrRequired.Visibility = Visibility.Hidden;

            }

            //checking if date was selected
            if (dayOfStudy.SelectedDate == null)
            {
                //displaying "required" label
                studyDtRequired.Visibility = Visibility.Visible;

            }
            else //box not empty
            {
                //hiding "required" label
                studyDtRequired.Visibility = Visibility.Hidden;

            }

            //window object
            ErrorMessage error = new ErrorMessage();

            //checking for empty boxes
            if (dedicatedHrs.Text != "" && recordedModules.Text != ""
                && dayOfStudy.SelectedDate != null)
            {
                //calling method
                ProcessInfo();

                //closing window
                Close();

                //creating window object
                ViewSchedule schedule = new ViewSchedule();

                //opening next window
                schedule.Show();
            }
            else
            {
                //finding specific error
                if (dedicatedHrs.Text == "" || recordedModules.Text == "")
                {
                    //setting specific error message content
                    error.specificError.Content = "\t*Please select module." +
                        "\n*Please enter a number for study hours. \n\t All boxes are required.";
                }

                //finding specific error
                if (dayOfStudy.SelectedDate == null)
                {
                    //setting specific error message content
                    error.specificError.Content = "\t*Please select a sudy date.";
                }
                
                //showing error message window
                error.Show();
            }


        }

        //button to store information and clear boxes for next module
        private void Next_Click(object sender, RoutedEventArgs e)
        {
            //checking if module was selected
            if (recordedModules.Text == "")
            {
                //displaying "required" label
                modRequired.Visibility = Visibility.Visible;

            }
            else //box not empty
            {
                //hiding "required" label
                modRequired.Visibility = Visibility.Hidden;

            }

            //checking if a box is empty
            if (dedicatedHrs.Text == "")
            {
                //displaying "required" label
                studyHrRequired.Visibility = Visibility.Visible;

            }
            else //box not empty
            {
                //hiding "required" label
                studyHrRequired.Visibility = Visibility.Hidden;

            }

            //checking if date was selected
            if (dayOfStudy.SelectedDate == null)
            {
                //displaying "required" label
                studyDtRequired.Visibility = Visibility.Visible;

            }
            else //box not empty
            {
                //hiding "required" label
                studyDtRequired.Visibility = Visibility.Hidden;

            }

            //window object
            ErrorMessage error = new ErrorMessage();

            //checking for empty boxes
            if (dedicatedHrs.Text != "" && recordedModules.Text != ""
                && dayOfStudy.SelectedDate != null)
            {

                //calling method
                ProcessInfo();

                //clearing text box
                dedicatedHrs.Text = "";
                //clearing datepicker
                dayOfStudy.SelectedDate = null;
                //clearing combo box selection
                recordedModules.SelectedItem = null;

            }
            else
            {
                //showing error message window
                error.Show();

                //finding specific error
                if (dedicatedHrs.Text == "" || recordedModules.Text == "")
                {
                    //setting specific error message content
                    error.specificError.Content = "\t*Please select module." +
                        "\nPlease enter a number for study hours. \n\t All boxes are required.";
                }

                //finding specific error
                if (dayOfStudy.SelectedDate == null)
                {
                    //setting specific error message content
                    error.specificError.Content = "\t*Please select a study date.";
                }
                
            }
        }
    }//End of class
}
//---------------------------------------END OF CODE-----------------------------------------------//
