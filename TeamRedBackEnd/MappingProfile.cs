using AutoMapper;
using TeamRedBackEnd.Database.Models;
using TeamRedBackEnd.DataTransferObject;
using TeamRedBackEnd.DataTransferObjects;

namespace TeamRedBackEnd
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateUserDto, User>();
            CreateMap<EditUserDto, User>();

            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<User, OtherUserDto>();


            CreateMap<Habit, HabitDto>();
            CreateMap<HabitDto, Habit>();
            CreateMap<EditHabitDto, Habit>();

            CreateMap<History, HabitHistoryDto>();
            CreateMap<HabitHistoryDto, History>();
            CreateMap<EditHistoryDto, History>();
            CreateMap<History, EditHistoryDto>();

        }
    }
}
