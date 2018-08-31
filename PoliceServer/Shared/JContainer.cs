using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PoliceServer.Models;

namespace PoliceServer.Shared
{
    public class JContainer
    {
        public string ContainerNumber;
        public string ContainerType;
        public List<JCommodity> Commoditys;


        public static List<JContainer> ConvertToJContainers(List<Container> containers)
        {
            List<JContainer> result = new List<JContainer>();
            for (int i = 0; i < containers.Count; i++)
            {
                result.Add(new JContainer()
                {
                    Commoditys = JCommodity.ConvertToJCommodities(containers[i].Commoditys.ToList()),
                    ContainerNumber = containers[i].ContainerNumber,
                    ContainerType = containers[i].ContainerType
                });
            }
            return result;
        }
    }
}