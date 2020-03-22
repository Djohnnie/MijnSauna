using System;
using Microsoft.Extensions.DependencyInjection;
using MijnSauna.Frontend.Phone.Factories.Interfaces;

namespace MijnSauna.Frontend.Phone.Factories
{
    public class ViewModelFactory : IViewModelFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ViewModelFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public TViewModel Get<TViewModel>()
        {
            return _serviceProvider.GetService<TViewModel>();
        }
    }
}