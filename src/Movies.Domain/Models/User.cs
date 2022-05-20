using System.ComponentModel.DataAnnotations;

namespace Movies.Domain.Models
{
    public class User : BaseEntity<Guid>
    {
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        public IList<Rating> Ratings { get; set; }
    }
}