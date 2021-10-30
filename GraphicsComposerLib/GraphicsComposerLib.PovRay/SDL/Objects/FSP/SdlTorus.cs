using GraphicsComposerLib.PovRay.SDL.Values;

namespace GraphicsComposerLib.PovRay.SDL.Objects.FSP
{
    public class SdlTorus : SdlPolynomialObject, ISdlFspObject
    {
        public ISdlScalarValue MajorRadius { get; set; }

        public ISdlScalarValue MinorRadius { get; set; }
    }
}
