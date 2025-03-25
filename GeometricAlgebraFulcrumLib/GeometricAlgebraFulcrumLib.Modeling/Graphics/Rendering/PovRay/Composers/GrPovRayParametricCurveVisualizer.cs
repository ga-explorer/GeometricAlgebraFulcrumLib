using System.Collections.Immutable;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Basic;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Styles;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Surfaces;
using GeometricAlgebraFulcrumLib.Modeling.Signals;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Web.Colors;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Composers;

public class GrPovRayParametricCurveVisualizer(
    string workingFolder,
    Float64SamplingSpecs samplingSpecs,
    Float64Path3D curve
) : GrPovRaySceneSequenceComposer(workingFolder, samplingSpecs)
{
    public Float64Path3D Curve { get; } = curve;


    protected override void AddLaTeXTextures()
    {
        KaTeXComposer.AddLaTeXEquation(
            "v1VectorText",
            @"x\left(t\right)"
        );

        KaTeXComposer.AddLaTeXEquation(
            "v2VectorText",
            @"y\left(t\right)"
        );

        KaTeXComposer.AddLaTeXEquation(
            "v3VectorText",
            @"z\left(t\right)"
        );

        KaTeXComposer.AddLaTeXEquation(
            "vVectorText",
            @"\boldsymbol{v}\left(t\right)"
        );
        
        KaTeXComposer.AddLaTeXEquation(
            "e1VectorText",
            @"\boldsymbol{v}^{\prime}\left(t\right)"
        );
    }

    private void AddPhaseVector1(LinFloat64Vector3D x)
    {
        var (minValue, maxValue) = Curve.GetValueRange().Item1;

        ActiveSceneComposer.AddVector(
            "v1Vector",
            LinFloat64Vector3D.Zero,
            x,
            Color.Red,
            0.05
        ).AddLaTeXText(
            "v1VectorText",
            ImageSet["latex", "v1VectorText"],
            x + x.ToUnitLinVector3D() * 0.25d + (LinFloat64Vector3D.E2 + LinFloat64Vector3D.E3) * 0.25d / 2d.Sqrt(),
            LaTeXScalingFactor
        );

        ActiveSceneComposer.AddLineSegment(
            "v1Trail",
            LinFloat64Vector3D.Create(minValue, 0, 0),
            LinFloat64Vector3D.Create(maxValue, 0, 0),
            Color.Red.SetAlpha(0.25f),
            0.045
        );
    }

    private void AddPhaseVector2(LinFloat64Vector3D y)
    {
        var (minValue, maxValue) = Curve.GetValueRange().Item2;

        ActiveSceneComposer.AddVector(
            "v2Vector", 
            LinFloat64Vector3D.Zero, 
            y,
            Color.Green,
            0.05
        ).AddLaTeXText(
            "v2VectorText",
            ImageSet["latex", "v2VectorText"],
            y + y.ToUnitLinVector3D() * 0.25d + (LinFloat64Vector3D.E1 + LinFloat64Vector3D.E3) * 0.25d / 2d.Sqrt(),
            LaTeXScalingFactor
        );
            
        ActiveSceneComposer.AddLineSegment(
            "v2TrailSegment",
            LinFloat64Vector3D.Create(0, minValue, 0),
            LinFloat64Vector3D.Create(0, maxValue, 0),
            Color.Green.SetAlpha(0.25f),
            0.045
        );
    }

    private void AddPhaseVector3(LinFloat64Vector3D z)
    {
        var (minValue, maxValue) = Curve.GetValueRange().Item3;

        ActiveSceneComposer.AddVector(
            "v3Vector", 
            LinFloat64Vector3D.Zero, 
            z,
            Color.Blue,
            0.05
        ).AddLaTeXText(
            "v3VectorText",
            ImageSet["latex", "v3VectorText"],
            z + z.ToUnitLinVector3D() * 0.25d + (LinFloat64Vector3D.E1 + LinFloat64Vector3D.E2) * 0.25d / 2d.Sqrt(),
            LaTeXScalingFactor
        );
        
        ActiveSceneComposer.AddLineSegment(
            "v3TrailSegment",
            LinFloat64Vector3D.Create(0, 0, minValue),
            LinFloat64Vector3D.Create(0, 0, maxValue),
            Color.Blue.SetAlpha(0.25f),
            0.045
        );
    }

    private void AddSignalVector(LinFloat64Vector3D x, LinFloat64Vector3D y, LinFloat64Vector3D z)
    {
        var v = x + y + z;
        
        ActiveSceneComposer.AddVector(
            "vVector",
            LinFloat64Vector3D.Zero,
            v,
            Color.RosyBrown,
            0.05
        ).AddLaTeXText(
            "vVectorText",
            ImageSet["latex", "vVectorText"],
            v - v.ToUnitLinVector3D(),
            LaTeXScalingFactor
        );

        var lineMaterial = Color.DarkGray.WithAlpha(0.75).ToPovRayMaterial();
        var lineThickness = 0.02;

        ActiveSceneComposer.AddLineSegment(
            "xySegment1",
            x + y,
            x,
            lineMaterial, 
            lineThickness
        ).AddLineSegment(
            "xySegment2",
            x + y,
            y,
            lineMaterial, 
            lineThickness
        ).AddLineSegment(
            "xySegment3",
            x + y,
            v,
            lineMaterial, 
            lineThickness
        );

        ActiveSceneComposer.AddLineSegment(
            "yzSegment1",
            y + z,
            y,
            lineMaterial, 
            lineThickness
        ).AddLineSegment(
            "yzSegment2",
            y + z,
            z,
            lineMaterial, 
            lineThickness
        ).AddLineSegment(
            "yzSegment3",
            y + z,
            v,
            lineMaterial, 
            lineThickness
        );

        ActiveSceneComposer.AddLineSegment(
            "zxSegment1",
            z + x,
            z,
            lineMaterial, 
            lineThickness
        ).AddLineSegment(
            "zxSegment2",
            z + x,
            x,
            lineMaterial, 
            lineThickness
        ).AddLineSegment(
            "zxSegment3",
            z + x,
            v,
            lineMaterial, 
            lineThickness
        );
    }

    private double ImageSampleIndexToSignalTime(int imageIndex)
    {
        return SceneSamplingSpecs.GetSampleTime(imageIndex) *
            Curve.MaxTime / SceneSamplingSpecs.MaxTime;
    }
    
    private void AddSignalVectors(int imageSampleIndex)
    {
        var t = ImageSampleIndexToSignalTime(imageSampleIndex);

        var (x, y, z) = 
            Curve.GetValue(t).GetComponentVectors();
        
        AddPhaseVector1(x);
        AddPhaseVector2(y);
        AddPhaseVector3(z);
        AddSignalVector(x, y, z);
    }

    private void AddSignalCurve(int imageSampleIndex)
    {
        if (imageSampleIndex < 2)
            return;

        //var t = ImageSampleIndexToSignalTime(imageSampleIndex);

        var material = Color.RosyBrown.ToPovRayMaterial();

        var pointList = 
            Curve
                .TimeRange
                .GetLinearSamples(
                    10 * SceneSamplingSpecs.SampleCount, 
                    false
                ).Take(10 * imageSampleIndex)
                .Select(Curve.GetValue)
                .ToImmutableArray();

        ActiveSceneComposer.AddLinePath(
            "curve",
            pointList,
            material, 
            0.05
        );

    }

    private void AddSignalFrame(int imageSampleIndex)
    {
        var t = ImageSampleIndexToSignalTime(imageSampleIndex);

        var position = Curve.GetValue(t);
        var tangentVector = Curve.GetUnitTangent(t);
        
        ActiveSceneComposer.AddElement(
            GrVisualPoint3D.CreateStatic(
                "curvePoint",
                Color.DarkRed
                    .ToPovRayMaterial()
                    .CreateThickSurfaceStyle(0.06),
                position
            )
        );

        ActiveSceneComposer.AddElement(
            GrVisualVector3D.CreateStatic(
                "curveTangent",
                Color.DarkOrange
                    .ToPovRayMaterial()
                    .CreateTubeCurveStyle(0.05),
                position,
                tangentVector
            )
        );

        ActiveSceneComposer.AddLaTeXText(
            "e1VectorText",
            ImageSet["latex", "e1VectorText"],
            position + tangentVector * 1.25d,
            LaTeXScalingFactor
        );
        
        ActiveSceneComposer.AddElement(
            GrVisualCircleSurface3D.CreateStatic(
                "curveNormal",
                Color.Yellow
                    .ToPovRayMaterial()
                    .CreateThickSurfaceStyle(0.05),
                position,
                tangentVector,
                1 / Math.PI.Sqrt(),
                false
            )
        );

    }

    protected override void ComposeScene(int imageSampleIndex)
    {
        if (ShowGrid)
            ActiveSceneComposer.AddGrid(
                "defaultZx",
                -5 * LinFloat64Vector3D.E2, 
                LinFloat64Quaternion.XyToZx, 
                GridUnitCount,
                1,
                1
            );

        if (ShowAxes)
            ActiveSceneComposer.AddAxes(
                "defaultAxes",
                LinFloat64Vector3D.Create(4, -5, 4),
                LinFloat64Quaternion.Identity,
                1, 
                1
            );
        
        AddSignalVectors(imageSampleIndex);
        //AddSignalNormal(frame.Direction3);
        //AddSignalPlane(frame.Direction3);
        AddSignalCurve(imageSampleIndex);
        AddSignalFrame(imageSampleIndex);
    }
}