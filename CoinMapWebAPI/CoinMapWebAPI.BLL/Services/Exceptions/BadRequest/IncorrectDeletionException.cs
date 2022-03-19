namespace CoinMapWebAPI.BLL.Services.Exceptions.BadRequest
{
    public class IncorrectDeletionException : BadRequestException
    {
        public IncorrectDeletionException()
            : base("You cannot delete yourself!")
        {
        }
    }
}
