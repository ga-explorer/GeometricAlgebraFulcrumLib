using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Elements;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space1D.Scalars;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space2D.Bivectors;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space2D.Curves;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space3D.Bivectors;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space3D.Curves;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space3D.Trivectors;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Visualizer;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Composers;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Animations;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Basic;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Curves;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Styles;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Surfaces;
using GeometricAlgebraFulcrumLib.Modeling.Signals;
using GeometricAlgebraFulcrumLib.Modeling.Temporal.Float64.Scalars;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Web.Colors;
using GeometricAlgebraFulcrumLib.Utilities.Web.LaTeX.KaTeX;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Visualizer;

public class CGaFloat64Visualizer
{
    public CGaFloat64GeometricSpace GeometricSpace { get; }

    public CGaFloat64VisualizerDirectionStyle DirectionStyle { get; }

    public CGaFloat64VisualizerTangentStyle TangentStyle { get; }

    public CGaFloat64VisualizerFlatStyle FlatStyle { get; }

    public CGaFloat64VisualizerRoundStyle RoundStyle { get; }

    public GrBabylonJsGeometryAnimationComposer AnimationComposer { get; set; }

    public Float64SamplingSpecs SamplingSpecs 
        => AnimationComposer.SamplingSpecs;

    public GrBabylonJsSceneComposer SceneComposer 
        => AnimationComposer.SceneComposer;

    public WclKaTeXComposer KaTeXComposer 
        => AnimationComposer.KaTeXComposer;


