using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;

using System.IO;
using System.IO.IsolatedStorage;

using System.Windows.Media;
using System.Windows.Media.Animation;

using USSDManager.AllClasses;
using USSDManager.ViewModels;

namespace USSDManager
{
    public partial class MainPage : PhoneApplicationPage
    {
        string shablnUSSD;
        string shablnType;
        string tmpPhone;

        // Конструктор
        public MainPage()
        {
            InitializeComponent();
            TiltEffect.SetIsTiltEnabled(this, true);

            // Задайте для контекста данных элемента управления listbox пример данных
            operatorList.DataContext = App.OperatorModel;
            tmpPhone = String.Empty;
        }

        private string  GetOperator()
        {
            string RetValue;
            RetValue = Options.Operator;
            if (RetValue == String.Empty)
            {
                Options.Operator = RetValue = Microsoft.Phone.Net.NetworkInformation.DeviceNetworkInformation.CellularMobileOperator;
                string Operator = Options.Operator.ToUpper();
            }
            else if (RetValue.ToUpper().IndexOf("PRIVET") >= 0)
                RetValue = "PRIVET";
            else if (RetValue.ToUpper().IndexOf("KORP") >= 0)
                RetValue = "KORP";
            else if (RetValue.ToUpper().IndexOf("JORN") >= 0)
                RetValue = "JORN";
            else if (RetValue.ToUpper().IndexOf("OTHER") >= 0)
                RetValue = "OTHER";

            return RetValue;
        }

        // Загрузка данных для элементов ViewModel
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            GetOperator();
            ReloadData();
            //App.OperatorModel.LoadData();
        }

        // Срабатывает
        // После выбора телефонного номера
        private void MainLoaded(object sender, RoutedEventArgs e)
        {
            string mHeader = GetOperator();

            if (mHeader.IndexOf("PRIVET") >= 0)
                operatorPivot.Header = "Velcom Привет";
            else if (mHeader.IndexOf("KORP") >= 0)
                operatorPivot.Header = "Velcom корпорация";
            else if (mHeader.IndexOf("JORN") >= 0)
                operatorPivot.Header = "ЖУРНАЛ";
            else if (mHeader.IndexOf("OTHER") >= 0)
                operatorPivot.Header = "Прочие";
            else
                operatorPivot.Header = mHeader;
            AdvTextBox_TextChanged(null, null);

            // Обработать номер телефона
            if (tmpPhone != String.Empty)
            {
                int phLen = tmpPhone.Length;
                tmpPhone = tmpPhone.Remove(0, phLen - 9);
                String shblnUSSD = shablnUSSD.Replace("%phone9%", tmpPhone);

                if (shablnType == "33")
                {
                    shablnUSSD = shblnUSSD;
                    StartUSSDEdit();
                }
                else
                    ShowAddToClipboard(shblnUSSD);
                tmpPhone = String.Empty;
            }

            if (Options.phnumber != String.Empty)
            {
                ShowAddToClipboard(Options.phnumber);
                Options.phnumber = String.Empty;
            }
        }

        private void AdvTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            App.OperatorModel.GeneRateList(finder.Text);
        }

        private void abButton1_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/info.xaml", UriKind.Relative));
        }

        private void abButton2_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Nastr.xaml", UriKind.Relative));
        }

        private void ReloadData()
        {
            App.OperatorModel.LoadData();
            MainLoaded(null, null);
            mainPivot.DefaultItem = mainPivot.Items[0];
        }

        private void vel_Click(object sender, RoutedEventArgs e)
        {
            Options.Operator = "VELCOM";
            ReloadData();
        }

        private void mts_Click(object sender, RoutedEventArgs e)
        {
            Options.Operator = "MTS";
            ReloadData();
        }

        private void life_Click(object sender, RoutedEventArgs e)
        {
            Options.Operator = "life:)";
            ReloadData();
        }

        private void velp_Click(object sender, RoutedEventArgs e)
        {
            Options.Operator = "PRIVET";
            ReloadData();
        }

        private void velk_Click(object sender, RoutedEventArgs e)
        {
            Options.Operator = "KORP";
            ReloadData();
        }

        private void others_Click(object sender, RoutedEventArgs e)
        {
            Options.Operator = "OTHER";
            ReloadData();
        }

        private void ShowAddToClipboard(string USSD)
        {
            App.OperatorModel.AddData(USSD);

            Clipboard.SetText(USSD);
            ClipboardAnimation.Begin();
        }

        void phn_Complete(object sender, PhoneNumberResult e)
        {
            tmpPhone = String.Empty;
            if (e.TaskResult == TaskResult.OK)
                tmpPhone = e.PhoneNumber;
        //    else
        //        StartUSSDEdit();
        }

        private void StartUSSDEdit()
        {
            Options.shablon = shablnUSSD;

            if (shablnUSSD.IndexOf("%summa%") >= 0)
                NavigationService.Navigate(new Uri("/ChangeNumber.xaml?NTYPE=" + nTypes.SummaType, UriKind.Relative));
            else if (shablnUSSD.IndexOf("%number14%") >= 0)
                NavigationService.Navigate(new Uri("/ChangeNumber.xaml?NTYPE=" + nTypes.NumberType14, UriKind.Relative));
            else if (shablnUSSD.IndexOf("%phone9%") >= 0)
                NavigationService.Navigate(new Uri("/ChangeNumber.xaml?NTYPE=" + nTypes.PhoneType, UriKind.Relative));
            else if (shablnUSSD.IndexOf("%number4%") >= 0)
                NavigationService.Navigate(new Uri("/ChangeNumber.xaml?NTYPE=" + nTypes.NumberType4, UriKind.Relative));
            else //%number%
                NavigationService.Navigate(new Uri("/ChangeNumber.xaml?NTYPE=" + nTypes.NumberType, UriKind.Relative));
        }

        private void StartPhoneTask()
        {
            PhoneNumberChooserTask phn = new PhoneNumberChooserTask();
            phn.Completed += new EventHandler<PhoneNumberResult>(phn_Complete);
            phn.Show();
        }

        private void operatorList_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (operatorList.SelectedItem != null)
            {
                clpb.Visibility = System.Windows.Visibility.Visible;
                OperatorItem sItem = operatorList.SelectedItem as OperatorItem;

                shablnUSSD = sItem.SHBLN;
                shablnType = sItem.TYPE;
                // Выбрать номер телефона
                if (shablnType == "31") 
                {
                    if (Options.phonenumberhelp == true)
                    {
                        StartPhoneTask();
                        return;
                    }
                    ShowAddToClipboard(sItem.USSD);
                }
                // Ввести число 
                else if (shablnType == "32")
                {
                    if (Options.phonenumberhelp == true)
                    {
                        StartUSSDEdit();
                        return;
                    }
                    ShowAddToClipboard(sItem.USSD);
                }
                // Ввести число и номер телефона
                else if (shablnType == "33")
                {
                    if (Options.phonenumberhelp == true)
                    {
                        StartPhoneTask();
                        return;
                    }
                    ShowAddToClipboard(sItem.USSD);
                }
                else
                    ShowAddToClipboard(sItem.USSD);
            }
        }

        private void clpb_Completed(object sender, EventArgs e)
        {
            clpb.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Options.Operator = "JORN";
            ReloadData();
        }

        private void PhoneApplicationPage_Unloaded(object sender, RoutedEventArgs e)
        {
            App.OperatorModel.SaveData();
        }

        private void PhoneApplicationPage_BackKeyPress(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.OperatorModel.SaveData();
        }
    }
}