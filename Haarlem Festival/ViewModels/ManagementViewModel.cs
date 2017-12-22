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

        public ManagementViewModel()
        {

        }
    }
}