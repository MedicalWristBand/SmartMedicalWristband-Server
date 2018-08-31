using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PoliceServer.Models;

namespace PoliceServer.Utilities
{
    public class PasgahHelper
    {
        public static Dictionary<string,int> GetAllPasgahNamesAndID(List<Pasgah> pasgahs )
        {
            Dictionary<string,int> names = new Dictionary<string,int>();
            pasgahs.ForEach(p => names.Add(p.FarsiName + " " + p.PasgahId, p.Id));
            return names;
        } 
    }

}