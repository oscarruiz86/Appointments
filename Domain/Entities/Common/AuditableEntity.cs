namespace Domain.Entities.Common
{
    public abstract class AuditableEntity : IAuditableEntity
    {
        public bool IsDeleted { get; set; }

        public DateTime CreatedAt { get; set; }
        public Guid? CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }
        public Guid? UpdatedBy { get; set; }

        public DateTime? DeletedAt { get; set; }
        public Guid? DeletedBy { get; set; }
    }
}
