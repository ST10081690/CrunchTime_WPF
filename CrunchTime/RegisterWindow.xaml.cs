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
using System.Security.Cryptography;
using System.Threading;


namespace CrunchTime
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        //bools to determine that textbox is not empty
        public bool userNmNotEmpty = false;
        public bool nameNotEmpty = false;
        public bool surnameNotEmpty = false;
        public bool fieldNotEmpty = false;
        public bool passNotEmpty = false;

        //string to hold password during hashing
        string tempPassword;

        //byte arrays to be used when hashing
        byte[] source;
        byte[] hashPass;
        byte[] hashBit;

        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            //method to check for empty boxes 
            CheckForEmpty();
        }

        public void CheckForEmpty()
        {
            //checking if a box is empty
            if (regUsernameText.Text == "")
            {
                //displaying "required" label
                usrNmRequired.Visibility = Visibility.Visible;

            }
            else //box not empty
            {
                //hiding "required" label
                usrNmRequired.Visibility = Visibility.Hidden;

                //making bool true
                userNmNotEmpty = true;
            }

            //checking if a box is empty
            if (regNameText.Text == "")
            {
                //displaying "required" label
                nameRequired.Visibility = Visibility.Visible;

            }
            else //box not empty
            {
                //hiding "required" label
                nameRequired.Visibility = Visibility.Hidden;

                //making bool true
                nameNotEmpty = true;
            }

            //checking if a box is empty
            if (regSurnameText.Text == "")
            {
                //displaying "required" label
                surnameRequired.Visibility = Visibility.Visible;

            }
            else //box not empty
            {
                //hiding "required" label
                surnameRequired.Visibility = Visibility.Hidden;

                //making bool true
                surnameNotEmpty = true;
            }

            //checking if a box is empty
            if (regCareerPathText.Text == "")
            {
                //displaying "required" label
                fieldRequired.Visibility = Visibility.Visible;

            }
            else //box not empty
            {
                //hiding "required" label
                fieldRequired.Visibility = Visibility.Hidden;

                //making bool true
                fieldNotEmpty = true;
            }

            //checking if a box is empty
            if (regPasswordText.Text == "")
            {
                //displaying "required" label
                passwordRequired.Visibility = Visibility.Visible;

            }
            else //box not empty
            {
                //hiding "required" label
                passwordRequired.Visibility = Visibility.Hidden;

                //making bool true
                passNotEmpty = true;
            }

            //calling method to processs details
            ProcessInfo();
        }

        public void ProcessInfo()
        {
            //window object references
            Menu menu = new Menu();
            DatabaseController ctrl = new DatabaseController();
            ErrorMessage error = new ErrorMessage();

            //implementing multi threading when registering
            //thread that is in charge of running method to process details
            Thread processRegistration = new Thread(ctrl.RegisterUserToDB);

            //bool to determine if there are errors
            bool noErrors = false;

            //loop to run until no errors are found
            while (noErrors == false)
            {
                try
                {
                    //running if no boxes are empty
                    if (userNmNotEmpty && nameNotEmpty && surnameNotEmpty
                        && fieldNotEmpty && passNotEmpty)
                    {
                        //saving user information
                        User.Username = regUsernameText.Text;

                        User.FirstName = regNameText.Text;

                        User.Surname = regSurnameText.Text;

                        User.CareerPath = regCareerPathText.Text;

                        tempPassword = regPasswordText.Text;

                        //byte array from input
                        source = ASCIIEncoding.ASCII.GetBytes(tempPassword);

                        //computing hash for the input
                        hashPass = new MD5CryptoServiceProvider().ComputeHash(source);

                        //saving hashed password
                        User.Password = TurnToString(hashPass);

                        //thread to run method that saves user info to database
                        processRegistration.Start();

                        //closing current window
                        Close();

                        //opening menu window
                        menu.Show();
                    }

                    else //name is taken
                    {
                        //setting specific error message content
                        error.specificError.Content = "\t*All boxes are required. " +
                                             "\n*Please make sure you do not leave empty boxes." +
                                             "\n    **No taken usernames are accepted**";

                        //showing error message window
                        error.Show();
                    }

                    //exiting loop
                    noErrors = true;

                }
                catch
                {
                    //setting specific error message content
                    error.specificError.Content = "\t*All boxes are required. " +
                                         "\n*Please make sure you do not leave empty boxes." +
                                         "\n    **No taken usernames are accepted**";

                    //showing error message window
                    error.Show();

                    //exiting loop
                    noErrors = true;

                }

            }
        }


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


        private void BackToStart_Click(object sender, RoutedEventArgs e)
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
