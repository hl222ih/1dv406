using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Project.Model
{
    public class Item
    {
        //I datalagret så finns ju constraints för att dessa egenskaper som FK motsvarar 
        //motsvarande PK i andra tabeller. Den informationen saknas här,
        //men ett tal 0 eller mindre är iallafall garanterat felaktigt så jag
        //har lagt till dataannotations för det.
        
        [Key]
        public Int16 ItemId { get; set; }
        [Range(1,Int16.MaxValue,ErrorMessage="Giltigt värde på betydelse-id:t saknas.")]
        public Int16 MeaningId { get; set; }
        [Range(1, Int16.MaxValue, ErrorMessage = "Giltigt värde på bild-id:t saknas.")]
        public Int16 ImageId { get; set; }
        public Int16? CatRefId { get; set; } //får vara null
        [Range(1, Int16.MaxValue, ErrorMessage = "Giltigt värde på kategori-id:t saknas.")]
        public Int16 CatId { get; set; }
        [Range(1, Int16.MaxValue, ErrorMessage = "Giltigt värde på positions-id:t saknas.")]
        public byte PosId { get; set; }
    }
}