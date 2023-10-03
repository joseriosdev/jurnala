namespace Domain.Models.Commons
{
    public interface IAuditableEntity
    {
        DateTime? CreatedAt { get; set; }
        DateTime? UpdatedAt { get; set; }
        Guid? UpdatedBy { get; set; }
        Guid? CreatedBy { get; set; }
    }
}
