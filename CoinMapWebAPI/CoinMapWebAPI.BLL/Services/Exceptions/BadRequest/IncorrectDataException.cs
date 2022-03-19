namespace CoinMapWebAPI.BLL.Services.Exceptions.BadRequest
{
    public class IncorrectDataException : BadRequestException
    {
        public IncorrectDataException(string message) : base(message)
        {
        }
    }
}
