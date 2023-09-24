using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.PovRay.SDL.Values;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.PovRay.SDL.Objects.FSP
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
