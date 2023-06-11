using Domain.Models;

namespace Repository.Business_Models
{
    public class FileEntityBusiness : IFileEntity
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] FileData { get; set; }
        public int CompanyId { get; set; }
    }
}
