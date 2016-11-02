using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PR3
{
    public class ImgTree // QuadTree stockage for the image
    {
        public ImgTree Parent { get; private set; }
        public List<ImgTree> Childs { get; private set; }
        public System.Drawing.Image Image { get; set; }
        public System.Drawing.Point TopLeft { get; private set; }

    }
}