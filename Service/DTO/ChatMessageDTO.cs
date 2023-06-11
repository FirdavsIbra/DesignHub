using Domain.Models;

namespace Service.DTO
{
    public class ChatMessageDTO:IChatMessage
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int RecieverId { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
        public string MediaUrl { get; set; }
    }
}
