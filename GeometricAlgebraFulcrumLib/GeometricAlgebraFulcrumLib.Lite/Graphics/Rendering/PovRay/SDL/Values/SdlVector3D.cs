using System.Text;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.PovRay.SDL.Values;

public sealed class SdlVector3D : SdlValue, ISdlVectorValue
{
    public ISdlScalarValue X { get; set; }

    public ISdlScalarValue Y { get; set; }

    public ISdlScalarValue Z { get; set; }

    public override string Value => new StringBuilder()
        .Append(X.ScalarOrDefault())
        .Append(',')
        .Append(Y.ScalarOrDefault())
        .Append(',')
        .Append(Z.ScalarOrDefault())
        .ToString();


    public SdlVector3D(ISdlScalarValue x, ISdlScalarValue y, ISdlScalarValue z)
    {
        X = x;
        Y = y;
        Z = z;
    }


    public override string ToString()
    {
        return TaggedValue;
    }
}