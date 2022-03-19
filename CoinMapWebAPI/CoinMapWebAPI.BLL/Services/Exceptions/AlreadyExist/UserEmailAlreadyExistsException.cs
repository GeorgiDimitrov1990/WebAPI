namespace CoinMapWebAPI.BLL.Services.Exceptions.AlreadyExist
{
	public class UserEmailAlreadyExistsException : AlreadyExistsException
	{
		public UserEmailAlreadyExistsException(string email) :
			base($"User with email '{email}' already exists.")
		{
		}
	}
}
