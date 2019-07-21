using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BilgeAdam.Sinemaskop.Models
{
    [Table("Tickets")]
    public class Ticket
    {
        public Ticket()
        {
            CreatedDate = DateTime.Now;
        }
        [Key]
        public int Id { get; set; }
        [Required]
        public int MovieId { get; set; }
        [Required]
        [MaxLength(2)]
        public string SeatNumber { get; set; }
        public DateTime? CreatedDate { get; set; }

        [ForeignKey(nameof(MovieId))]
        public virtual Movie Movie { get; set; }
    }
}
