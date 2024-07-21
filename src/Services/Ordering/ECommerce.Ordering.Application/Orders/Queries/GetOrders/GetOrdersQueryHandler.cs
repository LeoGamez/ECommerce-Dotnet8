using ECommerce.Shared.Pagination;

namespace ECommerce.Ordering.Application.Orders.Queries.GetOrders;

public class GetOrdersHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrdersQuery, GetOrdersResult>
{
    public async Task<GetOrdersResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
    {
        var pageIndex = query.PaginatedRequest.pageIndex;
        var pageSize=query.PaginatedRequest.pageSize;

        var totalCount = await dbContext.Orders.LongCountAsync(cancellationToken);
        var orders = await dbContext.Orders
            .Include(o => o.OrderItems)
            .OrderBy(o=>o.OrderName.Value)
            .AsNoTracking()
            .Skip(pageSize*pageIndex) 
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new GetOrdersResult(new PaginatedResult<OrderDto>(pageIndex, pageSize, totalCount, orders.MapOrdersToDto()));
    }
}
