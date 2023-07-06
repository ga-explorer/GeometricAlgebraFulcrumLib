using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Maps;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;

namespace GraphicsComposerLib.Samples.Xeogl
{
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
}
