using Domain.Models;

namespace Repository.Business_Models
{
    public class DesignBusiness: IDesign
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}
