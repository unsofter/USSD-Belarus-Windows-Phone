using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Input;

using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

using USSDManager.AllClasses;

namespace USSDManager
{
    public partial class ChangeNumber : PhoneApplicationPage
    {
        private String spgType;
        private String shablon;
        String newUSSD;
        private nTypes pgType = nTypes.NumberType;
        int restNum = 0;

        public ChangeNumber()
        {
            InitializeComponent();

            Options.phnumber = String.Empty;
            newUSSD = String.Empty;
        }

        private String CreateUSSD(String newText)
        {
            switch (pgType)
            {
                case nTypes.NumberType14:
                    newUSSD = shablon.Replace("%number14%", newText);
                    break;
                case nTypes.NumberType4:
                    newUSSD = shablon.Replace("%number4%", newText);
                    break;
                case nTypes.PhoneType:
                    newUSSD = shablon.Replace("%phone9%", newText);
                    break;
                case nTypes.SummaType:
                    newUSSD = shablon.Replace("%summa%", newText);
                    break;
                default: // nTypes.NumberType
                    newUSSD = shablon.Replace("%number%", newText);
                    break;
            }
            
            return "USSD: " + newUSSD;
        }

        private void numberEdit_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            newUSSD = shablon;
            phoneEdit.Text = CreateUSSD(textBox.Text);
            int rest = restNum - textBox.Text.Length;
            restCount.Text = rest.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Options.phnumber = newUSSD;
            NavigationService.GoBack();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Options.phnumber = String.Empty;
            NavigationService.GoBack();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            shablon = Options.shablon;
            if (shablon == String.Empty)
                shablon = "%ussd%";

            if (NavigationContext.QueryString.TryGetValue("NTYPE", out spgType))
            {
                if (spgType == "PhoneType")
                {
                    pgType = nTypes.PhoneType;
                    restNum = 9;
                }
                else if (spgType == "SummaType")
                {
                    pgType = nTypes.SummaType;
                    restNum = 0;
                }
                else if (spgType == "NumberType4")
                {
                    pgType = nTypes.NumberType4;
                    restNum = 4;
                }
                else if (spgType == "NumberType14")
                {
                    pgType = nTypes.NumberType14;
                    restNum = 14;
                }
                else if (spgType == "All")
                {
                    pgType = nTypes.All;
                    restNum = 0;
                }
                else
                {
                    pgType = nTypes.NumberType;
                    restNum = 0;
                }
                restCount.Text = restNum.ToString();
            }

            // Видимость для ввода номера телефона
            switch (pgType)
            {
                case nTypes.NumberType14:
                case nTypes.NumberType4:
                case nTypes.SummaType:
                case nTypes.NumberType:
                    phoneNumberText.Visibility = System.Windows.Visibility.Collapsed;
                    phoneNumberEdit.Visibility = System.Windows.Visibility.Collapsed;
                    break;
            }

            // Видимость ввода суммы
            switch (pgType)
            {
                case nTypes.PhoneType:
                    numberText.Visibility = System.Windows.Visibility.Collapsed;
                    numberEdit.Visibility = System.Windows.Visibility.Collapsed;
                    break;
            }
            
            // Видимость отсчета оставшегося количества цифр номера
            switch (pgType)
            {
                case nTypes.SummaType:
                case nTypes.NumberType:
                    restText.Visibility = System.Windows.Visibility.Collapsed;
                    restCount.Visibility = System.Windows.Visibility.Collapsed;
                    break;
            }

            if (pgType != nTypes.SummaType)
                numberText.Text = "Номер";

            phoneEdit.Text = CreateUSSD(String.Empty);

            InputScope inputScope = new InputScope();
            InputScopeName inputScopeName = new InputScopeName();
            inputScopeName.NameValue = InputScopeNameValue.NumberFullWidth;
            inputScope.Names.Add(inputScopeName);
            numberEdit.tbMain.InputScope = inputScope;

            numberEdit.Focus();
        }
    }
}