using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Security;
using System.Configuration;

namespace CrunchTime
{
    public class DatabaseController
    {
        //bool to determine if user was found upon login
        public bool found = false;

        //strings to hold values saved in the database for later usage in the program
        public static string savedModule;
        public static int savedSelfStudy;

        //data tables to hold command results for info retrived from database
        DataTable moduleTbl = new DataTable(); 

        DataTable moduleNameTbl = new DataTable();

        DataTable scheduleHrsTbl = new DataTable();

        DataTable remainingHrsTbl = new DataTable();

        //Sql connection
        //string connectLink = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\juste\source\repos\CrunchTime\CrunchTime\bin\Debug\CrunchTimeDB.mdf;Integrated Security=True";
        
        //Connection to database
        SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\juste\source\repos\CrunchTime\CrunchTime\bin\Debug\CrunchTimeDB.mdf;Integrated Security=True");

        

        //get and set methods for data tables created above
        public DataTable ModuleTbl { get => moduleTbl; set => moduleTbl = value; }
        public DataTable ModuleNameTbl { get => moduleNameTbl; set => moduleNameTbl = value; }
        public DataTable ScheduleHrsTbl { get => scheduleHrsTbl; set => scheduleHrsTbl = value; }
        public DataTable RemainingHrsTbl { get => remainingHrsTbl; set => remainingHrsTbl = value; }

        //constructor
        public DatabaseController()
        {
            //making connection to database be recognised by any pc
            //string CurrentDirectory = Environment.CurrentDirectory;

             //string replaceDirect = CurrentDirectory.Replace(@"bin\Debug", "CrunchTimeDB.mdf");

            string connectLink = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\juste\source\repos\CrunchTime\CrunchTime\bin\Debug\CrunchTimeDB.mdf;Integrated Security=True";
            //Connection to database
            SqlConnection connection = new SqlConnection(connectLink);

        }

        public void RegisterUserToDB()
        {          
            //creating sql command
            SqlCommand command = new SqlCommand();

            //specifying command type
            command.CommandType = CommandType.Text;

            //query statement to insert values to database
            command.CommandText = "insert into [Users] (username, u_FirstName, u_Surname, u_Career, u_Password) values ('" + User.Username + "','" +  User.FirstName + "','" + User.Surname + "','" + User.CareerPath +"','" + User.Password + "')";

            //specyfying command connection to database
            command.Connection = connection;

            //opening connection to database
            connection.Open();

            //executing query statement
            command.ExecuteNonQuery();

            //closing connection to database
            connection.Close();
        }

        public void SearchForUser()
        {
            //opening connection to database
            connection.Open();    

            //searching through database for username
            SqlCommand search = new SqlCommand("select * from [Users] where [username] = @name and [u_Password] = @pass", connection);

            //adding parameters to command
            search.Parameters.AddWithValue("@name", User.LoginName);
            search.Parameters.AddWithValue("@pass", User.LoginPassword);

            //executing reader
            SqlDataReader read = search.ExecuteReader();

            //checking if reader found a result
            if (read.Read() == true)
            {
                //making bool true
                found = true;

            }

            //closing reader
            read.Close();

            //closing connection to database
            connection.Close();
        }

        public void FindUser()
        {
            //local variable holding username of logged in user
            string thisUser = LoginWindow.logged;

            //searching through database with select query
            SqlCommand findId = new SqlCommand("select [u_Id] from [Users] where [username] = @loggedIn", connection);
            
            //opening connection to database
            connection.Open();

            //adding parameters to command
            findId.Parameters.AddWithValue("@loggedIn", thisUser);

            //executing reader
            SqlDataReader read = findId.ExecuteReader();

            //running if reader found a result
            if(read.Read()==true)
            {
                //saving data the reader found
                Module.ModuleUser = (int)read[0];

            }

            //closing reader
            read.Close();

            //closing connection to database
            connection.Close();
        }

        public void SaveModule()
        {
            //creating sql command
            SqlCommand command = new SqlCommand();

            //specifying command type
            command.CommandType = CommandType.Text;

            //query statement to insert values to database
            command.CommandText = "insert into [Module] (m_code, m_name, m_credits, m_hours, m_selfStudy, u_Id) values ('" + Module.MCode + "','" + Module.MName + "','" + Module.MCredits + "','" + Module.MHours + "','" + Module.MSelfStudy + "','" + Module.ModuleUser + "')";

            //specyfying command connection to database
            command.Connection = connection;

            //opening connection to database
            connection.Open();

            //executing query statement
            command.ExecuteNonQuery();

            //closing connection to database
            connection.Close();

        }

