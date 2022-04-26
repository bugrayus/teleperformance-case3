using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using teleperformance_case3.Application.Common.Interfaces;
using teleperformance_case3.Application.Models;
using teleperformance_case3.Application.Queries;

namespace teleperformance_case3.Application.Handlers.Queries;

public class GetCategoriesHandler : IRequestHandler<GetCategoriesQuery, GetCategoriesResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCategoriesHandler(IMapper mapper, IApplicationDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<GetCategoriesResponse> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories =
            await _context.Categories
                .Where(e => e.IsActive)
                .Select(category => _mapper.Map<GetCategoryResponse>(category))
                .ToListAsync(cancellationToken);

        return new GetCategoriesResponse
        {
            Categories = categories
        };
    }
}