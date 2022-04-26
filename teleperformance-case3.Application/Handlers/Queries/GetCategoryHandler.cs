using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using teleperformance_case3.Application.Common.Interfaces;
using teleperformance_case3.Application.Models;
using teleperformance_case3.Application.Queries;

namespace teleperformance_case3.Application.Handlers.Queries;

public class GetCategoryHandler : IRequestHandler<GetCategoryQuery, GetCategoryResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCategoryHandler(IMapper mapper, IApplicationDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<GetCategoryResponse> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        var category =
            await _context.Categories
                .FirstOrDefaultAsync(e => e.IsActive && e.Id == request.Id, cancellationToken);

        return _mapper.Map<GetCategoryResponse>(category);
    }
}