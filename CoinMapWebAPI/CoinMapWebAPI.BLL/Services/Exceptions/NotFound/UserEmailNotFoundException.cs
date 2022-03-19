namespace CoinMapWebAPI.BLL.Services.Exceptions.NotFound
{
	public class UserEmailNotFoundException : NotFoundException
	{
		public UserEmailNotFoundException(string email) :
			base($"User with email '{email}' was not found.")
		{
		}
	}
}
