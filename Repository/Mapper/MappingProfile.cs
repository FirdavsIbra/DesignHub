using AutoMapper;
using database.Entities;
using Domain.Models;
using Repository.Business_Models;

namespace Repository.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<IUser, database.Entities.User>();
            CreateMap<database.Entities.User, UserBusiness>();

            CreateMap<database.Entities.FileEntity, IFileEntity>();
            CreateMap<FileEntityBusiness, IFileEntity>().ReverseMap();

            CreateMap<ICompany, database.Entities.Company>();
            CreateMap<database.Entities.Company, CompanyBusiness>();

            CreateMap<database.Entities.ChatMessage, IChatMessage>();
            CreateMap<ChatMessageBusiness, database.Entities.ChatMessage>();

            CreateMap<IDesign, database.Entities.Design>();
            CreateMap<database.Entities.Design, DesignBusiness>();

            CreateMap<ISketch, database.Entities.Sketch>();
            CreateMap<database.Entities.Sketch, SketchBusiness>();
        }
    }
}
