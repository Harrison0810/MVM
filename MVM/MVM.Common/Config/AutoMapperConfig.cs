using AutoMapper;
using MVM.Domain.Models;
using MVM.Infrastructure.Contracts;
using MVM.Infrastructure.Entities;

namespace MVM.Common.Config
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<CorrespondencesEntity, CorrespondencesContract>().ReverseMap();
            CreateMap<CorrespondencesContract, CorrespondencesModel>().ReverseMap();
            CreateMap<CorrespondenceTypesEntity, CorrespondenceTypesContract>().ReverseMap();
            CreateMap<CorrespondenceTypesContract, CorrespondenceTypesModel>().ReverseMap();
            CreateMap<LogEntity, LogContract>().ReverseMap();
            CreateMap<RolesEntity, RolesContract>().ReverseMap();
            CreateMap<UsersEntity, UsersContract>().ReverseMap();
        }
    }
}
