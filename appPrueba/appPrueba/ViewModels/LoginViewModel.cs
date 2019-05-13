using appPrueba.Models;
using appPrueba.Services;
using appPrueba.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace appPrueba.ViewModels
{
    public class LoginViewModel :BaseViewModel
    {
        public Command LoginIn { get; set; }
        public Login User { get; set; }
        public IDataStore<Login> DataStoreLogin => DependencyService.Get<IDataStore<Login>>() ?? new UserDtaStore();
        public LoginViewModel()
        {
            User = new Login();
            Title = "Iniciar sesión";
            LoginIn = new Command(async () => await ExecuteLoginInCommand());
        }
        async Task ExecuteLoginInCommand()
        {
            ShowLoading(true);
            if (await ValidateCampos())
            {
                bool IsSucces;
                if (netService.IsConected())
                {
                    var response = await apiService.APICosumeGet<Login>(generateUrl());
                    IsSucces = response.IsSucces;
                    User.authToken = response.Result != null ? ((Login)response.Result).authToken : String.Empty;
                }
                else
                {
                    var user = await DataStoreLogin.GetItemAsync(User.email, User.Pass);
                    User.authToken = user != null ? user.authToken : String.Empty;
                    IsSucces = user != null;
                }
                await repoceLoginIn(IsSucces);
            }
            ShowLoading(false);
        }

        private async Task<bool> ValidateCampos()
        {
            if (string.IsNullOrEmpty(User.email) || string.IsNullOrEmpty(User.Pass))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Campos vacios", "OK");
                return await Task.FromResult(false);
            }
            if (!ValidateEmail(User.email))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Formato de correo no valido", "OK");
                return await Task.FromResult(false);
            }
            return await Task.FromResult(true);
        }
        private Boolean ValidateEmail(String email)
        {
            String expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        async Task repoceLoginIn(bool isSucces)
        {
            if (isSucces)
            {
                await DataStoreLogin.DeleteItemAsync(User.email);
                await DataStoreLogin.AddItemAsync(User);
                App.Current.MainPage = new MainPage();
            }
            else {
                await Application.Current.MainPage.DisplayAlert("Error", "Usuario o contraseña incorrectos o revisa tu conexion", "OK");
            }
        }
        private string generateUrl()
        {
            return $"application/login?email={User.email}&password={User.Pass}";
        }
    }
}
