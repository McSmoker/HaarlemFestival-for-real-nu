using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Haarlem_Festival.Models;

namespace Haarlem_Festival.ViewModels
{
    public class ManagementViewModel
    {
        public List<Event> events { get; set; }
        public List<Performer> performer { get; set; }
        public List<Jazz> jazz { get; set; }
        public List<Talking> talking { get; set; }
        public List<ManagementViewModel> managements { get; set; }

        //these values are used for the selecting of content in Sitemanagement
        //they used to be viewbags but that is ILLEGAL
        public int selected { get; set; }
        public int selectedPerformer { get; set; }
        public ManagementViewModel()
        {

        }
    }
}