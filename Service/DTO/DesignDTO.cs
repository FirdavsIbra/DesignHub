using Domain.Models;

namespace Service.DTO
{
    public class DesignDTO:IDesign
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}
