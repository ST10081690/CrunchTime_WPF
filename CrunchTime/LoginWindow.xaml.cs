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
using System.Data.SqlClient;
using System.Security;
using System.Security.Cryptography;
using System.Threading;

namespace CrunchTime
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    { 
        //string to hold textbox input
        public static string logged;

        //string to hold password input during hashing
        string tempPassword;

        //byte arrays to be used when hashing
        byte[] source;
        byte[] hashPass;
        byte[] hashBit;

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            //object references
            DatabaseController ctrl = new DatabaseController();
            Menu menu = new Menu();
            ErrorMessage error = new ErrorMessage();

            //bool to determine if there are errors
            bool noErrors = false;

            //loop to run until no errors found
            while(noErrors == false)
            {
                try
                {
                    //storing textbox input
                    logged = logUsernameText.Text;

                    //saving username input
                    User.LoginName = logged;

                    //calling method to look for user credentials in database
                    ctrl.FindUser();

                    //saving password input before validation(temporarily)
                    tempPassword = logPasswordText.Text;

                    //byte array from input
                    source = ASCIIEncoding.ASCII.GetBytes(tempPassword);

                    //computing hash for the input
                    hashPass = new MD5CryptoServiceProvider().ComputeHash(source);

                    //saving hashed password input
                    User.LoginPassword = TurnToString(hashPass);

                    //starting thread
                    ctrl.SearchForUser();

                    if (ctrl.found == true)
                    {
                        //opening menun window
                        menu.Show();

                        //closing current window
                        Close();

                        //exiting loop
                        noErrors = true;
                    }

                    else //error handling
                    {
                        //setting specific error message content
                        error.specificError.Content = "\t*Both boxes are required. " +
                                             "\n *Please make sure the details are correct.";

                        //showing error message window
                        error.Show();

                        //exiting loop
                        noErrors = true;
                    }
                }

                catch //error handling
                {
                    //setting specific error message content
                    error.specificError.Content = "\t*Something went wrong. " +
                                         "\n *Please make sure the details are correct.";

                    //showing error message window
                    error.Show();

                    //exiting loop
                    noErrors = true;
                }

            }
  
        }
        //End of method

        public static string TurnToString(byte[] hashBit)
        {
            //creating string builder
            StringBuilder text = new StringBuilder(hashBit.Length);

            //loop to append string builder
            for (int i = 0; i < hashBit.Length; i++)
            {
                text.Append(hashBit[i].ToString("X2"));

            }

            //returning hashed password
            return text.ToString();
        }

        private void BackToStart_Click_1(object sender, RoutedEventArgs e)
        {
            //window object
            MainWindow main = new MainWindow();

            //closing curent window
            Close();

            //opening next window
            main.Show();

        }
    }
    //End of class
}
//-------------------------------------------------END OF CODE----------------------------------------------------//
