using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space1D.Scalars.Harmonic;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Visualizer;
using GeometricAlgebraFulcrumLib.Modeling.Signals;
using GeometricAlgebraFulcrumLib.Utilities.Web.Colors;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Modeling.Samples.Modeling.Graphics.BabylonJs;

public static class AnimatedCirclesSample
{
    public static void CircleCurveExample()
    {
        var samplingSpecs =
            Float64SamplingSpecs.Create(10, 5);

        var center =
            LinFloat64Vector3D.Zero;

        var normal =
            LinFloat64Vector3D.E2;

        var radius = CosWaveParametricScalar.Create(
            samplingSpecs.TimeRange,
            1,
            5,
            1
        );

        var animationComposer = new GrBabylonJsGeometryAnimationComposer(
            @"D:\Projects\Study\Web\Babylon.js",
            samplingSpecs
        );

        animationComposer.SetFileNameAndTitle(
            "AnimatedCircleScene",
            "Animated Circle Curve"
        );
        
        animationComposer
            .BeginDrawing3D()
            .SetCurveStyleTube(0.05)
            .DrawCircleCurve(Color.BlueViolet, center, normal, radius)
            .SaveHtmlFile();
    }

    public static void CircleSurfaceExample()
    {
        var samplingSpecs =
            Float64SamplingSpecs.Create(10, 5);

        var center =
            LinFloat64Vector3D.Zero;

        var normal =
            LinFloat64Vector3D.E2;

        var radius = CosWaveParametricScalar.Create(
            samplingSpecs.TimeRange,
            1,
            5,
            1
        );

        var animationComposer = new GrBabylonJsGeometryAnimationComposer(
            @"D:\Projects\Study\Web\Babylon.js",
            samplingSpecs
        );

        animationComposer.SetFileNameAndTitle(
            "CircleSurfaceScene",
            "Circle Surface Scene"
        );

        animationComposer
            .BeginDrawing3D()
            .SetSurfaceStyleThick(0.025)
            .DrawCircleSurface(Color.BlueViolet.SetAlpha(0.5), center, normal, radius)
            .SaveHtmlFile();
    }
}