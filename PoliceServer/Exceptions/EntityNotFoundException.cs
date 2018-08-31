using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoliceServer.Exceptions
{
    [Serializable]
    public class EntityNotFoundException : UserInterfaceException
    {
        public EntityNotFoundException(string message) : base(message) { }

        public EntityNotFoundException() : base("موجودیت مورد نظر در سیستم ذخیره نشده است") { }

        public EntityNotFoundException(Int32 code, String message) : base(code, message) { }
    }
}