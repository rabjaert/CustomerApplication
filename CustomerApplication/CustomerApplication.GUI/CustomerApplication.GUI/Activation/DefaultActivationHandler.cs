using System;
using System.Threading.Tasks;

using CustomerApplication.GUI.Services;

using Windows.ApplicationModel.Activation;

namespace CustomerApplication.GUI.Activation
{
    internal class DefaultActivationHandler : ActivationHandler<IActivatedEventArgs>
    {
        private readonly Type _navElement;

        public DefaultActivationHandler(Type navElement)
        {
            _navElement = navElement;
        }

        protected override async Task HandleInternalAsync(IActivatedEventArgs args)
        {
            // When the navigation stack isn't restored, navigate to the first page and configure
            // the new page by passing required information in the navigation parameter
            object arguments = null;
            if (args is LaunchActivatedEventArgs launchArgs)
            {
                arguments = launchArgs.Arguments;
            }

            NavigationService.Navigate(_navElement, arguments);
#pragma warning disable CA2007 // Consider calling ConfigureAwait on the awaited task
            await Task.CompletedTask;
#pragma warning restore CA2007 // Consider calling ConfigureAwait on the awaited task
        }

        protected override bool CanHandleInternal(IActivatedEventArgs args)
        {
            // None of the ActivationHandlers has handled the app activation
            return NavigationService.Frame.Content == null && _navElement != null;
        }
    }
}
