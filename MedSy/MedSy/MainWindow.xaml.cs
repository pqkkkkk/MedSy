using MedSy.Views;
using MedSy.Views.Admin;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MedSy
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public Page currentPage { get; set; }
        public MainWindow(string roleOfUser)
        {
            this.InitializeComponent();
            
            switch (roleOfUser)
            {
                case "patient":
                    content.Navigate(typeof(UserMainPage));
                    
                    break;
                case "doctor":
                    content.Navigate(typeof(DoctorMainPage));
                    break;

                case "admin":
                    content.Navigate(typeof(AdminDashboard));
                    break;
            }
            
        }

       
    }
}
