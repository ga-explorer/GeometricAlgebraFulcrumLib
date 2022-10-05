using GraphicsComposerLib.Rendering.PovRay.SDL.Values;

namespace GraphicsComposerLib.Rendering.PovRay.SDL.Objects.ISP
{
    public class SdlPolySurface : SdlPolynomialObject, ISdlIspObject
    {
        public int Order { get; private set; }

        public ISdlScalarValue[] Coefs { get; }

        public int CoefsCount => Coefs.Length;


        public SdlPolySurface(int order)
        {
            Order = order;

            var n = (order + 1)*(order + 2)*(order + 3)/6;
            Coefs = new ISdlScalarValue[n];
        }

    }
}
