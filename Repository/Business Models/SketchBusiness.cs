using Domain.Models;

namespace Repository.Business_Models
{
    public class SketchBusiness: ISketch
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FilePath { get; set; }
    }
}
