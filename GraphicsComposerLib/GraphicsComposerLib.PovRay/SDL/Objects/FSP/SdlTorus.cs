using GraphicsComposerLib.POVRay.SDL.Values;

namespace GraphicsComposerLib.POVRay.SDL.Objects.FSP
{
    public class SdlTorus : SdlPolynomialObject, ISdlFspObject
    {
        public ISdlScalarValue MajorRadius { get; set; }

        public ISdlScalarValue MinorRadius { get; set; }
    }
}
