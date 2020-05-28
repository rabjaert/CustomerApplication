using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CustomerApplication.GUI.Activation;

using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace CustomerApplication.GUI.Services
{
    // For more information on understanding and extending activation flow see
    // https://github.com/Microsoft/WindowsTemplateStudio/blob/master/docs/activation.md
    internal class ActivationService
    {
#pragma warning disable IDE0052 // Remove unread private members
        private readonly App _app;
#pragma warning restore IDE0052 // Remove unread private members
        private readonly Type _defaultNavItem;
#pragma warning disable IDE0044 // Add readonly modifier
        private Lazy<UIElement> _shell;
#pragma warning restore IDE0044 // Add readonly modifier

#pragma warning disable IDE0052 // Remove unread private members
        private object _lastActivationArgs;
#pragma warning restore IDE0052 // Remove unread private members

        public ActivationService(App app, Type defaultNavItem, Lazy<UIElement> shell = null)
        {
            _app = app;
            _shell = shell;
            _defaultNavItem = defaultNavItem;
        }

        public async Task ActivateAsync(object activationArgs)
        {
            if (IsInteractive(activationArgs))
            {
                // Initialize services that you need before app activation
                // take into account that the splash screen is shown while this code runs.
#pragma warning disable CA2007 // Consider calling ConfigureAwait on the awaited task
                await InitializeAsync();
#pragma warning restore CA2007 // Consider calling ConfigureAwait on the awaited task

                // Do not repeat app initialization when the Window already has content,
                // just ensure that the window is active
                if (Window.Current.Content == null)
                {
                    // Create a Shell or Frame to act as the navigation context
                    Window.Current.Content = _shell?.Value ?? new Frame();
                }
            }

            // Depending on activationArgs one of ActivationHandlers or DefaultActivationHandler
            // will navigate to the first page
#pragma warning disable CA2007 // Consider calling ConfigureAwait on the awaited task
            await HandleActivationAsync(activationArgs);
#pragma warning restore CA2007 // Consider calling ConfigureAwait on the awaited task
            _lastActivationArgs = activationArgs;

            if (IsInteractive(activationArgs))
            {
                // Ensure the current window is active
                Window.Current.Activate();

                // Tasks after activation
#pragma warning disable CA2007 // Consider calling ConfigureAwait on the awaited task
                await StartupAsync();
#pragma warning restore CA2007 // Consider calling ConfigureAwait on the awaited task
            }
        }

        private async Task InitializeAsync()
        {
#pragma warning disable CA2007 // Consider calling ConfigureAwait on the awaited task
            await Task.CompletedTask;
#pragma warning restore CA2007 // Consider calling ConfigureAwait on the awaited task
        }

        private async Task HandleActivationAsync(object activationArgs)
        {
            var activationHandler = GetActivationHandlers()
                                                .FirstOrDefault(h => h.CanHandle(activationArgs));

            if (activationHandler != null)
            {
#pragma warning disable CA2007 // Consider calling ConfigureAwait on the awaited task
                await activationHandler.HandleAsync(activationArgs);
#pragma warning restore CA2007 // Consider calling ConfigureAwait on the awaited task
            }

            if (IsInteractive(activationArgs))
            {
                var defaultHandler = new DefaultActivationHandler(_defaultNavItem);
                if (defaultHandler.CanHandle(activationArgs))
                {
#pragma warning disable CA2007 // Consider calling ConfigureAwait on the awaited task
                    await defaultHandler.HandleAsync(activationArgs);
#pragma warning restore CA2007 // Consider calling ConfigureAwait on the awaited task
                }
            }
        }

        private async Task StartupAsync()
        {
#pragma warning disable CA2007 // Consider calling ConfigureAwait on the awaited task
            await Task.CompletedTask;
#pragma warning restore CA2007 // Consider calling ConfigureAwait on the awaited task
        }

        private IEnumerable<ActivationHandler> GetActivationHandlers()
        {
            yield break;
        }

        private bool IsInteractive(object args)
        {
            return args is IActivatedEventArgs;
        }
    }
}
