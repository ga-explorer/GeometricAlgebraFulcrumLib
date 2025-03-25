using System.Collections.Immutable;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Calculus.Curves;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Cameras;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Composers;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Lights;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Basic;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Styles;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Surfaces;
using GeometricAlgebraFulcrumLib.Modeling.Signals;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Utilities.Web.Colors;
using GeometricAlgebraFulcrumLib.Utilities.Web.Html.Media;
using GeometricAlgebraFulcrumLib.Utilities.Web.Images;
using GeometricAlgebraFulcrumLib.Utilities.Web.LaTeX.KaTeX;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace GeometricAlgebraFulcrumLib.Applications.PowerSystems;

public class GrPovRayPowerSignalVisualizer :
    GrPovRaySceneSequenceComposer
{
    protected Image[] SignalTextLaTeXImages;


    public Float64PowerSignal3D Signal { get; }

    public double TimeScaling { get; set; } = 1;

    public double SignalScaling { get; set; } = 1;

    public int MeanSampleCount { get; set; }

    public int TrailSampleCount { get; set; }

    public int PlotSampleCount { get; set; }

    public int FrameSeparationCount { get; set; } = 20;

    public bool ShowLeftPanel { get; set; } = true;

    public bool ShowRightPanel { get; set; } = true;
    
    public LinFloat64Vector3D OmegaFrameOrigin 
        => LinFloat64Vector3D.Zero; //{ get; set; } = LinFloat64Vector3D.Create(-6, 2, 1);
    
    //public int SignalTextImageMaxWidth { get; private set; }

    //public int SignalTextImageMaxHeight { get; private set; }
    
    //public GrPovRaySceneComposer OmegaSceneComposer 
    //    => CodeFilesComposer.GetSceneComposer("omegaScene");


    public GrPovRayPowerSignalVisualizer(string workingFolder, Float64SamplingSpecs samplingSpecs, Float64PowerSignal3D powerSignal)
        : base(workingFolder, samplingSpecs)
    {
        Signal = powerSignal;
    }

    
    protected override void AddTemporalValues()
    {
        
    }

    protected override void AddLaTeXTextures()
    {
        KaTeXComposer.AddLaTeXEquation(
            "basis1VectorText",
            @"\boldsymbol{\sigma}_{1}"
        );
            
        KaTeXComposer.AddLaTeXEquation(
            "basis2VectorText",
            @"\boldsymbol{\sigma}_{2}"
        );
            
        KaTeXComposer.AddLaTeXEquation(
            "basis3VectorText",
            @"\boldsymbol{\sigma}_{3}"
        );

        KaTeXComposer.AddLaTeXEquation(
            "v1VectorText",
            @"\boldsymbol{v}_{1}"
        );

        KaTeXComposer.AddLaTeXEquation(
            "v2VectorText",
            @"\boldsymbol{v}_{2}"
        );

        KaTeXComposer.AddLaTeXEquation(
            "v3VectorText",
            @"\boldsymbol{v}_{3}"
        );

        KaTeXComposer.AddLaTeXEquation(
            "vVectorText",
            @"\boldsymbol{v}"
        );
            
        KaTeXComposer.AddLaTeXEquation(
            "e1VectorText",
            @"\boldsymbol{e}_{1}"
        );

        KaTeXComposer.AddLaTeXEquation(
            "e2VectorText",
            @"\boldsymbol{e}_{2}"
        );

        KaTeXComposer.AddLaTeXEquation(
            "e3VectorText",
            @"\boldsymbol{e}_{3}"
        );

        KaTeXComposer.AddLaTeXEquation(
            "kVectorText",
            @"\hat{\boldsymbol{k}}"
        );

        KaTeXComposer.AddLaTeXEquation(
            "e1DsVectorText",
            @"\dot{\boldsymbol{e}}_{1}"
        );

        KaTeXComposer.AddLaTeXEquation(
            "e2DsVectorText",
            @"\dot{\boldsymbol{e}}_{2}"
        );

        KaTeXComposer.AddLaTeXEquation(
            "e3DsVectorText",
            @"\dot{\boldsymbol{e}}_{3}"
        );

        KaTeXComposer.AddLaTeXEquation(
            "omega1BivectorText",
            @"\boldsymbol{\Omega}_{1}"
        );

        KaTeXComposer.AddLaTeXEquation(
            "omega2BivectorText",
            @"\boldsymbol{\Omega}"
        );

        KaTeXComposer.AddLaTeXEquation(
            "omega3BivectorText",
            @"\boldsymbol{\Omega}_{2}"
        );


        //ImageCache.MarginSize = 20;
        ////ImageCache.BackgroundColor = Color.FromRgba(32, 32, 255, 16);
        
        KaTeXComposer.AddLaTeXCode("symbolicSignalText", Signal.LaTeXCode);

        //for (var i = 0; i < Signal.TimeValues.Count; i++)
        //{
        //    var t = Signal.TimeValues[i];
        //    var (x, y, z) = Signal.GetComponentVectors(t);
        //    var v = x + y + z;

        //    var frame = Signal.FrameList[i]; //GetSignalFrame(t);

        //    var e1 = frame.Direction1.ToUnitLinVector3D();
        //    var e2 = frame.Direction2.ToUnitLinVector3D();
        //    var e3 = frame.Direction3.ToUnitLinVector3D();
            
        //    var sDt = Signal.GetTangentNormValue(t);

        //    var (kappa1, kappa2) = Signal.CurvatureList[i];

        //    var omega = Signal.DarbouxBivectorList[i];
        //    var omegaNorm = omega.Norm();

        //    var omegaMean = Signal.GetDarbouxBivectorMean(i);
        //    var omegaMeanNorm = omegaMean.Norm();

        //    var frequencyHz = omegaNorm / (Math.Tau);
        //    var frequencyHzMean = omegaMeanNorm / (Math.Tau);

        //    var e1Ds = kappa1 * e2;
        //    var e2Ds = kappa2 * e3 - kappa1 * e1;
        //    var e3Ds = -kappa2 * e2;

        //    var kVector = omega.NormalToUnitDirection3D();
        //    var kVectorMean = omegaMean.NormalToUnitDirection3D();

        //    KaTeXComposer.AddLaTeXAlignedEquations(
        //        $"signalText-{i:D6}",
        //        new Pair<string>[]
        //        {
        //            new (@"t", @$"{t:F4}"),
        //            new (@"\boldsymbol{v}\left( t \right)", @$"\left( {v.X:F4}, {v.Y:F4}, {v.Z:F4} \right)"),
        //            new (@"\left\Vert \boldsymbol{v}^{\prime}\left(t\right)\right\Vert", @$"s^{{\prime}} \left( t \right) = {sDt:F4}"),
        //            new (@"\boldsymbol{e}_{1}\left( t \right)", @$"\left( {e1.X:F4}, {e1.Y:F4}, {e1.Z:F4} \right)"),
        //            new (@"\boldsymbol{e}_{2}\left( t \right)", @$"\left( {e2.X:F4}, {e2.Y:F4}, {e2.Z:F4} \right)"),
        //            new (@"\boldsymbol{e}_{3}\left( t \right)", @$"\left( {e3.X:F4}, {e3.Y:F4}, {e3.Z:F4} \right)"),
        //            new (@"\left\Vert \boldsymbol{\Omega}_{1}\left(t\right)\right\Vert", @$"\left| s^{{\prime}} \kappa_{{1}} \right| = {kappa1:F4}"),
        //            new (@"\left\Vert \boldsymbol{\Omega}_{2}\left(t\right)\right\Vert", @$"\left| s^{{\prime}} \right| \sqrt{{\kappa_{{1}}^{{2}}+\kappa_{{2}}^{{2}}}} = {omegaNorm:F4}"),
        //            new (@"\left\Vert \boldsymbol{\Omega}_{3}\left(t\right)\right\Vert", @$"\left| s^{{\prime}} \kappa_{{2}} \right| = {kappa2:F4}"),
        //            new (@"f \left( t \right)", $@"{frequencyHz:F4} \textrm{{ Hz}}"),
        //            new (@"\hat{\boldsymbol{k}}\left( t \right)", @$"\left( {kVector.X:F4}, {kVector.Y:F4}, {kVector.Z:F4} \right)"),
        //            new (@"\overline{f}", $@"{frequencyHzMean:F4} \textrm{{ Hz}}"),
        //            new (@"\overline{\boldsymbol{k}}", @$"\left( {kVectorMean.X:F4}, {kVectorMean.Y:F4}, {kVectorMean.Z:F4} \right)")
        //        }
        //    );
        //}
    }

    protected override void AddImageTextures()
    {
        SignalTextLaTeXImages = RenderSignalTextLaTeXImages();
    }

    private void AddPhaseVector1(LinFloat64Vector3D x)
    {
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
            LinFloat64Vector3D.Create(Signal.VectorBounds.MinX, 0, 0),
            LinFloat64Vector3D.Create(Signal.VectorBounds.MaxX, 0, 0),
            Color.Red.SetAlpha(0.25f),
            0.045
        );
    }

    private void AddPhaseVector2(LinFloat64Vector3D y)
    {
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
            LinFloat64Vector3D.Create(0, Signal.VectorBounds.MinY, 0),
            LinFloat64Vector3D.Create(0, Signal.VectorBounds.MaxY, 0),
            Color.Green.SetAlpha(0.25f),
            0.045
        );
    }

    private void AddPhaseVector3(LinFloat64Vector3D z)
    {
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
            LinFloat64Vector3D.Create(0, 0, Signal.VectorBounds.MinZ),
            LinFloat64Vector3D.Create(0, 0, Signal.VectorBounds.MaxZ),
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
            Color.DarkOrange,
            0.05
        ).AddLaTeXText(
            "vVectorText",
            ImageSet["latex", "vVectorText"],
            v + v.ToUnitLinVector3D() * 0.25d,
            LaTeXScalingFactor
        );

        var lineMaterial = Color.DarkGray.WithAlpha(0.75).ToPovRayMaterial();
        var lineThickness = 0.015;

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

    //private void AddSignalNormal(Tuple3D k)
    //{
    //    var scene = SceneComposer.SceneObject;

    //    var material = scene.AddSimpleMaterial("kVectorMaterial", Color.SaddleBrown);

    //    SceneComposer.AddVector(
    //        GrVisualVector3D.Create("e1Vector")
    //        {
    //            Origin = Tuple3D.Zero,
    //            Direction = k,
    //            Style = new GrVisualVectorStyle3D
    //            {
    //                Material = material,
    //                Thickness = 0.075
    //            }
    //        }
    //    );

    //    SceneComposer.AddLaTeXText(
    //        new GrVisualLaTeXText3D(ImageCache, "e1VectorText")
    //        {
    //            ScalingFactor = LaTeXScalingFactor,
    //            Origin = k + k.ToUnitVector() * 0.5d
    //        }
    //    );

    //    return SceneComposer;
    //}

    private void AddSignalPlane(ILinFloat64Vector3D k)
    {
        var scene = ActiveSceneComposer.SceneObject;
        
        var disc = GrVisualCircleSurface3D.CreateStatic(
            "disc",
            Color.BurlyWood.WithAlpha(0.55).ToPovRayMaterial().CreateThickSurfaceStyle(0.025),
            LinFloat64Vector3D.Zero,
            k,
            Signal.ScalarBounds.MaxValue * 1.5d, //Math.Sqrt(3d / 2d)
            false
        );

        var ring = GrVisualCircleRingSurface3D.Create(
            "ring",
            Color.BurlyWood.WithAlpha(0.55).ToPovRayMaterial().CreateThickSurfaceStyle(0.025),
            LinFloat64Vector3D.Zero,
            k,
            Signal.ScalarBounds.MaxValue * Math.Sqrt(3d / 2d) - 0.5d,
            Signal.ScalarBounds.MaxValue * Math.Sqrt(3d / 2d) + 0.5d,
            Float64SamplingSpecs.Static
        );

        ActiveSceneComposer.AddElement(ring);
    }
    
    private double ImageSampleIndexToSignalTime(int imageIndex)
    {
        return SceneSamplingSpecs.GetSampleTime(imageIndex) *
            Signal.SamplingSpecs.MaxTime / SceneSamplingSpecs.MaxTime;
    }

    private int ImageSampleIndexToSignalSampleIndex(int imageIndex)
    {
        return Signal.SamplingSpecs.GetSampleIndex(
            SceneSamplingSpecs.GetSampleTime(imageIndex) *
            Signal.SamplingSpecs.MaxTime / SceneSamplingSpecs.MaxTime
        ).FloorToInt32().ClampInt(Signal.SamplingSpecs.MaxSampleIndex);
    }
    
    private int SignalSampleIndexToImageSampleIndex(int signalIndex)
    {
        return SceneSamplingSpecs.GetSampleIndex(
            Signal.SamplingSpecs.GetSampleTime(signalIndex)
        ).FloorToInt32().ClampInt(SceneSamplingSpecs.MaxSampleIndex);
    }

    private void AddSignalVectors(int imageSampleIndex)
    {
        var t = ImageSampleIndexToSignalTime(imageSampleIndex);

        var (x, y, z) = 
            Signal.GetComponentVectors(t);
        
        AddPhaseVector1(x);
        AddPhaseVector2(y);
        AddPhaseVector3(z);
        AddSignalVector(x, y, z);
    }

    private void AddSignalCurve(int imageSampleIndex)
    {
        var signalSampleIndex = ImageSampleIndexToSignalSampleIndex(imageSampleIndex);

        if (signalSampleIndex < 2 || Signal.FrameList is null) 
            return;

        var scene = ActiveSceneComposer.SceneObject;

        //var material = scene.AddStandardMaterial(
        //    "curveMaterial",
        //    Color.Yellow
        //);

        //var length = 
        //    sampledCurve.Take(index + 1).ComputeCurveLength();

        var textureImage = GrVisualImageUtils.Create(
            1, 
            256,
            (i, j) => Color.Yellow.SetAlpha(1f - j / 255f)
        ).PngToHtmlDataUrlBase64();

        var material = Color.DarkOliveGreen.ToPovRayMaterial();
        
        //var texture = scene.AddTexture(
        //    "curveMaterialTexture",
        //    textureImage,
        //    new GrPovRayTextureProperties
        //    {
        //        HasAlpha = true
        //    }
        //);

        //var material = scene.AddStandardMaterial(
        //    "curveMaterial",
        //    new GrPovRayStandardMaterialProperties
        //    {
        //        //DiffuseColor = Color.Yellow,
        //        TransparencyMode = GrPovRayMaterialTransparencyMode.AlphaBlend,
        //        DiffuseTexture = texture.ConstName,
        //        UseAlphaFromDiffuseTexture = true,
        //        BackFaceCulling = true
        //    }
        //);


        var trailStartIndex = Math.Max(0, signalSampleIndex - TrailSampleCount);
        var trailLength = signalSampleIndex - trailStartIndex;

        var pointList = 
            Signal
                .FrameList
                .Skip(trailStartIndex)
                .Take(trailLength)
                .ToImmutableArray();

        ActiveSceneComposer.AddLinePath(
            "curve",
            pointList,
            material, 
            0.03
        );

        //var magnitude = Magnitudes.Max();
        //SceneComposer.AddElement(
        //    new GrVisualCircleCurve3D("curve")
        //    {
        //        Center = Tuple3D.Zero,
        //        Radius = magnitude * Math.Sqrt(3d / 2d),
        //        Normal = new Tuple3D(1, 1, 1).ToUnitVector(),
        //        Style = new GrVisualCurveTubeStyle3D
        //        {
        //            Material = material,
        //            Thickness = 0.035
        //        }
        //    }
        //);
    }

    private void AddSignalFrame(int imageSampleIndex)
    {
        var signalSampleIndex = ImageSampleIndexToSignalSampleIndex(imageSampleIndex);

        if (Signal.FrameList is null || Signal.CurvatureList is null || Signal.SampledCurve is null)
            return;

        var trailStartIndex = Math.Max(0, signalSampleIndex - TrailSampleCount);
        var trailLength = signalSampleIndex - trailStartIndex;

        var frameList = 
            Signal
                .FrameList
                .Skip(trailStartIndex)
                .Take(trailLength)
                .ToImmutableArray();

        var n = 0;
        var i = trailStartIndex - 1;
        foreach (var frame in frameList)
        {
            if (i % FrameSeparationCount == 0)
            {
                var alpha = n / (double)TrailSampleCount;
                n += FrameSeparationCount;

                ActiveSceneComposer.AddElement(
                    GrVisualFrame3D.Create(
                        $"curveFrame{i}",
                        new GrVisualFrameStyle3D
                        {
                            OriginStyle = 
                                Color.DarkGray.WithAlpha(alpha)
                                    .ToPovRayMaterial()
                                    .CreateThickSurfaceStyle(0.075),

                            Direction1Style = 
                                Color.DarkRed.WithAlpha(alpha)
                                    .ToPovRayMaterial()
                                    .CreateTubeCurveStyle(0.035),

                            Direction2Style = 
                                Color.DarkGreen.WithAlpha(alpha)
                                    .ToPovRayMaterial()
                                    .CreateTubeCurveStyle(0.035),

                            Direction3Style = 
                                Color.DarkBlue.WithAlpha(alpha)
                                    .ToPovRayMaterial()
                                    .CreateTubeCurveStyle(0.035)
                        },
                        frame.Origin,
                        frame.Direction1.SetLength(0.4),
                        frame.Direction2.SetLength(0.4),
                        frame.Direction3.SetLength(0.4),
                        Float64SamplingSpecs.Static
                    )
                );
            }

            i++;
        }

        var curveFrame = Signal.FrameList[signalSampleIndex];
        ActiveSceneComposer.AddElement(
            GrVisualFrame3D.Create(
                "curveFrame",
                new GrVisualFrameStyle3D
                {
                    OriginStyle = 
                        Color.DarkGray
                            .ToPovRayMaterial()
                            .CreateThickSurfaceStyle(0.075),

                    Direction1Style = 
                        Color.DarkRed
                            .ToPovRayMaterial()
                            .CreateTubeCurveStyle(0.035),

                    Direction2Style = 
                        Color.DarkGreen
                            .ToPovRayMaterial()
                            .CreateTubeCurveStyle(0.035),

                    Direction3Style = 
                        Color.DarkBlue
                            .ToPovRayMaterial()
                            .CreateTubeCurveStyle(0.035)
                },
                curveFrame.Origin,
                curveFrame.Direction1,
                curveFrame.Direction2,
                curveFrame.Direction3,
                Float64SamplingSpecs.Static
            )
        );

        ActiveSceneComposer.AddLaTeXText(
            "e1VectorText",
            ImageSet["latex", "e1VectorText"],
            curveFrame.Origin + 
            curveFrame.Direction1 + 
            curveFrame.Direction1.ToUnitLinVector3D() * 0.25d,
            LaTeXScalingFactor
        );
        
        ActiveSceneComposer.AddLaTeXText(
            "e2VectorText",
            ImageSet["latex", "e2VectorText"],
            curveFrame.Origin + 
            curveFrame.Direction2 + 
            curveFrame.Direction2.ToUnitLinVector3D() * 0.25d,
            LaTeXScalingFactor
        );
        
        ActiveSceneComposer.AddLaTeXText(
            "e3VectorText",
            ImageSet["latex", "e3VectorText"],
            curveFrame.Origin + 
            curveFrame.Direction3 + 
            curveFrame.Direction3.ToUnitLinVector3D() * 0.25d,
            LaTeXScalingFactor
        );

        //var (kappa1, kappa2) = Signal.CurvatureList[signalSampleIndex];

        //var e1 = curveFrame.Direction1.ToUnitLinVector3D();
        //var e2 = curveFrame.Direction2.ToUnitLinVector3D();
        //var e3 = curveFrame.Direction3.ToUnitLinVector3D();

        //var t = Signal.TimeValues[signalSampleIndex];
        //var sDt = Signal.GetTangentNormValue(t);
        //var k21Vector = (kappa2 * e3 - kappa1 * e1) / 2;
        //var omegaNorm = (kappa1.Square() + kappa2.Square()).Sqrt() / 2;
        
        //var radius = sDt / (2 * omegaNorm);
        //var center = LinFloat64Vector3D.Zero; //curveFrame.Origin + e2 * radius; 
        //var normal = e2.VectorUnitCross(k21Vector);

        //ActiveSceneComposer.AddDisc(
        //    "curveFrameCircleSurface",
        //    center,
        //    normal,
        //    radius,
        //    Color.YellowGreen.SetAlpha(0.2f),
        //    0.035d
        //);

        //ActiveSceneComposer.AddVector(
        //    "curveFrameCircleNormal",
        //    center,
        //    normal,
        //    Color.Brown,
        //    0.05d
        //);

        //ActiveSceneComposer.AddLaTeXText(
        //    "kVectorText",
        //    ImageSet["latex", "kVectorText"],
        //    center + normal * 1.25d,
        //    LaTeXScalingFactor
        //);

        ////var material = scene.AddStandardMaterial(
        ////    "curveFrameCircleMaterial",
        ////    Color.Yellow
        ////);

        //ActiveSceneComposer.AddCircleCurve(
        //    "curveFrameCircleCurve",
        //    center,
        //    normal,
        //    radius,
        //    Color.Yellow,
        //    0.035d
        //);
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
                -5 * LinFloat64Vector3D.E2,
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

    private Image[] RenderSignalTextLaTeXImages()
    {
        var imageArray = 
            FrameRange
                .Select(RenderSignalTextLaTeXImage)
                .ToImmutableArray();

        var maxWidth = imageArray.Max(image => image.Width);

        var imageList = new Image[imageArray.Length];
        for (var i = 0; i < imageArray.Length; i++)
        {
            var image = imageArray[i];
            var delta = maxWidth - image.Width;

            imageList[i] = 
                delta == 0
                ? image
                : image.CloneExpandFromRight(delta, Color.Transparent);
        }

        return imageList;
    }

    private Image RenderSignalTextLaTeXImage(int imageSampleIndex)
    {
        var signalSampleIndex = ImageSampleIndexToSignalSampleIndex(imageSampleIndex);

        var katexComposer = new WclKaTeXComposer(WorkingFolder)
        {
            FontSizeEm = 2,
            Output = WclKaTeXComposer.OutputKind.Html,
            ThrowOnError = false,
            SaveImages = false
        };

        var t = Signal.TimeValues[signalSampleIndex];
        var (x, y, z) = 
            Signal.GetComponentVectors(t);
        var v = x + y + z;

        var frame = Signal.FrameList![signalSampleIndex]; //GetSignalFrame(t);

        var e1 = frame.Direction1.ToUnitLinVector3D();
        var e2 = frame.Direction2.ToUnitLinVector3D();
        var e3 = frame.Direction3.ToUnitLinVector3D();
        
        var sDt = Signal.GetTangentNormValue(t);

        var (kappa1, kappa2) = Signal.CurvatureList![signalSampleIndex];

        var omega = Signal.DarbouxBivectorList![signalSampleIndex];
        var omegaNorm = omega.Norm();

        var omegaMean = Signal.GetDarbouxBivectorMean(
            Math.Max(0, signalSampleIndex - MeanSampleCount),
            signalSampleIndex
        );
        var omegaMeanNorm = omegaMean.Norm();

        var frequencyHz = omegaNorm / (Math.Tau);
        var frequencyHzMean = omegaMeanNorm / (Math.Tau);

        var e1Ds = kappa1 * e2;
        var e2Ds = kappa2 * e3 - kappa1 * e1;
        var e3Ds = -kappa2 * e2;

        var kVector = omega.NormalToUnitDirection3D();
        var kVectorMean = omegaMean.NormalToUnitDirection3D();

        var imageKey = $"signalText-{signalSampleIndex:D6}";

        katexComposer.AddLaTeXAlignedEquations(
            imageKey,
            [
                new Pair<string>(@"t", @$"{t * TimeScaling:F4}"),
                new Pair<string>(@"\boldsymbol{v}\left( t \right)", @$"\left( {v.X * SignalScaling:F4}, {v.Y * SignalScaling:F4}, {v.Z * SignalScaling:F4} \right)"),
                new Pair<string>(@"\left\Vert \boldsymbol{v}^{\prime}\left(t\right)\right\Vert", @$"s^{{\prime}} \left( t \right) = {sDt * SignalScaling / TimeScaling:F4}"),
                new Pair<string>(@"\boldsymbol{e}_{1}\left( t \right)", @$"\left( {e1.X:F4}, {e1.Y:F4}, {e1.Z:F4} \right)"),
                new Pair<string>(@"\boldsymbol{e}_{2}\left( t \right)", @$"\left( {e2.X:F4}, {e2.Y:F4}, {e2.Z:F4} \right)"),
                new Pair<string>(@"\boldsymbol{e}_{3}\left( t \right)", @$"\left( {e3.X:F4}, {e3.Y:F4}, {e3.Z:F4} \right)"),
                new Pair<string>(@"\left\Vert \boldsymbol{\Omega}_{1}\left(t\right)\right\Vert", @$"\left| s^{{\prime}} \kappa_{{1}} \right| = {kappa1 / TimeScaling:F4}"),
                new Pair<string>(@"\left\Vert \boldsymbol{\Omega}_{2}\left(t\right)\right\Vert", @$"\left| s^{{\prime}} \kappa_{{2}} \right| = {kappa2 / TimeScaling:F4}"),
                new Pair<string>(@"\left\Vert \boldsymbol{\Omega}\left(t\right)\right\Vert", @$"\left| s^{{\prime}} \right| \sqrt{{\kappa_{{1}}^{{2}}+\kappa_{{2}}^{{2}}}} = {omegaNorm / TimeScaling:F4}"),
                new Pair<string>(@"f \left( t \right)", $@"{frequencyHz / TimeScaling:F4} \textrm{{ Hz}}"),
                new Pair<string>(@"\hat{\boldsymbol{k}}\left( t \right)", @$"\left( {kVector.X:F4}, {kVector.Y:F4}, {kVector.Z:F4} \right)"),
                new Pair<string>(@"\overline{f}_{T}", $@"{frequencyHzMean / TimeScaling:F4} \textrm{{ Hz}}"),
                new Pair<string>(@"\overline{\boldsymbol{k}}_{T}", @$"\left( {kVectorMean.X:F4}, {kVectorMean.Y:F4}, {kVectorMean.Z:F4} \right)")
            ]
        );

        katexComposer.RenderKaTeX();

        return katexComposer[imageKey].PngImage;
    }
    
    private Image RenderOmegaScene(int imageSampleIndex)
    {
        var signalSampleIndex = ImageSampleIndexToSignalSampleIndex(imageSampleIndex);

        if (Signal.FrameList is null || Signal.CurvatureList is null || Signal.SampledCurve is null)
            throw new InvalidOperationException();

        var omegaSceneComposer = new GrPovRaySceneComposer(
            GetOutputPath(),
            GetFrameName(signalSampleIndex), 
            new GrPovRayRenderingOptions(DefaultRenderingOptions)
            {
                Width = (0.5 * CameraSpecs.CanvasHeight).FloorToInt32(),
                Height = (0.5 * CameraSpecs.CanvasHeight).FloorToInt32(),
                Display = false
            }
        ).AddBackground(Color.Transparent);
        
        var scene = omegaSceneComposer.SceneObject;
        
        // Display bivector omega = kappa1 e12 + kappa2 e23
        var (kappa1, kappa2) = Signal.CurvatureList[signalSampleIndex];

        var curveFrame = Signal.FrameList[signalSampleIndex];

        var origin = curveFrame.Origin;
        var e1 = curveFrame.Direction1; //LinFloat64Vector3D.E1;
        var e2 = curveFrame.Direction2; //LinFloat64Vector3D.E2;
        var e3 = curveFrame.Direction3; //LinFloat64Vector3D.E3;

        var e1Ds = kappa1 * e2;
        var e2Ds = kappa2 * e3 - kappa1 * e1;
        var e3Ds = -kappa2 * e2;
        
        //scene.Camera = GrPovRayCamera.ArcRotateOrthographic(
        //    135.DegreesToPolarAngle(),
        //    45.DegreesToPolarAngle(),
        //    10,
        //    OmegaFrameOrigin, 
        //    4,
        //    4
        //);

        //var (alpha, beta, _) = 
        //    GetCameraAlphaBetaDistanceAtFrame(imageSampleIndex);

        scene.Camera = GrPovRayCamera.ArcRotatePerspective(
            135.DegreesToPolarAngle(),
            45.DegreesToPolarAngle(),
            3,
            origin, 
            67.DegreesToPolarAngle(),
            1
        );

        scene.AddStatement(
            GrPovRayLightSource.PointLight(
                scene.Camera.Position,
                Color.White
            )
        );

        omegaSceneComposer.AddElement(
            GrVisualFrame3D.Create(
                "axisFrame",
                new GrVisualFrameStyle3D
                {
                    OriginStyle =
                        Color.DarkGray
                            .ToPovRayMaterial()
                            .CreateThickSurfaceStyle(0.03),

                    Direction1Style =
                        Color.DarkRed
                            .ToPovRayMaterial()
                            .CreateTubeCurveStyle(0.02),

                    Direction2Style =
                        Color.DarkGreen
                            .ToPovRayMaterial()
                            .CreateTubeCurveStyle(0.02),

                    Direction3Style =
                        Color.DarkBlue
                            .ToPovRayMaterial()
                            .CreateTubeCurveStyle(0.02)
                },
                origin,
                LinFloat64Vector3D.E1, 
                LinFloat64Vector3D.E2,
                LinFloat64Vector3D.E3,
                Float64SamplingSpecs.Static
            )
        );

        omegaSceneComposer.AddElement(
            GrVisualFrame3D.Create(
                "curveFrame",
                new GrVisualFrameStyle3D
                {
                    OriginStyle =
                        Color.DarkGray
                            .ToPovRayMaterial()
                            .CreateThickSurfaceStyle(0.08),

                    Direction1Style =
                        Color.DarkRed
                            .ToPovRayMaterial()
                            .CreateTubeCurveStyle(0.04),

                    Direction2Style =
                        Color.DarkGreen
                            .ToPovRayMaterial()
                            .CreateTubeCurveStyle(0.04),

                    Direction3Style =
                        Color.DarkBlue
                            .ToPovRayMaterial()
                            .CreateTubeCurveStyle(0.04)
                },
                origin,
                e1,
                e2,
                e3,
                Float64SamplingSpecs.Static
            )
        );

        omegaSceneComposer.AddLaTeXText(
            "e1VectorText",
            ImageSet["latex", "e1VectorText"],
            origin + e1 * 1.25d,
            LaTeXScalingFactor * 0.75
        ).AddLaTeXText(
            "e2VectorText",
            ImageSet["latex", "e2VectorText"],
            origin + e2 * 1.25d,
            LaTeXScalingFactor * 0.75
        ).AddLaTeXText(
            "e3VectorText",
            ImageSet["latex", "e3VectorText"],
            origin + e3 * 1.25d,
            LaTeXScalingFactor * 0.75
        );

        if (e1Ds.VectorENorm() > 0.1)
        {
            omegaSceneComposer.AddVector(
                "e1DsVector",
                origin,
                e1Ds,
                Color.IndianRed,
                0.035
            );

            omegaSceneComposer.AddLaTeXText(
                "e1DsVectorText",
                ImageSet["latex", "e1DsVectorText"],
                origin + e1Ds + e1Ds.ToUnitLinVector3D() * 0.25d,
                LaTeXScalingFactor * 0.75
            );

            omegaSceneComposer.AddParallelogram(
                "omega1Bivector",
                origin,
                e1,
                e1Ds,
                Color.IndianRed.WithAlpha(0.5)
            );

            omegaSceneComposer.AddLaTeXText(
                "omega1BivectorText",
                ImageSet["latex", "omega1BivectorText"],
                origin + (e1 + e1Ds),
                LaTeXScalingFactor * 0.75
            );
        }

        if (e2Ds.VectorENorm() > 0.1)
        {
            omegaSceneComposer.AddVector(
                "e2DsVector",
                origin,
                e2Ds,
                Color.LimeGreen,
                0.035
            );

            omegaSceneComposer.AddLaTeXText(
                "e2DsVectorText",
                ImageSet["latex", "e2DsVectorText"],
                origin + e2Ds + e2Ds.ToUnitLinVector3D() * 0.25d,
                LaTeXScalingFactor * 0.75
            );

            omegaSceneComposer.AddParallelogram(
                "omegaBivector",
                origin,
                e2,
                e2Ds,
                Color.LimeGreen.WithAlpha(0.5)
            );

            omegaSceneComposer.AddLaTeXText(
                "omega2BivectorText",
                ImageSet["latex", "omega2BivectorText"],
                origin + (e2 + e2Ds),
                LaTeXScalingFactor * 0.75
            );
        }

        if (e3Ds.VectorENorm() > 0.1)
        {
            omegaSceneComposer.AddVector(
                "e3DsVector",
                origin,
                e3Ds,
                Color.DodgerBlue,
                0.035
            );

            omegaSceneComposer.AddLaTeXText(
                "e3DsVectorText",
                ImageSet["latex", "e3DsVectorText"],
                origin + e3Ds + e3Ds.ToUnitLinVector3D() * 0.25d,
                LaTeXScalingFactor * 0.75
            );

            omegaSceneComposer.AddParallelogram(
                "omega3Bivector",
                origin,
                e3,
                e3Ds,
                Color.DodgerBlue.WithAlpha(0.5)
            );

            omegaSceneComposer.AddLaTeXText(
                "omega3BivectorText",
                ImageSet["latex", "omega3BivectorText"],
                origin + (e3 + e3Ds),
                LaTeXScalingFactor * 0.75
            );
        }

        omegaSceneComposer.RenderScene($"Omega-{imageSampleIndex:D6}");

        return Image.Load<Rgba32>(
            GetOutputFilePath($"Omega-{imageSampleIndex:D6}", "png")
        ); //.MutateSetPixelsAlphaInverseRgbMin();
    }

    protected override void PostProcessImageFile(int imageSampleIndex)
    {
        var signalSampleIndex = ImageSampleIndexToSignalSampleIndex(imageSampleIndex);

        var saveFlag = true;
        var frameImage = GetImage(imageSampleIndex);
        
        var omegaImage = RenderOmegaScene(imageSampleIndex);
        frameImage.MutateDrawImage(
            omegaImage,
            0,
            0,
            1
        );
        

        if (ShowCopyright)
        {
            var copyrightImage =
                ImageSet["gui", "Copyright"]
                    .GetImage()
                    .CloneSetWidth(
                        (0.4 * frameImage.Width).FloorToInt32(),
                        KnownResamplers.CatmullRom
                    );

            frameImage.MutateDrawImage(
                copyrightImage,
                frameImage.Height - copyrightImage.Height - 16,
                16,
                0.9
            );

            saveFlag = true;
        }

        if (ShowGuiLayer)
        {
            if (ShowRightPanel)
            {
                var signalAnalyzer = Signal.CreateAnalyzer();

                var panelWidth = (0.25 * frameImage.Width).FloorToInt32();
                var signalPlotImage = signalAnalyzer.GetSignalPlotImage(signalSampleIndex, PlotSampleCount).MutateSetWidth(panelWidth, KnownResamplers.CatmullRom);
                var curvaturesPlotImage = signalAnalyzer.GetCurvaturesPlotImage(signalSampleIndex, PlotSampleCount).MutateSetWidth(panelWidth, KnownResamplers.CatmullRom);
                var frequencyHzPlotImage = signalAnalyzer.GetFrequencyHzPlotImage("Frequency", signalSampleIndex, PlotSampleCount).MutateSetWidth(panelWidth, KnownResamplers.CatmullRom);
                var dataLaTeXImage = SignalTextLaTeXImages[imageSampleIndex - FrameRange.MinValue].MutateSetWidth(panelWidth, KnownResamplers.CatmullRom);
                //var dataLaTeXImage = RenderSignalTextLaTeXImage(imageSampleIndex).MutateSetWidth(panelWidth, KnownResamplers.CatmullRom);
                
                var rightImage = 
                    signalPlotImage
                        .CloneExtendFromBottom(curvaturesPlotImage, 16, Color.Transparent)
                        .CloneExtendFromBottom(frequencyHzPlotImage, 16, Color.Transparent)
                        .CloneExtendFromBottom(dataLaTeXImage, 16, Color.Transparent);

                frameImage.MutateDrawImage(
                    rightImage,
                    16,
                    frameImage.Width - panelWidth - 16,
                    1
                );

                saveFlag = true;
            }
        }

        if (saveFlag)
        {
            frameImage.SaveAsPng(
                GetImageFilePath(imageSampleIndex),
                new PngEncoder()
                {
                    ColorType = PngColorType.RgbWithAlpha
                }
            );
        }
    }
}