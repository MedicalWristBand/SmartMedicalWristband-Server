using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace PoliceServer.Shared
{
    public class JsonResult
    {
        public bool isSuccess;
        public Object[] messages;

        public String GetMessage()
        {
            if (messages == null)
            {
                return null;
            }
            StringBuilder builder = new StringBuilder("");
            foreach (object message in messages)
            {
                builder.Append(message);
            }
            return builder.ToString();
        }
    }
}