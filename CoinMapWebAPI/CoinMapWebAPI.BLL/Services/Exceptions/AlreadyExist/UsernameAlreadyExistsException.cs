namespace CoinMapWebAPI.BLL.Services.Exceptions.AlreadyExist
{
    public class UserNameAlreadyExistsException : AlreadyExistsException
	{
		public UserNameAlreadyExistsException(string username) :
			base($"User with username '{username}' already exists.")
		{
		}
	}
}
