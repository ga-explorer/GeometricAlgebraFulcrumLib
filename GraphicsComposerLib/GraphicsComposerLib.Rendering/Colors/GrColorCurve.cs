using System.Collections.Immutable;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space3D.Curves;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space3D.Curves.Adaptive;
using GeometricAlgebraFulcrumLib.MathBase.Graphics.SdfShapes;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GraphicsComposerLib.Rendering.Colors
{
    public class GrColorCurve
    {
        public static GrColorCurve Create(params Color[] colorArray)
        {
            var curve = 
                colorArray
                    .Select(c => c.ToTuple3D())
                    .CreateCatmullRomSpline3D(
                        CatmullRomSplineType.Centripetal, 
                        false
                    )
                    .CreateAdaptiveCurve3D(
                        new AdaptiveCurveSamplingOptions3D(
                            5d.DegreesToRadians(), 
                            3, 
                            16
                        )
                    );
            
            var colorList =
                0d.GetLinearRange(
                    curve.Length, 
                    256, 
                    false
                ).Select(k => 
                    curve.GetPoint(curve.LengthToParameter(k)).ToColor()
                ).ToImmutableArray();

            return new GrColorCurve(colorList);
        }

        public IReadOnlyList<Color> ColorList { get; }


        private GrColorCurve(IReadOnlyList<Color> colorList)
        {
            ColorList = colorList;
        }


        public Color GetColor(double t)
        {
            if (t is > 1 or < 0)
                t -= Math.Truncate(t);

            if (t < 0)
                t = 1d - t;

            var x = t * (ColorList.Count - 1);

            if (x.IsInteger()) return ColorList[(int) x];

            var index1 = (int) x.IntegerPart();
            var index2 = index1 + 1;

            var c1 = ColorList[index1].ToTuple3D();
            var c2 = ColorList[index2].ToTuple3D();

            return (x - x.IntegerPart()).Lerp(c1, c2).ToColor();
        }
    }
}
