using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using teleperformance_case3.Application.Common.Interfaces;
using teleperformance_case3.Application.Models;
using teleperformance_case3.Application.Queries;

namespace teleperformance_case3.Application.Handlers.Queries;

public class GetProductsHandler : IRequestHandler<GetProductsQuery, GetProductsResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetProductsHandler(IMapper mapper, IApplicationDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<GetProductsResponse> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products =
            await _context.Products
                .AsNoTracking()
                .Include(e => e.Category)
                .Where(e => e.IsActive)
                .ToListAsync(cancellationToken);

        var productResponses = new List<GetProductResponse>();

        foreach (var product in products)
        {
            var productResponse = new GetProductResponse
            {
                Category = _mapper.Map<GetCategoryResponse>(product.Category)
            };
            productResponses.Add(_mapper.Map(product, productResponse));
        }

        return new GetProductsResponse
        {
            Products = productResponses
        };
    }
}