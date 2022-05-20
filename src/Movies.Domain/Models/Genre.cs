using System.ComponentModel.DataAnnotations;

namespace Movies.Domain.Models;
public class Genre : BaseEntity<Guid>
{
    [StringLength(200)]
    public string Name { get; set; }
}
