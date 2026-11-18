using pc28168u20241a911.Open.Platfform.Settlements.Domain.Model.Aggregates;
using pc28168u20241a911.Open.Platfform.Settlements.Domain.Model.ValueObjects;
using Acme.CatchUp.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using Acme.CatchUp.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace Acme.CatchUp.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.AddInterceptors(new AuditableEntityInterceptor());
        base.OnConfiguring(builder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<DisbursementOrder>(entity =>
        {
            entity.HasKey(order => order.Id);
            entity.Property(order => order.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(order => order.DisbursementOrderId)
                .HasConversion(valueObject => valueObject.Value, value => new DisbursementOrderId(value))
                .IsRequired();
            entity.Property(order => order.TransferCap)
                .HasConversion(valueObject => valueObject.Value, value => new FinancialAmount(value))
                .IsRequired();
            entity.Property(order => order.OrderTrackingCode)
                .HasConversion(valueObject => valueObject.Value, value => new OpenpayReference(value))
                .IsRequired();
            entity.Property(order => order.Status).IsRequired();
            entity.HasIndex(order => order.DisbursementOrderId).IsUnique();
        });

        builder.UseSnakeCaseNamingConvention();
    }
}