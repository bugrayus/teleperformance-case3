using AutoMapper;
using teleperformance_case3.Application.Commands;
using teleperformance_case3.Domain.Entities;
using teleperformance_case3.Models;

namespace teleperformance_case3.Mappers;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<LoginRequest, LoginCommand>();
        CreateMap<RegisterRequest, RegisterCommand>().ReverseMap();
        CreateMap<RegisterCommand, User>();
    }
}