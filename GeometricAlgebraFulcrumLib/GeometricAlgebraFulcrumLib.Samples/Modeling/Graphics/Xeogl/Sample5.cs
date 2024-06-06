using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Maps;

namespace GeometricAlgebraFulcrumLib.Samples.Modeling.Graphics.Xeogl;

public static class Sample5
{
    public static string Generate()
    {
        var map = TrstMap3D.CreateFromXSectionToLineSectionMap(
            section1StartX: 0,
            section1EndX: 1,
            section2Start: LinFloat64Vector3D.Create(0, 0, 0),
            section2End: LinFloat64Vector3D.Create(2, 2, 0)
        );

        var point = map.MapPoint(LinFloat64Vector3D.Create(1, 0, 0));

        return point.ToString();
    }
}