using Domain.Models;

namespace Service.DTO
{
    public class SketchDTO : ISketch
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FilePath { get; set; }
    }
}
