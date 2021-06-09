using AutoMapper;
using Phonebook.DAL.Models;
using Phonebook.DAL.Models.DTO;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<ContactInfoDTO, Entry>().ReverseMap();
    }
}
