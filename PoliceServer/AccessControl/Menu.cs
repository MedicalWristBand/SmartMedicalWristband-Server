using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PoliceServer.Annotation;

namespace PoliceServer.AccessControl
{
    public class Menu
    {
        public string Address { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public List<Menu> Childs = new List<Menu>();

        public HeaderType MenuType { get; set; }
    }
}