using System.Collections.Immutable;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Adaptive;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Composers;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Colors;

public class GrColorCurve
{
    public static GrColorCurve Create(params Color[] colorArray)
    {
        var curve = 
            colorArray
                .Select(c => c.RgbToVector3D())
                .CreateCatmullRomSpline3D(
                    CatmullRomSplineType.Centripetal, 
                    false
                )
                .CreateAdaptiveCurve3D(
                    new Float64AdaptivePath3DSamplingOptions(
                        LinFloat64PolarAngle.CreateFromDegrees(5), 
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
                curve.GetValue(curve.LengthToTime(k)).ToRgbColor()
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

        var c1 = ColorList[index1].RgbToVector3D();
        var c2 = ColorList[index2].RgbToVector3D();

        return (x - x.IntegerPart()).Lerp(c1, c2).ToRgbColor();
    }
}