using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.Maps;

namespace GeometricAlgebraFulcrumLib.Samples.Graphics.Xeogl;

public static class Sample5
{
    public static string Generate()
    {
        var map = TrstMap3D.CreateFromXSectionToLineSectionMap(
            section1StartX : 0,
            section1EndX : 1,
            section2Start : Float64Vector3D.Create(0, 0, 0),
            section2End : Float64Vector3D.Create(2, 2, 0)
        );

        var point = map.MapPoint(Float64Vector3D.Create(1, 0, 0));

        return point.ToString();
    }
}