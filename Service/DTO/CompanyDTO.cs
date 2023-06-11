using Domain.Models;

namespace Service.DTO
{
    public class CompanyDTO:ICompany
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
