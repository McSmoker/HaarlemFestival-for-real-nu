using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Haarlem_Festival.Models
{
    [Table("Jazz")]
    public class Jazz : Event
    {
        public Performer Artist { get; set; }
        public string Hall { get; set; }

        public int PerformerId { get; set; }

        public Jazz()
        {

        }

        public Jazz(int eventId, DateTime eventStart, DateTime eventEnd, string location, int seats, int ticketsSold, Performer artist, string hall, float price, string comment)
            : base(eventId, eventStart, eventEnd, location, seats, ticketsSold, price, comment)
        {
            this.Artist = artist;
            this.Hall = hall;
        }
    }
}