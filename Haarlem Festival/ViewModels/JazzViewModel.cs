using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Haarlem_Festival.Models;

namespace Haarlem_Festival.ViewModels
{
    public class JazzViewModel
    {
        // Properties
        public int EventId { get; set; }
        public DateTime EventStart { get; set; }
        public DateTime EventEnd { get; set; }
        public string Location { private get; set; }
        public string Hall { private get; set; }
        public int Seats { private get; set; }
        public int TicketsSold { private get; set; }
        public Performer Artist { get; set; }
        public float Price { private get; set; }
                
        // Constructor
        public JazzViewModel()
        {

        }
        
        // Methods
        public string LocationHall()
        {
            if (Hall != "")
                return Location + " | " + Hall;
            else
                return Location;
        }
        
        public string FormatPrice()
        {
            if (Price > 0)
                return Price.ToString("€0.00");
            else
                return "Event is free!";
        }

        public string SeatsAvailable()
        {
            if (Location != "Grote Markt")
                return (Seats - TicketsSold).ToString();
            else
                return "No seats at this venue.";
        }

        public string ActionButtonValue()
        {
            if (Location != "Grote Markt")
                return "Add ticket to cart";
            else
                return "No ticket required for this venue";
        }
    }
}