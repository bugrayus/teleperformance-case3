using AutoMapper;
using teleperformance_case3.Application.Commands;
using teleperformance_case3.Application.Models;
using teleperformance_case3.Domain.Entities;
using teleperformance_case3.Models;

namespace teleperformance_case3.Mappers;

public class ShoppingListProfile : Profile
{
    public ShoppingListProfile()
    {
        CreateMap<CreateShoppingListRequest, CreateShoppingListCommand>();
        CreateMap<ShoppingList, GetShoppingListResponse>();
        CreateMap<CreateShoppingListCommand, ShoppingList>();
    }
}