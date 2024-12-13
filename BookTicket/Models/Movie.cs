using BookTicket.Data.Enums;
using System.ComponentModel.DataAnnotations;
namespace BookTicket.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Required]
        [MinLength(1)]
        [MaxLength(255)]
        public string Name { get; set; }
        [MinLength(5)]
        public string Description { get; set; }
        [Required]
        [Range(0,10000)]
        public double Price { get; set; }
        public string ImgUrl { get; set; }
        public string TrailerUrl { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public MovieStatus MovieStatus { get; set; }
        public int CinemaId { get; set; }
        public int CategoryId { get; set; }

        public Cinema Cinema { get; set; }

        public int views { set; get; }

        public Category Category { get; set; }


        public List<Actor> Actors { get; set; }
        public List<ActorMovie> ActorMovies { get; set; }



    }
}
