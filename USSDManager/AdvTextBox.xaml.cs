using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace USSDManager
{
    public partial class AdvTextBox : UserControl
    {
        private string placeholder = "";

        public event TextChangedEventHandler TextChanged;

        public string Placeholder
        {
            get
            {
                return this.placeholder;
            }
            set
            {
                this.placeholder = value;
                tbMain.Text = this.placeholder;
            }
        }

        public AdvTextBox()
        {
            InitializeComponent();
        }

        private void tbMain_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbMain.Text.Trim() == this.placeholder)
            {
                tbMain.Text = "";
            }
            bClear.Visibility = System.Windows.Visibility.Visible;
        }

        private void tbMain_LostFocus(object sender, RoutedEventArgs e)
        {
            if (tbMain.Text.Trim() == "")
            {
                tbMain.Text = this.placeholder;
            }
            bClear.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void bClear_Click(object sender, RoutedEventArgs e)
        {
            tbMain.Text = string.Empty;
            tbMain.Focus();
        }

        public string Text
        {
            get { return tbMain.Text; }
            set { tbMain.Text = value; }
        }

        public TextBox tbMainTextBox { get { return tbMain; } }

        private void tbMain_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextChanged != null)
                TextChanged(sender, e);
        }
    }
}