    internal CGaFloat64Visualizer(CGaFloat64GeometricSpace cgaGeometricSpace)
    {
        GeometricSpace = cgaGeometricSpace;

        DirectionStyle = new CGaFloat64VisualizerDirectionStyle(this, 0.08);
        TangentStyle = new CGaFloat64VisualizerTangentStyle(this, 0.08);
        FlatStyle = new CGaFloat64VisualizerFlatStyle(this, 0.08, 6);
        RoundStyle = new CGaFloat64VisualizerRoundStyle(this, 0.08);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected IReadOnlyList<int> GetInvalidFrameIndices(CGaFloat64ParametricElement element, CGaFloat64ElementKind kind, int egaDirectionGrade)
    {
        return SamplingSpecs
            .GetSampleIndexValuePairs(element.GetElement)
            .Where(indexElement =>
                indexElement.Value.Specs.Kind != kind ||
                indexElement.Value.Specs.VGaDirectionGrade != egaDirectionGrade
            ).SelectToImmutableArray(
                indexElement => indexElement.Key
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected IReadOnlyList<int> GetDirectionPointInvalidFrameIndices(CGaFloat64ParametricElement element)
    {
        return GetInvalidFrameIndices(
            element,
            CGaFloat64ElementKind.Direction,
            0
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected IReadOnlyList<int> GetDirectionLineInvalidFrameIndices(CGaFloat64ParametricElement element)
    {
        return GetInvalidFrameIndices(
            element,
            CGaFloat64ElementKind.Direction,
            1
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected IReadOnlyList<int> GetDirectionPlaneInvalidFrameIndices(CGaFloat64ParametricElement element)
    {
        return GetInvalidFrameIndices(
            element,
            CGaFloat64ElementKind.Direction,
            2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected IReadOnlyList<int> GetDirectionVolumeInvalidFrameIndices(CGaFloat64ParametricElement element)
    {
        return GetInvalidFrameIndices(
            element,
            CGaFloat64ElementKind.Direction,
            3
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected IReadOnlyList<int> GetTangentPointInvalidFrameIndices(CGaFloat64ParametricElement element)
    {
        return GetInvalidFrameIndices(
            element,
            CGaFloat64ElementKind.Tangent,
            0
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected IReadOnlyList<int> GetTangentLineInvalidFrameIndices(CGaFloat64ParametricElement element)
    {
        return GetInvalidFrameIndices(
            element,
            CGaFloat64ElementKind.Tangent,
            1
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected IReadOnlyList<int> GetTangentPlaneInvalidFrameIndices(CGaFloat64ParametricElement element)
    {
        return GetInvalidFrameIndices(
            element,
            CGaFloat64ElementKind.Tangent,
            2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected IReadOnlyList<int> GetTangentVolumeInvalidFrameIndices(CGaFloat64ParametricElement element)
    {
        return GetInvalidFrameIndices(
            element,
            CGaFloat64ElementKind.Tangent,
            3
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected IReadOnlyList<int> GetFlatPointInvalidFrameIndices(CGaFloat64ParametricElement element)
    {
        return GetInvalidFrameIndices(
            element,
            CGaFloat64ElementKind.Flat,
            0
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected IReadOnlyList<int> GetFlatLineInvalidFrameIndices(CGaFloat64ParametricElement element)
    {
        return GetInvalidFrameIndices(
            element,
            CGaFloat64ElementKind.Flat,
            1
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected IReadOnlyList<int> GetFlatPlaneInvalidFrameIndices(CGaFloat64ParametricElement element)
    {
        return GetInvalidFrameIndices(
            element,
            CGaFloat64ElementKind.Flat,
            2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected IReadOnlyList<int> GetFlatVolumeInvalidFrameIndices(CGaFloat64ParametricElement element)
    {
        return GetInvalidFrameIndices(
            element,
            CGaFloat64ElementKind.Flat,
            3
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected IReadOnlyList<int> GetRoundPointInvalidFrameIndices(CGaFloat64ParametricElement element)
    {
        return GetInvalidFrameIndices(
            element,
            CGaFloat64ElementKind.Round,
            0
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected IReadOnlyList<int> GetRoundPointPairInvalidFrameIndices(CGaFloat64ParametricElement element)
    {
        return GetInvalidFrameIndices(
            element,
            CGaFloat64ElementKind.Round,
            1
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected IReadOnlyList<int> GetRoundCircleInvalidFrameIndices(CGaFloat64ParametricElement element)
    {
        return GetInvalidFrameIndices(
            element,
            CGaFloat64ElementKind.Round,
            2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected IReadOnlyList<int> GetRoundSphereInvalidFrameIndices(CGaFloat64ParametricElement element)
    {
        return GetInvalidFrameIndices(
            element,
            CGaFloat64ElementKind.Round,
            3
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Visualizer SetAnimationComposer(GrBabylonJsGeometryAnimationComposer animationComposer)
    {
        AnimationComposer = animationComposer;

        return this;
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Visualizer BeginDrawing2D(string workingFolder, Float64SamplingSpecs samplingSpecs)
    {
        return BeginDrawing2D(
            new GrBabylonJsGeometryAnimationComposer(workingFolder, samplingSpecs)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Visualizer BeginDrawing2D(string workingFolder, int samplingRate = 1, double maxTime = 1)
    {
        return BeginDrawing2D(
            new GrBabylonJsGeometryAnimationComposer(workingFolder, samplingRate, maxTime)
        );
    }

    public CGaFloat64Visualizer BeginDrawing2D(GrBabylonJsGeometryAnimationComposer animationComposer)
    {
        SetAnimationComposer(animationComposer);

        AnimationComposer.BeginDrawing2D();

        DirectionStyle.SetStyle(0.08);
        TangentStyle.SetStyle(0.08);
        FlatStyle.SetStyle(0.08, 6);
        RoundStyle.SetStyle(0.08);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Visualizer BeginDrawing3D(string workingFolder, Float64SamplingSpecs samplingSpecs)
    {
        return BeginDrawing3D(
            new GrBabylonJsGeometryAnimationComposer(workingFolder, samplingSpecs)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Visualizer BeginDrawing3D(string workingFolder, int samplingRate = 1, double maxTime = 1)
    {
        return BeginDrawing3D(
            new GrBabylonJsGeometryAnimationComposer(workingFolder, samplingRate, maxTime)
        );
    }

    public CGaFloat64Visualizer BeginDrawing3D(GrBabylonJsGeometryAnimationComposer animationComposer)
    {
        SetAnimationComposer(animationComposer);

        AnimationComposer.BeginDrawing3D();

        DirectionStyle.SetStyle(0.08);
        TangentStyle.SetStyle(0.08);
        FlatStyle.SetStyle(0.08, 6);
        RoundStyle.SetStyle(0.08);

        return this;
    }


    public CGaFloat64Visualizer SetDirectionStyle(double thickness, bool drawPosition = true, double directionRadius = 1, double normalDirectionRadius = 1, double auxGeometryColorAlpha = 0.5)
    {
        DirectionStyle.SetStyle(
            thickness,
            drawPosition,
            directionRadius,
            normalDirectionRadius,
            auxGeometryColorAlpha
        );

        return this;
    }

    public CGaFloat64Visualizer SetTangentStyle(double thickness, bool drawPosition = true, double directionRadius = 1, double normalDirectionRadius = 1, double auxGeometryColorAlpha = 0.5)
    {
        TangentStyle.SetStyle(
            thickness,
            drawPosition,
            directionRadius,
            normalDirectionRadius,
            auxGeometryColorAlpha
        );

        return this;
    }

    public CGaFloat64Visualizer SetFlatStyle(double thickness, double radius, bool drawPosition = true, double directionRadius = 1, double normalDirectionRadius = 1, double auxGeometryColorAlpha = 0.5)
    {
        FlatStyle.SetStyle(
            thickness,
            radius,
            drawPosition,
            directionRadius,
            normalDirectionRadius,
            auxGeometryColorAlpha
        );

        return this;
    }

    public CGaFloat64Visualizer SetRoundStyle(double thickness, bool drawCenter = true, bool drawSphere = true, double directionRadius = 1, double normalDirectionRadius = 1, double auxGeometryColorAlpha = 0.5)
    {
        RoundStyle.SetStyle(
            thickness,
            drawCenter,
            drawSphere,
            directionRadius,
            normalDirectionRadius,
            auxGeometryColorAlpha
        );

        return this;
    }


    public CGaFloat64Visualizer DrawDirectionPoint2D(Color color, LinFloat64Vector2D position)
    {
        SceneComposer.AddPoint(
            GrVisualPoint3D.CreateStatic(
                AnimationComposer.GetNewSceneObjectName("point"),
                DirectionStyle.GetPointVisualStyle(color),
                position.ToXyLinVector3D()
            )
        );

        return this;
    }

    public CGaFloat64Visualizer DrawDirectionPoint2D(Color color, IFloat64ParametricCurve2D position)
    {
        SceneComposer.AddPoint(
            GrVisualPoint3D.CreateAnimated(
                AnimationComposer.GetNewSceneObjectName("point"),
                DirectionStyle.GetPointVisualStyle(color),
                SamplingSpecs.CreateXyAnimatedVector3D(position)
            )
        );

        return this;
    }

    public CGaFloat64Visualizer DrawDirectionPoint2D(Color color, CGaFloat64ParametricElement element)
    {
        var animatedPosition =
            SamplingSpecs.CreateXyAnimatedVector3D(
                element.PositionToParametricCurve2D()
            );

        SceneComposer.AddPoint(
            GrVisualPoint3D.CreateAnimated(
                AnimationComposer.GetNewSceneObjectName("point"),
                DirectionStyle.GetPointVisualStyle(color),
                animatedPosition
            )
        );

        return this;
    }


    public CGaFloat64Visualizer DrawDirectionPoint3D(Color color, LinFloat64Vector3D position)
    {
        SceneComposer.AddPoint(
            GrVisualPoint3D.CreateStatic(
                AnimationComposer.GetNewSceneObjectName("point"),
                DirectionStyle.GetPointVisualStyle(color),
                position
            )
        );

        return this;
    }

    public CGaFloat64Visualizer DrawDirectionPoint3D(Color color, IParametricCurve3D position)
    {
        SceneComposer.AddPoint(
            GrVisualPoint3D.CreateAnimated(
                AnimationComposer.GetNewSceneObjectName("point"),
                DirectionStyle.GetPointVisualStyle(color),
                SamplingSpecs.CreateAnimatedVector3D(position)
            )
        );

        return this;
    }

    public CGaFloat64Visualizer DrawDirectionPoint3D(Color color, CGaFloat64ParametricElement element)
    {
        var animatedPosition =
            SamplingSpecs.CreateAnimatedVector3D(
                element.PositionToParametricCurve3D()
            );

        SceneComposer.AddPoint(
            GrVisualPoint3D.CreateAnimated(
                AnimationComposer.GetNewSceneObjectName("point"),
                DirectionStyle.GetPointVisualStyle(color),
                animatedPosition
            )
        );

        return this;
    }


    public CGaFloat64Visualizer DrawDirectionLine2D(Color color, LinFloat64Vector2D position, LinFloat64Vector2D direction)
    {
        if (DirectionStyle.DrawPosition)
        {
            SceneComposer.AddPoint(
                GrVisualPoint3D.CreateStatic(
                    AnimationComposer.GetNewSceneObjectName("point"),
                    DirectionStyle.GetPointVisualStyle(
                        color.SetAlpha(DirectionStyle.AuxGeometryColorAlpha),
                        DirectionStyle.Thickness * 1.5
                    ),
                    position.ToXyLinVector3D()
                )
            );
        }

        if (DirectionStyle.DrawDirection)
        {
            SceneComposer.AddVector(
                GrVisualVector3D.CreateStatic(
                    AnimationComposer.GetNewSceneObjectName("vector"),
                    DirectionStyle.GetVectorVisualStyle(
                        color.SetAlpha(DirectionStyle.AuxGeometryColorAlpha),
                        DirectionStyle.Thickness
                    ),
                    position.ToXyLinVector3D(),
                    direction.SetLength(DirectionStyle.DirectionRadius).ToXyLinVector3D()
                )
            );
        }

        if (DirectionStyle.DrawNormalDirection)
        {
            SceneComposer.AddBivector(
                GrVisualBivector3D.CreateStatic(
                    AnimationComposer.GetNewSceneObjectName("bivector"),
                    DirectionStyle.GetVectorVisualStyle(
                        color.SetAlpha(DirectionStyle.AuxGeometryColorAlpha),
                        DirectionStyle.Thickness
                    ),
                    position.ToXyLinVector3D(),
                    direction.ToXyLinVector3D(),
                    DirectionStyle.NormalDirectionRadius
                )
            );
        }

        return this;
    }

    public CGaFloat64Visualizer DrawDirectionLine2D(Color color, IFloat64ParametricCurve2D position, IFloat64ParametricCurve2D direction)
    {
        var animatedPosition =
            SamplingSpecs.CreateXyAnimatedVector3D(position);

        var animatedDirection =
            SamplingSpecs.CreateXyAnimatedVector3D(direction);

        if (DirectionStyle.DrawPosition)
        {
            SceneComposer.AddPoint(
                GrVisualPoint3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("point"),
                    DirectionStyle.GetPointVisualStyle(
                        color.SetAlpha(DirectionStyle.AuxGeometryColorAlpha),
                        DirectionStyle.Thickness * 1.5
                    ),
                    animatedPosition
                )
            );
        }

        if (DirectionStyle.DrawDirection)
        {
            var animatedScaledVector =
                SamplingSpecs.CreateAnimatedVector3D(t =>
                    direction.GetPoint(t).SetLength(DirectionStyle.DirectionRadius).ToXyLinVector3D()
                );

            SceneComposer.AddVector(
                GrVisualVector3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("vector"),
                    DirectionStyle.GetVectorVisualStyle(
                        color.SetAlpha(DirectionStyle.AuxGeometryColorAlpha),
                        DirectionStyle.Thickness
                    ),
                    animatedPosition,
                    animatedScaledVector
                )
            );
        }

        if (DirectionStyle.DrawNormalDirection)
        {
            SceneComposer.AddBivector(
                GrVisualBivector3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("bivector"),
                    DirectionStyle.GetVectorVisualStyle(
                        color.SetAlpha(DirectionStyle.AuxGeometryColorAlpha),
                        DirectionStyle.Thickness
                    ),
                    animatedPosition,
                    animatedDirection,
                    DirectionStyle.NormalDirectionRadius
                )
            );
        }

        return this;
    }

    public CGaFloat64Visualizer DrawDirectionLine2D(Color color, CGaFloat64ParametricElement element)
    {
        var animatedPosition =
            SamplingSpecs.CreateXyAnimatedVector3D(
                element.PositionToParametricCurve2D()
            );

        var animatedDirection =
            SamplingSpecs.CreateXyAnimatedVector3D(
                element.DirectionToParametricCurve2D()
            );

        var animatedNormal =
            SamplingSpecs.CreateXyAnimatedVector3D(
                element.NormalDirectionToParametricCurve2D()
            );

        if (DirectionStyle.DrawPosition)
        {
            SceneComposer.AddPoint(
                GrVisualPoint3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("point"),
                    DirectionStyle.GetPointVisualStyle(
                        color.SetAlpha(DirectionStyle.AuxGeometryColorAlpha),
                        DirectionStyle.Thickness * 1.5
                    ),
                    animatedPosition
                )
            );
        }

        if (DirectionStyle.DrawDirection)
        {
            SceneComposer.AddVector(
                GrVisualVector3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("vector"),
                    DirectionStyle.GetVectorVisualStyle(
                        color.SetAlpha(DirectionStyle.AuxGeometryColorAlpha),
                        DirectionStyle.Thickness
                    ),
                    animatedPosition,
                    animatedDirection.SetLength(DirectionStyle.DirectionRadius)
                )
            );
        }

        if (DirectionStyle.DrawNormalDirection)
        {
            SceneComposer.AddVector(
                GrVisualVector3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("vector"),
                    RoundStyle.GetVectorVisualStyle(
                        color.SetAlpha(RoundStyle.AuxGeometryColorAlpha),
                        RoundStyle.Thickness
                    ),
                    animatedPosition,
                    animatedNormal.SetLength(RoundStyle.NormalDirectionRadius)
                )
            );
        }

        return this;
    }


    public CGaFloat64Visualizer DrawDirectionLine3D(Color color, LinFloat64Vector3D position, LinFloat64Vector3D direction)
    {
        if (DirectionStyle.DrawPosition)
        {
            SceneComposer.AddPoint(
                GrVisualPoint3D.CreateStatic(
                    AnimationComposer.GetNewSceneObjectName("point"),
                    DirectionStyle.GetPointVisualStyle(
                        color.SetAlpha(DirectionStyle.AuxGeometryColorAlpha),
                        DirectionStyle.Thickness * 1.5
                    ),
                    position
                )
            );
        }

        if (DirectionStyle.DrawDirection)
        {
            SceneComposer.AddVector(
                GrVisualVector3D.CreateStatic(
                    AnimationComposer.GetNewSceneObjectName("vector"),
                    DirectionStyle.GetVectorVisualStyle(
                        color.SetAlpha(DirectionStyle.AuxGeometryColorAlpha),
                        DirectionStyle.Thickness
                    ),
                    position,
                    direction.SetLength(DirectionStyle.DirectionRadius)
                )
            );
        }

        if (DirectionStyle.DrawNormalDirection)
        {
            SceneComposer.AddBivector(
                GrVisualBivector3D.CreateStatic(
                    AnimationComposer.GetNewSceneObjectName("bivector"),
                    DirectionStyle.GetVectorVisualStyle(
                        color.SetAlpha(DirectionStyle.AuxGeometryColorAlpha),
                        DirectionStyle.Thickness
                    ),
                    position,
                    direction,
                    DirectionStyle.NormalDirectionRadius
                )
            );
        }

        return this;
    }

    public CGaFloat64Visualizer DrawDirectionLine3D(Color color, IParametricCurve3D position, IParametricCurve3D direction)
    {
        var animatedPosition =
            SamplingSpecs.CreateAnimatedVector3D(position);

        var animatedDirection =
            SamplingSpecs.CreateAnimatedVector3D(direction);

        if (DirectionStyle.DrawPosition)
        {
            SceneComposer.AddPoint(
                GrVisualPoint3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("point"),
                    DirectionStyle.GetPointVisualStyle(
                        color.SetAlpha(DirectionStyle.AuxGeometryColorAlpha),
                        DirectionStyle.Thickness * 1.5
                    ),
                    animatedPosition
                )
            );
        }

        if (DirectionStyle.DrawDirection)
        {
            var animatedScaledVector =
                SamplingSpecs.CreateAnimatedVector3D(t =>
                    direction.GetPoint(t).SetLength(DirectionStyle.DirectionRadius)
                );

            SceneComposer.AddVector(
                GrVisualVector3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("vector"),
                    DirectionStyle.GetVectorVisualStyle(
                        color.SetAlpha(DirectionStyle.AuxGeometryColorAlpha),
                        DirectionStyle.Thickness
                    ),
                    animatedPosition,
                    animatedScaledVector
                )
            );
        }

        if (DirectionStyle.DrawNormalDirection)
        {
            SceneComposer.AddBivector(
                GrVisualBivector3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("bivector"),
                    DirectionStyle.GetVectorVisualStyle(
                        color.SetAlpha(DirectionStyle.AuxGeometryColorAlpha),
                        DirectionStyle.Thickness
                    ),
                    animatedPosition,
                    animatedDirection,
                    DirectionStyle.NormalDirectionRadius
                )
            );
        }

        return this;
    }

    public CGaFloat64Visualizer DrawDirectionLine3D(Color color, CGaFloat64ParametricElement element)
    {
        var animatedPosition =
            SamplingSpecs.CreateAnimatedVector3D(
                element.PositionToParametricCurve3D()
            );

        var animatedDirection =
            SamplingSpecs.CreateAnimatedVector3D(
                element.DirectionToParametricCurve3D()
            );

        if (DirectionStyle.DrawPosition)
        {
            SceneComposer.AddPoint(
                GrVisualPoint3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("point"),
                    DirectionStyle.GetPointVisualStyle(
                        color.SetAlpha(DirectionStyle.AuxGeometryColorAlpha),
                        DirectionStyle.Thickness * 1.5
                    ),
                    animatedPosition
                )
            );
        }

        if (DirectionStyle.DrawDirection)
        {
            SceneComposer.AddVector(
                GrVisualVector3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("vector"),
                    DirectionStyle.GetVectorVisualStyle(
                        color.SetAlpha(DirectionStyle.AuxGeometryColorAlpha),
                        DirectionStyle.Thickness
                    ),
                    animatedPosition,
                    animatedDirection.SetLength(DirectionStyle.DirectionRadius)
                )
            );
        }

        if (DirectionStyle.DrawNormalDirection)
        {
            SceneComposer.AddBivector(
                GrVisualBivector3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("bivector"),
                    DirectionStyle.GetVectorVisualStyle(
                        color.SetAlpha(DirectionStyle.AuxGeometryColorAlpha),
                        DirectionStyle.Thickness
                    ),
                    animatedPosition,
                    animatedDirection,
                    DirectionStyle.NormalDirectionRadius
                )
            );
        }

        return this;
    }


    public CGaFloat64Visualizer DrawDirectionPlane2D(Color color, LinFloat64Vector2D position, LinFloat64Bivector2D direction)
    {
        if (DirectionStyle.DrawPosition)
        {
            SceneComposer.AddPoint(
                GrVisualPoint3D.CreateStatic(
                    AnimationComposer.GetNewSceneObjectName("point"),
                    DirectionStyle.GetPointVisualStyle(
                        color.SetAlpha(DirectionStyle.AuxGeometryColorAlpha),
                        DirectionStyle.Thickness * 1.5
                    ),
                    position.ToXyLinVector3D()
                )
            );
        }

        var normal =
            direction.ToXyBivector3D().DirectionToUnitNormal3D(LinFloat64Vector3D.UnitSymmetric);

        if (DirectionStyle.DrawDirection)
        {
            SceneComposer.AddBivector(
                GrVisualBivector3D.CreateStatic(
                    AnimationComposer.GetNewSceneObjectName("bivector"),
                    DirectionStyle.GetVectorVisualStyle(
                        color.SetAlpha(DirectionStyle.AuxGeometryColorAlpha),
                        DirectionStyle.Thickness
                    ),
                    position.ToXyLinVector3D(),
                    normal,
                    DirectionStyle.NormalDirectionRadius
                )
            );
        }

        if (DirectionStyle.DrawNormalDirection)
        {
            SceneComposer.AddVector(
                GrVisualVector3D.CreateStatic(
                    AnimationComposer.GetNewSceneObjectName("vector"),
                    DirectionStyle.GetVectorVisualStyle(
                        color.SetAlpha(DirectionStyle.AuxGeometryColorAlpha),
                        DirectionStyle.Thickness
                    ),
                    position.ToXyLinVector3D(),
                    normal.SetLength(DirectionStyle.NormalDirectionRadius)
                )
            );
        }

        return this;
    }

    public CGaFloat64Visualizer DrawDirectionPlane2D(Color color, IFloat64ParametricCurve2D position, IParametricBivector2D direction)
    {
        var animatedPosition =
            SamplingSpecs.CreateXyAnimatedVector3D(position);

        if (DirectionStyle.DrawPosition)
        {
            SceneComposer.AddPoint(
                GrVisualPoint3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("point"),
                    DirectionStyle.GetPointVisualStyle(
                        color.SetAlpha(DirectionStyle.AuxGeometryColorAlpha),
                        DirectionStyle.Thickness * 1.5
                    ),
                    animatedPosition
                )
            );
        }

        var animatedNormal =
            SamplingSpecs.CreateAnimatedVector3D(
                direction
                    .ToParametricCurve3D(b =>
                        b.ToXyBivector3D().DirectionToUnitNormal3D(LinFloat64Vector3D.UnitSymmetric)
                    )
            );

        if (DirectionStyle.DrawDirection)
        {
            SceneComposer.AddBivector(
                GrVisualBivector3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("bivector"),
                    DirectionStyle.GetVectorVisualStyle(
                        color.SetAlpha(DirectionStyle.AuxGeometryColorAlpha),
                        DirectionStyle.Thickness
                    ),
                    animatedPosition,
                    animatedNormal,
                    DirectionStyle.NormalDirectionRadius
                )
            );
        }

        if (DirectionStyle.DrawNormalDirection)
        {
            SceneComposer.AddVector(
                GrVisualVector3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("vector"),
                    DirectionStyle.GetVectorVisualStyle(
                        color.SetAlpha(DirectionStyle.AuxGeometryColorAlpha),
                        DirectionStyle.Thickness
                    ),
                    animatedPosition,
                    animatedNormal.SetLength(DirectionStyle.NormalDirectionRadius)
                )
            );
        }

        return this;
    }

    public CGaFloat64Visualizer DrawDirectionPlane2D(Color color, CGaFloat64ParametricElement element)
    {
        var animatedPosition =
            SamplingSpecs.CreateXyAnimatedVector3D(
                element.PositionToParametricCurve2D()
            );

        var animatedNormal =
            SamplingSpecs.CreateAnimatedVector3D(
                element
                    .DirectionToParametricBivector2D()
                    .XyDirectionToNormal3D(LinFloat64Vector3D.UnitSymmetric)
            );

        if (DirectionStyle.DrawPosition)
        {
            SceneComposer.AddPoint(
                GrVisualPoint3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("point"),
                    DirectionStyle.GetPointVisualStyle(
                        color.SetAlpha(DirectionStyle.AuxGeometryColorAlpha),
                        DirectionStyle.Thickness * 1.5
                    ),
                    animatedPosition
                )
            );
        }

        if (DirectionStyle.DrawDirection)
        {
            SceneComposer.AddBivector(
                GrVisualBivector3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("bivector"),
                    DirectionStyle.GetVectorVisualStyle(
                        color.SetAlpha(DirectionStyle.AuxGeometryColorAlpha),
                        DirectionStyle.Thickness
                    ),
                    animatedPosition,
                    animatedNormal,
                    DirectionStyle.NormalDirectionRadius
                )
            );
        }

        if (DirectionStyle.DrawNormalDirection)
        {
            SceneComposer.AddVector(
                GrVisualVector3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("vector"),
                    DirectionStyle.GetVectorVisualStyle(
                        color.SetAlpha(DirectionStyle.AuxGeometryColorAlpha),
                        DirectionStyle.Thickness
                    ),
                    animatedPosition,
                    animatedNormal.SetLength(DirectionStyle.NormalDirectionRadius)
                )
            );
        }

        return this;
    }


    public CGaFloat64Visualizer DrawDirectionPlane3D(Color color, LinFloat64Vector3D position, LinFloat64Bivector3D direction)
    {
        var normal =
            direction.DirectionToUnitNormal3D(LinFloat64Vector3D.UnitSymmetric);

        if (DirectionStyle.DrawPosition)
        {
            SceneComposer.AddPoint(
                GrVisualPoint3D.CreateStatic(
                    AnimationComposer.GetNewSceneObjectName("point"),
                    DirectionStyle.GetPointVisualStyle(
                        color.SetAlpha(DirectionStyle.AuxGeometryColorAlpha),
                        DirectionStyle.Thickness * 1.5
                    ),
                    position
                )
            );
        }

        if (DirectionStyle.DrawDirection)
        {
            SceneComposer.AddBivector(
                GrVisualBivector3D.CreateStatic(
                    AnimationComposer.GetNewSceneObjectName("bivector"),
                    DirectionStyle.GetBivectorVisualStyle(
                        color.SetAlpha(DirectionStyle.AuxGeometryColorAlpha),
                        DirectionStyle.Thickness
                    ),
                    position,
                    normal,
                    DirectionStyle.NormalDirectionRadius
                )
            );
        }

        if (DirectionStyle.DrawNormalDirection)
        {
            SceneComposer.AddVector(
                GrVisualVector3D.CreateStatic(
                    AnimationComposer.GetNewSceneObjectName("vector"),
                    DirectionStyle.GetVectorVisualStyle(
                        color.SetAlpha(DirectionStyle.AuxGeometryColorAlpha),
                        DirectionStyle.Thickness
                    ),
                    position,
                    normal.SetLength(DirectionStyle.NormalDirectionRadius)
                )
            );
        }

        return this;
    }

    public CGaFloat64Visualizer DrawDirectionPlane3D(Color color, IParametricCurve3D position, IParametricBivector3D direction)
    {
        var animatedPosition =
            SamplingSpecs.CreateAnimatedVector3D(position);

        var animatedNormal =
            SamplingSpecs.CreateAnimatedVector3D(
                direction.GetNormalVectorCurve()
            );

        if (DirectionStyle.DrawPosition)
        {
            SceneComposer.AddPoint(
                GrVisualPoint3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("point"),
                    DirectionStyle.GetPointVisualStyle(
                        color.SetAlpha(DirectionStyle.AuxGeometryColorAlpha),
                        DirectionStyle.Thickness * 1.5
                    ),
                    animatedPosition
                )
            );
        }

        if (DirectionStyle.DrawDirection)
        {
            SceneComposer.AddBivector(
                GrVisualBivector3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("bivector"),
                    DirectionStyle.GetBivectorVisualStyle(
                        color.SetAlpha(DirectionStyle.AuxGeometryColorAlpha),
                        DirectionStyle.Thickness
                    ),
                    animatedPosition,
                    animatedNormal,
                    DirectionStyle.NormalDirectionRadius
                )
            );
        }

        if (DirectionStyle.DrawNormalDirection)
        {
            SceneComposer.AddVector(
                GrVisualVector3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("vector"),
                    DirectionStyle.GetVectorVisualStyle(
                        color.SetAlpha(DirectionStyle.AuxGeometryColorAlpha),
                        DirectionStyle.Thickness
                    ),
                    animatedPosition,
                    animatedNormal.SetLength(DirectionStyle.NormalDirectionRadius)
                )
            );
        }

        return this;
    }

    public CGaFloat64Visualizer DrawDirectionPlane3D(Color color, CGaFloat64ParametricElement element)
    {
        var animatedPosition =
            SamplingSpecs.CreateAnimatedVector3D(
                element.PositionToParametricCurve3D()
            );

        var animatedNormal =
            SamplingSpecs.CreateAnimatedVector3D(
                element.NormalDirectionToParametricCurve3D()
            );

        if (DirectionStyle.DrawPosition)
        {
            SceneComposer.AddPoint(
                GrVisualPoint3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("point"),
                    DirectionStyle.GetPointVisualStyle(
                        color.SetAlpha(DirectionStyle.AuxGeometryColorAlpha),
                        DirectionStyle.Thickness * 1.5
                    ),
                    animatedPosition
                )
            );
        }

        if (DirectionStyle.DrawDirection)
        {
            SceneComposer.AddBivector(
                GrVisualBivector3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("bivector"),
                    DirectionStyle.GetVectorVisualStyle(
                        color.SetAlpha(DirectionStyle.AuxGeometryColorAlpha),
                        DirectionStyle.Thickness
                    ),
                    animatedPosition,
                    animatedNormal,
                    DirectionStyle.NormalDirectionRadius
                )
            );
        }

        if (DirectionStyle.DrawNormalDirection)
        {
            SceneComposer.AddVector(
                GrVisualVector3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("vector"),
                    DirectionStyle.GetVectorVisualStyle(
                        color.SetAlpha(DirectionStyle.AuxGeometryColorAlpha),
                        DirectionStyle.Thickness
                    ),
                    animatedPosition,
                    animatedNormal.SetLength(DirectionStyle.NormalDirectionRadius)
                )
            );
        }

        return this;
    }


    public CGaFloat64Visualizer DrawDirection(Color color, CGaFloat64Direction element)
    {
        if (GeometricSpace.Is4D)
            return element.Direction.Grade switch
            {
                0 => DrawDirectionPoint2D(color, element.PositionToVector2D()),
                1 => DrawDirectionLine2D(color, element.PositionToVector2D(), element.DirectionToVector2D()),
                2 => DrawDirectionPlane2D(color, element.PositionToVector2D(), element.DirectionToBivector2D()),
                _ => this
            };

        return element.Direction.Grade switch
        {
            0 => DrawDirectionPoint3D(color, element.PositionToVector3D()),
            1 => DrawDirectionLine3D(color, element.PositionToVector3D(), element.DirectionToVector3D()),
            2 => DrawDirectionPlane3D(color, element.PositionToVector3D(), element.DirectionToBivector3D()),
            _ => this
        };
    }


    public CGaFloat64Visualizer DrawTangentPoint2D(Color color, LinFloat64Vector2D position)
    {
        SceneComposer.AddPoint(
            GrVisualPoint3D.CreateStatic(
                AnimationComposer.GetNewSceneObjectName("point"),
                TangentStyle.GetPointVisualStyle(color),
                position.ToXyLinVector3D()
            )
        );

        return this;
    }

    public CGaFloat64Visualizer DrawTangentPoint2D(Color color, IFloat64ParametricCurve2D position)
    {
        SceneComposer.AddPoint(
            GrVisualPoint3D.CreateAnimated(
                AnimationComposer.GetNewSceneObjectName("point"),
                TangentStyle.GetPointVisualStyle(color),
                SamplingSpecs.CreateXyAnimatedVector3D(position)
            )
        );

        return this;
    }

    public CGaFloat64Visualizer DrawTangentPoint2D(Color color, CGaFloat64ParametricElement element)
    {
        var animatedPosition =
            SamplingSpecs.CreateXyAnimatedVector3D(
                element.PositionToParametricCurve2D()
            );

        SceneComposer.AddPoint(
            GrVisualPoint3D.CreateAnimated(
                AnimationComposer.GetNewSceneObjectName("point"),
                TangentStyle.GetPointVisualStyle(color),
                animatedPosition
            )
        );

        return this;
    }


    public CGaFloat64Visualizer DrawTangentPoint3D(Color color, LinFloat64Vector3D position)
    {
        SceneComposer.AddPoint(
            GrVisualPoint3D.CreateStatic(
                AnimationComposer.GetNewSceneObjectName("point"),
                TangentStyle.GetPointVisualStyle(color),
                position
            )
        );

        return this;
    }

    public CGaFloat64Visualizer DrawTangentPoint3D(Color color, IParametricCurve3D position)
    {
        SceneComposer.AddPoint(
            GrVisualPoint3D.CreateAnimated(
                AnimationComposer.GetNewSceneObjectName("point"),
                TangentStyle.GetPointVisualStyle(color),
                SamplingSpecs.CreateAnimatedVector3D(position)
            )
        );

        return this;
    }

    public CGaFloat64Visualizer DrawTangentPoint3D(Color color, CGaFloat64ParametricElement element)
    {
        var animatedPosition =
            SamplingSpecs.CreateAnimatedVector3D(
                element.PositionToParametricCurve3D()
            );

        SceneComposer.AddPoint(
            GrVisualPoint3D.CreateAnimated(
                AnimationComposer.GetNewSceneObjectName("point"),
                TangentStyle.GetPointVisualStyle(color),
                animatedPosition
            )
        );

        return this;
    }


    public CGaFloat64Visualizer DrawTangentLine2D(Color color, LinFloat64Vector2D position, LinFloat64Vector2D direction)
    {
        if (TangentStyle.DrawPosition)
        {
            SceneComposer.AddPoint(
                GrVisualPoint3D.CreateStatic(
                    AnimationComposer.GetNewSceneObjectName("point"),
                    TangentStyle.GetPointVisualStyle(
                        color.SetAlpha(TangentStyle.AuxGeometryColorAlpha),
                        TangentStyle.Thickness * 1.5
                    ),
                    position.ToXyLinVector3D()
                )
            );
        }

        if (TangentStyle.DrawDirection)
        {
            SceneComposer.AddVector(
                GrVisualVector3D.CreateStatic(
                    AnimationComposer.GetNewSceneObjectName("vector"),
                    TangentStyle.GetVectorVisualStyle(
                        color.SetAlpha(TangentStyle.AuxGeometryColorAlpha),
                        TangentStyle.Thickness
                    ),
                    position.ToXyLinVector3D(),
                    direction.SetLength(TangentStyle.DirectionRadius).ToXyLinVector3D()
                )
            );
        }

        if (TangentStyle.DrawNormalDirection)
        {
            SceneComposer.AddBivector(
                GrVisualBivector3D.CreateStatic(
                    AnimationComposer.GetNewSceneObjectName("bivector"),
                    TangentStyle.GetVectorVisualStyle(
                        color.SetAlpha(TangentStyle.AuxGeometryColorAlpha),
                        TangentStyle.Thickness
                    ),
                    position.ToXyLinVector3D(),
                    direction.ToXyLinVector3D(),
                    TangentStyle.NormalDirectionRadius
                )
            );
        }

        return this;
    }

    public CGaFloat64Visualizer DrawTangentLine2D(Color color, IFloat64ParametricCurve2D position, IFloat64ParametricCurve2D direction)
    {
        var animatedPosition =
            SamplingSpecs.CreateXyAnimatedVector3D(position);

        var animatedDirection =
            SamplingSpecs.CreateXyAnimatedVector3D(direction);

        if (TangentStyle.DrawPosition)
        {
            SceneComposer.AddPoint(
                GrVisualPoint3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("point"),
                    TangentStyle.GetPointVisualStyle(
                        color.SetAlpha(TangentStyle.AuxGeometryColorAlpha),
                        TangentStyle.Thickness * 1.5
                    ),
                    animatedPosition
                )
            );
        }

        if (TangentStyle.DrawDirection)
        {
            var animatedScaledVector =
                SamplingSpecs.CreateAnimatedVector3D(t =>
                    direction.GetPoint(t).SetLength(TangentStyle.DirectionRadius).ToXyLinVector3D()
                );

            SceneComposer.AddVector(
                GrVisualVector3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("vector"),
                    TangentStyle.GetVectorVisualStyle(
                        color.SetAlpha(TangentStyle.AuxGeometryColorAlpha),
                        TangentStyle.Thickness
                    ),
                    animatedPosition,
                    animatedScaledVector
                )
            );
        }

        if (TangentStyle.DrawNormalDirection)
        {
            SceneComposer.AddBivector(
                GrVisualBivector3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("bivector"),
                    TangentStyle.GetVectorVisualStyle(
                        color.SetAlpha(TangentStyle.AuxGeometryColorAlpha),
                        TangentStyle.Thickness
                    ),
                    animatedPosition,
                    animatedDirection,
                    TangentStyle.NormalDirectionRadius
                )
            );
        }

        return this;
    }

    public CGaFloat64Visualizer DrawTangentLine2D(Color color, CGaFloat64ParametricElement element)
    {
        var animatedPosition =
            SamplingSpecs.CreateXyAnimatedVector3D(
                element.PositionToParametricCurve2D()
            );

        var animatedDirection =
            SamplingSpecs.CreateXyAnimatedVector3D(
                element.DirectionToParametricCurve2D()
            );

        var animatedNormal =
            SamplingSpecs.CreateXyAnimatedVector3D(
                element.NormalDirectionToParametricCurve2D()
            );

        if (TangentStyle.DrawPosition)
        {
            SceneComposer.AddPoint(
                GrVisualPoint3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("point"),
                    TangentStyle.GetPointVisualStyle(
                        color.SetAlpha(TangentStyle.AuxGeometryColorAlpha),
                        TangentStyle.Thickness * 1.5
                    ),
                    animatedPosition
                )
            );
        }

        if (TangentStyle.DrawDirection)
        {
            SceneComposer.AddVector(
                GrVisualVector3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("vector"),
                    TangentStyle.GetVectorVisualStyle(
                        color.SetAlpha(TangentStyle.AuxGeometryColorAlpha),
                        TangentStyle.Thickness
                    ),
                    animatedPosition,
                    animatedDirection.SetLength(TangentStyle.DirectionRadius)
                )
            );
        }

        if (TangentStyle.DrawNormalDirection)
        {
            SceneComposer.AddVector(
                GrVisualVector3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("vector"),
                    TangentStyle.GetVectorVisualStyle(
                        color.SetAlpha(TangentStyle.AuxGeometryColorAlpha),
                        TangentStyle.Thickness
                    ),
                    animatedPosition,
                    animatedNormal.SetLength(TangentStyle.NormalDirectionRadius)
                )
            );
        }

        return this;
    }


    public CGaFloat64Visualizer DrawTangentLine3D(Color color, LinFloat64Vector3D position, LinFloat64Vector3D direction)
    {
        if (TangentStyle.DrawPosition)
        {
            SceneComposer.AddPoint(
                GrVisualPoint3D.CreateStatic(
                    AnimationComposer.GetNewSceneObjectName("point"),
                    TangentStyle.GetPointVisualStyle(
                        color.SetAlpha(TangentStyle.AuxGeometryColorAlpha),
                        TangentStyle.Thickness * 1.5
                    ),
                    position
                )
            );
        }

        if (TangentStyle.DrawDirection)
        {
            SceneComposer.AddVector(
                GrVisualVector3D.CreateStatic(
                    AnimationComposer.GetNewSceneObjectName("vector"),
                    TangentStyle.GetVectorVisualStyle(
                        color.SetAlpha(TangentStyle.AuxGeometryColorAlpha),
                        TangentStyle.Thickness
                    ),
                    position,
                    direction.SetLength(TangentStyle.DirectionRadius)
                )
            );
        }

        if (TangentStyle.DrawNormalDirection)
        {
            SceneComposer.AddBivector(
                GrVisualBivector3D.CreateStatic(
                    AnimationComposer.GetNewSceneObjectName("bivector"),
                    TangentStyle.GetVectorVisualStyle(
                        color.SetAlpha(TangentStyle.AuxGeometryColorAlpha),
                        TangentStyle.Thickness
                    ),
                    position,
                    direction,
                    TangentStyle.NormalDirectionRadius
                )
            );
        }

        return this;
    }

    public CGaFloat64Visualizer DrawTangentLine3D(Color color, IParametricCurve3D position, IParametricCurve3D direction)
    {
        var animatedPosition =
            SamplingSpecs.CreateAnimatedVector3D(position);

        var animatedDirection =
            SamplingSpecs.CreateAnimatedVector3D(direction);

        if (TangentStyle.DrawPosition)
        {
            SceneComposer.AddPoint(
                GrVisualPoint3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("point"),
                    TangentStyle.GetPointVisualStyle(
                        color.SetAlpha(TangentStyle.AuxGeometryColorAlpha),
                        TangentStyle.Thickness * 1.5
                    ),
                    animatedPosition
                )
            );
        }

        if (TangentStyle.DrawDirection)
        {
            var animatedScaledVector =
                SamplingSpecs.CreateAnimatedVector3D(t =>
                    direction.GetPoint(t).SetLength(TangentStyle.DirectionRadius)
                );

            SceneComposer.AddVector(
                GrVisualVector3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("vector"),
                    TangentStyle.GetVectorVisualStyle(
                        color.SetAlpha(TangentStyle.AuxGeometryColorAlpha),
                        TangentStyle.Thickness
                    ),
                    animatedPosition,
                    animatedScaledVector
                )
            );
        }

        if (TangentStyle.DrawNormalDirection)
        {
            SceneComposer.AddBivector(
                GrVisualBivector3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("bivector"),
                    TangentStyle.GetVectorVisualStyle(
                        color.SetAlpha(TangentStyle.AuxGeometryColorAlpha),
                        TangentStyle.Thickness
                    ),
                    animatedPosition,
                    animatedDirection,
                    TangentStyle.NormalDirectionRadius
                )
            );
        }

        return this;
    }

    public CGaFloat64Visualizer DrawTangentLine3D(Color color, CGaFloat64ParametricElement element)
    {
        var animatedPosition =
            SamplingSpecs.CreateAnimatedVector3D(
                element.PositionToParametricCurve3D()
            );

        var animatedDirection =
            SamplingSpecs.CreateAnimatedVector3D(
                element.DirectionToParametricCurve3D()
            );

        if (TangentStyle.DrawPosition)
        {
            SceneComposer.AddPoint(
                GrVisualPoint3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("point"),
                    TangentStyle.GetPointVisualStyle(
                        color.SetAlpha(TangentStyle.AuxGeometryColorAlpha),
                        TangentStyle.Thickness * 1.5
                    ),
                    animatedPosition
                )
            );
        }

        if (TangentStyle.DrawDirection)
        {
            SceneComposer.AddVector(
                GrVisualVector3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("vector"),
                    TangentStyle.GetVectorVisualStyle(
                        color.SetAlpha(TangentStyle.AuxGeometryColorAlpha),
                        TangentStyle.Thickness
                    ),
                    animatedPosition,
                    animatedDirection.SetLength(TangentStyle.AuxGeometryColorAlpha)
                )
            );
        }

        if (TangentStyle.DrawNormalDirection)
        {
            SceneComposer.AddBivector(
                GrVisualBivector3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("bivector"),
                    TangentStyle.GetVectorVisualStyle(
                        color.SetAlpha(TangentStyle.AuxGeometryColorAlpha),
                        TangentStyle.Thickness
                    ),
                    animatedPosition,
                    animatedDirection,
                    TangentStyle.NormalDirectionRadius
                )
            );
        }

        return this;
    }


    public CGaFloat64Visualizer DrawTangentPlane2D(Color color, LinFloat64Vector2D position, LinFloat64Bivector2D direction)
    {
        if (TangentStyle.DrawPosition)
        {
            SceneComposer.AddPoint(
                GrVisualPoint3D.CreateStatic(
                    AnimationComposer.GetNewSceneObjectName("point"),
                    TangentStyle.GetPointVisualStyle(
                        color.SetAlpha(TangentStyle.AuxGeometryColorAlpha),
                        TangentStyle.Thickness * 1.5
                    ),
                    position.ToXyLinVector3D()
                )
            );
        }

        var normal =
            direction.ToXyBivector3D().DirectionToUnitNormal3D(LinFloat64Vector3D.UnitSymmetric);

        if (TangentStyle.DrawDirection)
        {
            SceneComposer.AddBivector(
                GrVisualBivector3D.CreateStatic(
                    AnimationComposer.GetNewSceneObjectName("bivector"),
                    TangentStyle.GetVectorVisualStyle(
                        color.SetAlpha(TangentStyle.AuxGeometryColorAlpha),
                        TangentStyle.Thickness
                    ),
                    position.ToXyLinVector3D(),
                    normal,
                    TangentStyle.NormalDirectionRadius
                )
            );
        }

        if (TangentStyle.DrawNormalDirection)
        {
            SceneComposer.AddVector(
                GrVisualVector3D.CreateStatic(
                    AnimationComposer.GetNewSceneObjectName("vector"),
                    TangentStyle.GetVectorVisualStyle(
                        color.SetAlpha(TangentStyle.AuxGeometryColorAlpha),
                        TangentStyle.Thickness
                    ),
                    position.ToXyLinVector3D(),
                    normal.SetLength(TangentStyle.NormalDirectionRadius)
                )
            );
        }

        return this;
    }

    public CGaFloat64Visualizer DrawTangentPlane2D(Color color, IFloat64ParametricCurve2D position, IParametricBivector2D direction)
    {
        var animatedPosition =
            SamplingSpecs.CreateXyAnimatedVector3D(position);

        var animatedNormal =
            SamplingSpecs.CreateAnimatedVector3D(
                direction
                    .ToParametricCurve3D(b =>
                        b.ToXyBivector3D().DirectionToUnitNormal3D(LinFloat64Vector3D.UnitSymmetric)
                    )
            );

        if (TangentStyle.DrawPosition)
        {
            SceneComposer.AddPoint(
                GrVisualPoint3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("point"),
                    TangentStyle.GetPointVisualStyle(
                        color.SetAlpha(TangentStyle.AuxGeometryColorAlpha),
                        TangentStyle.Thickness * 1.5
                    ),
                    animatedPosition
                )
            );
        }

        if (TangentStyle.DrawDirection)
        {
            SceneComposer.AddBivector(
                GrVisualBivector3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("bivector"),
                    TangentStyle.GetVectorVisualStyle(
                        color.SetAlpha(TangentStyle.AuxGeometryColorAlpha),
                        TangentStyle.Thickness
                    ),
                    animatedPosition,
                    animatedNormal,
                    TangentStyle.NormalDirectionRadius
                )
            );
        }

        if (TangentStyle.DrawNormalDirection)
        {
            SceneComposer.AddVector(
                GrVisualVector3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("vector"),
                    TangentStyle.GetVectorVisualStyle(
                        color.SetAlpha(TangentStyle.AuxGeometryColorAlpha),
                        TangentStyle.Thickness
                    ),
                    animatedPosition,
                    animatedNormal.SetLength(TangentStyle.NormalDirectionRadius)
                )
            );
        }

        return this;
    }

    public CGaFloat64Visualizer DrawTangentPlane2D(Color color, CGaFloat64ParametricElement element)
    {
        var animatedPosition =
            SamplingSpecs.CreateXyAnimatedVector3D(
                element.PositionToParametricCurve2D()
            );

        var animatedNormal =
            SamplingSpecs.CreateAnimatedVector3D(
                element
                    .DirectionToParametricBivector2D()
                    .XyDirectionToNormal3D(LinFloat64Vector3D.UnitSymmetric)
            );

        if (TangentStyle.DrawPosition)
        {
            SceneComposer.AddPoint(
                GrVisualPoint3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("point"),
                    TangentStyle.GetPointVisualStyle(
                        color.SetAlpha(TangentStyle.AuxGeometryColorAlpha),
                        TangentStyle.Thickness * 1.5
                    ),
                    animatedPosition
                )
            );
        }

        if (TangentStyle.DrawDirection)
        {
            SceneComposer.AddBivector(
                GrVisualBivector3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("bivector"),
                    TangentStyle.GetVectorVisualStyle(
                        color.SetAlpha(TangentStyle.AuxGeometryColorAlpha),
                        TangentStyle.Thickness
                    ),
                    animatedPosition,
                    animatedNormal,
                    TangentStyle.NormalDirectionRadius
                )
            );
        }

        if (TangentStyle.DrawNormalDirection)
        {
            SceneComposer.AddVector(
                GrVisualVector3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("vector"),
                    TangentStyle.GetVectorVisualStyle(
                        color.SetAlpha(TangentStyle.AuxGeometryColorAlpha),
                        TangentStyle.Thickness
                    ),
                    animatedPosition,
                    animatedNormal.SetLength(TangentStyle.NormalDirectionRadius)
                )
            );
        }

