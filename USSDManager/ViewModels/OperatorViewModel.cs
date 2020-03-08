using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;

using System.Windows;
using System.Windows.Media;
using System.Threading;

using System.Xml;
using System.Xml.Linq;
using System.Linq;

using System.IO.IsolatedStorage;

using USSDManager.AllClasses;

namespace USSDManager.ViewModels
{
    public class OperatorViewModel : BaseModel
    {
        List<OperatorItem> uList;
        List<OperatorItem> ListJorn;

        public OperatorViewModel()
            : base()
        {
            uList = new List<OperatorItem>();
            ListJorn = new List<OperatorItem>();
        }

        /// <summary>
        /// Создает и добавляет несколько объектов ItemViewModel в коллекцию элементов.
        /// </summary>
        override public void LoadData() 
        {
            string Operator = Options.Operator.ToUpper();
            StreamReader Reader;
            IsolatedStorageFileStream isoStream = null;
            string RSSString;

            if (ListJorn.Count < 1)
            {
                IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForApplication();

                if (isoStore.FileExists("jorn.txt"))
                {
                    isoStream = new IsolatedStorageFileStream("jorn.txt", FileMode.Open, isoStore);
                    Reader = new StreamReader(isoStream);
                }
                else
                    Reader = new StreamReader("USSD/empty.txt");

                RSSString = Reader.ReadToEnd();

                XElement iUSSD = XElement.Parse(RSSString);

                var postList =
                    from oneUSSD in iUSSD.Descendants("item")
                    select new OperatorItem
                    {
                        USSD = oneUSSD.Element("USSD").Value,
                        INFO = oneUSSD.Element("INFO").Value,
                        TYPE = oneUSSD.Element("TYPE").Value,
                        SHBLN = oneUSSD.Element("SHBL").Value,
                        COUNT = Convert.ToInt64(oneUSSD.Element("COUNT").Value)
                    };

                foreach (var oneItem in postList)
                    ListJorn.Add(new OperatorItem() { USSD = oneItem.USSD, INFO = oneItem.INFO, TYPE = oneItem.TYPE, SHBLN = oneItem.SHBLN, COUNT = oneItem.COUNT });
                Reader.Dispose();
                if (isoStream != null)
                    isoStream.Dispose();
            }


            if (Operator.IndexOf("LIFE") >= 0)
                Reader = new StreamReader("USSD/life.txt");
            else if (Operator.IndexOf("MTS") >= 0)
                Reader = new StreamReader("USSD/mts.txt");
            else if (Operator.IndexOf("VELCOM") >= 0)
                Reader = new StreamReader("USSD/velcom.txt");
            else if (Operator.ToUpper().IndexOf("PRIVET") >= 0)
                Reader = new StreamReader("USSD/velcomp.txt");
            else if (Operator.ToUpper().IndexOf("KORP") >= 0)
                Reader = new StreamReader("USSD/velcomk.txt");
            else if (Operator.ToUpper().IndexOf("JORN") >= 0)
            {
                uList.Clear();
                foreach (var oneItem in ListJorn)
                    uList.Add(new OperatorItem() { USSD = oneItem.USSD, INFO = oneItem.INFO, TYPE = oneItem.TYPE, SHBLN = oneItem.SHBLN, COUNT = oneItem.COUNT });
                GeneRateList(String.Empty);
                return;
            }
            else
                Reader = new StreamReader("USSD/other.txt");


            RSSString = Reader.ReadToEnd();

            XElement iUSSD_ = XElement.Parse(RSSString);

            var postList_ =
                from oneUSSD in iUSSD_.Descendants("item")
                select new OperatorItem
                {
                    USSD = oneUSSD.Element("USSD").Value,
                    INFO = oneUSSD.Element("INFO").Value,
                    TYPE = oneUSSD.Element("TYPE").Value,
                    SHBLN = oneUSSD.Element("SHBL").Value,
                    COUNT = Convert.ToInt64(oneUSSD.Element("COUNT").Value)
                };

            uList.Clear();
            foreach (var oneItem in postList_)
                uList.Add(new OperatorItem() { USSD = oneItem.USSD, INFO = oneItem.INFO, TYPE = oneItem.TYPE, SHBLN = oneItem.SHBLN, COUNT = oneItem.COUNT });

            GeneRateList(String.Empty);
            base.LoadData();
            Reader.Dispose();
        }

        public void AddData(String USSD)
        {
            foreach (var oneItem in ListJorn)
                if (oneItem.USSD == USSD)
                {
                    oneItem.COUNT++;
                    return;
                }

            if (Options.Operator.ToUpper().IndexOf("JORN") >= 0)
                this.Items.Add(new OperatorItem() { USSD = USSD, INFO = USSD, TYPE = "2", SHBLN = "%ussd%", COUNT = 1 });

            ListJorn.Add(new OperatorItem() { USSD = USSD, INFO = USSD, TYPE = "2", SHBLN = "%ussd%", COUNT = 1 });
        }

        public void SaveData()
        {
            String SaveString = "<?xml version='1.0' encoding='utf-8' ?><rss version='2.0'><channel><language>ru-ru</language>";

            foreach (var oneItem in ListJorn)
            {
                SaveString += "<item>";
                SaveString += "<USSD>" + oneItem.USSD + "</USSD>";
                SaveString += "<INFO>" + oneItem.INFO + "</INFO>";
                SaveString += "<TYPE>" + oneItem.TYPE + "</TYPE>";
                SaveString += "<SHBL>%ussd%</SHBL>";
                SaveString += "<COUNT>" + oneItem.COUNT.ToString() + "</COUNT>";
                SaveString += "</item>";
            }
            SaveString += "</channel></rss>";


            IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForApplication();
            IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream("jorn.txt", FileMode.Create, isoStore);
            StreamWriter writer = new StreamWriter(isoStream);
            writer.Write(SaveString);
            writer.Dispose();
            isoStream.Dispose();
        }

        public void GeneRateList(String filter)
        {
            this.Items.Clear();
            foreach (var oneItem in uList)
            {
                if ((filter != String.Empty) && (filter != "Поиск"))
                {
                    if ((oneItem.USSD.ToUpper().IndexOf(filter.ToUpper()) >= 0) || (oneItem.INFO.ToUpper().IndexOf(filter.ToUpper()) >= 0))
                        this.Items.Add(new OperatorItem() { USSD = oneItem.USSD, INFO = oneItem.INFO, TYPE = oneItem.TYPE, SHBLN = oneItem.SHBLN, COUNT = oneItem.COUNT });
                }
                else
                    this.Items.Add(new OperatorItem() { USSD = oneItem.USSD, INFO = oneItem.INFO, TYPE = oneItem.TYPE, SHBLN = oneItem.SHBLN, COUNT = oneItem.COUNT });
            }
        }
    }
}