using System.ComponentModel.DataAnnotations;

namespace Satrack.Domain.Entities
{
    public class CategoryEntity : BaseDomainModel
    {
        [Required]
        [StringLength(100)]
        public required string Name { get; set; }

        public ICollection<TaskEntity> Tasks { get; set; } = new List<TaskEntity>();
    }
}

