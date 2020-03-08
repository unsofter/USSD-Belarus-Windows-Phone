using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

using USSDManager.AllClasses;

namespace USSDManager
{
    public partial class Nastr : PhoneApplicationPage
    {

        bool phonenumberhelp;
        bool numberhelp;

        public Nastr()
        {
            InitializeComponent();

            phonenumberhelp = Options.phonenumberhelp;
        }

        private void PhoneApplicationPage_Unloaded(object sender, RoutedEventArgs e)
        {
            Options.phonenumberhelp = phonenumberhelp;
        }

        private void ToggleSwitch_Click(object sender, RoutedEventArgs e)
        {
            ToggleSwitch togle = sender as ToggleSwitch;
            phonenumberhelp = (bool)togle.IsChecked;
        }

        private void ToggleSwitch_Click_1(object sender, RoutedEventArgs e)
        {
            ToggleSwitch togle = sender as ToggleSwitch;
            numberhelp = (bool)togle.IsChecked;
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            phHelp.IsChecked = phonenumberhelp;
        }
    }
}