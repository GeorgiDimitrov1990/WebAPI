namespace CoinMapWebAPI.BLL.Services.Exceptions.BadRequest
{
    public class IncorrectPasswordException : BadRequestException
	{
		public IncorrectPasswordException(string message) : base(message)
		{
		}
	}
}
