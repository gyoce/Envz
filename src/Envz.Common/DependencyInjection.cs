using Envz.Common.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace Envz.Common;

public static class DependencyInjection
{
    public static IServiceCollection AddPage<TView, TViewModel>(this IServiceCollection services)
        where TView : FrameworkElement
        where TViewModel : PageViewModel
    {
        services.AddSingleton<TViewModel>();
        services.AddKeyedSingleton<FrameworkElement, TView>(typeof(TViewModel));
        return services;
    }

    public static IServiceCollection AddDialog<TView, TViewModel>(this IServiceCollection services)
        where TView : FrameworkElement
        where TViewModel : class, IDialogViewModel
    {
        services.AddTransient<TViewModel>();
        services.AddKeyedTransient<FrameworkElement, TView>(typeof(TViewModel));
        return services;
    }
}