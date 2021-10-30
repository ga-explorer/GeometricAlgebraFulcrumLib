using System.Collections.Generic;
using GraphicsComposerLib.PovRay.SDL.Values;

namespace GraphicsComposerLib.PovRay.SDL.Objects.FSP
{
    public class SdlSurfaceOfRevolution : SdlPolynomialObject, ISdlFspObject
    {
        public List<ISdlVectorValue> Points { get; private set; }

        public bool Open { get; set; }


        public SdlSurfaceOfRevolution()
        {
            Points = new List<ISdlVectorValue>();
        }
    }
}
