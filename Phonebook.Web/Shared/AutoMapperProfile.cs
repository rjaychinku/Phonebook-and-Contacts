using AutoMapper;
using models = Phonebook.DAL.Models;
using Phonebook.DAL.Models.DTO;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<ContactInfoDTO, models.Contact>().ReverseMap();
        CreateMap<PhonebookDTO, models.Phonebook>().ReverseMap();
    }
}
