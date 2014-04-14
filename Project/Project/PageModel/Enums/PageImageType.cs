using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.PageModel
{
    //ingen egentlig implementation för bildtyp i applikationen som den är nu, men
    //PageItem:s innehåller iallafall information om det, hämtat från databasen
    //så att presentationen hade kunnat anpassats efter det, exvis vad gäller hur
    //bilden ska förstoras/förminskas, ev. klippas osv.
    public enum PageImageType
    {
        Blissymbol,
        SignLanguage,
        Photo
    }
}