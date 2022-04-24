using AutoMapper;
using teleperformance_case3.Application.Models;
using teleperformance_case3.Domain.Entities;

namespace teleperformance_case3.Mappers;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, GetUserResponse>();
    }
}