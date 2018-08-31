using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using ZXing;
using ZXing.Common;

namespace PoliceServer.Utilities
{
    public class BarcodeWriter : BarcodeWriter<Bitmap> , IBarcodeWriter
    {
    }

    public class Builder
    {
        public Bitmap Build()
        {
            BarcodeWriter bw = new BarcodeWriter();
            Bitmap bm = bw.Write("test");
            return bm;
        }
    }
}