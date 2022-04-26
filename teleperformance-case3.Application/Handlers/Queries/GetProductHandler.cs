using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using teleperformance_case3.Application.Common.Interfaces;
using teleperformance_case3.Application.Models;
using teleperformance_case3.Application.Queries;

namespace teleperformance_case3.Application.Handlers.Queries;

public class GetProductHandler : IRequestHandler<GetProductQuery, GetProductResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetProductHandler(IMapper mapper, IApplicationDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<GetProductResponse> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var product =
            await _context.Products
                .AsNoTracking()
                .Include(e => e.Category)
                .FirstOrDefaultAsync(e => e.IsActive && e.Id == request.Id, cancellationToken);
        if (product == null) return null;

        var response = _mapper.Map<GetProductResponse>(product);
        response.Category = _mapper.Map<GetCategoryResponse>(product.Category);
        return response;
    }
}