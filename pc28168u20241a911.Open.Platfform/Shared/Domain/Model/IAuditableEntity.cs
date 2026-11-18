namespace Acme.CatchUp.Platform.Shared.Domain.Model;

public interface IAuditableEntity
{
    DateTimeOffset? CreatedAt { get; set; }

    DateTimeOffset? UpdatedAt { get; set; }
}