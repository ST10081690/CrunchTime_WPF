using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrunchTime
{
    public class Module
    {
        //creating variables
        private static string mCode; //module code
        private static string mName; //module name
        private static double mCredits; //module credits
        private static double mHours; //module class hours per week
        private static double mSelfStudy; //module self study hours
        private static string dayToStudy; //day recorded or scheduled to study 
        private static double mScheduledHrs; //number of study hours recorded or scheduled
        private static double mRemainingHrs; //remaining study hours after recorded hours
        private static int moduleUser; //user linked to module being saved
        private static string selectedModule; //module selected in comboBox
        private static int moduleId; //module id in database
        private static double savedStudyHours; //self study hours saved in database for specific module


       //get and set methods for all variables created above
        public static string MCode { get => mCode; set => mCode = value; }
        public static string MName { get => mName; set => mName = value; }
        public static double MCredits { get => mCredits; set => mCredits = value; }
        public static double MHours { get => mHours; set => mHours = value; }
        public static double MSelfStudy { get => mSelfStudy; set => mSelfStudy = value; }
        public static string DayToStudy { get => dayToStudy; set => dayToStudy = value; }
        public static double MScheduledHrs { get => mScheduledHrs; set => mScheduledHrs = value; }
        public static double MRemainingHrs { get => mRemainingHrs; set => mRemainingHrs = value; }

        public static int ModuleUser { get => moduleUser; set => moduleUser = value; }
        public static string SelectedModule { get => selectedModule; set => selectedModule = value; }
        public static int ModuleId { get => moduleId; set => moduleId = value; }
        public static double SavedStudyHours { get => savedStudyHours; set => savedStudyHours = value; }
    }
    //End of class
}
//------------------------------------------------END OF CODE----------------------------------------------------------//
