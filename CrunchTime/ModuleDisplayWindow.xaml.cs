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
    /// Interaction logic for ModuleDisplayWindow.xaml
    /// </summary>
    public partial class ModuleDisplayWindow : Window
    {
        public List<ExistingModule> existingModules = new List<ExistingModule>();
        public ModuleDisplayWindow()
        {
            InitializeComponent();

            ModuleDataGrid();
    
        }

        public void ModuleDataGrid()
        {
            DatabaseController ctrl = new DatabaseController();

            ctrl.GetModuleInfo();

            moduleDisplay.ItemsSource = ctrl.ModuleTbl.DefaultView;
 
        }

        //button to exit app
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            //exiting app
            Environment.Exit(0);

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
    } //End of class
}
//---------------------------------------END OF CODE-----------------------------------------------//
