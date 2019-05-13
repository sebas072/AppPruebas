using appPrueba.Models;
using appPrueba.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace appPrueba.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class MenuPage : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        List<HomeMenuItem> menuItems;
        public MenuPage()
        {
            InitializeComponent();

            menuItems = new List<HomeMenuItem>
            {
                new HomeMenuItem {Id = MenuItemType.Clientes, Title="Clientes" },
                new HomeMenuItem {Id = MenuItemType.Salir, Title="Salir" }
            };

            ListViewMenu.ItemsSource = menuItems;

            ListViewMenu.SelectedItem = menuItems[0];
            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                if (((HomeMenuItem)e.SelectedItem).Id == MenuItemType.Salir)
                {
                    UpdateUser();
                    App.Current.MainPage = new LoginPage();
                }
                else
                {
                    var id = (int)((HomeMenuItem)e.SelectedItem).Id;
                    await RootPage.NavigateFromMenu(id);    
                }
            };
        }
        private async void UpdateUser()
        {
            IDataStore<Login> da = DependencyService.Get<IDataStore<Login>>() ?? new UserDtaStore();
            var user = await da.GetItemsAsync();
            Login us = new Login();
            foreach (var u in user)
                us = u;

            us.Remember = false;
            us.authToken = string.Empty;
            await da.UpdateItemAsync(us);
        }
    }
}