        public void GetModuleInfo()
        {
            //opening connection to database
            connection.Open();

            //select statement to find data
            SqlCommand moduleCmd = new SqlCommand("select m_name as 'Module Name',m_selfStudy as 'Self Study Hours' from [Module] where [u_Id] = @loggedIn", connection);

            //adding parameters to command
            moduleCmd.Parameters.AddWithValue("@loggedIn", Module.ModuleUser);

            //creating data adapter
            SqlDataAdapter data = new SqlDataAdapter(moduleCmd);

            //filling data table with the info that was retrieved
            data.Fill(moduleTbl);

            //closing connection to database
            connection.Close();

        }

        public void ModuleForScheduling()
        {
            //opening connection to database
            connection.Open();

            //query to select module name
            SqlCommand findName = new SqlCommand("select m_name from [Module] where [u_Id] = @loggedIn", connection);

            //adding parameters to command
            findName.Parameters.AddWithValue("@loggedIn", Module.ModuleUser);

            //creating data adapter
            SqlDataAdapter data = new SqlDataAdapter(findName);

            //filling data table with retieved information
            data.Fill(moduleNameTbl);

            //closing connection to database
            connection.Close();

        }

        public void FindModuleId()
        {
            //query to find module ID
            SqlCommand findId = new SqlCommand("select [m_id] from [Module] where [u_Id] = @loggedIn and [m_name] = @selectedMod", connection);

            //opening connection to database
            connection.Open();

            //adding parameters to command
            findId.Parameters.AddWithValue("@loggedIn", Module.ModuleUser);
            findId.Parameters.AddWithValue("@selectedMod", Module.SelectedModule);

            //executing reader
            SqlDataReader read = findId.ExecuteReader();

            //running if reader found result
            if (read.Read() == true)
            {
                //saving data retrieved
                Module.ModuleId = (int)read[0];

            }

            //closing reader
            read.Close();

            //closing connection to database
            connection.Close();
        }

        public void SaveSchedule()
        {
            //creating sql command
            SqlCommand command = new SqlCommand();

            //specifying command type
            command.CommandType = CommandType.Text;

            //query statement to update row values
            command.CommandText = "update [Module] set m_studyDay = @day, m_scheduledHours = @scheduled, m_remainingHours = @remaining where [m_id] = @modId";

            //specyfying command connection to database
            command.Connection = connection;

            //opening connection to database
            connection.Open();

            //adding parameters to command
            command.Parameters.AddWithValue("@day", Module.DayToStudy);
            command.Parameters.AddWithValue("@scheduled", Module.MScheduledHrs);
            command.Parameters.AddWithValue("@remaining", Module.MRemainingHrs);
            command.Parameters.AddWithValue("@modId", Module.ModuleId);

            //executing query statement
            command.ExecuteNonQuery();

            //closing connection to database
            connection.Close();
        }

        public void FetchStudyHours()
        {
            //query to select self study hours from database
            SqlCommand study = new SqlCommand("select [m_selfStudy] from [Module] where [u_Id] = @loggedIn and [m_name] = @selectedMod", connection);

            //opening connection to database
            connection.Open();

            //adding parameters to command
            study.Parameters.AddWithValue("@loggedIn", Module.ModuleUser);
            study.Parameters.AddWithValue("@selectedMod", Module.SelectedModule);

            //executing reader
            SqlDataReader read = study.ExecuteReader();

            //running if reader found result
            if (read.Read() == true)
            {
                //saving data retrieved
                Module.SavedStudyHours = (int)read[0];

            }

            //closing reader
            read.Close();

            //closing connection to database
            connection.Close();
        }

        public void FetchScheduledHours()
        {
            //opening connection to database
            connection.Open();

            //query to select module name and scheduled study hours
            SqlCommand getHours = new SqlCommand("select m_name as 'Module Name',m_scheduledHours as 'Study Hours' from [Module] where [u_Id] = @loggedIn", connection);

            //adding parameters to command
            getHours.Parameters.AddWithValue("@loggedIn", Module.ModuleUser);

            //creating data adapter
            SqlDataAdapter data = new SqlDataAdapter(getHours);

            //filling data table with retieved info
            data.Fill(scheduleHrsTbl);

            //closing connection to database
            connection.Close();
        }

        public void FetchRemainingHours()
        {
            //opening connection to database
            connection.Open();

            //query to select module name and remaining study hours
            SqlCommand getHours = new SqlCommand("select m_name as 'Module Name',m_remainingHours as 'Study Hours' from [Module] where [u_Id] = @loggedIn", connection);

            //adding parameters to command
            getHours.Parameters.AddWithValue("@loggedIn", Module.ModuleUser);

            //creating data adapter
            SqlDataAdapter data = new SqlDataAdapter(getHours);

            //filling data table with retieved info
            data.Fill(remainingHrsTbl);

            //closing connection to database
            connection.Close();

        }
    }
    //End of class
}
//---------------------------------------------------------------------------END OF CODE----------------------------------------------------------------------------------------------//