using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Haarlem_Festival.Models;

namespace Haarlem_Festival.ViewModels
{
    public class JazzViewModel
    {
        public DateTime EventDate { get; set; }
        public string EventTimeStart { get; set; }
        public string EventTimeEnd { get; set; }
        public string Location { get; set; }
        public string Hall { get; set; }
        public int Seats { get; set; }
        public int TicketsSold { get; set; }
        public Performer Artist { get; set; }
        public double Price { get; set; }
    }
}