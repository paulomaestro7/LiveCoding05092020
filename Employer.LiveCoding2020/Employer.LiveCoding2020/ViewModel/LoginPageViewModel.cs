using Live.Coding.Caqui.Consumption.Interface;
using Live.Coding.Caqui.Model;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Employer.LiveCoding2020.ViewModel
{
    public class LoginPageViewModel : ViewModelBase
    {
        private readonly ILogin _login;

        private string _loginParameter;
        public string LoginParameter
        {
            get { return _loginParameter; }
            set { SetProperty(ref _loginParameter, value); }
        }

        private string _passwordParameter;
        public string PasswordParameter
        {
            get { return _passwordParameter; }
            set { SetProperty(ref _passwordParameter, value); }
        }

        public DelegateCommand LoginCommand
        {
            get
            {
                return new DelegateCommand(async () =>
                {
                    await LoginUser();
                });
            }
        }

        public DelegateCommand RegisterCommand
        {
            get
            {
                return new DelegateCommand(async () =>
                {
                    await RegisterUser();
                });
            }
        }

        public LoginPageViewModel(ILogin Login, INavigationService navigationService, IPageDialogService pageDialogService) : base(navigationService, pageDialogService)
        {
            _login = Login;
        }

        public async Task LoginUser()
        {
            UserModel userModel = new UserModel();

            userModel.Login = LoginParameter;
            userModel.Password = PasswordParameter;

            UserModel.Hash = await _login.GetUser(userModel);

            if (!string.IsNullOrEmpty(UserModel.Hash))
                await _navigationService.NavigateAsync("VotingPage");
            else
                await _pageDialogService.DisplayAlertAsync("Erro", "Usuário e/ou senha incorretos.", "OK");
        }

        public async Task RegisterUser()
        {
            await _navigationService.NavigateAsync("RegisterUser");
        }
    }
}