        return this;
    }


    public CGaFloat64Visualizer DrawTangentPlane3D(Color color, LinFloat64Vector3D position, LinFloat64Bivector3D direction)
    {
        var normal =
            direction.DirectionToUnitNormal3D(LinFloat64Vector3D.UnitSymmetric);

        if (TangentStyle.DrawPosition)
        {
            SceneComposer.AddPoint(
                GrVisualPoint3D.CreateStatic(
                    AnimationComposer.GetNewSceneObjectName("point"),
                    TangentStyle.GetPointVisualStyle(
                        color.SetAlpha(TangentStyle.AuxGeometryColorAlpha),
                        TangentStyle.Thickness * 1.5
                    ),
                    position
                )
            );
        }

        if (TangentStyle.DrawDirection)
        {
            SceneComposer.AddBivector(
                GrVisualBivector3D.CreateStatic(
                    AnimationComposer.GetNewSceneObjectName("bivector"),
                    TangentStyle.GetVectorVisualStyle(
                        color.SetAlpha(TangentStyle.AuxGeometryColorAlpha),
                        TangentStyle.Thickness
                    ),
                    position,
                    normal,
                    TangentStyle.NormalDirectionRadius
                )
            );
        }

        if (TangentStyle.DrawNormalDirection)
        {
            SceneComposer.AddVector(
                GrVisualVector3D.CreateStatic(
                    AnimationComposer.GetNewSceneObjectName("vector"),
                    TangentStyle.GetVectorVisualStyle(
                        color.SetAlpha(TangentStyle.AuxGeometryColorAlpha),
                        TangentStyle.Thickness
                    ),
                    position,
                    normal.SetLength(TangentStyle.NormalDirectionRadius)
                )
            );
        }

        return this;
    }

    public CGaFloat64Visualizer DrawTangentPlane3D(Color color, IParametricCurve3D position, IParametricBivector3D direction)
    {
        var animatedPosition =
            SamplingSpecs.CreateAnimatedVector3D(position);

        if (TangentStyle.DrawPosition)
        {
            SceneComposer.AddPoint(
                GrVisualPoint3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("point"),
                    TangentStyle.GetPointVisualStyle(
                        color.SetAlpha(TangentStyle.AuxGeometryColorAlpha),
                        TangentStyle.Thickness * 1.5
                    ),
                    animatedPosition
                )
            );
        }

        var animatedNormal =
            SamplingSpecs.CreateAnimatedVector3D(
                direction.GetNormalVectorCurve()
            );

        if (TangentStyle.DrawDirection)
        {
            SceneComposer.AddBivector(
                GrVisualBivector3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("bivector"),
                    TangentStyle.GetVectorVisualStyle(
                        color.SetAlpha(TangentStyle.AuxGeometryColorAlpha),
                        TangentStyle.Thickness
                    ),
                    animatedPosition,
                    animatedNormal,
                    TangentStyle.NormalDirectionRadius
                )
            );
        }

        if (TangentStyle.DrawNormalDirection)
        {
            SceneComposer.AddVector(
                GrVisualVector3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("vector"),
                    TangentStyle.GetVectorVisualStyle(
                        color.SetAlpha(TangentStyle.AuxGeometryColorAlpha),
                        TangentStyle.Thickness
                    ),
                    animatedPosition,
                    animatedNormal.SetLength(TangentStyle.NormalDirectionRadius)
                )
            );
        }

        return this;
    }

    public CGaFloat64Visualizer DrawTangentPlane3D(Color color, CGaFloat64ParametricElement element)
    {
        var animatedPosition =
            SamplingSpecs.CreateAnimatedVector3D(
                element.PositionToParametricCurve3D()
            );

        var animatedNormal =
            SamplingSpecs.CreateAnimatedVector3D(
                element.NormalDirectionToParametricCurve3D()
            );

        if (TangentStyle.DrawPosition)
        {
            SceneComposer.AddPoint(
                GrVisualPoint3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("point"),
                    TangentStyle.GetPointVisualStyle(
                        color.SetAlpha(TangentStyle.AuxGeometryColorAlpha),
                        TangentStyle.Thickness * 1.5
                    ),
                    animatedPosition
                )
            );
        }

        if (TangentStyle.DrawDirection)
        {
            SceneComposer.AddBivector(
                GrVisualBivector3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("bivector"),
                    TangentStyle.GetVectorVisualStyle(
                        color.SetAlpha(TangentStyle.AuxGeometryColorAlpha),
                        TangentStyle.Thickness
                    ),
                    animatedPosition,
                    animatedNormal,
                    TangentStyle.NormalDirectionRadius
                )
            );
        }

        if (TangentStyle.DrawNormalDirection)
        {
            SceneComposer.AddVector(
                GrVisualVector3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("vector"),
                    TangentStyle.GetVectorVisualStyle(
                        color.SetAlpha(TangentStyle.AuxGeometryColorAlpha),
                        TangentStyle.Thickness
                    ),
                    animatedPosition,
                    animatedNormal.SetLength(TangentStyle.NormalDirectionRadius)
                )
            );
        }

        return this;
    }


    public CGaFloat64Visualizer DrawTangent(Color color, CGaFloat64Tangent element)
    {
        if (GeometricSpace.Is4D)
            return element.Direction.Grade switch
            {
                0 => DrawTangentPoint2D(color, element.PositionToVector2D()),
                1 => DrawTangentLine2D(color, element.PositionToVector2D(), element.DirectionToVector2D()),
                2 => DrawTangentPlane2D(color, element.PositionToVector2D(), element.DirectionToBivector2D()),
                _ => this
            };

        return element.Direction.Grade switch
        {
            0 => DrawTangentPoint3D(color, element.PositionToVector3D()),
            1 => DrawTangentLine3D(color, element.PositionToVector3D(), element.DirectionToVector3D()),
            2 => DrawTangentPlane3D(color, element.PositionToVector3D(), element.DirectionToBivector3D()),
            _ => this
        };
    }


    public CGaFloat64Visualizer DrawFlatPoint2D(Color color, LinFloat64Vector2D position)
    {
        SceneComposer.AddPoint(
            GrVisualPoint3D.CreateStatic(
                AnimationComposer.GetNewSceneObjectName("point"),
                FlatStyle.GetPointVisualStyle(color),
                position.ToXyLinVector3D()
            )
        );

        return this;
    }

    public CGaFloat64Visualizer DrawFlatPoint2D(Color color, IFloat64ParametricCurve2D position)
    {
        SceneComposer.AddPoint(
            GrVisualPoint3D.CreateAnimated(
                AnimationComposer.GetNewSceneObjectName("point"),
                FlatStyle.GetPointVisualStyle(color),
                SamplingSpecs.CreateXyAnimatedVector3D(position)
            )
        );

        return this;
    }

    public CGaFloat64Visualizer DrawFlatPoint2D(Color color, CGaFloat64ParametricElement element)
    {
        var animatedPosition =
            SamplingSpecs.CreateXyAnimatedVector3D(
                element.PositionToParametricCurve2D()
            );

        SceneComposer.AddPoint(
            GrVisualPoint3D.CreateAnimated(
                AnimationComposer.GetNewSceneObjectName("point"),
                FlatStyle.GetPointVisualStyle(color),
                animatedPosition
            )
        );

        return this;
    }


    public CGaFloat64Visualizer DrawFlatPoint3D(Color color, LinFloat64Vector3D position)
    {
        SceneComposer.AddPoint(
            GrVisualPoint3D.CreateStatic(
                AnimationComposer.GetNewSceneObjectName("point"),
                FlatStyle.GetPointVisualStyle(color),
                position
            )
        );

        return this;
    }

    public CGaFloat64Visualizer DrawFlatPoint3D(Color color, IParametricCurve3D position)
    {
        SceneComposer.AddPoint(
            GrVisualPoint3D.CreateAnimated(
                AnimationComposer.GetNewSceneObjectName("point"),
                FlatStyle.GetPointVisualStyle(color),
                SamplingSpecs.CreateAnimatedVector3D(position)
            )
        );

        return this;
    }

    public CGaFloat64Visualizer DrawFlatPoint3D(Color color, CGaFloat64ParametricElement element)
    {
        var animatedPosition =
            SamplingSpecs.CreateAnimatedVector3D(
                element.PositionToParametricCurve3D()
            );

        SceneComposer.AddPoint(
            GrVisualPoint3D.CreateAnimated(
                AnimationComposer.GetNewSceneObjectName("point"),
                FlatStyle.GetPointVisualStyle(color),
                animatedPosition
            )
        );

        return this;
    }


    public CGaFloat64Visualizer DrawFlatLine2D(Color color, LinFloat64Vector2D position, LinFloat64Vector2D direction)
    {
        SceneComposer.AddLineSegment(
            GrVisualLineSegment3D.CreateStatic(
                AnimationComposer.GetNewSceneObjectName("lineSegment"),
                FlatStyle.GetLineVisualStyle(color),
                (position - direction.SetLength(FlatStyle.Radius)).ToXyLinVector3D(),
                (position + direction.SetLength(FlatStyle.Radius)).ToXyLinVector3D()
            )
        );

        if (FlatStyle.DrawPosition)
        {
            SceneComposer.AddPoint(
                GrVisualPoint3D.CreateStatic(
                    AnimationComposer.GetNewSceneObjectName("point"),
                    FlatStyle.GetPointVisualStyle(
                        color.SetAlpha(FlatStyle.AuxGeometryColorAlpha),
                        FlatStyle.Thickness * 1.5
                    ),
                    position.ToXyLinVector3D()
                )
            );
        }

        if (FlatStyle.DrawDirection)
        {
            SceneComposer.AddVector(
                GrVisualVector3D.CreateStatic(
                    AnimationComposer.GetNewSceneObjectName("vector"),
                    FlatStyle.GetVectorVisualStyle(
                        color.SetAlpha(FlatStyle.AuxGeometryColorAlpha),
                        FlatStyle.Thickness
                    ),
                    position.ToXyLinVector3D(),
                    direction.SetLength(FlatStyle.DirectionRadius).ToXyLinVector3D()
                )
            );
        }

        if (FlatStyle.DrawNormalDirection)
        {
            SceneComposer.AddBivector(
                GrVisualBivector3D.CreateStatic(
                    AnimationComposer.GetNewSceneObjectName("bivector"),
                    FlatStyle.GetVectorVisualStyle(
                        color.SetAlpha(FlatStyle.AuxGeometryColorAlpha),
                        FlatStyle.Thickness
                    ),
                    position.ToXyLinVector3D(),
                    direction.ToXyLinVector3D(),
                    FlatStyle.NormalDirectionRadius
                )
            );
        }

        return this;
    }

    public CGaFloat64Visualizer DrawFlatLine2D(Color color, IFloat64ParametricCurve2D position, IFloat64ParametricCurve2D direction)
    {
        var animatedPosition =
            SamplingSpecs.CreateXyAnimatedVector3D(position);

        var animatedDirection =
            SamplingSpecs.CreateXyAnimatedVector3D(direction);

        var animatedPoint1 =
            SamplingSpecs.CreateAnimatedVector3D(t =>
                (position.GetPoint(t) - direction.GetPoint(t).SetLength(FlatStyle.Radius)).ToXyLinVector3D()
            );

        var animatedPoint2 =
            SamplingSpecs.CreateAnimatedVector3D(t =>
                (position.GetPoint(t) + direction.GetPoint(t).SetLength(FlatStyle.Radius)).ToXyLinVector3D()
            );

        SceneComposer.AddLineSegment(
            GrVisualLineSegment3D.CreateAnimated(
                AnimationComposer.GetNewSceneObjectName("lineSegment"),
                FlatStyle.GetLineVisualStyle(color),
                animatedPoint1,
                animatedPoint2
            )
        );

        if (FlatStyle.DrawPosition)
        {
            SceneComposer.AddPoint(
                GrVisualPoint3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("point"),
                    FlatStyle.GetPointVisualStyle(
                        color.SetAlpha(FlatStyle.AuxGeometryColorAlpha),
                        FlatStyle.Thickness * 1.5
                    ),
                    animatedPosition
                )
            );
        }

        if (FlatStyle.DrawDirection)
        {
            var animatedScaledVector =
                SamplingSpecs.CreateAnimatedVector3D(t =>
                    direction.GetPoint(t).SetLength(FlatStyle.DirectionRadius).ToXyLinVector3D()
                );

            SceneComposer.AddVector(
                GrVisualVector3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("vector"),
                    FlatStyle.GetVectorVisualStyle(
                        color.SetAlpha(FlatStyle.AuxGeometryColorAlpha),
                        FlatStyle.Thickness
                    ),
                    animatedPosition,
                    animatedScaledVector
                )
            );
        }

        if (FlatStyle.DrawNormalDirection)
        {
            SceneComposer.AddBivector(
                GrVisualBivector3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("bivector"),
                    FlatStyle.GetVectorVisualStyle(
                        color.SetAlpha(FlatStyle.AuxGeometryColorAlpha),
                        FlatStyle.Thickness
                    ),
                    animatedPosition,
                    animatedDirection,
                    FlatStyle.NormalDirectionRadius
                )
            );
        }

        return this;
    }

    public CGaFloat64Visualizer DrawFlatLine2D(Color color, CGaFloat64ParametricElement element)
    {
        var animatedPosition =
            SamplingSpecs.CreateXyAnimatedVector3D(
                element.PositionToParametricCurve2D()
            );

        var animatedDirection =
            SamplingSpecs.CreateXyAnimatedVector3D(
                element.DirectionToParametricCurve2D()
            );

        var animatedNormal =
            SamplingSpecs.CreateXyAnimatedVector3D(
                element.NormalDirectionToParametricCurve2D()
            //element.DirectionToParametricCurve2D().GetNormalCurve()
            );

        var animatedPoint1 =
            SamplingSpecs.CreateAnimatedVector3D(
                t =>
                {
                    var el =
                        element.GetElement(t);

                    return (el.PositionToVector2D() - el.DirectionToVector2D(FlatStyle.Radius)).ToXyLinVector3D();
                }
            );

        var animatedPoint2 =
            SamplingSpecs.CreateAnimatedVector3D(
                t =>
                {
                    var el =
                        element.GetElement(t);

                    return (el.PositionToVector2D() + el.DirectionToVector2D(FlatStyle.Radius)).ToXyLinVector3D();
                }
            );

        SceneComposer.AddLineSegment(
            GrVisualLineSegment3D.CreateAnimated(
                AnimationComposer.GetNewSceneObjectName("lineSegment"),
                FlatStyle.GetLineVisualStyle(color),
                animatedPoint1,
                animatedPoint2
            )
        );

        if (FlatStyle.DrawPosition)
        {
            SceneComposer.AddPoint(
                GrVisualPoint3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("point"),
                    FlatStyle.GetPointVisualStyle(
                        color.SetAlpha(FlatStyle.AuxGeometryColorAlpha),
                        FlatStyle.Thickness * 1.5
                    ),
                    animatedPosition
                )
            );
        }

        if (FlatStyle.DrawDirection)
        {
            SceneComposer.AddVector(
                GrVisualVector3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("vector"),
                    FlatStyle.GetVectorVisualStyle(
                        color.SetAlpha(FlatStyle.AuxGeometryColorAlpha),
                        FlatStyle.Thickness
                    ),
                    animatedPosition,
                    animatedDirection.SetLength(FlatStyle.DirectionRadius)
                )
            );
        }

        if (FlatStyle.DrawNormalDirection)
        {
            SceneComposer.AddVector(
                GrVisualVector3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("vector"),
                    FlatStyle.GetVectorVisualStyle(
                        color.SetAlpha(FlatStyle.AuxGeometryColorAlpha),
                        FlatStyle.Thickness
                    ),
                    animatedPosition,
                    animatedNormal.SetLength(FlatStyle.NormalDirectionRadius)
                )
            );
        }

        return this;
    }


    public CGaFloat64Visualizer DrawFlatLine3D(Color color, LinFloat64Vector3D position, LinFloat64Vector3D direction)
    {
        SceneComposer.AddLineSegment(
            GrVisualLineSegment3D.CreateStatic(
                AnimationComposer.GetNewSceneObjectName("lineSegment"),
                FlatStyle.GetLineVisualStyle(color),
                position - direction.SetLength(FlatStyle.Radius),
                position + direction.SetLength(FlatStyle.Radius)
            )
        );

        if (FlatStyle.DrawPosition)
        {
            SceneComposer.AddPoint(
                GrVisualPoint3D.CreateStatic(
                    AnimationComposer.GetNewSceneObjectName("point"),
                    FlatStyle.GetPointVisualStyle(
                        color.SetAlpha(FlatStyle.AuxGeometryColorAlpha),
                        FlatStyle.Thickness * 1.5
                    ),
                    position
                )
            );
        }

        if (FlatStyle.DrawDirection)
        {
            SceneComposer.AddVector(
                GrVisualVector3D.CreateStatic(
                    AnimationComposer.GetNewSceneObjectName("vector"),
                    FlatStyle.GetVectorVisualStyle(
                        color.SetAlpha(FlatStyle.AuxGeometryColorAlpha),
                        FlatStyle.Thickness
                    ),
                    position,
                    direction.SetLength(FlatStyle.DirectionRadius)
                )
            );
        }

        if (FlatStyle.DrawNormalDirection)
        {
            SceneComposer.AddBivector(
                GrVisualBivector3D.CreateStatic(
                    AnimationComposer.GetNewSceneObjectName("bivector"),
                    FlatStyle.GetVectorVisualStyle(
                        color.SetAlpha(FlatStyle.AuxGeometryColorAlpha),
                        FlatStyle.Thickness
                    ),
                    position,
                    direction,
                    FlatStyle.NormalDirectionRadius
                )
            );
        }

        return this;
    }

    public CGaFloat64Visualizer DrawFlatLine3D(Color color, IParametricCurve3D position, IParametricCurve3D direction)
    {
        var animatedPosition =
            SamplingSpecs.CreateAnimatedVector3D(position);

        var animatedDirection =
            SamplingSpecs.CreateAnimatedVector3D(direction);

        var animatedPoint1 =
            SamplingSpecs.CreateAnimatedVector3D(t =>
                position.GetPoint(t) - direction.GetPoint(t).SetLength(FlatStyle.Radius)
            );

        var animatedPoint2 =
            SamplingSpecs.CreateAnimatedVector3D(t =>
                position.GetPoint(t) + direction.GetPoint(t).SetLength(FlatStyle.Radius)
            );

        SceneComposer.AddLineSegment(
            GrVisualLineSegment3D.CreateAnimated(
                AnimationComposer.GetNewSceneObjectName("lineSegment"),
                FlatStyle.GetLineVisualStyle(color),
                animatedPoint1,
                animatedPoint2
            )
        );

        if (FlatStyle.DrawPosition)
        {
            SceneComposer.AddPoint(
                GrVisualPoint3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("point"),
                    FlatStyle.GetPointVisualStyle(
                        color.SetAlpha(FlatStyle.AuxGeometryColorAlpha),
                        FlatStyle.Thickness * 1.5
                    ),
                    animatedPosition
                )
            );
        }

        if (FlatStyle.DrawDirection)
        {
            var animatedScaledVector =
                SamplingSpecs.CreateAnimatedVector3D(t =>
                    direction.GetPoint(t).SetLength(FlatStyle.DirectionRadius)
                );

            SceneComposer.AddVector(
                GrVisualVector3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("vector"),
                    FlatStyle.GetVectorVisualStyle(
                        color.SetAlpha(FlatStyle.AuxGeometryColorAlpha),
                        FlatStyle.Thickness
                    ),
                    animatedPosition,
                    animatedScaledVector
                )
            );
        }

        if (FlatStyle.DrawNormalDirection)
        {
            SceneComposer.AddBivector(
                GrVisualBivector3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("bivector"),
                    FlatStyle.GetVectorVisualStyle(
                        color.SetAlpha(FlatStyle.AuxGeometryColorAlpha),
                        FlatStyle.Thickness
                    ),
                    animatedPosition,
                    animatedDirection,
                    FlatStyle.NormalDirectionRadius
                )
            );
        }

        return this;
    }

    public CGaFloat64Visualizer DrawFlatLine3D(Color color, CGaFloat64ParametricElement element)
    {
        var animatedPosition =
            SamplingSpecs.CreateAnimatedVector3D(
                element.PositionToParametricCurve3D()
            );

        var animatedDirection =
            SamplingSpecs.CreateAnimatedVector3D(
                element.DirectionToParametricCurve3D()
            );

        var animatedPoint1 =
            SamplingSpecs.CreateAnimatedVector3D(
                t =>
                {
                    var el =
                        element.GetElement(t);

                    return el.PositionToVector3D() - el.DirectionToVector3D(FlatStyle.Radius);
                }
            );

        var animatedPoint2 =
            SamplingSpecs.CreateAnimatedVector3D(
                t =>
                {
                    var el =
                        element.GetElement(t);

                    return el.PositionToVector3D() + el.DirectionToVector3D(FlatStyle.Radius);
                }
            );

        SceneComposer.AddLineSegment(
            GrVisualLineSegment3D.CreateAnimated(
                AnimationComposer.GetNewSceneObjectName("lineSegment"),
                FlatStyle.GetLineVisualStyle(color),
                animatedPoint1,
                animatedPoint2
            )
        );

        if (FlatStyle.DrawPosition)
        {
            SceneComposer.AddPoint(
                GrVisualPoint3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("point"),
                    FlatStyle.GetPointVisualStyle(
                        color.SetAlpha(FlatStyle.AuxGeometryColorAlpha),
                        FlatStyle.Thickness * 1.5
                    ),
                    animatedPosition
                )
            );
        }

        if (FlatStyle.DrawDirection)
        {
            SceneComposer.AddVector(
                GrVisualVector3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("vector"),
                    FlatStyle.GetVectorVisualStyle(
                        color.SetAlpha(FlatStyle.AuxGeometryColorAlpha),
                        FlatStyle.Thickness
                    ),
                    animatedPosition,
                    animatedDirection.SetLength(FlatStyle.DirectionRadius)
                )
            );
        }

        if (FlatStyle.DrawNormalDirection)
        {
            SceneComposer.AddBivector(
                GrVisualBivector3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("bivector"),
                    FlatStyle.GetVectorVisualStyle(
                        color.SetAlpha(FlatStyle.AuxGeometryColorAlpha),
                        FlatStyle.Thickness
                    ),
                    animatedPosition,
                    animatedDirection,
                    FlatStyle.NormalDirectionRadius
                )
            );
        }

        return this;
    }


    public CGaFloat64Visualizer DrawFlatPlane2D(Color color, LinFloat64Vector2D position, LinFloat64Bivector2D direction)
    {
        var normal =
            direction.ToXyBivector3D().DirectionToUnitNormal3D(LinFloat64Vector3D.UnitSymmetric);

        if (FlatStyle.DrawPosition)
        {
            SceneComposer.AddPoint(
                GrVisualPoint3D.CreateStatic(
                    AnimationComposer.GetNewSceneObjectName("point"),
                    FlatStyle.GetPointVisualStyle(
                        color.SetAlpha(FlatStyle.AuxGeometryColorAlpha),
                        FlatStyle.Thickness * 1.5
                    ),
                    position.ToXyLinVector3D()
                )
            );
        }

        if (FlatStyle.DrawDirection)
        {
            SceneComposer.AddBivector(
                GrVisualBivector3D.CreateStatic(
                    AnimationComposer.GetNewSceneObjectName("bivector"),
                    FlatStyle.GetVectorVisualStyle(
                        color.SetAlpha(FlatStyle.AuxGeometryColorAlpha),
                        FlatStyle.Thickness
                    ),
                    position.ToXyLinVector3D(),
                    normal,
                    FlatStyle.NormalDirectionRadius
                )
            );
        }

        if (FlatStyle.DrawNormalDirection)
        {
            SceneComposer.AddVector(
                GrVisualVector3D.CreateStatic(
                    AnimationComposer.GetNewSceneObjectName("vector"),
                    FlatStyle.GetVectorVisualStyle(
                        color.SetAlpha(FlatStyle.AuxGeometryColorAlpha),
                        FlatStyle.Thickness
                    ),
                    position.ToXyLinVector3D(),
                    normal.SetLength(FlatStyle.NormalDirectionRadius)
                )
            );
        }

        return this;
    }

    public CGaFloat64Visualizer DrawFlatPlane2D(Color color, IFloat64ParametricCurve2D position, IParametricBivector2D direction)
    {
        var animatedPosition =
            SamplingSpecs.CreateXyAnimatedVector3D(position);

        var animatedNormal =
            SamplingSpecs.CreateAnimatedVector3D(
                direction
                    .ToParametricCurve3D(b =>
                        b.ToXyBivector3D().DirectionToUnitNormal3D(LinFloat64Vector3D.UnitSymmetric)
                    )
            );

        if (FlatStyle.DrawPosition)
        {
            SceneComposer.AddPoint(
                GrVisualPoint3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("point"),
                    FlatStyle.GetPointVisualStyle(
                        color.SetAlpha(FlatStyle.AuxGeometryColorAlpha),
                        FlatStyle.Thickness * 1.5
                    ),
                    animatedPosition
                )
            );
        }

        if (FlatStyle.DrawDirection)
        {
            SceneComposer.AddBivector(
                GrVisualBivector3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("bivector"),
                    FlatStyle.GetVectorVisualStyle(
                        color.SetAlpha(FlatStyle.AuxGeometryColorAlpha),
                        FlatStyle.Thickness
                    ),
                    animatedPosition,
                    animatedNormal,
                    FlatStyle.NormalDirectionRadius
                )
            );
        }

        if (FlatStyle.DrawNormalDirection)
        {
            SceneComposer.AddVector(
                GrVisualVector3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("vector"),
                    FlatStyle.GetVectorVisualStyle(
                        color.SetAlpha(FlatStyle.AuxGeometryColorAlpha),
                        FlatStyle.Thickness
                    ),
                    animatedPosition,
                    animatedNormal.SetLength(FlatStyle.NormalDirectionRadius)
                )
            );
        }

        return this;
    }

    public CGaFloat64Visualizer DrawFlatPlane2D(Color color, CGaFloat64ParametricElement element)
    {
        var animatedPosition =
            SamplingSpecs.CreateXyAnimatedVector3D(
                element.PositionToParametricCurve2D()
            );

        var animatedNormal =
            SamplingSpecs.CreateAnimatedVector3D(
                element
                    .DirectionToParametricBivector2D()
                    .XyDirectionToNormal3D(LinFloat64Vector3D.UnitSymmetric)
            );

        if (FlatStyle.DrawPosition)
        {
            SceneComposer.AddPoint(
                GrVisualPoint3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("point"),
                    FlatStyle.GetPointVisualStyle(
                        color.SetAlpha(FlatStyle.AuxGeometryColorAlpha),
                        FlatStyle.Thickness * 1.5
                    ),
                    animatedPosition
                )
            );
        }

        if (FlatStyle.DrawDirection)
        {
            SceneComposer.AddBivector(
                GrVisualBivector3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("bivector"),
                    FlatStyle.GetVectorVisualStyle(
                        color.SetAlpha(FlatStyle.AuxGeometryColorAlpha),
                        FlatStyle.Thickness
                    ),
                    animatedPosition,
                    animatedNormal,
                    FlatStyle.NormalDirectionRadius
                )
            );
        }

        if (FlatStyle.DrawNormalDirection)
        {
            SceneComposer.AddVector(
                GrVisualVector3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("vector"),
                    FlatStyle.GetVectorVisualStyle(
                        color.SetAlpha(FlatStyle.AuxGeometryColorAlpha),
                        FlatStyle.Thickness
                    ),
                    animatedPosition,
                    animatedNormal.SetLength(FlatStyle.NormalDirectionRadius)
                )
            );
        }

        return this;
    }


    public CGaFloat64Visualizer DrawFlatPlane3D(Color color, LinFloat64Vector3D position, LinFloat64Bivector3D direction)
    {
        var normal =
            direction.DirectionToUnitNormal3D(LinFloat64Vector3D.UnitSymmetric);

        SceneComposer.AddDisc(
            GrVisualCircleSurface3D.CreateStatic(
                AnimationComposer.GetNewSceneObjectName("disc"),
                FlatStyle.GetPlaneVisualStyle(color),
                position,
                normal,
                FlatStyle.Radius,
                false
            )
        );

        if (FlatStyle.DrawPosition)
        {
            SceneComposer.AddPoint(
                GrVisualPoint3D.CreateStatic(
                    AnimationComposer.GetNewSceneObjectName("point"),
                    FlatStyle.GetPointVisualStyle(
                        color.SetAlpha(FlatStyle.AuxGeometryColorAlpha),
                        FlatStyle.Thickness * 1.5
                    ),
                    position
                )
            );
        }

        if (FlatStyle.DrawDirection)
        {
            SceneComposer.AddBivector(
                GrVisualBivector3D.CreateStatic(
                    AnimationComposer.GetNewSceneObjectName("bivector"),
                    FlatStyle.GetVectorVisualStyle(
                        color.SetAlpha(FlatStyle.AuxGeometryColorAlpha),
                        FlatStyle.Thickness
                    ),
                    position,
                    normal,
                    FlatStyle.NormalDirectionRadius
                )
            );
        }

        if (FlatStyle.DrawNormalDirection)
        {
            SceneComposer.AddVector(
                GrVisualVector3D.CreateStatic(
                    AnimationComposer.GetNewSceneObjectName("vector"),
                    FlatStyle.GetVectorVisualStyle(
                        color.SetAlpha(FlatStyle.AuxGeometryColorAlpha),
                        FlatStyle.Thickness
                    ),
                    position,
                    normal.SetLength(FlatStyle.NormalDirectionRadius)
                )
            );
        }

        return this;
    }

    public CGaFloat64Visualizer DrawFlatPlane3D(Color color, IParametricCurve3D position, IParametricBivector3D direction)
    {
        var animatedPosition =
            SamplingSpecs.CreateAnimatedVector3D(position);

        var animatedNormal =
            SamplingSpecs.CreateAnimatedVector3D(
                direction.GetNormalVectorCurve()
            );

        SceneComposer.AddDisc(
            GrVisualCircleSurface3D.CreateAnimated(
                AnimationComposer.GetNewSceneObjectName("disc"),
                FlatStyle.GetPlaneVisualStyle(color),
                animatedPosition,
                animatedNormal,
                FlatStyle.Radius,
                false
            )
        );

        if (FlatStyle.DrawPosition)
        {
            SceneComposer.AddPoint(
                GrVisualPoint3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("point"),
                    FlatStyle.GetPointVisualStyle(
                        color.SetAlpha(FlatStyle.AuxGeometryColorAlpha),
                        FlatStyle.Thickness * 1.5
                    ),
                    animatedPosition
                )
            );
        }

        if (FlatStyle.DrawDirection)
        {
            SceneComposer.AddBivector(
                GrVisualBivector3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("bivector"),
                    FlatStyle.GetVectorVisualStyle(
                        color.SetAlpha(FlatStyle.AuxGeometryColorAlpha),
                        FlatStyle.Thickness
                    ),
                    animatedPosition,
                    animatedNormal,
                    FlatStyle.NormalDirectionRadius
                )
            );
        }

        if (FlatStyle.DrawNormalDirection)
        {
            SceneComposer.AddVector(
                GrVisualVector3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("vector"),
                    FlatStyle.GetVectorVisualStyle(
                        color.SetAlpha(FlatStyle.AuxGeometryColorAlpha),
                        FlatStyle.Thickness
                    ),
                    animatedPosition,
                    animatedNormal.SetLength(FlatStyle.NormalDirectionRadius)
                )
            );
        }

        return this;
    }

    public CGaFloat64Visualizer DrawFlatPlane3D(Color color, CGaFloat64ParametricElement element)
    {
        var animatedPosition =
            SamplingSpecs.CreateAnimatedVector3D(
                element.PositionToParametricCurve3D()
            );

        var animatedNormal =
            SamplingSpecs.CreateAnimatedVector3D(
                element.NormalDirectionToParametricCurve3D()
            );

        SceneComposer.AddDisc(
            GrVisualCircleSurface3D.CreateAnimated(
                AnimationComposer.GetNewSceneObjectName("disc"),
                FlatStyle.GetPlaneVisualStyle(color),
                animatedPosition,
                animatedNormal,
                FlatStyle.Radius,
                false
            )
        );

        if (FlatStyle.DrawPosition)
        {
            SceneComposer.AddPoint(
                GrVisualPoint3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("point"),
                    FlatStyle.GetPointVisualStyle(
                        color.SetAlpha(FlatStyle.AuxGeometryColorAlpha),
                        FlatStyle.Thickness * 1.5
                    ),
                    animatedPosition
                )
            );
        }

        if (FlatStyle.DrawDirection)
        {
            SceneComposer.AddBivector(
                GrVisualBivector3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("bivector"),
                    FlatStyle.GetVectorVisualStyle(
                        color.SetAlpha(FlatStyle.AuxGeometryColorAlpha),
                        FlatStyle.Thickness
                    ),
                    animatedPosition,
                    animatedNormal,
                    FlatStyle.NormalDirectionRadius
                )
            );
        }

        if (FlatStyle.DrawNormalDirection)
        {
            SceneComposer.AddVector(
                GrVisualVector3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("vector"),
                    FlatStyle.GetVectorVisualStyle(
                        color.SetAlpha(FlatStyle.AuxGeometryColorAlpha),
                        FlatStyle.Thickness
                    ),
                    animatedPosition,
                    animatedNormal.SetLength(FlatStyle.NormalDirectionRadius)
                )
            );
        }

        return this;
    }


    public CGaFloat64Visualizer DrawFlat(Color color, CGaFloat64Flat element)
    {
        if (GeometricSpace.Is4D)
            return element.Direction.Grade switch
            {
                0 => DrawFlatPoint2D(color, element.PositionToVector2D()),
                1 => DrawFlatLine2D(color, element.PositionToVector2D(), element.DirectionToVector2D()),
                2 => DrawFlatPlane2D(color, element.PositionToVector2D(), element.DirectionToBivector2D()),
                _ => this
            };

        return element.Direction.Grade switch
        {
            0 => DrawFlatPoint3D(color, element.PositionToVector3D()),
            1 => DrawFlatLine3D(color, element.PositionToVector3D(), element.DirectionToVector3D()),
            2 => DrawFlatPlane3D(color, element.PositionToVector3D(), element.DirectionToBivector3D()),
            _ => this
        };
    }


    public CGaFloat64Visualizer DrawRoundPoint2D(Color color, LinFloat64Vector2D center)
    {
        SceneComposer.AddPoint(
            GrVisualPoint3D.CreateStatic(
                AnimationComposer.GetNewSceneObjectName("point"),
                RoundStyle.GetPointVisualStyle(color),
                center.ToXyLinVector3D()
            )
        );

        return this;
    }

    public CGaFloat64Visualizer DrawRoundPoint2D(Color color, IFloat64ParametricCurve2D center)
    {
        SceneComposer.AddPoint(
            GrVisualPoint3D.CreateAnimated(
                AnimationComposer.GetNewSceneObjectName("point"),
                RoundStyle.GetPointVisualStyle(color),
                SamplingSpecs.CreateAnimatedVector3D(center.ToXyParametricCurve3D())
            )
        );

        return this;
    }

    public CGaFloat64Visualizer DrawRoundPoint2D(Color color, CGaFloat64ParametricElement element)
    {
        var animatedPosition =
            SamplingSpecs.CreateXyAnimatedVector3D(
                element.PositionToParametricCurve2D()
            );

        SceneComposer.AddPoint(
            GrVisualPoint3D.CreateAnimated(
                AnimationComposer.GetNewSceneObjectName("point"),
                RoundStyle.GetPointVisualStyle(color),
                animatedPosition
            )
        );

        return this;
    }


    public CGaFloat64Visualizer DrawRoundPoint3D(Color color, LinFloat64Vector3D center)
    {
        SceneComposer.AddPoint(
            GrVisualPoint3D.CreateStatic(
                AnimationComposer.GetNewSceneObjectName("point"),
                RoundStyle.GetPointVisualStyle(color),
                center
            )
        );

        return this;
    }

    public CGaFloat64Visualizer DrawRoundPoint3D(Color color, IParametricCurve3D center)
    {
        SceneComposer.AddPoint(
            GrVisualPoint3D.CreateAnimated(
                AnimationComposer.GetNewSceneObjectName("point"),
                RoundStyle.GetPointVisualStyle(color),
                SamplingSpecs.CreateAnimatedVector3D(center)
            )
        );

        return this;
    }

    public CGaFloat64Visualizer DrawRoundPoint3D(Color color, CGaFloat64ParametricElement element)
    {
        var animatedPosition =
            SamplingSpecs.CreateAnimatedVector3D(
                element.PositionToParametricCurve3D()
            );

        SceneComposer.AddPoint(
            GrVisualPoint3D.CreateAnimated(
                AnimationComposer.GetNewSceneObjectName("point"),
                RoundStyle.GetPointVisualStyle(color),
                animatedPosition
            )
        );

        return this;
    }


    public CGaFloat64Visualizer DrawRoundPointPair2D(Color color, LinFloat64Vector2D center, LinFloat64Vector2D direction, double radius)
    {
        var point1 = (center - direction.SetLength(radius)).ToXyLinVector3D();
        var point2 = (center + direction.SetLength(radius)).ToXyLinVector3D();

        SceneComposer.AddPoint(
            GrVisualPoint3D.CreateStatic(
                AnimationComposer.GetNewSceneObjectName("point"),
                RoundStyle.GetPointPairVisualStyle(color),
                point1
            )
        );

        SceneComposer.AddPoint(
            GrVisualPoint3D.CreateStatic(
                AnimationComposer.GetNewSceneObjectName("point"),
                RoundStyle.GetPointPairVisualStyle(color),
                point2
            )
        );

        SceneComposer.AddLineSegment(
            GrVisualLineSegment3D.CreateStatic(
                AnimationComposer.GetNewSceneObjectName("line"),
                color.SetAlpha(RoundStyle.AuxGeometryColorAlpha).CreateDashedLineCurveStyle(5, 3, 16),
                point1,
                point2
            )
        );

        if (RoundStyle.DrawSphere)
        {
            SceneComposer.AddSphere(
                GrVisualSphereSurface3D.CreateStatic(
                    AnimationComposer.GetNewSceneObjectName("sphere"),
                    RoundStyle.GetSphereVisualStyle(
                        color.SetAlpha(RoundStyle.AuxGeometryColorAlpha)
                    ),
                    center.ToXyLinVector3D(),
                    radius
                )
            );
        }

        if (RoundStyle.DrawPosition)
        {
            SceneComposer.AddPoint(
                GrVisualPoint3D.CreateStatic(
                    AnimationComposer.GetNewSceneObjectName("point"),
                    RoundStyle.GetPointVisualStyle(
                        color.SetAlpha(RoundStyle.AuxGeometryColorAlpha),
                        RoundStyle.Thickness * 1.5
                    ),
                    center.ToXyLinVector3D()
                )
            );
        }

        if (RoundStyle.DrawDirection)
        {
            SceneComposer.AddVector(
                GrVisualVector3D.CreateStatic(
                    AnimationComposer.GetNewSceneObjectName("vector"),
                    RoundStyle.GetVectorVisualStyle(
                        color.SetAlpha(RoundStyle.AuxGeometryColorAlpha),
                        RoundStyle.Thickness
                    ),
                    center.ToXyLinVector3D(),
                    direction.SetLength(RoundStyle.DirectionRadius).ToXyLinVector3D()
                )
            );
        }

        if (RoundStyle.DrawNormalDirection)
        {
            SceneComposer.AddBivector(
                GrVisualBivector3D.CreateStatic(
                    AnimationComposer.GetNewSceneObjectName("bivector"),
                    RoundStyle.GetVectorVisualStyle(
                        color.SetAlpha(RoundStyle.AuxGeometryColorAlpha),
                        RoundStyle.Thickness
                    ),
                    center.ToXyLinVector3D(),
                    direction.ToXyLinVector3D(),
                    RoundStyle.NormalDirectionRadius
                )
            );
        }

        return this;
    }

    public CGaFloat64Visualizer DrawRoundPointPair2D(Color color, IFloat64ParametricCurve2D center, IFloat64ParametricCurve2D direction, IFloat64ParametricScalar radius)
    {
        var animatedCenter =
            SamplingSpecs.CreateXyAnimatedVector3D(center);

        var animatedDirection =
            SamplingSpecs.CreateXyAnimatedVector3D(direction);

        var animatedRadius =
            SamplingSpecs.CreateAnimatedScalar(radius.ToTemporalScalar());

        var animatedPoint1 =
            SamplingSpecs.CreateXyAnimatedVector3D(t =>
                center.GetPoint(t) - direction.GetPoint(t).SetLength(radius.GetValue(t))
            );

        var animatedPoint2 =
            SamplingSpecs.CreateXyAnimatedVector3D(t =>
                center.GetPoint(t) + direction.GetPoint(t).SetLength(radius.GetValue(t))
            );

        SceneComposer.AddPoint(
            GrVisualPoint3D.CreateAnimated(
                AnimationComposer.GetNewSceneObjectName("point"),
                RoundStyle.GetPointPairVisualStyle(color),
                animatedPoint1
            )
        );

        SceneComposer.AddPoint(
            GrVisualPoint3D.CreateAnimated(
                AnimationComposer.GetNewSceneObjectName("point"),
                RoundStyle.GetPointPairVisualStyle(color),
                animatedPoint2
            )
        );

        SceneComposer.AddLineSegment(
            GrVisualLineSegment3D.CreateAnimated(
                AnimationComposer.GetNewSceneObjectName("line"),
                color.SetAlpha(RoundStyle.AuxGeometryColorAlpha).CreateDashedLineCurveStyle(5, 3, 16),
                animatedPoint1,
                animatedPoint2
            )
        );

        if (RoundStyle.DrawSphere)
        {
            SceneComposer.AddSphere(
                GrVisualSphereSurface3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("sphere"),
                    RoundStyle.GetSphereVisualStyle(
                        color.SetAlpha(RoundStyle.AuxGeometryColorAlpha)
                    ),
                    animatedCenter,
                    animatedRadius
                )
            );
        }

        if (RoundStyle.DrawPosition)
        {
            SceneComposer.AddPoint(
                GrVisualPoint3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("point"),
                    RoundStyle.GetPointVisualStyle(
                        color.SetAlpha(RoundStyle.AuxGeometryColorAlpha),
                        RoundStyle.Thickness * 1.5
                    ),
                    animatedCenter
                )
            );
        }

        if (RoundStyle.DrawDirection)
        {
            var animatedScaledDirection =
                SamplingSpecs.CreateXyAnimatedVector3D(
                    t =>
                        direction.GetPoint(t).SetLength(RoundStyle.DirectionRadius)
                );

            SceneComposer.AddVector(
                GrVisualVector3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("vector"),
                    RoundStyle.GetVectorVisualStyle(
                        color.SetAlpha(RoundStyle.AuxGeometryColorAlpha),
                        RoundStyle.Thickness
                    ),
                    animatedCenter,
                    animatedScaledDirection
                )
            );
        }

        if (RoundStyle.DrawNormalDirection)
        {
            SceneComposer.AddBivector(
                GrVisualBivector3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("bivector"),
                    RoundStyle.GetVectorVisualStyle(
                        color.SetAlpha(RoundStyle.AuxGeometryColorAlpha),
                        RoundStyle.Thickness
                    ),
                    animatedCenter,
                    animatedDirection,
                    RoundStyle.NormalDirectionRadius
                )
            );
        }

        return this;
    }

    public CGaFloat64Visualizer DrawRoundPointPair2D(Color color, CGaFloat64ParametricElement element)
    {
        var animatedPosition =
            SamplingSpecs.CreateXyAnimatedVector3D(
                element.PositionToParametricCurve2D()
            );

        var animatedDirection =
            SamplingSpecs.CreateXyAnimatedVector3D(
                element.DirectionToParametricCurve2D()
            );

        var animatedNormal =
            SamplingSpecs.CreateXyAnimatedVector3D(
                element.NormalDirectionToParametricCurve2D()
            );

        var animatedRadius =
            SamplingSpecs.CreateAnimatedScalar(
                element.RealRadiusToTemporalScalar()
            );

        var animatedPoint1 =
            SamplingSpecs.CreateXyAnimatedVector3D(t =>
                {
                    var el = element.GetElement(t);

                    return el.PositionToVector2D() - el.DirectionToVector2D().SetLength(el.RealRadius);
                }
            );

        var animatedPoint2 =
            SamplingSpecs.CreateXyAnimatedVector3D(t =>
                {
                    var el = element.GetElement(t);

                    return el.PositionToVector2D() + el.DirectionToVector2D().SetLength(el.RealRadius);
                }
            );

        SceneComposer.AddPoint(
            GrVisualPoint3D.CreateAnimated(
                AnimationComposer.GetNewSceneObjectName("point"),
                RoundStyle.GetPointPairVisualStyle(color),
                animatedPoint1
            )
        );

        SceneComposer.AddPoint(
            GrVisualPoint3D.CreateAnimated(
                AnimationComposer.GetNewSceneObjectName("point"),
                RoundStyle.GetPointPairVisualStyle(color),
                animatedPoint2
            )
        );

        SceneComposer.AddLineSegment(
            GrVisualLineSegment3D.CreateAnimated(
                AnimationComposer.GetNewSceneObjectName("line"),
                color.SetAlpha(RoundStyle.AuxGeometryColorAlpha).CreateDashedLineCurveStyle(5, 3, 16),
                animatedPoint1,
                animatedPoint2
            )
        );

        if (RoundStyle.DrawSphere)
        {
            SceneComposer.AddSphere(
                GrVisualSphereSurface3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("sphere"),
                    RoundStyle.GetSphereVisualStyle(
                        color.SetAlpha(RoundStyle.AuxGeometryColorAlpha)
                    ),
                    animatedPosition,
                    animatedRadius
                )
            );
        }

        if (RoundStyle.DrawPosition)
        {
            SceneComposer.AddPoint(
                GrVisualPoint3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("point"),
                    RoundStyle.GetPointVisualStyle(
                        color.SetAlpha(RoundStyle.AuxGeometryColorAlpha),
                        RoundStyle.Thickness * 1.5
                    ),
                    animatedPosition
                )
            );
        }

        if (RoundStyle.DrawDirection)
        {
            SceneComposer.AddVector(
                GrVisualVector3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("vector"),
                    RoundStyle.GetVectorVisualStyle(
                        color.SetAlpha(RoundStyle.AuxGeometryColorAlpha),
                        RoundStyle.Thickness
                    ),
                    animatedPosition,
                    animatedDirection.SetLength(RoundStyle.DirectionRadius)
                )
            );
        }

        if (RoundStyle.DrawNormalDirection)
        {
            SceneComposer.AddVector(
                GrVisualVector3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("vector"),
                    RoundStyle.GetVectorVisualStyle(
                        color.SetAlpha(RoundStyle.AuxGeometryColorAlpha),
                        RoundStyle.Thickness
                    ),
                    animatedPosition,
                    animatedNormal.SetLength(RoundStyle.NormalDirectionRadius)
                )
            );
        }

        return this;
    }


    public CGaFloat64Visualizer DrawRoundPointPair3D(Color color, LinFloat64Vector3D center, LinFloat64Vector3D direction, double radius)
    {
        var point1 = center - direction.SetLength(radius);
        var point2 = center + direction.SetLength(radius);

        SceneComposer.AddPoint(
            GrVisualPoint3D.CreateStatic(
                AnimationComposer.GetNewSceneObjectName("point"),
                RoundStyle.GetPointPairVisualStyle(color),
                point1
            )
        );

        SceneComposer.AddPoint(
            GrVisualPoint3D.CreateStatic(
                AnimationComposer.GetNewSceneObjectName("point"),
                RoundStyle.GetPointPairVisualStyle(color),
                point2
            )
        );

        SceneComposer.AddLineSegment(
            GrVisualLineSegment3D.CreateStatic(
                AnimationComposer.GetNewSceneObjectName("line"),
                color.SetAlpha(RoundStyle.AuxGeometryColorAlpha).CreateDashedLineCurveStyle(5, 3, 16),
                point1,
                point2
            )
        );

        if (RoundStyle.DrawSphere)
        {
            SceneComposer.AddSphere(
                GrVisualSphereSurface3D.CreateStatic(
                    AnimationComposer.GetNewSceneObjectName("sphere"),
                    RoundStyle.GetSphereVisualStyle(
                        color.SetAlpha(RoundStyle.AuxGeometryColorAlpha)
                    ),
                    center,
                    radius
                )
            );
        }

        if (RoundStyle.DrawPosition)
        {
            SceneComposer.AddPoint(
                GrVisualPoint3D.CreateStatic(
                    AnimationComposer.GetNewSceneObjectName("point"),
                    RoundStyle.GetPointVisualStyle(
                        color.SetAlpha(RoundStyle.AuxGeometryColorAlpha),
                        RoundStyle.Thickness * 1.5
                    ),
                    center
                )
            );
        }

        if (RoundStyle.DrawDirection)
        {
            SceneComposer.AddVector(
                GrVisualVector3D.CreateStatic(
                    AnimationComposer.GetNewSceneObjectName("vector"),
                    RoundStyle.GetVectorVisualStyle(
                        color.SetAlpha(RoundStyle.AuxGeometryColorAlpha),
                        RoundStyle.Thickness
                    ),
                    center,
                    direction.SetLength(RoundStyle.DirectionRadius)
                )
            );
        }

        if (RoundStyle.DrawNormalDirection)
        {
            SceneComposer.AddBivector(
                GrVisualBivector3D.CreateStatic(
                    AnimationComposer.GetNewSceneObjectName("bivector"),
                    RoundStyle.GetVectorVisualStyle(
                        color.SetAlpha(RoundStyle.AuxGeometryColorAlpha),
                        RoundStyle.Thickness
                    ),
                    center,
                    direction,
                    RoundStyle.NormalDirectionRadius
                )
            );
        }

        return this;
    }

    public CGaFloat64Visualizer DrawRoundPointPair3D(Color color, IParametricCurve3D center, IParametricCurve3D direction, IFloat64ParametricScalar radius)
    {
        var animatedCenter =
            SamplingSpecs.CreateAnimatedVector3D(center);

        var animatedDirection =
            SamplingSpecs.CreateAnimatedVector3D(direction);

        var animatedRadius =
            SamplingSpecs.CreateAnimatedScalar(radius);

        var animatedPoint1 =
            SamplingSpecs.CreateAnimatedVector3D(t =>
                center.GetPoint(t) - direction.GetPoint(t).SetLength(radius.GetValue(t))
            );

        var animatedPoint2 =
            SamplingSpecs.CreateAnimatedVector3D(t =>
                center.GetPoint(t) + direction.GetPoint(t).SetLength(radius.GetValue(t))
            );

        SceneComposer.AddPoint(
            GrVisualPoint3D.CreateAnimated(
                AnimationComposer.GetNewSceneObjectName("point"),
                RoundStyle.GetPointPairVisualStyle(color),
                animatedPoint1
            )
        );

        SceneComposer.AddPoint(
            GrVisualPoint3D.CreateAnimated(
                AnimationComposer.GetNewSceneObjectName("point"),
                RoundStyle.GetPointPairVisualStyle(color),
                animatedPoint2
            )
        );

        SceneComposer.AddLineSegment(
            GrVisualLineSegment3D.CreateAnimated(
                AnimationComposer.GetNewSceneObjectName("line"),
                color.SetAlpha(RoundStyle.AuxGeometryColorAlpha).CreateDashedLineCurveStyle(5, 3, 16),
                animatedPoint1,
                animatedPoint2
            )
        );

        if (RoundStyle.DrawSphere)
        {
            SceneComposer.AddSphere(
                GrVisualSphereSurface3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("sphere"),
                    RoundStyle.GetSphereVisualStyle(
                        color.SetAlpha(RoundStyle.AuxGeometryColorAlpha)
                    ),
                    animatedCenter,
                    animatedRadius
                )
            );
        }

        if (RoundStyle.DrawPosition)
        {
            SceneComposer.AddPoint(
                GrVisualPoint3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("point"),
                    RoundStyle.GetPointVisualStyle(
                        color.SetAlpha(RoundStyle.AuxGeometryColorAlpha),
                        RoundStyle.Thickness * 1.5
                    ),
                    animatedCenter
                )
            );
        }

        if (RoundStyle.DrawDirection)
        {
            var animatedScaledDirection =
                SamplingSpecs.CreateAnimatedVector3D(
                    t =>
                        direction.GetPoint(t).SetLength(RoundStyle.DirectionRadius)
                );

            SceneComposer.AddVector(
                GrVisualVector3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("vector"),
                    RoundStyle.GetVectorVisualStyle(
                        color.SetAlpha(RoundStyle.AuxGeometryColorAlpha),
                        RoundStyle.Thickness
                    ),
                    animatedCenter,
                    animatedScaledDirection
                )
            );
        }

        if (RoundStyle.DrawNormalDirection)
        {
            SceneComposer.AddBivector(
                GrVisualBivector3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("bivector"),
                    RoundStyle.GetVectorVisualStyle(
                        color.SetAlpha(RoundStyle.AuxGeometryColorAlpha),
                        RoundStyle.Thickness
                    ),
                    animatedCenter,
                    animatedDirection,
                    RoundStyle.NormalDirectionRadius
                )
            );
        }

        return this;
    }

    public CGaFloat64Visualizer DrawRoundPointPair3D(Color color, CGaFloat64ParametricElement element)
    {
        var animatedPosition =
            SamplingSpecs.CreateAnimatedVector3D(
                element.PositionToParametricCurve3D()
            );

        var animatedDirection =
            SamplingSpecs.CreateAnimatedVector3D(
                element.DirectionToParametricCurve3D()
            );

        var animatedRadius =
            SamplingSpecs.CreateAnimatedScalar(
                element.RealRadiusToParametricScalar()
            );

        var animatedPoint1 =
            SamplingSpecs.CreateAnimatedVector3D(t =>
                {
                    var el = element.GetElement(t);

                    return el.PositionToVector3D() - el.DirectionToVector3D().SetLength(el.RealRadius);
                }
            );

        var animatedPoint2 =
            SamplingSpecs.CreateAnimatedVector3D(t =>
                {
                    var el = element.GetElement(t);

                    return el.PositionToVector3D() + el.DirectionToVector3D().SetLength(el.RealRadius);
                }
            );

        SceneComposer.AddPoint(
            GrVisualPoint3D.CreateAnimated(
                AnimationComposer.GetNewSceneObjectName("point"),
                RoundStyle.GetPointPairVisualStyle(color),
                animatedPoint1
            )
        );

        SceneComposer.AddPoint(
            GrVisualPoint3D.CreateAnimated(
                AnimationComposer.GetNewSceneObjectName("point"),
                RoundStyle.GetPointPairVisualStyle(color),
                animatedPoint2
            )
        );

        SceneComposer.AddLineSegment(
            GrVisualLineSegment3D.CreateAnimated(
                AnimationComposer.GetNewSceneObjectName("line"),
                color.SetAlpha(RoundStyle.AuxGeometryColorAlpha).CreateDashedLineCurveStyle(5, 3, 16),
                animatedPoint1,
                animatedPoint2
            )
        );

        if (RoundStyle.DrawSphere)
        {
            SceneComposer.AddSphere(
                GrVisualSphereSurface3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("sphere"),
                    RoundStyle.GetSphereVisualStyle(
                        color.SetAlpha(RoundStyle.AuxGeometryColorAlpha)
                    ),
                    animatedPosition,
                    animatedRadius
                )
            );
        }

        if (RoundStyle.DrawPosition)
        {
            SceneComposer.AddPoint(
                GrVisualPoint3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("point"),
                    RoundStyle.GetPointVisualStyle(
                        color.SetAlpha(RoundStyle.AuxGeometryColorAlpha),
                        RoundStyle.Thickness * 1.5
                    ),
                    animatedPosition
                )
            );
        }

        if (RoundStyle.DrawDirection)
        {
            SceneComposer.AddVector(
                GrVisualVector3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("vector"),
                    RoundStyle.GetVectorVisualStyle(
                        color.SetAlpha(RoundStyle.AuxGeometryColorAlpha),
                        RoundStyle.Thickness
                    ),
                    animatedPosition,
                    animatedDirection.SetLength(RoundStyle.DirectionRadius)
                )
            );
        }

        if (RoundStyle.DrawNormalDirection)
        {
            SceneComposer.AddBivector(
                GrVisualBivector3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("bivector"),
                    RoundStyle.GetVectorVisualStyle(
                        color.SetAlpha(RoundStyle.AuxGeometryColorAlpha),
                        RoundStyle.Thickness
                    ),
                    animatedPosition,
                    animatedDirection,
                    RoundStyle.NormalDirectionRadius
                )
            );
        }

        return this;
    }


    public CGaFloat64Visualizer DrawRoundCircle2D(Color color, LinFloat64Vector2D center, LinFloat64Bivector2D direction, double radius)
    {
        var normal =
            direction.ToXyBivector3D().DirectionToUnitNormal3D(LinFloat64Vector3D.UnitSymmetric);

        SceneComposer.AddCircleCurve(
            GrVisualCircleCurve3D.CreateStatic(
                AnimationComposer.GetNewSceneObjectName("circle"),
                RoundStyle.GetCircleVisualStyle(color),
                center.ToXyLinVector3D(),
                normal,
                radius
            )
        );

        if (RoundStyle.DrawSphere)
        {
            SceneComposer.AddSphere(
                GrVisualSphereSurface3D.CreateStatic(
                    AnimationComposer.GetNewSceneObjectName("sphere"),
                    RoundStyle.GetSphereVisualStyle(
                        color.SetAlpha(RoundStyle.AuxGeometryColorAlpha)
                    ),
                    center.ToXyLinVector3D(),
                    radius
                )
            );
        }

        if (RoundStyle.DrawCenter)
        {
            SceneComposer.AddPoint(
                GrVisualPoint3D.CreateStatic(
                    AnimationComposer.GetNewSceneObjectName("point"),
                    RoundStyle.GetPointVisualStyle(
                        color.SetAlpha(RoundStyle.AuxGeometryColorAlpha)
                    ),
                    center.ToXyLinVector3D()
                )
            );
        }

        if (RoundStyle.DrawPosition)
        {
            SceneComposer.AddPoint(
                GrVisualPoint3D.CreateStatic(
                    AnimationComposer.GetNewSceneObjectName("point"),
                    RoundStyle.GetPointVisualStyle(
                        color.SetAlpha(RoundStyle.AuxGeometryColorAlpha),
                        RoundStyle.Thickness * 1.5
                    ),
                    center.ToXyLinVector3D()
                )
            );
        }

        if (RoundStyle.DrawDirection)
        {
            SceneComposer.AddBivector(
                GrVisualBivector3D.CreateStatic(
                    AnimationComposer.GetNewSceneObjectName("bivector"),
                    RoundStyle.GetVectorVisualStyle(
                        color.SetAlpha(RoundStyle.AuxGeometryColorAlpha),
                        RoundStyle.Thickness
                    ),
                    center.ToXyLinVector3D(),
                    normal,
                    RoundStyle.DirectionRadius
                )
            );
        }

        if (RoundStyle.DrawNormalDirection)
        {
            SceneComposer.AddVector(
                GrVisualVector3D.CreateStatic(
                    AnimationComposer.GetNewSceneObjectName("vector"),
                    RoundStyle.GetVectorVisualStyle(
                        color.SetAlpha(RoundStyle.AuxGeometryColorAlpha),
                        RoundStyle.Thickness
                    ),
                    center.ToXyLinVector3D(),
                    normal
                )
            );
        }

        return this;
    }

    public CGaFloat64Visualizer DrawRoundCircle2D(Color color, IFloat64ParametricCurve2D center, IParametricBivector2D direction, IFloat64ParametricScalar radius)
    {
        var animatedCenter =
            SamplingSpecs.CreateAnimatedVector3D(center.ToXyParametricCurve3D());

        var animatedNormal =
            SamplingSpecs.CreateAnimatedVector3D(
                direction
                .ToParametricCurve3D(b =>
                    b.ToXyBivector3D().DirectionToUnitNormal3D(LinFloat64Vector3D.UnitSymmetric)
                )
            );

        var animatedRadius =
            SamplingSpecs.CreateAnimatedScalar(radius);

        SceneComposer.AddCircleCurve(
            GrVisualCircleCurve3D.CreateAnimated(
                AnimationComposer.GetNewSceneObjectName("circle"),
                RoundStyle.GetCircleVisualStyle(color),
                animatedCenter,
                animatedNormal,
                animatedRadius
            )
        );

        if (RoundStyle.DrawSphere)
        {
            SceneComposer.AddSphere(
                GrVisualSphereSurface3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("sphere"),
                    RoundStyle.GetSphereVisualStyle(
                        color.SetAlpha(RoundStyle.AuxGeometryColorAlpha)
                    ),
                    animatedCenter,
                    animatedRadius
                )
            );
        }

        if (RoundStyle.DrawCenter)
        {
            SceneComposer.AddPoint(
                GrVisualPoint3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("point"),
                    RoundStyle.GetPointVisualStyle(
                        color.SetAlpha(RoundStyle.AuxGeometryColorAlpha)
                    ),
                    animatedCenter
                )
            );
        }

        if (RoundStyle.DrawPosition)
        {
            SceneComposer.AddPoint(
                GrVisualPoint3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("point"),
                    RoundStyle.GetPointVisualStyle(
                        color.SetAlpha(RoundStyle.AuxGeometryColorAlpha),
                        RoundStyle.Thickness * 1.5
                    ),
                    animatedCenter
                )
            );
        }

        if (RoundStyle.DrawDirection)
        {
            SceneComposer.AddBivector(
                GrVisualBivector3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("bivector"),
                    RoundStyle.GetVectorVisualStyle(
                        color.SetAlpha(RoundStyle.AuxGeometryColorAlpha),
                        RoundStyle.Thickness
                    ),
                    animatedCenter,
                    animatedNormal,
                    RoundStyle.DirectionRadius
                )
            );
        }

        if (RoundStyle.DrawNormalDirection)
        {
            SceneComposer.AddVector(
                GrVisualVector3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("vector"),
                    RoundStyle.GetVectorVisualStyle(
                        color.SetAlpha(RoundStyle.AuxGeometryColorAlpha),
                        RoundStyle.Thickness
                    ),
                    animatedCenter,
                    animatedNormal.SetLength(RoundStyle.NormalDirectionRadius)
                )
            );
        }

        return this;
    }

    public CGaFloat64Visualizer DrawRoundCircle2D(Color color, CGaFloat64ParametricElement element)
    {
        var animatedPosition =
            SamplingSpecs.CreateXyAnimatedVector3D(
                element.PositionToParametricCurve2D()
            );

        var animatedNormal =
            SamplingSpecs.CreateAnimatedVector3D(
                element
                    .DirectionToParametricBivector2D()
                    .XyDirectionToNormal3D(LinFloat64Vector3D.UnitSymmetric)
            );

        var animatedRadius =
            SamplingSpecs.CreateAnimatedScalar(
                element.RealRadiusToParametricScalar()
            );

        SceneComposer.AddCircleCurve(
            GrVisualCircleCurve3D.CreateAnimated(
                AnimationComposer.GetNewSceneObjectName("circle"),
                RoundStyle.GetCircleVisualStyle(color),
                animatedPosition,
                animatedNormal,
                animatedRadius
            )
        );

        if (RoundStyle.DrawSphere)
        {
            SceneComposer.AddSphere(
                GrVisualSphereSurface3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("sphere"),
                    RoundStyle.GetSphereVisualStyle(
                        color.SetAlpha(RoundStyle.AuxGeometryColorAlpha)
                    ),
                    animatedPosition,
                    animatedRadius
                )
            );
        }

        if (RoundStyle.DrawCenter)
        {
            SceneComposer.AddPoint(
                GrVisualPoint3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("point"),
                    RoundStyle.GetPointVisualStyle(
                        color.SetAlpha(RoundStyle.AuxGeometryColorAlpha)
                    ),
                    animatedPosition
                )
            );
        }

        if (RoundStyle.DrawPosition)
        {
            SceneComposer.AddPoint(
                GrVisualPoint3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("point"),
                    RoundStyle.GetPointVisualStyle(
                        color.SetAlpha(RoundStyle.AuxGeometryColorAlpha),
                        RoundStyle.Thickness * 1.5
                    ),
                    animatedPosition
                )
            );
        }

        if (RoundStyle.DrawDirection)
        {
            SceneComposer.AddBivector(
                GrVisualBivector3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("bivector"),
                    RoundStyle.GetVectorVisualStyle(
                        color.SetAlpha(RoundStyle.AuxGeometryColorAlpha),
                        RoundStyle.Thickness
                    ),
                    animatedPosition,
                    animatedNormal,
                    RoundStyle.DirectionRadius
                )
            );
        }

        if (RoundStyle.DrawNormalDirection)
        {
            SceneComposer.AddVector(
                GrVisualVector3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("vector"),
                    RoundStyle.GetVectorVisualStyle(
                        color.SetAlpha(RoundStyle.AuxGeometryColorAlpha),
                        RoundStyle.Thickness
                    ),
                    animatedPosition,
                    animatedNormal.SetLength(RoundStyle.NormalDirectionRadius)
                )
            );
        }

        return this;
    }


    public CGaFloat64Visualizer DrawRoundCircle3D(Color color, LinFloat64Vector3D center, LinFloat64Bivector3D direction, double radius)
    {
        var normal =
            direction.DirectionToUnitNormal3D(LinFloat64Vector3D.UnitSymmetric);

        SceneComposer.AddCircleCurve(
            GrVisualCircleCurve3D.CreateStatic(
                AnimationComposer.GetNewSceneObjectName("circle"),
                RoundStyle.GetCircleVisualStyle(color),
                center,
                normal,
                radius
            )
        );

        if (RoundStyle.DrawSphere)
        {
            SceneComposer.AddSphere(
                GrVisualSphereSurface3D.CreateStatic(
                    AnimationComposer.GetNewSceneObjectName("sphere"),
                    RoundStyle.GetSphereVisualStyle(
                        color.SetAlpha(RoundStyle.AuxGeometryColorAlpha)
                    ),
                    center,
                    radius
                )
            );
        }

        if (RoundStyle.DrawCenter)
        {
            SceneComposer.AddPoint(
                GrVisualPoint3D.CreateStatic(
                    AnimationComposer.GetNewSceneObjectName("point"),
                    RoundStyle.GetPointVisualStyle(
                        color.SetAlpha(RoundStyle.AuxGeometryColorAlpha)
                    ),
                    center
                )
            );
        }

        if (RoundStyle.DrawPosition)
        {
            SceneComposer.AddPoint(
                GrVisualPoint3D.CreateStatic(
                    AnimationComposer.GetNewSceneObjectName("point"),
                    RoundStyle.GetPointVisualStyle(
                        color.SetAlpha(RoundStyle.AuxGeometryColorAlpha),
                        RoundStyle.Thickness * 1.5
                    ),
                    center
                )
            );
        }

        if (RoundStyle.DrawDirection)
        {
            SceneComposer.AddBivector(
                GrVisualBivector3D.CreateStatic(
                    AnimationComposer.GetNewSceneObjectName("bivector"),
                    RoundStyle.GetVectorVisualStyle(
                        color.SetAlpha(RoundStyle.AuxGeometryColorAlpha),
                        RoundStyle.Thickness
                    ),
                    center,
                    normal,
                    RoundStyle.DirectionRadius
                )
            );
        }

        if (RoundStyle.DrawNormalDirection)
        {
            SceneComposer.AddVector(
                GrVisualVector3D.CreateStatic(
                    AnimationComposer.GetNewSceneObjectName("vector"),
                    RoundStyle.GetVectorVisualStyle(
                        color.SetAlpha(RoundStyle.AuxGeometryColorAlpha),
                        RoundStyle.Thickness
                    ),
                    center,
                    normal
                )
            );
        }

        return this;
    }

    public CGaFloat64Visualizer DrawRoundCircle3D(Color color, IParametricCurve3D center, IParametricBivector3D direction, IFloat64ParametricScalar radius)
    {
        var animatedCenter =
            SamplingSpecs.CreateAnimatedVector3D(center);

        var animatedNormal =
            SamplingSpecs.CreateAnimatedVector3D(direction.GetNormalVectorCurve());

        var animatedRadius =
            SamplingSpecs.CreateAnimatedScalar(radius);

        SceneComposer.AddCircleCurve(
            GrVisualCircleCurve3D.CreateAnimated(
                AnimationComposer.GetNewSceneObjectName("circle"),
                RoundStyle.GetCircleVisualStyle(color),
                animatedCenter,
                animatedNormal,
                animatedRadius
            )
        );

        if (RoundStyle.DrawSphere)
        {
            SceneComposer.AddSphere(
                GrVisualSphereSurface3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("sphere"),
                    RoundStyle.GetSphereVisualStyle(
                        color.SetAlpha(RoundStyle.AuxGeometryColorAlpha)
                    ),
                    animatedCenter,
                    animatedRadius
                )
            );
        }

        if (RoundStyle.DrawCenter)
        {
            SceneComposer.AddPoint(
                GrVisualPoint3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("point"),
                    RoundStyle.GetPointVisualStyle(
                        color.SetAlpha(RoundStyle.AuxGeometryColorAlpha)
                    ),
                    animatedCenter
                )
            );
        }

        if (RoundStyle.DrawPosition)
        {
            SceneComposer.AddPoint(
                GrVisualPoint3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("point"),
                    RoundStyle.GetPointVisualStyle(
                        color.SetAlpha(RoundStyle.AuxGeometryColorAlpha),
                        RoundStyle.Thickness * 1.5
                    ),
                    animatedCenter
                )
            );
        }

        if (RoundStyle.DrawDirection)
        {
            SceneComposer.AddBivector(
                GrVisualBivector3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("bivector"),
                    RoundStyle.GetVectorVisualStyle(
                        color.SetAlpha(RoundStyle.AuxGeometryColorAlpha),
                        RoundStyle.Thickness
                    ),
                    animatedCenter,
                    animatedNormal,
                    RoundStyle.DirectionRadius
                )
            );
        }

        if (RoundStyle.DrawNormalDirection)
        {
            SceneComposer.AddVector(
                GrVisualVector3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("vector"),
                    RoundStyle.GetVectorVisualStyle(
                        color.SetAlpha(RoundStyle.AuxGeometryColorAlpha),
                        RoundStyle.Thickness
                    ),
                    animatedCenter,
                    animatedNormal.SetLength(RoundStyle.NormalDirectionRadius)
                )
            );
        }

        return this;
    }

    public CGaFloat64Visualizer DrawRoundCircle3D(Color color, CGaFloat64ParametricElement element)
    {
        var animatedPosition =
            SamplingSpecs.CreateAnimatedVector3D(
                element.PositionToParametricCurve3D()
            );

        var animatedNormal =
            SamplingSpecs.CreateAnimatedVector3D(
                element.NormalDirectionToParametricCurve3D()
            );

        var animatedRadius =
            SamplingSpecs.CreateAnimatedScalar(
                element.RealRadiusToParametricScalar()
            );

        SceneComposer.AddCircleCurve(
            GrVisualCircleCurve3D.CreateAnimated(
                AnimationComposer.GetNewSceneObjectName("circle"),
                RoundStyle.GetCircleVisualStyle(color),
                animatedPosition,
                animatedNormal,
                animatedRadius
            )
        );

        if (RoundStyle.DrawSphere)
        {
            SceneComposer.AddSphere(
                GrVisualSphereSurface3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("sphere"),
                    RoundStyle.GetSphereVisualStyle(
                        color.SetAlpha(RoundStyle.AuxGeometryColorAlpha)
                    ),
                    animatedPosition,
                    animatedRadius
                )
            );
        }

        if (RoundStyle.DrawCenter)
        {
            SceneComposer.AddPoint(
                GrVisualPoint3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("point"),
                    RoundStyle.GetPointVisualStyle(
                        color.SetAlpha(RoundStyle.AuxGeometryColorAlpha)
                    ),
                    animatedPosition
                )
            );
        }

        if (RoundStyle.DrawPosition)
        {
            SceneComposer.AddPoint(
                GrVisualPoint3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("point"),
                    RoundStyle.GetPointVisualStyle(
                        color.SetAlpha(RoundStyle.AuxGeometryColorAlpha),
                        RoundStyle.Thickness * 1.5
                    ),
                    animatedPosition
                )
            );
        }

        if (RoundStyle.DrawDirection)
        {
            SceneComposer.AddBivector(
                GrVisualBivector3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("bivector"),
                    RoundStyle.GetVectorVisualStyle(
                        color.SetAlpha(RoundStyle.AuxGeometryColorAlpha),
                        RoundStyle.Thickness
                    ),
                    animatedPosition,
                    animatedNormal,
                    RoundStyle.DirectionRadius
                )
            );
        }

        if (RoundStyle.DrawNormalDirection)
        {
            SceneComposer.AddVector(
                GrVisualVector3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("vector"),
                    RoundStyle.GetVectorVisualStyle(
                        color.SetAlpha(RoundStyle.AuxGeometryColorAlpha),
                        RoundStyle.Thickness
                    ),
                    animatedPosition,
                    animatedNormal.SetLength(RoundStyle.NormalDirectionRadius)
                )
            );
        }

        return this;
    }


    public CGaFloat64Visualizer DrawRoundSphere3D(Color color, LinFloat64Vector3D center, LinFloat64Trivector3D direction, double radius)
    {
        SceneComposer.AddSphere(
            GrVisualSphereSurface3D.CreateStatic(
                AnimationComposer.GetNewSceneObjectName("sphere"),
                RoundStyle.GetSphereVisualStyle(color),
                center,
                radius
            )
        );

        if (RoundStyle.DrawCenter)
        {
            SceneComposer.AddPoint(
                GrVisualPoint3D.CreateStatic(
                    AnimationComposer.GetNewSceneObjectName("point"),
                    RoundStyle.GetPointVisualStyle(
                        color.SetAlpha(RoundStyle.AuxGeometryColorAlpha),
                        RoundStyle.Thickness * 1.5
                    ),
                    center
                )
            );
        }

        return this;
    }

    public CGaFloat64Visualizer DrawRoundSphere3D(Color color, IParametricCurve3D center, IParametricTrivector3D direction, IFloat64ParametricScalar radius)
    {
        var animatedCenter =
            SamplingSpecs.CreateAnimatedVector3D(center);

        var animatedRadius =
            SamplingSpecs.CreateAnimatedScalar(radius);

        SceneComposer.AddSphere(
            GrVisualSphereSurface3D.CreateAnimated(
                AnimationComposer.GetNewSceneObjectName("sphere"),
                RoundStyle.GetSphereVisualStyle(color),
                animatedCenter,
                animatedRadius
            )
        );

        if (RoundStyle.DrawCenter)
        {
            SceneComposer.AddPoint(
                GrVisualPoint3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("point"),
                    RoundStyle.GetPointVisualStyle(
                        color.SetAlpha(RoundStyle.AuxGeometryColorAlpha),
                        RoundStyle.Thickness * 1.5
                    ),
                    animatedCenter
                )
            );
        }

        return this;
    }

    public CGaFloat64Visualizer DrawRoundSphere3D(Color color, CGaFloat64ParametricElement element)
    {
        var animatedPosition =
            SamplingSpecs.CreateAnimatedVector3D(
                element.PositionToParametricCurve3D()
            );

        var animatedRadius =
            SamplingSpecs.CreateAnimatedScalar(
                element.RealRadiusToParametricScalar()
            );

        SceneComposer.AddSphere(
            GrVisualSphereSurface3D.CreateAnimated(
                AnimationComposer.GetNewSceneObjectName("sphere"),
                RoundStyle.GetSphereVisualStyle(color),
                animatedPosition,
                animatedRadius
            )
        );

        if (RoundStyle.DrawCenter)
        {
            SceneComposer.AddPoint(
                GrVisualPoint3D.CreateAnimated(
                    AnimationComposer.GetNewSceneObjectName("point"),
                    RoundStyle.GetPointVisualStyle(
                        color.SetAlpha(RoundStyle.AuxGeometryColorAlpha),
                        RoundStyle.Thickness * 1.5
                    ),
                    animatedPosition
                )
            );
        }

        return this;
    }


    public CGaFloat64Visualizer DrawRound(Color color, CGaFloat64Round element)
    {
        if (GeometricSpace.Is4D)
            return element.Direction.Grade switch
            {
                0 => DrawRoundPoint2D(color, element.PositionToVector2D()),
                1 => DrawRoundPointPair2D(color, element.PositionToVector2D(), element.DirectionToVector2D(), element.RealRadius),
                2 => DrawRoundCircle2D(color, element.PositionToVector2D(), element.DirectionToBivector2D(), element.RealRadius),
                _ => this
            };

        return element.Direction.Grade switch
        {
            0 => DrawRoundPoint3D(color, element.PositionToVector3D()),
            1 => DrawRoundPointPair3D(color, element.PositionToVector3D(), element.DirectionToVector3D(), element.RealRadius),
            2 => DrawRoundCircle3D(color, element.PositionToVector3D(), element.DirectionToBivector3D(), element.RealRadius),
            3 => DrawRoundSphere3D(color, element.PositionToVector3D(), element.DirectionToTrivector3D(), element.RealRadius),
            _ => this
        };
    }


    public CGaFloat64Visualizer DrawElement(Color color, CGaFloat64Element element)
    {
        return element switch
        {
            CGaFloat64Direction direction => DrawDirection(color, direction),
            CGaFloat64Tangent tangent => DrawTangent(color, tangent),
            CGaFloat64Flat flat => DrawFlat(color, flat),
            CGaFloat64Round round => DrawRound(color, round),
            _ => this
        };
    }


}