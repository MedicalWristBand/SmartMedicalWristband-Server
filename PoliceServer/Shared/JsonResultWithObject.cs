using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace PoliceServer.Shared
{
    public class JsonResultWithObject<T>
    {
        public bool isSuccess;
        public T result;
        public Object[] messages;

        public String GetMessage()
        {
            StringBuilder builder = new StringBuilder("");
            if (messages == null)
            {
                return null;
            }
            foreach (object message in messages)
            {
                builder.Append(message);
            }
            return builder.ToString();
        }
    }
}