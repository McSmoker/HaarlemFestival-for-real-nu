using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Haarlem_Festival.Models;

namespace Haarlem_Festival.ViewModels
{
    public class CartItemViewModel
    {
        public int EventId { get; set; }
        public string PerformerOneName { private get; set; }
        public string PerformerTwoName { private get; set; }
        public string EventDate { get; set; }
        public string EventTime { get; set; }
        public string Location { get; set; }
        public string Hall { get; set; }
        public int Amount { get; set; }
        public TicketType TicketType { get; set; }
        public float Price { get; set; }

        public CartItemViewModel()
        {

        }

        public string Performers()
        {
            return PerformerOneName + " " + PerformerTwoName;
        }

        public string LocationHall()
        {
            if (Hall != null)
                return Location + " | " + Hall;
            else
                return Location;
        }
    }
}