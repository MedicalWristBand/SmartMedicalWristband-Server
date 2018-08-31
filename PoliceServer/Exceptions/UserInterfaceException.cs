using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoliceServer.Exceptions
{
    [Serializable]
    public class UserInterfaceException : System.Exception
    {
        public Int32 ErrorCode { get; set; }

        public UserInterfaceException(String message)
            : base(message)
        {
            this.ErrorCode = 0;
        }

        public UserInterfaceException(Int32 code, String message)
            : base("خطای " + code + ": " + message)
        {
            this.ErrorCode = code;
        }

        public UserInterfaceException(Int32 code, String message, Exception innerException)
            : base("خطای " + code + ": " + message, innerException)
        {
            this.ErrorCode = code;
        }

        public UserInterfaceException(String message, Exception innerException)
            : base(message, innerException)
        {
            this.ErrorCode = 0;
        }


    }
}