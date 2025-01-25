using GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space3D;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Transforms;

public sealed class GrPovRayAffineTransform : 
    GrPovRayTransform
{
    public IFloat64AffineMap3D AffineMap { get; }


    internal GrPovRayAffineTransform(IFloat64AffineMap3D affineMap)
    {
        AffineMap = affineMap;
    }


    public override string GetPovRayCode()
    {
        var m = AffineMap.GetSquareMatrix4();

        var composer = new LinearTextComposer();

        composer
            .AppendLine("matrix <")
            .IncreaseIndentation()
            .AppendAtNewLine($"{m.Scalar00.GetPovRayCode()}, {m.Scalar10.GetPovRayCode()}, {m.Scalar20.GetPovRayCode()},")
            .AppendAtNewLine($"{m.Scalar01.GetPovRayCode()}, {m.Scalar11.GetPovRayCode()}, {m.Scalar21.GetPovRayCode()},")
            .AppendAtNewLine($"{m.Scalar02.GetPovRayCode()}, {m.Scalar12.GetPovRayCode()}, {m.Scalar22.GetPovRayCode()},")
            .AppendAtNewLine($"{m.Scalar03.GetPovRayCode()}, {m.Scalar13.GetPovRayCode()}, {m.Scalar23.GetPovRayCode()}")
            .DecreaseIndentation()
            .AppendAtNewLine(">");

        return composer.ToString();
    }
}