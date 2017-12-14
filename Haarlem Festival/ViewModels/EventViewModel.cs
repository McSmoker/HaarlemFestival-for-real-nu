using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Haarlem_Festival.ViewModels
{
    public class EventViewModel
    {
        public int EventId { get; set; }
        public DateTime EventStart { get; set; }
        public DateTime EventEnd { get; set; }
        public string Location { get; set; }
        public int Seats { get; set; }
        public int TicketsSold { get; set; }
    }
}