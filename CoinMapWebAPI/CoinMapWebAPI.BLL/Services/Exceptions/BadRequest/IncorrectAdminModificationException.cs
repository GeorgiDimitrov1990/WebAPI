namespace CoinMapWebAPI.BLL.Services.Exceptions.BadRequest
{
    public class IncorrectAdminModificationException : BadRequestException
    {
        public IncorrectAdminModificationException()
            : base("The initial admin cannot be modified or deleted!")
        {
        }
    }
}
