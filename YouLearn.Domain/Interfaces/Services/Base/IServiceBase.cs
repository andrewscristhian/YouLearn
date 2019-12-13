using prmToolkit.NotificationPattern;
using System;

namespace YouLearn.Domain.Interfaces.Services.Base
{
    // Toda a classe que implementa-lo, sera notificavel
    public interface IServiceBase : INotifiable, IDisposable
    {
    }
}