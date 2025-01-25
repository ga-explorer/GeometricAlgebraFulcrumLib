using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space3D.Curves;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space3D.Curves.Adaptive;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Constants;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.GUI;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Animations;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Basic;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Curves;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Grids;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Styles;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Surfaces;
using GeometricAlgebraFulcrumLib.Modeling.Signals;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Composers;

public sealed class GrBabylonJsDifferentialCurveAnimationComposer :
    GrBabylonJsAnimationComposer
{
    public IParametricC2Curve3D Curve { get; }

    public ComputedParametricCurve3D CurveDerivative1 { get; }

    public ComputedParametricCurve3D CurveDerivative2 { get; }

    public ComputedParametricCurve3D CurveDerivative3 { get; }


    public GrBabylonJsDifferentialCurveAnimationComposer(string workingFolder, int frameRate, double maxTime, IParametricC2Curve3D curve)
        : this(workingFolder, Float64SamplingSpecs.Create(frameRate, maxTime), curve)
    {

    }

    public GrBabylonJsDifferentialCurveAnimationComposer(string workingFolder, Float64SamplingSpecs samplingSpecs, IParametricC2Curve3D curve)
        : base(workingFolder, samplingSpecs)
    {
        Curve = curve;

        CurveDerivative1 = ComputedParametricCurve3D.Create(
            time =>
                Curve
                    .GetArcLengthDerivative1Point(time)
                    .ToUnitLinVector3D(false)
        );

        CurveDerivative2 = ComputedParametricCurve3D.Create(
            time =>
                Curve
                    .GetArcLengthDerivative2Point(time)
                    .ToUnitLinVector3D(false)
        );

        CurveDerivative3 = ComputedParametricCurve3D.Create(
            time =>
                Curve
                    .GetArcLengthDerivative3Point(time)
                    .ToUnitLinVector3D(false)
        );
    }


    protected override void AddTemporalValues()
    {

    }

    protected override void AddImageTextures()
    {
        if (ShowCopyright)
        {
            TextureSet.AddTextureFromPngFile(
                "gui",
                "Copyright"
            );
        }
    }

    protected override void AddLaTeXTextures()
    {
        KaTeXComposer.AddLaTeXCode(
            "basis1VectorText",
            @"\boldsymbol{\sigma}_{1}"
        );

        KaTeXComposer.AddLaTeXCode(
            "basis2VectorText",
            @"\boldsymbol{\sigma}_{2}"
        );

        KaTeXComposer.AddLaTeXCode(
            "basis3VectorText",
            @"\boldsymbol{\sigma}_{3}"
        );

        KaTeXComposer.AddLaTeXCode(
            "v1VectorText",
            @"\boldsymbol{v}_{1}"
        );

        KaTeXComposer.AddLaTeXCode(
            "v2VectorText",
            @"\boldsymbol{v}_{2}"
        );

        KaTeXComposer.AddLaTeXCode(
            "v3VectorText",
            @"\boldsymbol{v}_{3}"
        );

        KaTeXComposer.AddLaTeXCode(
            "vVectorText",
            @"\boldsymbol{v}"
        );

        KaTeXComposer.AddLaTeXCode(
            "u1VectorText",
            @"\boldsymbol{u}_{1}"
        );

        KaTeXComposer.AddLaTeXCode(
            "u2VectorText",
            @"\boldsymbol{u}_{2}"
        );

        KaTeXComposer.AddLaTeXCode(
            "u3VectorText",
            @"\boldsymbol{u}_{3}"
        );

        KaTeXComposer.AddLaTeXCode(
            "e1VectorText",
            @"\boldsymbol{e}_{1}"
        );

        KaTeXComposer.AddLaTeXCode(
            "e2VectorText",
            @"\boldsymbol{e}_{2}"
        );

        KaTeXComposer.AddLaTeXCode(
            "e3VectorText",
            @"\boldsymbol{e}_{3}"
        );

        KaTeXComposer.AddLaTeXCode(
            "kVectorText",
            @"\hat{\boldsymbol{k}}"
        );

        KaTeXComposer.AddLaTeXCode(
            "e1DsVectorText",
            @"\dot{\boldsymbol{e}}_{1}"
        );

        KaTeXComposer.AddLaTeXCode(
            "e2DsVectorText",
            @"\dot{\boldsymbol{e}}_{2}"
        );

        KaTeXComposer.AddLaTeXCode(
            "e3DsVectorText",
            @"\dot{\boldsymbol{e}}_{3}"
        );

        KaTeXComposer.AddLaTeXCode(
            "omega1BivectorText",
            @"\boldsymbol{\Omega}_{1}"
        );

        KaTeXComposer.AddLaTeXCode(
            "omega2BivectorText",
            @"\boldsymbol{\Omega}_{2}"
        );

        KaTeXComposer.AddLaTeXCode(
            "omega3BivectorText",
            @"\boldsymbol{\Omega}_{3}"
        );
    }

    protected override void AddGuiLayer()
    {
        if (ShowCopyright)
        {
            // Add GUI layer
            var uiTexture = Scene.AddGuiFullScreenUi("uiTexture");

            var copyrightImage = TextureSet["gui", "Copyright"];
            var copyrightImageWidth = 0.5d * CodeFilesComposer.CanvasWidth;
            var copyrightImageHeight = 0.5d * CodeFilesComposer.CanvasWidth * copyrightImage.ImageHeightToWidth;

            uiTexture.AddGuiImage(
                "copyrightImage",
                copyrightImage.GetImageUrl(),
                new GrBabylonJsGuiImageProperties
                {
                    Stretch = GrBabylonJsImageStretch.Uniform,
                    //Alpha = 0.75d,
                    WidthInPixels = copyrightImageWidth,
                    HeightInPixels = copyrightImageHeight,
                    PaddingLeftInPixels = 10,
                    PaddingBottomInPixels = 10,
                    HorizontalAlignment = GrBabylonJsHorizontalAlignment.Left,
                    VerticalAlignment = GrBabylonJsVerticalAlignment.Bottom,
                }
            );
        }
    }

    protected override void ComposeScene()
    {
        var redMaterial =
            Color.Red.ToBabylonJsSimpleMaterial("redMaterial");

        var greenMaterial =
            Color.Green.ToBabylonJsSimpleMaterial("greenMaterial");

        var blueMaterial =
            Color.Blue.ToBabylonJsSimpleMaterial("blueMaterial");

        var orangeMaterial =
            Color.DarkOrange.ToBabylonJsSimpleMaterial("orangeMaterial");

        SceneComposer.AddMaterials(
            redMaterial,
            greenMaterial,
            blueMaterial,
            orangeMaterial
        );

        AddGrid(
            GrVisualSquareGrid3D.DefaultZx(
                LinFloat64Vector3D.Zero,
                16,
                1,
                0.5
            )
        );

        AddAxes(
            LinFloat64Vector3D.Create(-6, 0, -6)
        );

        var (v1AnimatedVector, v2AnimatedVector, v3AnimatedVector) =
            Curve
                .GetComponentCurves()
                .MapItems(curve =>
                    SamplingSpecs.CreateAnimatedVector3D(curve)
                );

        var v12AnimatedVector =
            v1AnimatedVector + v2AnimatedVector;

        var v23AnimatedVector =
            v2AnimatedVector + v3AnimatedVector;

        var v31AnimatedVector =
            v3AnimatedVector + v1AnimatedVector;

        var vAnimatedVector =
            SamplingSpecs.CreateAnimatedVector3D(Curve);

        var u1AnimatedVector =
            SamplingSpecs.CreateAnimatedVector3D(CurveDerivative1);

        var u2AnimatedVector =
            SamplingSpecs.CreateAnimatedVector3D(CurveDerivative2);

        var u3AnimatedVector =
            SamplingSpecs.CreateAnimatedVector3D(CurveDerivative3);


        SceneComposer.AddVector(
            GrVisualVector3D.CreateAnimated(
                "vVector",
                orangeMaterial.CreateTubeCurveStyle(0.05),
                vAnimatedVector
            )
        );

        SceneComposer.AddLaTeXText(
            "vVectorText",
            TextureSet["latex", "vVectorText"],
            vAnimatedVector.AddLength(0.25),
            CodeFilesComposer.LaTeXScalingFactor
        );


        SceneComposer.AddVector(
            GrVisualVector3D.CreateAnimated(
                "v1Vector",
                redMaterial.CreateTubeCurveStyle(0.05),
                v1AnimatedVector
            )
        );

        SceneComposer.AddLaTeXText(
            "v1VectorText",
            TextureSet["latex", "v1VectorText"],
            v1AnimatedVector.AddLength(0.25),
            CodeFilesComposer.LaTeXScalingFactor
        );


        SceneComposer.AddVector(
            GrVisualVector3D.CreateAnimated(
                "v2Vector",
                greenMaterial.CreateTubeCurveStyle(0.05),
                v2AnimatedVector
            )
        );

        SceneComposer.AddLaTeXText(
            "v2VectorText",
            TextureSet["latex", "v2VectorText"],
            v2AnimatedVector.AddLength(0.25),
            CodeFilesComposer.LaTeXScalingFactor
        );


        SceneComposer.AddVector(
            GrVisualVector3D.CreateAnimated(
                "v3Vector",
                blueMaterial.CreateTubeCurveStyle(0.05),
                v3AnimatedVector
            )
        );

        SceneComposer.AddLaTeXText(
            "v3VectorText",
            TextureSet["latex", "v3VectorText"],
            v3AnimatedVector.AddLength(0.25),
            CodeFilesComposer.LaTeXScalingFactor
        );


        var dashedLinesStyle =
            Color.DarkOrange.CreateDashedLineCurveStyle(3, 2, 16);

        SceneComposer.AddLineSegment(
            GrVisualLineSegment3D.CreateAnimated(
                "line12v1",
                dashedLinesStyle,
                v12AnimatedVector,
                v1AnimatedVector
            )
        );

        SceneComposer.AddLineSegment(
            GrVisualLineSegment3D.CreateAnimated(
                "line12v2",
                dashedLinesStyle,
                v12AnimatedVector,
                v2AnimatedVector
            )
        );

        SceneComposer.AddLineSegment(
            GrVisualLineSegment3D.CreateAnimated(
                "line23v2",
                dashedLinesStyle,
                v23AnimatedVector,
                v2AnimatedVector
            )
        );

        SceneComposer.AddLineSegment(
            GrVisualLineSegment3D.CreateAnimated(
                "line23v3",
                dashedLinesStyle,
                v23AnimatedVector,
                v3AnimatedVector
            )
        );

        SceneComposer.AddLineSegment(
            GrVisualLineSegment3D.CreateAnimated(
                "line31v3",
                dashedLinesStyle,
                v31AnimatedVector,
                v3AnimatedVector
            )
        );

        SceneComposer.AddLineSegment(
            GrVisualLineSegment3D.CreateAnimated(
                "line31v1",
                dashedLinesStyle,
                v31AnimatedVector,
                v1AnimatedVector
            )
        );

        SceneComposer.AddLineSegment(
            GrVisualLineSegment3D.CreateAnimated(
                "line12v",
                dashedLinesStyle,
                v12AnimatedVector,
                vAnimatedVector
            )
        );

        SceneComposer.AddLineSegment(
            GrVisualLineSegment3D.CreateAnimated(
                "line23v",
                dashedLinesStyle,
                v23AnimatedVector,
                vAnimatedVector
            )
        );

        SceneComposer.AddLineSegment(
            GrVisualLineSegment3D.CreateAnimated(
                "line31v",
                dashedLinesStyle,
                v31AnimatedVector,
                vAnimatedVector
            )
        );


        SceneComposer.AddLinePath(
            GrVisualPointPathCurve3D.CreateStatic(
                "curvePath",
                orangeMaterial.CreateTubeCurveStyle(0.025),
                Curve.CreateAdaptiveCurve3D(
                    SamplingSpecs.TimeRange,
                    new AdaptiveCurveSamplingOptions3D(
                        3.DegreesToDirectedAngle(),
                        1,
                        12
                    )
                )
            )
        );


        var curveFrame = GrVisualFrame3D.CreateAnimated(
            "curveFrame",
            new GrVisualFrameStyle3D
            {
                OriginStyle = orangeMaterial.CreateThickSurfaceStyle(0.065),
                Direction1Style = redMaterial.CreateTubeCurveStyle(0.05),
                Direction2Style = greenMaterial.CreateTubeCurveStyle(0.05),
                Direction3Style = blueMaterial.CreateTubeCurveStyle(0.05)
            },
            vAnimatedVector,
            u1AnimatedVector,
            u2AnimatedVector,
            u3AnimatedVector
        );

        SceneComposer.AddFrame(curveFrame);

        SceneComposer.AddLaTeXText(
            "u1VectorText",
            TextureSet["latex", "u1VectorText"],
            curveFrame.AnimatedOrigin +
            curveFrame.AnimatedDirection1.AddLength(0.25),
            CodeFilesComposer.LaTeXScalingFactor
        );

        SceneComposer.AddLaTeXText(
            "u2VectorText",
            TextureSet["latex", "u2VectorText"],
            curveFrame.AnimatedOrigin +
            curveFrame.AnimatedDirection2.AddLength(0.25),
            CodeFilesComposer.LaTeXScalingFactor
        );

        SceneComposer.AddLaTeXText(
            "u3VectorText",
            TextureSet["latex", "u3VectorText"],
            curveFrame.AnimatedOrigin +
            curveFrame.AnimatedDirection3.AddLength(0.25),
            CodeFilesComposer.LaTeXScalingFactor
        );

        var curveFrameBoundsMaterial =
            SceneComposer.AddOrGetColorMaterial(
                Color.Orange.WithAlpha(0.25f)
            );

        //MainSceneComposer.AddParallelepipedSurface(
        //    GrVisualParallelepipedSurface3D.CreateAnimated(
        //        "curveBounds",
        //        curveFrameBoundsMaterial.CreateThinSurfaceStyle(),
        //        Float64Tuple3D.Zero, 
        //        v1AnimatedVector,
        //        v2AnimatedVector,
        //        v3AnimatedVector,
        //        samplingSpecs
        //    )
        //);

        var quaternionCurve =
            Curve.GetFrenetFrameRotationQuaternionsCurve();

        var e1AnimatedVector =
            SamplingSpecs.CreateAnimatedVector3D(ComputedParametricCurve3D.Create(
                time => quaternionCurve.GetQuaternion(time).RotateVector(LinBasisVector3D.Px)
            ));

        var e2AnimatedVector =
            SamplingSpecs.CreateAnimatedVector3D(ComputedParametricCurve3D.Create(
                time => quaternionCurve.GetQuaternion(time).RotateVector(LinBasisVector3D.Py)
            ));

        var e3AnimatedVector =
            SamplingSpecs.CreateAnimatedVector3D(ComputedParametricCurve3D.Create(
                time => quaternionCurve.GetQuaternion(time).RotateVector(LinBasisVector3D.Pz)
            ));

        SceneComposer.AddParallelepipedSurface(
            GrVisualParallelepipedSurface3D.CreateAnimated(
                "curveFrameBounds",
                curveFrameBoundsMaterial.CreateThinSurfaceStyle(),
                vAnimatedVector,
                e1AnimatedVector,
                e2AnimatedVector,
                e3AnimatedVector
            )
        );

    }
}