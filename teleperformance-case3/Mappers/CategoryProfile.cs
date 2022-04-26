using AutoMapper;
using teleperformance_case3.Application.Commands;
using teleperformance_case3.Application.Models;
using teleperformance_case3.Domain.Entities;
using teleperformance_case3.Models;

namespace teleperformance_case3.Mappers;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<UpdateCategoryRequest, UpdateCategoryCommand>();
        CreateMap<CreateCategoryRequest, CreateCategoryCommand>();
        CreateMap<Category, GetCategoryResponse>();
        CreateMap<CreateCategoryCommand, Category>();
    }
}