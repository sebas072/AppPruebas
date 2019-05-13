using appPrueba.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace appPrueba.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogsPage : ContentPage
    {
        LogViewModel viewModel;
        public LogsPage(LogViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = this.viewModel = viewModel;
        }
    }
}