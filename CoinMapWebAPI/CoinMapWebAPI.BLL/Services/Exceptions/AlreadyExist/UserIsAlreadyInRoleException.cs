namespace CoinMapWebAPI.BLL.Services.Exceptions.AlreadyExist
{
	public class UserIsAlreadyInRoleException : AlreadyExistsException
	{
		public UserIsAlreadyInRoleException(string userId, string role)
			: base($"User with Id '{userId}' is already in role '{role}'.")
		{
		}
	}
}
