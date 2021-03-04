
using System;
using System.ComponentModel;
using System.Globalization;
using System.Resources;
using Xamarin.Forms;

namespace ProfileBook.Localization
{
    public class LocalizedResources : INotifyPropertyChanged
    {
        readonly ResourceManager ResourceManager;
        CultureInfo CurrentCultureInfo;

        public string this[string key] {
            get => ResourceManager.GetString(key, CurrentCultureInfo);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnCultureChanged(object s, CultureChangedMessage ccm)
        {
            CurrentCultureInfo = ccm.NewCultureInfo;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Item"));
        }

    }
}
