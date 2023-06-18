using Domain.Models;

namespace Service.DTO
{
    public class DesignDTO: IDesign
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Font { get; set; }
        public string Color { get; set; }
        public string Style { get; set; }
        public string Form { get; set; }
    }
}
