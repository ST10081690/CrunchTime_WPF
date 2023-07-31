using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrunchTime
{
    public class User
    {
        //creating variables
        private static string username; //username created by user
        private static string firstName; //user's first name
        private static string surname; //user's surname
        private static string password; //password created by user
        private static string careerPath; //user's field of studies
        private static string loginName; //usename of user that has logged in
        private static string loginPassword; //password of user that has logged in

        //get and set methods for all variables created above
        public static string Username { get => username; set => username = value; }
        public static string FirstName { get => firstName; set => firstName = value; }
        public static string Surname { get => surname; set => surname = value; }
        public static string Password { get => password; set => password = value; }
        public static string CareerPath { get => careerPath; set => careerPath = value; }
        public static string LoginName { get => loginName; set => loginName = value; }
        public static string LoginPassword { get => loginPassword; set => loginPassword = value; }
    }
    //End of class
}
//------------------------------------------------END OF CODE----------------------------------------------------------//