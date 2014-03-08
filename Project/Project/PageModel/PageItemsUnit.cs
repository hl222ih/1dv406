using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.PageModel
{
    //Denna klass objekt motsvarar en grupp med PageItem-objekt.
    //Endast en av dessa PageItem-objekt visas samtidigt, men
    //de hör ihop på så sätt att de delar Meaning och att det
    //går att navigera mellan dem i en viss ordning.
    public class PageItemsUnit
    {
        IEnumerable<PageItem> pageItems;


    }
}