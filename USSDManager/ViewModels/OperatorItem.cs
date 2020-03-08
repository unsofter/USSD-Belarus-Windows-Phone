using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace USSDManager.ViewModels
{
    public class OperatorItem : INotifyPropertyChanged
    {
        private string _USSD;

        public string USSD
        {
            get
            {
                return _USSD;
            }
            set
            {
                if (value != _USSD)
                {
                    _USSD = value;
                    NotifyPropertyChanged("USSD");
                }
            }
        }

        private string _INFO;

        public string INFO
        {
            get
            {
                return _INFO;
            }
            set
            {
                if (value != _INFO)
                {
                    _INFO = value;
                    NotifyPropertyChanged("INFO");
                }
            }
        }

        private string _TYPE;

        public string TYPE
        {
            get
            {
                return _TYPE;
            }
            set
            {
                if (value != _TYPE)
                {
                    _TYPE = value;
//                    NotifyPropertyChanged("TYPE");
                }
            }
        }

        private string _SHBLN;

        public string SHBLN
        {
            get
            {
                return _SHBLN;
            }
            set
            {
                if (value != _SHBLN)
                {
                    _SHBLN = value;
//                    NotifyPropertyChanged("TYPE");
                }
            }
        }

        private Int64 _COUNT;

        public Int64 COUNT
        {
            get
            {
                return _COUNT;
            }
            set
            {
                if (value != _COUNT)
                {
                    _COUNT = value;
                    //                    NotifyPropertyChanged("TYPE");
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}