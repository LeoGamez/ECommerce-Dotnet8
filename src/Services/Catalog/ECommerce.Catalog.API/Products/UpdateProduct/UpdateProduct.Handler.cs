﻿using ECommerce.Shared.Abstractions;

namespace ECommerce.Catalog.API.Products.UpdateProduct;

public record UpdateProductCommand(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price) : ICommand<Unit>;

internal class UpdateProductCommandHandler(IDocumentSession session) : ICommandHandler<UpdateProductCommand, Unit>
{
    public async Task<Unit> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await session.LoadAsync<Product>(command.Id);
        
        if(product == null)
        {
            throw new ProductNotFoundException();
        }
        product = command.Adapt<Product>();
        session.Update(product);
        await session.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}