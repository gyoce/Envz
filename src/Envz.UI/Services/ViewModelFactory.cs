using Microsoft.Extensions.DependencyInjection;

namespace Envz.UI.Services;

public class ViewModelFactory(IServiceProvider serviceProvider)
{
    public TViewModel Create<TViewModel>(params object[] args)
        => ActivatorUtilities.CreateInstance<TViewModel>(serviceProvider, args);
}