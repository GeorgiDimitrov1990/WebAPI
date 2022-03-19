using System;

namespace CoinMapWebAPI.BLL.Services.Exceptions.BadRequest
{
    public abstract class BadRequestException : Exception
    {
        protected BadRequestException(string message)
            : base (message)
        {
        }
    }
}
