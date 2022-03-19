using System;

namespace CoinMapWebAPI.BLL.Services.Exceptions.NotFound
{
    public abstract class NotFoundException : Exception
    {
        protected NotFoundException(string message) : base(message)
        {
        }
    }
}
