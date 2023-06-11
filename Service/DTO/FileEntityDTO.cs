using Domain.Models;

namespace Service.DTO
{
    public class FileEntityDTO : IFileEntity
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] FileData { get; set; }
        public int CompanyId { get; set; }
    }
}
