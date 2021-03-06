﻿using System;
using System.Threading.Tasks;

namespace CustomerApplication.GUI.Activation
{
    // For more information on understanding and extending activation flow see
    // https://github.com/Microsoft/WindowsTemplateStudio/blob/master/docs/activation.md
    internal abstract class ActivationHandler
    {
        public abstract bool CanHandle(object args);

        public abstract Task HandleAsync(object args);
    }

    // Extend this class to implement new ActivationHandlers
    internal abstract class ActivationHandler<T> : ActivationHandler
        where T : class
    {
        // Override this method to add the activation logic in your activation handler
        protected abstract Task HandleInternalAsync(T args);

        public override async Task HandleAsync(object args)
        {
#pragma warning disable CA2007 // Consider calling ConfigureAwait on the awaited task
            await HandleInternalAsync(args as T);
#pragma warning restore CA2007 // Consider calling ConfigureAwait on the awaited task
        }

        public override bool CanHandle(object args)
        {
            // CanHandle checks the args is of type you have configured
            return args is T && CanHandleInternal(args as T);
        }

        // You can override this method to add extra validation on activation args
        // to determine if your ActivationHandler should handle this activation args
        protected virtual bool CanHandleInternal(T args)
        {
            return true;
        }
    }
}
