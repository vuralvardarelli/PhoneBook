using AutoMapper;
using RepositoryService.Application.Commands;
using RepositoryService.Application.Responses;
using RepositoryService.Core.Entities;

namespace RepositoryService.Application.Mapper
{
    public class RecordMapperProfile : Profile
    {
        public RecordMapperProfile()
        {
            CreateMap<Record, AddRecordCommand>().ReverseMap();
            CreateMap<Record, RecordResponse>().ReverseMap();
            CreateMap<ContactInfo, ContactInfoResponse>().ReverseMap();
        }
    }
}
