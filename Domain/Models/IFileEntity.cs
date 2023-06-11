﻿
namespace Domain.Models
{
    public interface IFileEntity
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] FileData { get; set; }
        public int CompanyId { get; set; }
    }
}
