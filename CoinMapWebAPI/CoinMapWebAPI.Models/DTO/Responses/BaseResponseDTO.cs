namespace CoinMapWebAPI.Models.DTO.Responses
{
    public abstract class BaseResponseDTO
    {
        public int Id { get; set; }
        public string CreatorId { get; set; }
        public string CreationDate { get; set; }
        public string ModificationDate { get; set; }
    }
}
