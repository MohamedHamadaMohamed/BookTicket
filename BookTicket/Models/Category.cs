using System.ComponentModel.DataAnnotations;

namespace BookTicket.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        public string Name { get; set; }

        public List<Movie> Movies { get; set; }
    }
}
