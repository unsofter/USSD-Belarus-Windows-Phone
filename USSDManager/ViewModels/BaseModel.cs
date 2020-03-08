using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace USSDManager.ViewModels
{
    public class BaseModel : INotifyPropertyChanged
    {
        public ObservableCollection<OperatorItem> Items { get; private set; }

        public BaseModel()
        {
            this.Items = new ObservableCollection<OperatorItem>();
        }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        public virtual void LoadData()
        {
            this.IsDataLoaded = true;
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
