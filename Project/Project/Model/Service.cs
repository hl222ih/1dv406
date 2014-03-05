using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Project.Model.DAL;

namespace Project.Model
{
    public class Service
    {
        private CommunicationDAL communicationDAL;

        private CommunicationDAL CommunicationDAL
        {
            get { return communicationDAL ?? (communicationDAL = new CommunicationDAL()); }
        }

        public Service()
            : base()
        {
        }

        public IEnumerable<WordType> GetWordTypes()
        {
            return CommunicationDAL.SelectAllWordTypes();
        }
        //public void AddMeaning(string word, WordType wordType, string comment = "")

    }
}