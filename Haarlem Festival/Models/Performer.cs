using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Haarlem_Festival.Models
{
    [Table("Performer")]
    public class Performer
    {
        [Key]
        public int PerformerId { get; set; }
        public string PerformerName { get; set; }
        public string PerformerInfo { get; set; }
        public string PerformerImagePath { get; set; }

        public Performer()
        {

        }
    }
}