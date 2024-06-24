using System.ComponentModel.DataAnnotations;

namespace Satrack.Domain
{
    public abstract class BaseDomainModel
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public string? LastModifiedBy { get; set; }

    }
}

