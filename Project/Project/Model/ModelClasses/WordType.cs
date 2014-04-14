using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Project.Model
{
    //Motsvarar WordType i databas-tabellen. Ingen CRUD på denna, så felmeddelanden inte nödvändiga.
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