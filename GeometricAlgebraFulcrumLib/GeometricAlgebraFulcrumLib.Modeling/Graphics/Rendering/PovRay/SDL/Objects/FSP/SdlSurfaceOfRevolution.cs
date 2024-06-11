using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.SDL.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.SDL.Objects.FSP;

public class SdlSurfaceOfRevolution : SdlPolynomialObject, ISdlFspObject
{
    public List<ISdlVectorValue> Points { get; private set; }

    public bool Open { get; set; }


    public SdlSurfaceOfRevolution()
    {
        Points = new List<ISdlVectorValue>();
    }
}