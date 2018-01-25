using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Haarlem_Festival.Interfaces;
using Haarlem_Festival.Models;

namespace Haarlem_Festival.Repository
{
    public class TalkingRepository : ITalkingRepository
    {
        private HaarlemFestivalDB db = new HaarlemFestivalDB();
        public List<Talking> GetTalkingEvents()
        {
            var jazzevents = db.Talking.ToList();
            return jazzevents;
        }
    }
}