using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Haarlem_Festival.Models
{
    [Table("SiteContent")]
    public class SiteContent
    {
        [Key]
        public int SiteContentId { get; set; }
        public int PageNumber { get; set; }
        public int Position { get; set; }
        public string Content { get; set; }
    }
}