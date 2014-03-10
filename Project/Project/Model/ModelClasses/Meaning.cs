using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Project.Model
{
    public class Meaning
    {
        [Key]
        public Int16 MeaningId { get; set; }
        [Required]
        public byte WTypeId { get; set; }
        [Required, StringLength(30)]
        public string Word { get; set; }
        [Required, StringLength(100)]
        public string Comment { get; set; }
    }
}