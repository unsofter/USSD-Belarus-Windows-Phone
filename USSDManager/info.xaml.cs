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

namespace USSDManager
{
    public partial class info : PhoneApplicationPage
    {
        public info()
        {
            InitializeComponent();
            TiltEffect.SetIsTiltEnabled(this, true);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MarketplaceReviewTask marketplaceReviewTask = new MarketplaceReviewTask();
            marketplaceReviewTask.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MarketplaceSearchTask task = new MarketplaceSearchTask();
            task.SearchTerms = "unsofter";
            task.Show(); 
        }

        private void StartWeb(String url)
        {
            WebBrowserTask webBrowserTask = new WebBrowserTask();
            webBrowserTask.Uri = new Uri(url, UriKind.Absolute);
            webBrowserTask.Show();
        }

        private void HyperlinkButton_Click_1(object sender, RoutedEventArgs e)
        {
            StartWeb("http://velcom.by/ru/private/services/ussd.htm");
        }

        private void HyperlinkButton_Click_2(object sender, RoutedEventArgs e)
        {
            StartWeb("http://mts.by/help/selfservices/ussd/");
        }

        private void HyperlinkButton_Click_3(object sender, RoutedEventArgs e)
        {
            StartWeb("http://life.com.by/private/about/ussd");
        }

        private void HyperlinkButton_Click_4(object sender, RoutedEventArgs e)
        {
            StartWeb("http://7788.by/ussd/");
        }

        private void HyperlinkButton_Click_5(object sender, RoutedEventArgs e)
        {
            StartWeb("http://belinvestbank.by/private-clients/plastic/remote_banking/ussd_banking_for_mts.php");
        }

        private void HyperlinkButton_Click_6(object sender, RoutedEventArgs e)
        {
            StartWeb("http://vtb-bank.by/personal/cards/Services/ussd/");
        }

        private void HyperlinkButton_Click_7(object sender, RoutedEventArgs e)
        {
            StartWeb("http://belgazprombank.by/personal_banking/plastikovie_karti/dopolnitel_nij_servis/sms_servis_informacija_o_dostupnoj_summe/");
        }

        private void HyperlinkButton_Click_8(object sender, RoutedEventArgs e)
        {
            StartWeb("http://priorbank.by/r/retail/express/");
        }

        private void HyperlinkButton_Click_9(object sender, RoutedEventArgs e)
        {
            StartWeb("http://priorbank.by/r/retail/ussdbank/");
        }

        private void HyperlinkButton_Click_10(object sender, RoutedEventArgs e)
        {
            StartWeb("http://bps-sberbank.by/bank/ru.personal.plasticcards.ussdbank.html");
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            StartWeb("http://www.belapb.by/rus/about/press-sluzhba/press-centre/banks_news/belagroprombank-predlagaet-svoim-klientam-vospolzovatsya_4613/");
        }
    }
}