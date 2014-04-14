using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.PageModel
{
    /// <summary>
    /// Viktig information om en PageItems funktion och placering.
    /// ParentWordItem är ett ord som representeras av en miniatyrbild som förstoras om man klickar på den.
    /// ParentCategoryItem är en kategori-länk som representeras av en miniatyrbild som
    /// länkar till en ny sida när man klickar på den.
    /// ChildLeftWordItem är en bild (typiskt en teckenspråksillustration) som visas när man klickar 
    /// på vänsterpilen efter det att ParentWordItem förstorats. 
    /// ChildRightWordItem är en bild (typiskt ett fotografi) som visas när man klickar
    /// på högerpilen efter det att ParentWordItem förstorats.
    /// En PageItemUnit kan innehålla antingen ett ParentCategoryItem _eller_
    /// ett ParentWordItem ihop med noll till fem ChildLeftWordItem:s samt
    /// noll till fem ChildRightWordItem:s.
    /// </summary>
    public enum PageItemType
    {
        ParentWordItem,
        ChildLeftWordItem,
        ChildRightWordItem,
        ParentCategoryItem
    }
}