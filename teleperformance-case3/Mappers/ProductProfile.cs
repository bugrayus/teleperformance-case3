using AutoMapper;
using teleperformance_case3.Application.Commands;
using teleperformance_case3.Application.Models;
using teleperformance_case3.Domain.Entities;
using teleperformance_case3.Models;

namespace teleperformance_case3.Mappers;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<CreateProductRequest, CreateProductCommand>();
        CreateMap<UpdateProductRequest, UpdateProductCommand>();
        CreateMap<CreateShoppingListItemRequest, CreateShoppingListItemCommand>();
        CreateMap<CreateShoppingListItemCommand, ShoppingListItem>();
        CreateMap<Product, GetProductResponse>();
        CreateMap<CreateProductCommand, Product>();
        CreateMap<ShoppingListItem, GetShoppingListItemResponse>();
    }
}