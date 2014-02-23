using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Labb2_2.DAL;
using System.ComponentModel.DataAnnotations;  

namespace Labb2_2.BLL
{
    public class Contact
    {
        [Required]
        public int ContactID { get; set; }
        [StringLength(50), Required]
        public string EmailAddress { get; set; }
        [StringLength(50), Required]
        public string FirstName { get; set; }
        [StringLength(50), Required]
        public string LastName { get; set; }
    }
}