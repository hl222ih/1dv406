using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Labb2_2.Model.DAL;
using System.ComponentModel.DataAnnotations;  

namespace Labb2_2.Model
{
    //motsvarar fälten i databasen, extra constraints tillagda för epostadressen.
    public class Contact
    {
        public int ContactID { get; set; }
        [Required(ErrorMessage = "E-postadress måste anges")]
        [StringLength(50, ErrorMessage="E-postadressen får inte innehålla fler än 50 tecken")]
        [EmailAddress(ErrorMessage="E-postadressen måste vara giltig")]
        public string EmailAddress { get; set; }
        [StringLength(50, ErrorMessage = "Förnamnet får inte innehålla fler än 50 tecken")]
        [Required(ErrorMessage = "Förnamn måste anges")]
        public string FirstName { get; set; }
        [StringLength(50, ErrorMessage = "Efternamnet får inte innehålla fler än 50 tecken")]
        [Required(ErrorMessage = "Efternamn måste anges")]
        public string LastName { get; set; }
    }
}