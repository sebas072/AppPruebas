using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;

using appPrueba.Models;
using appPrueba.Services;
using Acr.UserDialogs;

namespace appPrueba.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        bool isBusy = false;
        public NetService netService;
        public ApiService apiService;

        public BaseViewModel()
        {
            netService = new NetService();
            apiService = new ApiService();
        }
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }
        public static void ShowLoading(bool isRunning, bool isCancel = false)
        {
            if (isRunning == true)
            {
                if (isCancel == true)
                {
                    UserDialogs.Instance.Loading("Cargando", new Action(async () => {
                        if (Application.Current.MainPage.Navigation.ModalStack.Count > 1)
                        {
                            await Application.Current.MainPage.Navigation.PopModalAsync();
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine("Navigation: Can't pop modal without any modals pushed");
                        }
                        UserDialogs.Instance.Loading().Hide();
                    }));
                }
                else
                {
                    UserDialogs.Instance.Loading("Cargando");
                }
            }
            else
            {
                UserDialogs.Instance.Loading().Hide();
            }
        }
        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
