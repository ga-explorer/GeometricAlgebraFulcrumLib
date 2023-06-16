using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;
using GeometricAlgebraFulcrumLib.MathBase.Transforms;

namespace GraphicsComposerLib.Samples.Xeogl
{
    public static class Sample5
    {
        public static string Generate()
        {
            var map = TrstMap3D.CreateFromXSectionToLineSectionMap(
                section1StartX : 0,
                section1EndX : 1,
                section2Start : new Float64Tuple3D(0, 0, 0),
                section2End : new Float64Tuple3D(2, 2, 0)
                );

            var point = map.MapPoint(new Float64Tuple3D(1, 0, 0));

            return point.ToString();
        }
    }
}
