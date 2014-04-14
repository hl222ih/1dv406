using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Project.Model
{
    /// <summary>
    /// Motsvarar Meaning-tabellen i databasen.
    /// </summary>
    public class Meaning
    {
        [Key]
        public Int16 MeaningId { get; set; }
        [Required(ErrorMessage="Ordtyp måste väljas.")]
        public byte WTypeId { get; set; }
        [Required(ErrorMessage="Måste innehålla ett ord."), 
        StringLength(30, ErrorMessage="Ordet får innehålla max 30 tecken.")]
        public string Word { get; set; }
        [StringLength(100, ErrorMessage="Kommentaren får innehålla max 100 tecken.")]
        public string Comment { get; set; }
    }
}