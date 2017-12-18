using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Haarlem_Festival.Models
{
    [Table("Talking")]
    public class Talking : Event
    {
        public virtual Performer SpeakerOne { get; set; }
        public virtual Performer SpeakerTwo { get; set; }

        public Talking()
        {

        }

        public Talking(int eventId, DateTime eventStart, DateTime eventEnd, string location, int seats, int ticketsSold, Performer speakerOne, Performer speakerTwo)
            : base(eventId, eventStart, eventEnd, location, seats, ticketsSold)
        {
            this.SpeakerOne = speakerOne;
            this.SpeakerTwo = speakerTwo;
        }
    }
}