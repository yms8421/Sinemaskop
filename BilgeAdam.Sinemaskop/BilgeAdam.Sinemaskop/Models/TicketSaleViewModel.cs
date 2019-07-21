using System.Collections.Generic;

namespace BilgeAdam.Sinemaskop.Models
{
    public class TicketSaleViewModel
    {
        public int MovieId { get; set; }
        public IEnumerable<string> Seats { get; set; }
    }
}
