using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Haarlem_Festival;
using Haarlem_Festival.Models;

namespace Haarlem_Festival.Models
{
    public class ManagementBusinessLayer
    {
        public List<Event> GetEvents()
        {
            HaarlemFestivalDB eventDal = new HaarlemFestivalDB();
            return eventDal.Events.ToList();
        }
    }
}