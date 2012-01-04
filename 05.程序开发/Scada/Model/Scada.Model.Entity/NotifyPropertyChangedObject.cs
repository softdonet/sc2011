using System;
using System.ComponentModel;

namespace Scada.Model.Entity
{

    public class NotifyPropertyChangedObject : INotifyPropertyChanged
    {

        public NotifyPropertyChangedObject()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }

}
