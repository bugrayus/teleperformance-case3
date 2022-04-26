using AutoMapper;
using teleperformance_case3.Application.Commands;
using teleperformance_case3.Models;

namespace teleperformance_case3.Mappers;

public class ShoppingListProfile : Profile
{
    public ShoppingListProfile()
    {
        CreateMap<CreateShoppingListRequest, CreateShoppingListCommand>();
    }
}