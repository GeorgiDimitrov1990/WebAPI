namespace CoinMapWebAPI.BLL.Services.Exceptions.NotFound
{
	public class UserInRoleNotFoundException : NotFoundException
	{
		public UserInRoleNotFoundException(string userId, string role)
			: base($"User with Id '{userId}', was not found in role '{role}'.")
		{
		}
	}
}
