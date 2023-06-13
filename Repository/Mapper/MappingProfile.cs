using AutoMapper;
using Domain.Models;
using Repository.Business_Models;

namespace Repository.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<IUser, database.Entities.User>().ReverseMap();
            CreateMap<database.Entities.User, UserBusiness>().ReverseMap();

            CreateMap<database.Entities.FileEntity, IFileEntity>().ReverseMap();
            CreateMap<FileEntityBusiness, IFileEntity>().ReverseMap();

            CreateMap< database.Entities.Company, ICompany >().ReverseMap();
            CreateMap<CompanyBusiness, database.Entities.Company>();

            CreateMap<database.Entities.ChatMessage, IChatMessage>().ReverseMap();
            CreateMap<ChatMessageBusiness, database.Entities.ChatMessage>();

            CreateMap<DesignBusiness, database.Entities.Design>();
            CreateMap<database.Entities.Design, IDesign>().ReverseMap();
        }
    }
}
