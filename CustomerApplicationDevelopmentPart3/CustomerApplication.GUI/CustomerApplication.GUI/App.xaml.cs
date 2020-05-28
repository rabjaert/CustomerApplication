using System;

using CustomerApplication.GUI.Services;

using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;

namespace CustomerApplication.GUI
{
    public sealed partial class App : Application
    {
#pragma warning disable IDE0044 // Add readonly modifier
        private Lazy<ActivationService> _activationService;
#pragma warning restore IDE0044 // Add readonly modifier

        private ActivationService ActivationService
        {
            get { return _activationService.Value; }
        }

        public App()
        {
            InitializeComponent();

            // Deferred execution until used. Check https://msdn.microsoft.com/library/dd642331(v=vs.110).aspx for further info on Lazy<T> class.
            _activationService = new Lazy<ActivationService>(CreateActivationService);
        }

        protected override async void OnLaunched(LaunchActivatedEventArgs args)
        {
#pragma warning disable CA1062 // Validate arguments of public methods
            if (!args.PrelaunchActivated)
#pragma warning restore CA1062 // Validate arguments of public methods
            {
#pragma warning disable CA2007 // Consider calling ConfigureAwait on the awaited task
                await ActivationService.ActivateAsync(args);
#pragma warning restore CA2007 // Consider calling ConfigureAwait on the awaited task
            }
        }

        protected override async void OnActivated(IActivatedEventArgs args)
        {
#pragma warning disable CA2007 // Consider calling ConfigureAwait on the awaited task
            await ActivationService.ActivateAsync(args);
#pragma warning restore CA2007 // Consider calling ConfigureAwait on the awaited task
        }

        private ActivationService CreateActivationService()
        {
            return new ActivationService(this, typeof(Views.MainPage), new Lazy<UIElement>(CreateShell));
        }

        private UIElement CreateShell()
        {
            return new Views.ShellPage();
        }
    }
}
