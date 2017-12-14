using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Haarlem_Festival.Models
{
    public class Volunteer
    {
        [Key]
        public int VolunteerId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        public void AddEvent()
        {

        }

        public void DeleteEvent()
        {

        }

        public void EditEvent()
        {

        }

        public void EditSiteContent()
        {

        }
    }
}