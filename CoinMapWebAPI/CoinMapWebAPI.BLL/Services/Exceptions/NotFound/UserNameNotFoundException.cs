namespace CoinMapWebAPI.BLL.Services.Exceptions.NotFound
{
    public class UserNameNotFoundException : NotFoundException
    {
        public UserNameNotFoundException(string username) :
            base($"User with username '{username}' was not found.")
        {
        }
    }
}
