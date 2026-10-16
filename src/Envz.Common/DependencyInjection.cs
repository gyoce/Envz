using Envz.Common.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using Envz.Common.Services;
using Envz.Domain.Entities;
using Envz.Domain.Ports;

namespace Envz.Common;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddPage<TView, TViewModel>()
            where TView : FrameworkElement
            where TViewModel : PageViewModel
        {
            services.AddSingleton<TViewModel>();
            services.AddKeyedSingleton<FrameworkElement, TView>(typeof(TViewModel));
            return services;
        }

        public IServiceCollection AddDialog<TView, TViewModel>()
            where TView : FrameworkElement
            where TViewModel : class, IDialogViewModel
        {
            services.AddTransient<TViewModel>();
            services.AddKeyedTransient<FrameworkElement, TView>(typeof(TViewModel));
            return services;
        }

        public IServiceCollection AddSuggestedApplications(params SuggestedApplicationCandidate[] candidates)
        {
            services.AddSingleton<ISuggestedApplicationSource>(new StaticSuggestedApplicationSource(candidates));
            return services;
        }
    }
}