﻿

using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ECommerce.Ordering.Infrastructure.Data.Extensions;

public static class EntityExtensions
{
    public static bool HasChangedOwnedEntities(this EntityEntry entry) =>
        entry.References.Any(r =>
           r.TargetEntry != null
        && r.TargetEntry.Metadata.IsOwned()
        && (r.TargetEntry.State == EntityState.Added
        || r.TargetEntry.State == EntityState.Modified));
}
