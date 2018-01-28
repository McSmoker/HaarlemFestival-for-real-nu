using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Haarlem_Festival.Models
{
    public class CartItem
    {
        public Jazz JazzEvent { get; set; }
        public Talking TalkingEvent { get; set; }
        public int Amount { get; set; }
        public TicketType TicketType { get; set; }
        public string PassePartoutDate { get; set; }
        public string PassePartoutTime { get; set; }
        public float PassePartoutPrice { get; set; }
    }
}