using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Satrack.Domain.Entities
{
    public class TaskEntity : BaseDomainModel
    {
        [Required]
        [StringLength(200)]
        public required string Title { get; set; }

        [Required]
        public required string Description { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        public bool IsCompleted { get; set; } = false;

        [ForeignKey("Category")]
        public Guid CategoryId { get; set; }
        public required CategoryEntity Category { get; set; }

    }
}

