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
            CreateMap<IUser, User>();
            CreateMap<User, UserBusiness>();

            CreateMap<FileEntity, IFileEntity>();
            CreateMap<FileEntityBusiness, IFileEntity>().ReverseMap();

            CreateMap<ICompany, Company>();
            CreateMap<Company, CompanyBusiness>();

            CreateMap<IChatMessage, ChatMessage>();
            CreateMap<ChatMessage, ChatMessageBusiness>();

            CreateMap<IDesign, Design>();
            CreateMap<Design, DesignBusiness>();

            CreateMap<ISketch, Sketch>();
            CreateMap<Sketch, SketchBusiness>();
        }
    }
}
