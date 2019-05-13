using System;
using System.ComponentModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using appPrueba.Models;
using appPrueba.ViewModels;

namespace appPrueba.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class ItemDetailPage : ContentPage
    {
        ClientDetailViewModel viewModel;

        public ItemDetailPage(ClientDetailViewModel viewModel)
        {
            InitializeComponent();
            viewModel.Navigation = Navigation;
            BindingContext = this.viewModel = viewModel;
        }
        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LogsPage(new LogViewModel(viewModel.Item)));
        }
    }
}