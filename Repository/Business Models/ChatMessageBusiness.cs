﻿using Domain.Models;

namespace Repository.Business_Models
{
    public class ChatMessageBusiness : IChatMessage
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int RecieverId { get; set; }
        public string Message { get; set; }
        public string DateTime { get; set; }
    }
}
