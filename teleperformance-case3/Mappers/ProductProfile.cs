using AutoMapper;
using teleperformance_case3.Application.Commands;
using teleperformance_case3.Models;

namespace teleperformance_case3.Mappers;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<CreateProductRequest, CreateProductCommand>();
        CreateMap<UpdateProductRequest, UpdateProductCommand>();
        CreateMap<CreateShoppingListItemRequest, CreateShoppingListItemCommand>();
    }
}