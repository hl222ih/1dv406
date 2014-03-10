using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Project.Model
{
    public class WordType
    {
        [Key]
        public byte WTypeId { get; set; }
        [Required]
        public byte ColorId { get; set; }
        [Required, StringLength(12)]
        public string WType { get; set; }
    }
}