using System;
using Argument.Check;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetTool.Builder.App
{
    public abstract class ServiceProviderBase
    {
        private readonly IServiceProvider _serviceProvider;

        protected ServiceProviderBase(IServiceProvider serviceProvider)
        {
            Throw.IfNull(() => serviceProvider);

            _serviceProvider = serviceProvider;
        }

        protected TService Use<TService>()
        {
            return _serviceProvider.GetService<TService>();
        }

        protected TService Get<TService>()
        {
            return _serviceProvider.GetService<TService>();
        }
    }
}
