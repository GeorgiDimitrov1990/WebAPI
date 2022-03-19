using System;

namespace CoinMapWebAPI.BLL.Services.Exceptions.AlreadyExist
{
    public abstract class AlreadyExistsException : Exception
	{
		protected AlreadyExistsException(string message) : base(message)
		{
		}
	}
}
