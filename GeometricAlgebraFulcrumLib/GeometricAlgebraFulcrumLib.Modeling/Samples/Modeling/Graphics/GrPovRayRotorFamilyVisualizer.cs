using System.Collections.Immutable;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Composers;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials.Finishes;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials.Patterns;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials.Pigments;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Objects;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Styles;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Surfaces;
using GeometricAlgebraFulcrumLib.Modeling.Signals;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Web.Colors;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.SkiaSharp;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Modeling.Samples.Modeling.Graphics;

public class GrPovRayRotorFamilyVisualizer :
    GrPovRaySceneSequenceComposer
{
    public LinFloat64Vector3D SourceVector { get; }

    public LinFloat64Vector3D TargetVector { get; }

    public Float64ScalarSignal ThetaValues 
        => TemporalScalars["theta"];
    
    public bool DrawRotorTrace { get; init; }

    public GrPovRayFinish DefaultMaterialFinish { get; set; }
        = GrPovRayFinish.Shiny;


    public GrPovRayRotorFamilyVisualizer(string workingFolder, Float64SamplingSpecs samplingSpecs, ILinFloat64Vector3D sourceVector, ILinFloat64Vector3D targetVector) 
        : base(workingFolder, samplingSpecs)
    {
        SourceVector = sourceVector.ToUnitLinVector3D();
        TargetVector = targetVector.ToUnitLinVector3D();
    }


    public LinFloat64Angle GetPhiMin()
    {
        return GetPhiMinCos().CosToPolarAngle();
    }
    
    public double GetPhiMinCos()
    {
        return SourceVector.GetUnitVectorsAngleCos(TargetVector);
    }

    public LinFloat64Angle ThetaToPhi(LinFloat64Angle theta)
    {
        return ThetaToPhiCos(theta).CosToPolarAngle();
    }

    public LinFloat64Vector3D ThetaToUnitNormal(LinFloat64Angle theta)
    {
        var n = 
            SourceVector.VectorUnitCross(TargetVector);

        return n.RotateVectorUsingAxisAngle(
            (TargetVector - SourceVector).ToUnitLinVector3D(),
            theta
        );
    }

    public double ThetaToPhiCos(LinFloat64Angle theta)
    {
        if (!theta.DegreesValue.IsNearInRange(-90, 90))
            throw new ArgumentOutOfRangeException(nameof(theta));

        var phiMinCos = GetPhiMinCos();

        return 1d + (2d * (1 - phiMinCos)) / (theta.Sin().Square() * (1 + phiMinCos) - 2d);
    }

    public LinFloat64Quaternion ThetaToQuaternion(LinFloat64Angle theta)
    {
        var phi = ThetaToPhi(theta);
        var normal = ThetaToUnitNormal(theta);

        return normal.RotationAxisAngleToQuaternion(phi);
    }

    public LinFloat64Angle PhiToTheta(LinFloat64Angle phi)
    {
        return PhiToThetaSin(phi).SinToPolarAngle();
    }

    public double PhiToThetaSin(LinFloat64Angle phi)
    {
        var phiMinCos = GetPhiMinCos();

        if (phi.RadiansValue < phiMinCos.ArcCos() || phi.RadiansValue > Math.PI)
            throw new ArgumentOutOfRangeException(nameof(phi));

        var phiCos = phi.Cos();

        return (2d * (phiCos - phiMinCos) / ((1 + phiMinCos) * (phiCos - 1))).Sqrt();
    }
    
    public LinFloat64Vector3D PhiToUnitNormal(LinFloat64Angle phi)
    {
        return ThetaToUnitNormal(PhiToTheta(phi));
    }
    
    public LinFloat64Quaternion PhiToQuaternion(LinFloat64Angle phi)
    {
        var normal = PhiToUnitNormal(phi);

        return normal.RotationAxisAngleToQuaternion(phi);
    }

    public LinFloat64Angle GetThetaAtFrame(int frameIndex)
    {
        return TemporalScalars
            .GetScalarAtFrame("theta", frameIndex)
            .RadiansToDirectedAngle();
    }

    private Image GetThetaPhiPlotImage(int frameIndex)
    {
        const int width = 1024;
        const int height = 768;
        const float resolution = 150f;
        
        var model = new PlotModel
        {
            Title = "Rotation Angle",
            DefaultFont = "Georgia",
            Background = OxyColors.Transparent,
            //PlotMargins = new OxyThickness(0, 0, 0, 0),
            Padding = new OxyThickness(0, 0, 0, 0)
        };

        model.Axes.Add(
            new LinearAxis
            {
                Minimum = -90,
                Maximum = 90,
                Position = AxisPosition.Bottom
            }
        );
        
        model.Axes.Add(
            new LinearAxis
            {
                Minimum = 0,
                Maximum = 185,
                Position = AxisPosition.Left
            }
        );

        model.Series.Add(
            new FunctionSeries(theta => ThetaToPhi(theta.DegreesToDirectedAngle()).DegreesValue, -90, 90, 180d / 450)
            {
                Color = System.Drawing.Color.DarkBlue.ToOxyColor(),
                CanTrackerInterpolatePoints = false,
                LineStyle = LineStyle.Solid,
                StrokeThickness = 2
            }
        );

        var theta = GetThetaAtFrame(frameIndex);
        var phi = ThetaToPhi(theta);
        model.Series.Add(
            new ScatterSeries
            {
                MarkerSize = 7,
                MarkerStroke = System.Drawing.Color.DarkRed.ToOxyColor(),
                MarkerFill = System.Drawing.Color.DarkRed.ToOxyColor(192),
                MarkerStrokeThickness = 1,
                MarkerType = MarkerType.Circle,
                Points =
                {
                    new ScatterPoint(theta.DegreesValue, phi.DegreesValue)
                }
            }
        );

        var pngExporter = new PngExporter
        {
            Width = width, 
            Height = height, 
            Dpi = resolution
        };

        using var stream = new MemoryStream();
        pngExporter.Export(model, stream);
        stream.Position = 0;

        return Image.Load(stream);
    }
    

    protected override void AddTemporalValues()
    {
        var thetaLimit = 90d.DegreesToRadians() - 5e-5d;
        TemporalScalars.SetScalar(
            "theta", 
            Float64ScalarSignal
                .FiniteCos()
                .Repeat(
                    1,
                    0, 
                    1,
                    -thetaLimit, 
                    thetaLimit
                )
        );

        TemporalScalars.SetScalar(
            "sourceVector.color.alpha",
            Float64ScalarSignal
                .FiniteSmoothRectangle()
                .Repeat(10, 0, 1, 0, 1)
        );
        
        TemporalScalars.SetScalar(
            "targetVector.color.alpha",
            Float64ScalarSignal
                .FiniteSmoothRectangle()
                .Repeat(10, 0, 1, 0, 1)
        );
    }

    protected override void AddLaTeXTextures()
    {
        KaTeXComposer.AddLaTeXEquation(
            "sourceVectorText",
            @"\boldsymbol{u}"
        );

        KaTeXComposer.AddLaTeXEquation(
            "targetVectorText",
            @"\boldsymbol{v}"
        );

        KaTeXComposer.AddLaTeXEquation(
            "sourceVector1Text",
            @"\boldsymbol{u}\left(\theta\right)"
        );

        KaTeXComposer.AddLaTeXEquation(
            "targetVector1Text",
            @"\boldsymbol{v}\left(\theta\right)"
        );

        KaTeXComposer.AddLaTeXEquation(
            "phiMinRotorText",
            @"\boldsymbol{R}\left(0\right)"
        );

        KaTeXComposer.AddLaTeXEquation(
            "thetaRotorText",
            @"\boldsymbol{S}\left(\theta\right)"
        );

        KaTeXComposer.AddLaTeXEquation(
            "phiRotorText",
            @"\boldsymbol{R}\left(\theta\right)"
        );

        KaTeXComposer.AddLaTeXEquation(
            "phiRotorCopyText",
            @"\boldsymbol{R}\left(\theta\right)"
        );

        KaTeXComposer.AddLaTeXEquation(
            "thetaRotorPlaneText",
            @"\boldsymbol{B}"
        );

        KaTeXComposer.AddLaTeXEquation(
            "phiRotorPlaneText",
            @"\boldsymbol{N}\left(\theta\right)"
        );

        //ImageCache.MarginSize = 20;
        ////ImageCache.BackgroundColor = Color.FromRgba(32, 32, 255, 16);

        var phiMin = GetPhiMin();
        var vuDiff = (TargetVector - SourceVector).ToUnitLinVector3D();
        var uvNormal = SourceVector.VectorUnitCross(TargetVector);

        //KaTeXComposer.AddLaTeXAlignedEquations(
        //    "modelText",
        //    new Pair<string>[]
        //    {
        //        new (@"\boldsymbol{u}", @$"\left( {SourceVector.X:F4}, {SourceVector.Y:F4}, {SourceVector.Z:F4} \right)"),
        //        new (@"\boldsymbol{v}", @$"\left( {TargetVector.X:F4}, {TargetVector.Y:F4}, {TargetVector.Z:F4} \right)"),
        //        new (@"\phi", @$"\cos^{{-1}}\left(\boldsymbol{{u}}\cdot\boldsymbol{{v}}\right) = {phiMin.DegreesValue:F4}"),
        //        new (@"\boldsymbol{B}", @"\left(\boldsymbol{v}-\boldsymbol{u}\right)\rfloor\boldsymbol{e}^{-1}_{123}"),
        //        new (@"\boldsymbol{\hat{B}}", @$"\frac{{\boldsymbol{{B}}}}{{\sqrt{{-\boldsymbol{{B}}^{{2}}}}}}"),
        //        new (@"", @$"\left( {vuDiff.X:F4}, {vuDiff.Y:F4}, {vuDiff.Z:F4} \right)^{{*}}"),
        //        new (@"\boldsymbol{S}\left( \theta \right)", @"\exp\left(\frac{1}{2}\theta\hat{\boldsymbol{B}}\right)"),
        //        new (@"\boldsymbol{N}\left( \theta \right)", @"\boldsymbol{S}\left(\theta\right)\left(\boldsymbol{v}\wedge\boldsymbol{u}\right)\boldsymbol{S}^{\sim}\left(\theta\right)"),
        //        new (@"\boldsymbol{\hat{N}}\left( \theta \right)", @$"\frac{{\boldsymbol{{N}}\left( \theta \right)}}{{\sqrt{{-\boldsymbol{{N}}^{{2}}\left( \theta \right)}}}}"),
        //        new (@"\boldsymbol{u}\left( \theta \right)", @"\left(\boldsymbol{u}\rfloor\boldsymbol{N}\left(\theta\right)\right)\rfloor\boldsymbol{N}^{-1}\left(\theta\right)"),
        //        new (@"\boldsymbol{v}\left( \theta \right)", @"\left(\boldsymbol{v}\rfloor\boldsymbol{N}\left(\theta\right)\right)\rfloor\boldsymbol{N}^{-1}\left(\theta\right)"),
        //        new (@"\boldsymbol{u}\left( \theta \right) \cdot \boldsymbol{v}\left( \theta \right)", @"1+\frac{2\left(1-\boldsymbol{u}\cdot\boldsymbol{v}\right)}{\sin^{2}\left(\theta\right)\left(1+\boldsymbol{u}\cdot\boldsymbol{v}\right)-2}"),
        //        new (@"\varphi\left( \theta \right)", @"\cos^{-1}\left(\boldsymbol{u}\left( \theta \right) \cdot \boldsymbol{v}\left( \theta \right) \right)"),
        //        new (@"\boldsymbol{R}\left( \theta \right)", @"\exp\left(\frac{1}{2}\varphi\left(\theta\right)\hat{\boldsymbol{N}}\left(\theta\right)\right)")
        //    }
        //);

        //for (var frameIndex = 0; frameIndex < FrameCount; frameIndex++)
        //{
        //    var theta = GetThetaAtFrame(frameIndex);
        //    var phi = ThetaToPhi(theta);
        //    var uvNormal1 = uvNormal.RotateVectorUsingAxisAngle(vuDiff, theta);
        //    var u1 = SourceVector.RejectOnVector(uvNormal1);
        //    var v1 = TargetVector.RejectOnVector(uvNormal1);

        //    KaTeXComposer.AddLaTeXAlignedEquations(
        //        $"dataText-{frameIndex:D6}",
        //        new Pair<string>[]
        //        {
        //            new (@"\theta", @$"{theta.DegreesValue:F4}"),
        //            new (@"\boldsymbol{\hat{N}}\left( \theta \right)", @$"\left( {uvNormal1.X:F4}, {uvNormal1.Y:F4}, {uvNormal1.Z:F4} \right)^{{*}}"),
        //            new (@"\boldsymbol{u}\left( \theta \right)", @$"\left( {u1.X:F4}, {u1.Y:F4}, {u1.Z:F4} \right)"),
        //            new (@"\boldsymbol{v}\left( \theta \right)", @$"\left( {v1.X:F4}, {v1.Y:F4}, {v1.Z:F4} \right)"),
        //            new (@"\varphi\left( \theta \right)", @$"{phi.DegreesValue:F4}")
        //        }
        //    );
        //}
    }


    private void AddInputVectors(int frameIndex)
    {
        var sourceVectorColorAlpha = 
            TemporalScalars.GetScalarAtFrame("sourceVector.color.alpha", frameIndex);

        var targetVectorColorAlpha = 
            TemporalScalars.GetScalarAtFrame("targetVector.color.alpha", frameIndex);

        ActiveSceneComposer.AddVector(
            "sourceVector",
            4 * SourceVector,
            Color.Red
                //.SetAlpha(sourceVectorColorAlpha)
                .ToPovRayMaterial(DefaultMaterialFinish),
            0.075
        ).AddLaTeXText(
            "sourceVectorText", 
            ImageSet["latex", "sourceVectorText"], 
            SourceVector * 4.35d, 
            LaTeXScalingFactor
        );

        ActiveSceneComposer.AddVector(
            "targetVector",
            4 * TargetVector,
            Color.Blue
                //.SetAlpha(targetVectorColorAlpha)
                .ToPovRayMaterial(DefaultMaterialFinish),
            0.075
        ).AddLaTeXText(
            "targetVectorText",
            ImageSet["latex", "targetVectorText"],
            TargetVector * 4.35d,
            LaTeXScalingFactor
        );
    }

    private void AddProjectedVectors(int index)
    {
        var u = SourceVector * 4;
        var v = TargetVector * 4;
        var uvNormal = u.VectorUnitCross(v);
        var thetaPlaneNormal = (v - u).ToUnitLinVector3D();
        var theta = GetThetaAtFrame(index);
        var uvNormal1 = uvNormal.RotateVectorUsingAxisAngle(thetaPlaneNormal, theta);
        var u1 = u.RejectOnVector(uvNormal1);
        var v1 = v.RejectOnVector(uvNormal1);

        var dashSpecs = new GrVisualDashedLineSpecs(1, 1, 20);

        ActiveSceneComposer.AddLineSegment(
            "sourceSegment",
            u,
            u1,
            Color.Red,
            dashSpecs
        ).AddLineSegment(
            "originSegment",
            u - u1,
            LinFloat64Vector3D.Zero,
            Color.DarkGreen,
            dashSpecs
        ).AddLineSegment(
            "targetSegment",
            v,
            v1,
            Color.Blue, 
            dashSpecs
        );

        ActiveSceneComposer
            .AddVector(
                "sourceVector1", 
                u1, 
                Color.Red.ToPovRayMaterial(DefaultMaterialFinish), 
                0.075
            ).AddLaTeXText(
                "sourceVector1Text", 
                ImageSet["latex", "sourceVector1Text"], 
                u1 + u1.ToUnitLinVector3D() * 0.35d, 
                LaTeXScalingFactor
            );

        ActiveSceneComposer
            .AddVector(
                "targetVector1", 
                v1, 
                Color.Blue.ToPovRayMaterial(DefaultMaterialFinish), 
                0.075
            ).AddLaTeXText(
                "targetVector1Text", 
                ImageSet["latex", "targetVector1Text"], 
                v1 + v1.ToUnitLinVector3D() * 0.35d, 
                LaTeXScalingFactor
            );
    }

    private void AddRotors(int frameIndex)
    {
        var scene = ActiveSceneComposer.SceneObject;

        var u = SourceVector * 4;
        var v = TargetVector * 4;
        var vuSum = (v + u) * 0.5;
        var vuDiff = (v - u).ToUnitLinVector3D();
        var uvNormal = u.VectorUnitCross(v);
        var theta = GetThetaAtFrame(frameIndex);
        var uvNormal1 = uvNormal.RotateVectorUsingAxisAngle(vuDiff, theta);
        var u1 = u.RejectOnVector(uvNormal1);
        var v1 = v.RejectOnVector(uvNormal1);
        var vuSum1 = vuSum.RotateVectorUsingAxisAngle(vuDiff, theta);

        ActiveSceneComposer.AddDiscSector(
            "phiMinRotor",
            LinFloat64Vector3D.Zero,
            u,
            v,
            u1.VectorENorm(),
            true,
            System.Drawing.Color.IndianRed.ToImageSharpColor(128).ToPovRayMaterial(DefaultMaterialFinish),
            0.05,
            System.Drawing.Color.IndianRed.ToImageSharpColor(128).ToPovRayMaterial(DefaultMaterialFinish)
        ).AddLaTeXText(
            "phiMinRotorText",
            ImageSet["latex", "phiMinRotorText"],
            0.5d.Lerp(u, v).SetLength(u1.VectorENorm() + 0.35d),
            LaTeXScalingFactor
        );

        if (!theta.DegreesValue.IsNearZero(1e-5))
        {
            ActiveSceneComposer.AddDiscSector(
                "thetaRotor",
                LinFloat64Vector3D.Zero,
                vuSum,
                vuSum1,
                u1.VectorENorm(),
                true,
                System.Drawing.Color.LightGreen.ToImageSharpColor(128).ToPovRayMaterial(DefaultMaterialFinish),
                0.05,
                System.Drawing.Color.LightGreen.ToImageSharpColor(128).ToPovRayMaterial(DefaultMaterialFinish)
            ).AddLaTeXText(
                "thetaRotorText",
                ImageSet["latex", "thetaRotorText"],
                0.5d.Lerp(vuSum, vuSum1).SetLength(u1.VectorENorm() + 0.35d),
                LaTeXScalingFactor
            );
        }

        ActiveSceneComposer.AddDiscSector(
            "phiRotor",
            LinFloat64Vector3D.Zero,
            u1,
            v1,
            u1.VectorENorm(),
            true,
            System.Drawing.Color.DarkSlateBlue.ToImageSharpColor(128).ToPovRayMaterial(DefaultMaterialFinish),
            0.05,
            System.Drawing.Color.DarkSlateBlue.ToImageSharpColor(128).ToPovRayMaterial(DefaultMaterialFinish)
        ).AddLaTeXText(
            "phiRotorText",
            ImageSet["latex", "phiRotorText"],
            0.5d.Lerp(u1, v1).SetLength(u1.VectorENorm() + 0.35d),
            LaTeXScalingFactor
        );

        ActiveSceneComposer.AddDiscSector(
            "phiRotorCopy",
            u - u1,
            u1,
            v1,
            u1.VectorENorm(),
            true,
            System.Drawing.Color.DarkSlateBlue.ToImageSharpColor(128).ToPovRayMaterial(DefaultMaterialFinish),
            0.05,
            System.Drawing.Color.DarkSlateBlue.ToImageSharpColor(128).ToPovRayMaterial(DefaultMaterialFinish)
        ).AddLaTeXText(
            "phiRotorCopyText",
            ImageSet["latex", "phiRotorCopyText"],
            u - u1 + 0.5d.Lerp(u1, v1).SetLength(u1.VectorENorm() + 0.35d),
            LaTeXScalingFactor
        );

        ActiveSceneComposer.AddDisc(
            "thetaRotorPlane",
            LinFloat64Vector3D.Zero,
            vuDiff,
            5d,
            System.Drawing.Color.YellowGreen.ToImageSharpColor(64).ToPovRayMaterial(DefaultMaterialFinish),
            0.035,
            System.Drawing.Color.YellowGreen.ToImageSharpColor(64)
        ).AddLaTeXText(
            "thetaRotorPlaneText",
            ImageSet["latex", "thetaRotorPlaneText"],
            vuSum.SetLength(-4d),
            LaTeXScalingFactor
        );

        ActiveSceneComposer.AddDisc(
            "phiRotorPlane",
            LinFloat64Vector3D.Zero,
            uvNormal1,
            5d,
            System.Drawing.Color.BurlyWood.ToImageSharpColor(64).ToPovRayMaterial(DefaultMaterialFinish),
            0.035,
            System.Drawing.Color.BurlyWood.ToImageSharpColor(64)
        ).AddLaTeXText(
            "phiRotorPlaneText",
            ImageSet["latex", "phiRotorPlaneText"],
            vuSum1.SetLength(-4d),
            LaTeXScalingFactor
        );
    }

    private void AddRotorTrace()
    {
        var scene = ActiveSceneComposer.SceneObject;

        var thetaDegrees = 
            Float64ScalarSignalUtils.GetSampledSignal(ThetaValues, ImageCount)
                .Select(t => t.RadiansToDegrees())
                .ToImmutableArray();

        var thetaMin = thetaDegrees.Min();
        var thetaMax = thetaDegrees.Max();

        var thetaDegreesArray = 
            thetaMin.GetLinearRange(thetaMax, 31, false).ToImmutableArray();

        var u = SourceVector * 4;
        var v = TargetVector * 4;
        var vuDiff = (v - u).ToUnitLinVector3D();
        var uvNormal = u.VectorUnitCross(v);
        
        for (var i = 0; i < thetaDegreesArray.Length; i++)
        {
            var theta = thetaDegreesArray[i].DegreesToDirectedAngle();
            var uvNormal1 = uvNormal.RotateVectorUsingAxisAngle(vuDiff, theta);
            var u1 = u.RejectOnVector(uvNormal1);
            var v1 = v.RejectOnVector(uvNormal1);

            ActiveSceneComposer.AddCircleCurve(
                $"phiRotorTrace{i}",
                u - u1,
                u1.VectorUnitCross(v1),
                u1.VectorENorm(),
                System.Drawing.Color.DarkSlateBlue.ToImageSharpColor(192).ToPovRayMaterial(DefaultMaterialFinish),
                0.025
            );

            //ActiveSceneComposer.AddCircleCurveArc(
            //    new GrVisualCircleCurveArc3D($"phiRotorTrace{i}")
            //    {
            //        Radius = u1.GetLength(),
            //        Center = u - u1,
            //        Direction1 = u1,
            //        Direction2 = v1,
            //        InnerArc = true,

            //        Style = new GrVisualCurveTubeStyle3D(
            //            material,
            //            0.05
            //        )
            //    }
            //);
        }
    }

    private void AddIntersections(int frameIndex)
    {
        var scene = ActiveSceneComposer.SceneObject;

        var u = SourceVector * 4;
        var v = TargetVector * 4;
        var vuSum = (v + u) * 0.5;
        var vuDiff = (v - u).ToUnitLinVector3D();
        var uvNormal = u.VectorUnitCross(v);
        var theta = GetThetaAtFrame(frameIndex);
        var uvNormal1 = uvNormal.RotateVectorUsingAxisAngle(vuDiff, theta);
        var u1 = u.RejectOnVector(uvNormal1);
        var v1 = v.RejectOnVector(uvNormal1);
        var vuSum1 = vuSum.RotateVectorUsingAxisAngle(vuDiff, theta);

        var intersectionPointColor = Color.DarkSlateGray.ToPovRayMaterial(DefaultMaterialFinish);
        var intersectionLineColor = Color.DarkSlateGray.SetAlpha(0.5f).ToPovRayMaterial(DefaultMaterialFinish);
        
        ActiveSceneComposer.AddPoints(
            i=> $"intersectionPoint{i + 1}", 
            intersectionPointColor, 
            0.08, 
            LinFloat64Vector3D.Zero, 
            vuSum1.SetLength(5d),
            vuSum1.SetLength(-5d),
            u - u1,
            u,
            v,
            u1,
            v1,
            vuSum.SetLength(u1.VectorENorm()),
            vuSum1.SetLength(u1.VectorENorm()),
            u.SetLength(u1.VectorENorm()),
            v.SetLength(u1.VectorENorm())
        );

        ActiveSceneComposer.AddLineSegment(
            "intersectionLine", 
            vuSum1.SetLength(5d), 
            vuSum1.SetLength(-5d),
            intersectionLineColor, 
            0.035
        );

        if ((u - u1).VectorENorm() > 1)
        {
            ActiveSceneComposer.AddRightAngle(
                "rightAngle1",
                u1,
                -u1,
                u - u1,
                0.5,
                Color.Red,
                Color.Red.SetAlpha(0.25f).ToPovRayMaterial(DefaultMaterialFinish)
            );

            ActiveSceneComposer.AddRightAngle(
                "rightAngle2",
                v1,
                -v1,
                v - v1,
                0.5,
                Color.Blue,
                Color.Blue.SetAlpha(0.25f).ToPovRayMaterial(DefaultMaterialFinish)
            );
        }
    }

    private void AddGroundPlane()
    {
        var pigment = GrPovRayPattern.Checker(
            Color.LightGoldenrodYellow,
            Color.LightGoldenrodYellow.ScaleRgbBy(0.75)
        ).ToColorListPigment();

        var finish = new GrPovRayFinish().SetProperties(
            new GrPovRayFinishProperties
            {
                ConserveEnergy = true,
                DiffuseAmount = 0.2,
                AmbientColor = 0,
                SpecularHighlightAmount = 0.7,
                Metallic = true,
                ReflectionColor = Color.AliceBlue,
                Reflection = new GrPovRayFinishReflectionProperties(Color.BlueViolet)
                {
                    Metallic = 1
                }
            }
        );

        ActiveSceneObject.AddStatement(
            GrPovRayObject
                .Box(GridUnitCount, 0.075, GridUnitCount)
                .Translate(AxesOrigin - 0.075 / 2 * LinFloat64Vector3D.E2)
                .SetMaterial(pigment, finish)
        );

        //ActiveSceneComposer.AddDisc(
        //    GrVisualCircleSurface3D.CreateStatic(
        //        "ground",
        //        Color.LightGoldenrodYellow.WithAlpha(0.4)
        //            .ToPovRayMaterial(finish)
        //            .CreateThickSurfaceStyle(
        //                Color.OrangeRed.ToPovRayMaterial(DefaultMaterialFinish), 
        //                0.075
        //            ),
        //        AxesOrigin - 0.075 / 2 * LinFloat64Vector3D.E2,
        //        LinFloat64Vector3D.E2, 
        //        GridUnitCount / 2d,
        //        true
        //    )
        //);
    }

    protected override void ComposeScene(int frameIndex)
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

        if (DrawRotorTrace) AddRotorTrace();

        AddInputVectors(frameIndex);
        AddRotors(frameIndex);
        AddProjectedVectors(frameIndex);
        AddIntersections(frameIndex);

        ActiveSceneComposer.AddSphere(
            GrVisualSphereSurface3D.CreateStatic(
                "rotorTraceSphere",
                Color.Yellow.WithAlpha(0.1)
                    .ToPovRayMaterial(DefaultMaterialFinish)
                    .CreateThinSurfaceStyle(),
                4 * SourceVector.Norm()
            )
        );

        AddGroundPlane();
    }

}