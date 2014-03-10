using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Project.Model
{
    public class Item
    {
        [Key]
        public Int16 ItemId { get; set; }
        [Required]
        public Int16 MeaningId { get; set; }
        [Required]
        public Int16 ImageId { get; set; }
        [Required]
        public Int16? CatRefId { get; set; }
        [Required]
        public Int16 CatId { get; set; }
        [Required]
        public byte PosId { get; set; }
    }
}