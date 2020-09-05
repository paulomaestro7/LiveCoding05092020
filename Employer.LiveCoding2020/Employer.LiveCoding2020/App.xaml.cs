using Employer.LiveCoding2020.View;
using Employer.LiveCoding2020.ViewModel;
using Live.Coding.Caqui.Consumption;
using Live.Coding.Caqui.Consumption.Interface;
using Live.Coding.Caqui.Consumption.Util;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Unity;



namespace Employer.LiveCoding2020
{
    public partial class App : PrismApplication
    {
        public App()
            : this(null)
        {

        }

        public App(IPlatformInitializer initializer)
            : this(initializer, true)
        {

        }

        public App(IPlatformInitializer platformInitializer, bool setFormsDependencyResolver) : base(platformInitializer, setFormsDependencyResolver)
        {
            InitializeComponent();
        }


        protected override async void OnInitialized()
        {
#if DEBUG
            HotReloader.Current.Run(this);
#endif

            await NavigationService.NavigateAsync("LoginPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<RegisterUser, RegisterUserViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<VotingPage, VotingPageViewModel>();

            containerRegistry.GetContainer().RegisterType<ISyncAsync, SyncAsync>();
            containerRegistry.GetContainer().RegisterType<ILogin, Login>();
            containerRegistry.GetContainer().RegisterType<ISatisfaction, Satisfaction>();
        }
    }
}
