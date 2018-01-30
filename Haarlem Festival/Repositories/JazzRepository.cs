using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Haarlem_Festival.Models;

namespace Haarlem_Festival.Repositories
{
    public class JazzRepository : IJazzRepository
    {
        private HaarlemFestivalDB db = new HaarlemFestivalDB();

        public List<Jazz> GetAllJazzEvents()
        {
            var jazzEvents = db.Jazz.Include(j => j.Artist);
            return jazzEvents.ToList();
        }

        public Jazz GetSingleEvent(int eventId)
        {
            Jazz jazzEvent = db.Jazz.Include(j => j.Artist).SingleOrDefault(c => c.EventId == eventId);

            return jazzEvent;
        }
    }
}