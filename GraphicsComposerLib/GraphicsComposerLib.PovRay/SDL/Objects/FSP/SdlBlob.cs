using System.Collections.Generic;
using GraphicsComposerLib.PovRay.SDL.Values;

namespace GraphicsComposerLib.PovRay.SDL.Objects.FSP
{
    public class SdlBlob : SdlPolynomialObject, ISdlFspObject
    {
        public List<ISdlBlobComponent> Components { get; private set; }

        public ISdlScalarValue Threshold { get; set; }


        public SdlBlob()
        {
            Components = new List<ISdlBlobComponent>();
        }
    }
}
