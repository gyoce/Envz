using Microsoft.Extensions.DependencyInjection;

namespace Envz.Common.Services;

public class ViewModelFactory(IServiceProvider serviceProvider)
{
    public TViewModel Create<TViewModel>(params object[] args)
        => ActivatorUtilities.CreateInstance<TViewModel>(serviceProvider, args);
}