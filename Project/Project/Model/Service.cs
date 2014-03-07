using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Project.Model.DAL;
using Project.PageModel;
using System.Drawing;

namespace Project.Model
{
    public class Service
    {
        private CommunicationDAL communicationDAL;
        private IEnumerable<WordTypeItem> wordTypeItems;

        private CommunicationDAL CommunicationDAL
        {
            get { return communicationDAL ?? (communicationDAL = new CommunicationDAL()); }
        }

        public IEnumerable<WordTypeItem> WordTypeItems
        {
            get 
            { 
                if (wordTypeItems == null)
                {
                    //hårdkodade värden från databasens Color-tabell.
                    var colors = new Dictionary<int, string>() { 
                        { 1, "#fde885" }, { 2, "#f9c7af" }, { 3, "#dce8b9" },
                        { 4, "#d6ecf7" }, { 5, "#dad5d2" }, { 6, "#ffffff" } };

                    wordTypeItems = GetWordTypes().Select(wt => new WordTypeItem {
                        WType = wt.WType,
                        WTypeId = wt.WTypeId,
                        ColorId = wt.ColorId,
                        ColorHexCode = colors[wt.ColorId]
                    });
                }

                return wordTypeItems;
            }
        }


        public Color GetColorById(int colorId)
        {
            return ColorTranslator.FromHtml(GetColorHexCodeById(colorId));
        }

        public string GetColorHexCodeById(int colorId)
        {
            return WordTypeItems.First(wti => wti.ColorId == colorId).ColorHexCode;
        }


        public Service()
            : base()
        {
            Initialize();
        }

        private void Initialize()
        {
        }

        private IEnumerable<WordType> GetWordTypes()
        {
            return CommunicationDAL.SelectAllWordTypes();
        }
        //public void AddMeaning(string word, WordType wordType, string comment = "")

    }
}