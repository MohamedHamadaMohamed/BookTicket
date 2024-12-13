using System.ComponentModel.DataAnnotations;

namespace BookTicket.Models
{
    public class Actor
    {
        public int Id { get; set; }
        [Required]
        [MinLength(1)]
        [MaxLength(255)]
        public string FirstName { get; set; }
        [Required]
        [MinLength(1)]
        [MaxLength(255)]
        public string LastName { get; set; }
        public string Bio { get; set; }
        public string ProfilePicture { get; set; }
        public string News { get; set; }
        public List<Movie> Movies { get; set; }

        public List<ActorMovie> ActorMovies { get; set; }

        

    }
}
