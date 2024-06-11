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
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Animations;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Basic;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Curves;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Styles;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Surfaces;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Web.Colors;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Visualizer;

public class CGaFloat64Visualizer :
    GeometryVisualizer
{
    public CGaFloat64GeometricSpace GeometricSpace { get; }

    public CGaFloat64VisualizerDirectionStyle DirectionStyle { get; }

    public CGaFloat64VisualizerTangentStyle TangentStyle { get; }

    public CGaFloat64VisualizerFlatStyle FlatStyle { get; }

    public CGaFloat64VisualizerRoundStyle RoundStyle { get; }


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
        return AnimationSpecs
            .GetFrameIndexValuePairs(element.GetElement)
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


    public override CGaFloat64Visualizer BeginDrawing2D(string title, IReadOnlyDictionary<string, string> laTeXDictionary)
    {
        base.BeginDrawing2D(title, laTeXDictionary);

        DirectionStyle.SetStyle(0.08);
        TangentStyle.SetStyle(0.08);
        FlatStyle.SetStyle(0.08, 6);
        RoundStyle.SetStyle(0.08);

        return this;
    }

    public override CGaFloat64Visualizer BeginDrawing3D(string title, IReadOnlyDictionary<string, string> laTeXDictionary)
    {
        base.BeginDrawing3D(title, laTeXDictionary);

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
        MainSceneComposer.AddPoint(
            GrVisualPoint3D.CreateStatic(
                GetNewSceneObjectName("point"),
                DirectionStyle.GetPointVisualStyle(color),
                position.ToXyLinVector3D()
            )
        );

        return this;
    }

    public CGaFloat64Visualizer DrawDirectionPoint2D(Color color, IFloat64ParametricCurve2D position)
    {
        MainSceneComposer.AddPoint(
            GrVisualPoint3D.CreateAnimated(
                GetNewSceneObjectName("point"),
                DirectionStyle.GetPointVisualStyle(color),
                AnimationSpecs.CreateXyAnimatedVector3D(position)
            )
        );

        return this;
    }

    public CGaFloat64Visualizer DrawDirectionPoint2D(Color color, CGaFloat64ParametricElement element)
    {
        var animatedPosition =
            AnimationSpecs.CreateXyAnimatedVector3D(
                element.PositionToParametricCurve2D()
            );

        MainSceneComposer.AddPoint(
            GrVisualPoint3D.CreateAnimated(
                GetNewSceneObjectName("point"),
                DirectionStyle.GetPointVisualStyle(color),
                animatedPosition
            )
        );

        return this;
    }


    public CGaFloat64Visualizer DrawDirectionPoint3D(Color color, LinFloat64Vector3D position)
    {
        MainSceneComposer.AddPoint(
            GrVisualPoint3D.CreateStatic(
                GetNewSceneObjectName("point"),
                DirectionStyle.GetPointVisualStyle(color),
                position
            )
        );

        return this;
    }

    public CGaFloat64Visualizer DrawDirectionPoint3D(Color color, IParametricCurve3D position)
    {
        MainSceneComposer.AddPoint(
            GrVisualPoint3D.CreateAnimated(
                GetNewSceneObjectName("point"),
                DirectionStyle.GetPointVisualStyle(color),
                AnimationSpecs.CreateAnimatedVector3D(position)
            )
        );

        return this;
    }

    public CGaFloat64Visualizer DrawDirectionPoint3D(Color color, CGaFloat64ParametricElement element)
    {
        var animatedPosition =
            AnimationSpecs.CreateAnimatedVector3D(
                element.PositionToParametricCurve3D()
            );

        MainSceneComposer.AddPoint(
            GrVisualPoint3D.CreateAnimated(
                GetNewSceneObjectName("point"),
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
            MainSceneComposer.AddPoint(
                GrVisualPoint3D.CreateStatic(
                    GetNewSceneObjectName("point"),
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
            MainSceneComposer.AddVector(
                GrVisualVector3D.CreateStatic(
                    GetNewSceneObjectName("vector"),
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
            MainSceneComposer.AddBivector(
                GrVisualBivector3D.CreateStatic(
                    GetNewSceneObjectName("bivector"),
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
            AnimationSpecs.CreateXyAnimatedVector3D(position);

        var animatedDirection =
            AnimationSpecs.CreateXyAnimatedVector3D(direction);

        if (DirectionStyle.DrawPosition)
        {
            MainSceneComposer.AddPoint(
                GrVisualPoint3D.CreateAnimated(
                    GetNewSceneObjectName("point"),
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
                AnimationSpecs.CreateAnimatedVector3D(t =>
                    direction.GetPoint(t).SetLength(DirectionStyle.DirectionRadius).ToXyLinVector3D()
                );

            MainSceneComposer.AddVector(
                GrVisualVector3D.CreateAnimated(
                    GetNewSceneObjectName("vector"),
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
            MainSceneComposer.AddBivector(
                GrVisualBivector3D.CreateAnimated(
                    GetNewSceneObjectName("bivector"),
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
            AnimationSpecs.CreateXyAnimatedVector3D(
                element.PositionToParametricCurve2D()
            );

        var animatedDirection =
            AnimationSpecs.CreateXyAnimatedVector3D(
                element.DirectionToParametricCurve2D()
            );

        var animatedNormal =
            AnimationSpecs.CreateXyAnimatedVector3D(
                element.NormalDirectionToParametricCurve2D()
            );

        if (DirectionStyle.DrawPosition)
        {
            MainSceneComposer.AddPoint(
                GrVisualPoint3D.CreateAnimated(
                    GetNewSceneObjectName("point"),
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
            MainSceneComposer.AddVector(
                GrVisualVector3D.CreateAnimated(
                    GetNewSceneObjectName("vector"),
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
            MainSceneComposer.AddVector(
                GrVisualVector3D.CreateAnimated(
                    GetNewSceneObjectName("vector"),
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
            MainSceneComposer.AddPoint(
                GrVisualPoint3D.CreateStatic(
                    GetNewSceneObjectName("point"),
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
            MainSceneComposer.AddVector(
                GrVisualVector3D.CreateStatic(
                    GetNewSceneObjectName("vector"),
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
            MainSceneComposer.AddBivector(
                GrVisualBivector3D.CreateStatic(
                    GetNewSceneObjectName("bivector"),
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
            AnimationSpecs.CreateAnimatedVector3D(position);

        var animatedDirection =
            AnimationSpecs.CreateAnimatedVector3D(direction);

        if (DirectionStyle.DrawPosition)
        {
            MainSceneComposer.AddPoint(
                GrVisualPoint3D.CreateAnimated(
                    GetNewSceneObjectName("point"),
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
                AnimationSpecs.CreateAnimatedVector3D(t =>
                    direction.GetPoint(t).SetLength(DirectionStyle.DirectionRadius)
                );

            MainSceneComposer.AddVector(
                GrVisualVector3D.CreateAnimated(
                    GetNewSceneObjectName("vector"),
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
            MainSceneComposer.AddBivector(
                GrVisualBivector3D.CreateAnimated(
                    GetNewSceneObjectName("bivector"),
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
            AnimationSpecs.CreateAnimatedVector3D(
                element.PositionToParametricCurve3D()
            );

        var animatedDirection =
            AnimationSpecs.CreateAnimatedVector3D(
                element.DirectionToParametricCurve3D()
            );

        if (DirectionStyle.DrawPosition)
        {
            MainSceneComposer.AddPoint(
                GrVisualPoint3D.CreateAnimated(
                    GetNewSceneObjectName("point"),
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
            MainSceneComposer.AddVector(
                GrVisualVector3D.CreateAnimated(
                    GetNewSceneObjectName("vector"),
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
            MainSceneComposer.AddBivector(
                GrVisualBivector3D.CreateAnimated(
                    GetNewSceneObjectName("bivector"),
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
            MainSceneComposer.AddPoint(
                GrVisualPoint3D.CreateStatic(
                    GetNewSceneObjectName("point"),
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
            MainSceneComposer.AddBivector(
                GrVisualBivector3D.CreateStatic(
                    GetNewSceneObjectName("bivector"),
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
            MainSceneComposer.AddVector(
                GrVisualVector3D.CreateStatic(
                    GetNewSceneObjectName("vector"),
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
            AnimationSpecs.CreateXyAnimatedVector3D(position);

        if (DirectionStyle.DrawPosition)
        {
            MainSceneComposer.AddPoint(
                GrVisualPoint3D.CreateAnimated(
                    GetNewSceneObjectName("point"),
                    DirectionStyle.GetPointVisualStyle(
                        color.SetAlpha(DirectionStyle.AuxGeometryColorAlpha),
                        DirectionStyle.Thickness * 1.5
                    ),
                    animatedPosition
                )
            );
        }

        var animatedNormal =
            AnimationSpecs.CreateAnimatedVector3D(
                direction
                    .ToParametricCurve3D(b =>
                        b.ToXyBivector3D().DirectionToUnitNormal3D(LinFloat64Vector3D.UnitSymmetric)
                    )
            );

        if (DirectionStyle.DrawDirection)
        {
            MainSceneComposer.AddBivector(
                GrVisualBivector3D.CreateAnimated(
                    GetNewSceneObjectName("bivector"),
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
            MainSceneComposer.AddVector(
                GrVisualVector3D.CreateAnimated(
                    GetNewSceneObjectName("vector"),
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
            AnimationSpecs.CreateXyAnimatedVector3D(
                element.PositionToParametricCurve2D()
            );

        var animatedNormal =
            AnimationSpecs.CreateAnimatedVector3D(
                element
                    .DirectionToParametricBivector2D()
                    .XyDirectionToNormal3D(LinFloat64Vector3D.UnitSymmetric)
            );

        if (DirectionStyle.DrawPosition)
        {
            MainSceneComposer.AddPoint(
                GrVisualPoint3D.CreateAnimated(
                    GetNewSceneObjectName("point"),
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
            MainSceneComposer.AddBivector(
                GrVisualBivector3D.CreateAnimated(
                    GetNewSceneObjectName("bivector"),
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
            MainSceneComposer.AddVector(
                GrVisualVector3D.CreateAnimated(
                    GetNewSceneObjectName("vector"),
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
            MainSceneComposer.AddPoint(
                GrVisualPoint3D.CreateStatic(
                    GetNewSceneObjectName("point"),
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
            MainSceneComposer.AddBivector(
                GrVisualBivector3D.CreateStatic(
                    GetNewSceneObjectName("bivector"),
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
            MainSceneComposer.AddVector(
                GrVisualVector3D.CreateStatic(
                    GetNewSceneObjectName("vector"),
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
            AnimationSpecs.CreateAnimatedVector3D(position);

        var animatedNormal =
            AnimationSpecs.CreateAnimatedVector3D(
                direction.GetNormalVectorCurve()
            );

        if (DirectionStyle.DrawPosition)
        {
            MainSceneComposer.AddPoint(
                GrVisualPoint3D.CreateAnimated(
                    GetNewSceneObjectName("point"),
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
            MainSceneComposer.AddBivector(
                GrVisualBivector3D.CreateAnimated(
                    GetNewSceneObjectName("bivector"),
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
            MainSceneComposer.AddVector(
                GrVisualVector3D.CreateAnimated(
                    GetNewSceneObjectName("vector"),
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
            AnimationSpecs.CreateAnimatedVector3D(
                element.PositionToParametricCurve3D()
            );

        var animatedNormal =
            AnimationSpecs.CreateAnimatedVector3D(
                element.NormalDirectionToParametricCurve3D()
            );

        if (DirectionStyle.DrawPosition)
        {
            MainSceneComposer.AddPoint(
                GrVisualPoint3D.CreateAnimated(
                    GetNewSceneObjectName("point"),
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
            MainSceneComposer.AddBivector(
                GrVisualBivector3D.CreateAnimated(
                    GetNewSceneObjectName("bivector"),
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
            MainSceneComposer.AddVector(
                GrVisualVector3D.CreateAnimated(
                    GetNewSceneObjectName("vector"),
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
        MainSceneComposer.AddPoint(
            GrVisualPoint3D.CreateStatic(
                GetNewSceneObjectName("point"),
                TangentStyle.GetPointVisualStyle(color),
                position.ToXyLinVector3D()
            )
        );

        return this;
    }

    public CGaFloat64Visualizer DrawTangentPoint2D(Color color, IFloat64ParametricCurve2D position)
    {
        MainSceneComposer.AddPoint(
            GrVisualPoint3D.CreateAnimated(
                GetNewSceneObjectName("point"),
                TangentStyle.GetPointVisualStyle(color),
                AnimationSpecs.CreateXyAnimatedVector3D(position)
            )
        );

        return this;
    }

    public CGaFloat64Visualizer DrawTangentPoint2D(Color color, CGaFloat64ParametricElement element)
    {
        var animatedPosition =
            AnimationSpecs.CreateXyAnimatedVector3D(
                element.PositionToParametricCurve2D()
            );

        MainSceneComposer.AddPoint(
            GrVisualPoint3D.CreateAnimated(
                GetNewSceneObjectName("point"),
                TangentStyle.GetPointVisualStyle(color),
                animatedPosition
            )
        );

        return this;
    }


    public CGaFloat64Visualizer DrawTangentPoint3D(Color color, LinFloat64Vector3D position)
    {
        MainSceneComposer.AddPoint(
            GrVisualPoint3D.CreateStatic(
                GetNewSceneObjectName("point"),
                TangentStyle.GetPointVisualStyle(color),
                position
            )
        );

        return this;
    }

    public CGaFloat64Visualizer DrawTangentPoint3D(Color color, IParametricCurve3D position)
    {
        MainSceneComposer.AddPoint(
            GrVisualPoint3D.CreateAnimated(
                GetNewSceneObjectName("point"),
                TangentStyle.GetPointVisualStyle(color),
                AnimationSpecs.CreateAnimatedVector3D(position)
            )
        );

        return this;
    }

    public CGaFloat64Visualizer DrawTangentPoint3D(Color color, CGaFloat64ParametricElement element)
    {
        var animatedPosition =
            AnimationSpecs.CreateAnimatedVector3D(
                element.PositionToParametricCurve3D()
            );

        MainSceneComposer.AddPoint(
            GrVisualPoint3D.CreateAnimated(
                GetNewSceneObjectName("point"),
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
            MainSceneComposer.AddPoint(
                GrVisualPoint3D.CreateStatic(
                    GetNewSceneObjectName("point"),
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
            MainSceneComposer.AddVector(
                GrVisualVector3D.CreateStatic(
                    GetNewSceneObjectName("vector"),
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
            MainSceneComposer.AddBivector(
                GrVisualBivector3D.CreateStatic(
                    GetNewSceneObjectName("bivector"),
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
            AnimationSpecs.CreateXyAnimatedVector3D(position);

        var animatedDirection =
            AnimationSpecs.CreateXyAnimatedVector3D(direction);

        if (TangentStyle.DrawPosition)
        {
            MainSceneComposer.AddPoint(
                GrVisualPoint3D.CreateAnimated(
                    GetNewSceneObjectName("point"),
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
                AnimationSpecs.CreateAnimatedVector3D(t =>
                    direction.GetPoint(t).SetLength(TangentStyle.DirectionRadius).ToXyLinVector3D()
                );

            MainSceneComposer.AddVector(
                GrVisualVector3D.CreateAnimated(
                    GetNewSceneObjectName("vector"),
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
            MainSceneComposer.AddBivector(
                GrVisualBivector3D.CreateAnimated(
                    GetNewSceneObjectName("bivector"),
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
            AnimationSpecs.CreateXyAnimatedVector3D(
                element.PositionToParametricCurve2D()
            );

        var animatedDirection =
            AnimationSpecs.CreateXyAnimatedVector3D(
                element.DirectionToParametricCurve2D()
            );

        var animatedNormal =
            AnimationSpecs.CreateXyAnimatedVector3D(
                element.NormalDirectionToParametricCurve2D()
            );

        if (TangentStyle.DrawPosition)
        {
            MainSceneComposer.AddPoint(
                GrVisualPoint3D.CreateAnimated(
                    GetNewSceneObjectName("point"),
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
            MainSceneComposer.AddVector(
                GrVisualVector3D.CreateAnimated(
                    GetNewSceneObjectName("vector"),
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
            MainSceneComposer.AddVector(
                GrVisualVector3D.CreateAnimated(
                    GetNewSceneObjectName("vector"),
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
            MainSceneComposer.AddPoint(
                GrVisualPoint3D.CreateStatic(
                    GetNewSceneObjectName("point"),
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
            MainSceneComposer.AddVector(
                GrVisualVector3D.CreateStatic(
                    GetNewSceneObjectName("vector"),
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
            MainSceneComposer.AddBivector(
                GrVisualBivector3D.CreateStatic(
                    GetNewSceneObjectName("bivector"),
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
            AnimationSpecs.CreateAnimatedVector3D(position);

        var animatedDirection =
            AnimationSpecs.CreateAnimatedVector3D(direction);

        if (TangentStyle.DrawPosition)
        {
            MainSceneComposer.AddPoint(
                GrVisualPoint3D.CreateAnimated(
                    GetNewSceneObjectName("point"),
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
                AnimationSpecs.CreateAnimatedVector3D(t =>
                    direction.GetPoint(t).SetLength(TangentStyle.DirectionRadius)
                );

            MainSceneComposer.AddVector(
                GrVisualVector3D.CreateAnimated(
                    GetNewSceneObjectName("vector"),
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
            MainSceneComposer.AddBivector(
                GrVisualBivector3D.CreateAnimated(
                    GetNewSceneObjectName("bivector"),
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
            AnimationSpecs.CreateAnimatedVector3D(
                element.PositionToParametricCurve3D()
            );

        var animatedDirection =
            AnimationSpecs.CreateAnimatedVector3D(
                element.DirectionToParametricCurve3D()
            );

        if (TangentStyle.DrawPosition)
        {
            MainSceneComposer.AddPoint(
                GrVisualPoint3D.CreateAnimated(
                    GetNewSceneObjectName("point"),
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
            MainSceneComposer.AddVector(
                GrVisualVector3D.CreateAnimated(
                    GetNewSceneObjectName("vector"),
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
            MainSceneComposer.AddBivector(
                GrVisualBivector3D.CreateAnimated(
                    GetNewSceneObjectName("bivector"),
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
            MainSceneComposer.AddPoint(
                GrVisualPoint3D.CreateStatic(
                    GetNewSceneObjectName("point"),
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
            MainSceneComposer.AddBivector(
                GrVisualBivector3D.CreateStatic(
                    GetNewSceneObjectName("bivector"),
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
            MainSceneComposer.AddVector(
                GrVisualVector3D.CreateStatic(
                    GetNewSceneObjectName("vector"),
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
            AnimationSpecs.CreateXyAnimatedVector3D(position);

        var animatedNormal =
            AnimationSpecs.CreateAnimatedVector3D(
                direction
                    .ToParametricCurve3D(b =>
                        b.ToXyBivector3D().DirectionToUnitNormal3D(LinFloat64Vector3D.UnitSymmetric)
                    )
            );

        if (TangentStyle.DrawPosition)
        {
            MainSceneComposer.AddPoint(
                GrVisualPoint3D.CreateAnimated(
                    GetNewSceneObjectName("point"),
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
            MainSceneComposer.AddBivector(
                GrVisualBivector3D.CreateAnimated(
                    GetNewSceneObjectName("bivector"),
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
            MainSceneComposer.AddVector(
                GrVisualVector3D.CreateAnimated(
                    GetNewSceneObjectName("vector"),
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
            AnimationSpecs.CreateXyAnimatedVector3D(
                element.PositionToParametricCurve2D()
            );

        var animatedNormal =
            AnimationSpecs.CreateAnimatedVector3D(
                element
                    .DirectionToParametricBivector2D()
                    .XyDirectionToNormal3D(LinFloat64Vector3D.UnitSymmetric)
            );

        if (TangentStyle.DrawPosition)
        {
            MainSceneComposer.AddPoint(
                GrVisualPoint3D.CreateAnimated(
                    GetNewSceneObjectName("point"),
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
            MainSceneComposer.AddBivector(
                GrVisualBivector3D.CreateAnimated(
                    GetNewSceneObjectName("bivector"),
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
            MainSceneComposer.AddVector(
                GrVisualVector3D.CreateAnimated(
                    GetNewSceneObjectName("vector"),
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
            MainSceneComposer.AddPoint(
                GrVisualPoint3D.CreateStatic(
                    GetNewSceneObjectName("point"),
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
            MainSceneComposer.AddBivector(
                GrVisualBivector3D.CreateStatic(
                    GetNewSceneObjectName("bivector"),
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
            MainSceneComposer.AddVector(
                GrVisualVector3D.CreateStatic(
                    GetNewSceneObjectName("vector"),
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
            AnimationSpecs.CreateAnimatedVector3D(position);

        if (TangentStyle.DrawPosition)
        {
            MainSceneComposer.AddPoint(
                GrVisualPoint3D.CreateAnimated(
                    GetNewSceneObjectName("point"),
                    TangentStyle.GetPointVisualStyle(
                        color.SetAlpha(TangentStyle.AuxGeometryColorAlpha),
                        TangentStyle.Thickness * 1.5
                    ),
                    animatedPosition
                )
            );
        }

        var animatedNormal =
            AnimationSpecs.CreateAnimatedVector3D(
                direction.GetNormalVectorCurve()
            );

        if (TangentStyle.DrawDirection)
        {
            MainSceneComposer.AddBivector(
                GrVisualBivector3D.CreateAnimated(
                    GetNewSceneObjectName("bivector"),
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
            MainSceneComposer.AddVector(
                GrVisualVector3D.CreateAnimated(
                    GetNewSceneObjectName("vector"),
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
            AnimationSpecs.CreateAnimatedVector3D(
                element.PositionToParametricCurve3D()
            );

        var animatedNormal =
            AnimationSpecs.CreateAnimatedVector3D(
                element.NormalDirectionToParametricCurve3D()
            );

        if (TangentStyle.DrawPosition)
        {
            MainSceneComposer.AddPoint(
                GrVisualPoint3D.CreateAnimated(
                    GetNewSceneObjectName("point"),
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
            MainSceneComposer.AddBivector(
                GrVisualBivector3D.CreateAnimated(
                    GetNewSceneObjectName("bivector"),
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
            MainSceneComposer.AddVector(
                GrVisualVector3D.CreateAnimated(
                    GetNewSceneObjectName("vector"),
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
        MainSceneComposer.AddPoint(
            GrVisualPoint3D.CreateStatic(
                GetNewSceneObjectName("point"),
                FlatStyle.GetPointVisualStyle(color),
                position.ToXyLinVector3D()
            )
        );

        return this;
    }

    public CGaFloat64Visualizer DrawFlatPoint2D(Color color, IFloat64ParametricCurve2D position)
    {
        MainSceneComposer.AddPoint(
            GrVisualPoint3D.CreateAnimated(
                GetNewSceneObjectName("point"),
                FlatStyle.GetPointVisualStyle(color),
                AnimationSpecs.CreateXyAnimatedVector3D(position)
            )
        );

        return this;
    }

    public CGaFloat64Visualizer DrawFlatPoint2D(Color color, CGaFloat64ParametricElement element)
    {
        var animatedPosition =
            AnimationSpecs.CreateXyAnimatedVector3D(
                element.PositionToParametricCurve2D()
            );

        MainSceneComposer.AddPoint(
            GrVisualPoint3D.CreateAnimated(
                GetNewSceneObjectName("point"),
                FlatStyle.GetPointVisualStyle(color),
                animatedPosition
            )
        );

        return this;
    }


    public CGaFloat64Visualizer DrawFlatPoint3D(Color color, LinFloat64Vector3D position)
    {
        MainSceneComposer.AddPoint(
            GrVisualPoint3D.CreateStatic(
                GetNewSceneObjectName("point"),
                FlatStyle.GetPointVisualStyle(color),
                position
            )
        );

        return this;
    }

    public CGaFloat64Visualizer DrawFlatPoint3D(Color color, IParametricCurve3D position)
    {
        MainSceneComposer.AddPoint(
            GrVisualPoint3D.CreateAnimated(
                GetNewSceneObjectName("point"),
                FlatStyle.GetPointVisualStyle(color),
                AnimationSpecs.CreateAnimatedVector3D(position)
            )
        );

        return this;
    }

    public CGaFloat64Visualizer DrawFlatPoint3D(Color color, CGaFloat64ParametricElement element)
    {
        var animatedPosition =
            AnimationSpecs.CreateAnimatedVector3D(
                element.PositionToParametricCurve3D()
            );

        MainSceneComposer.AddPoint(
            GrVisualPoint3D.CreateAnimated(
                GetNewSceneObjectName("point"),
                FlatStyle.GetPointVisualStyle(color),
                animatedPosition
            )
        );

        return this;
    }


    public CGaFloat64Visualizer DrawFlatLine2D(Color color, LinFloat64Vector2D position, LinFloat64Vector2D direction)
    {
        MainSceneComposer.AddLineSegment(
            GrVisualLineSegment3D.CreateStatic(
                GetNewSceneObjectName("lineSegment"),
                FlatStyle.GetLineVisualStyle(color),
                (position - direction.SetLength(FlatStyle.Radius)).ToXyLinVector3D(),
                (position + direction.SetLength(FlatStyle.Radius)).ToXyLinVector3D()
            )
        );

        if (FlatStyle.DrawPosition)
        {
            MainSceneComposer.AddPoint(
                GrVisualPoint3D.CreateStatic(
                    GetNewSceneObjectName("point"),
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
            MainSceneComposer.AddVector(
                GrVisualVector3D.CreateStatic(
                    GetNewSceneObjectName("vector"),
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
            MainSceneComposer.AddBivector(
                GrVisualBivector3D.CreateStatic(
                    GetNewSceneObjectName("bivector"),
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
            AnimationSpecs.CreateXyAnimatedVector3D(position);

        var animatedDirection =
            AnimationSpecs.CreateXyAnimatedVector3D(direction);

        var animatedPoint1 =
            AnimationSpecs.CreateAnimatedVector3D(t =>
                (position.GetPoint(t) - direction.GetPoint(t).SetLength(FlatStyle.Radius)).ToXyLinVector3D()
            );

        var animatedPoint2 =
            AnimationSpecs.CreateAnimatedVector3D(t =>
                (position.GetPoint(t) + direction.GetPoint(t).SetLength(FlatStyle.Radius)).ToXyLinVector3D()
            );

        MainSceneComposer.AddLineSegment(
            GrVisualLineSegment3D.CreateAnimated(
                GetNewSceneObjectName("lineSegment"),
                FlatStyle.GetLineVisualStyle(color),
                animatedPoint1,
                animatedPoint2
            )
        );

        if (FlatStyle.DrawPosition)
        {
            MainSceneComposer.AddPoint(
                GrVisualPoint3D.CreateAnimated(
                    GetNewSceneObjectName("point"),
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
                AnimationSpecs.CreateAnimatedVector3D(t =>
                    direction.GetPoint(t).SetLength(FlatStyle.DirectionRadius).ToXyLinVector3D()
                );

            MainSceneComposer.AddVector(
                GrVisualVector3D.CreateAnimated(
                    GetNewSceneObjectName("vector"),
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
            MainSceneComposer.AddBivector(
                GrVisualBivector3D.CreateAnimated(
                    GetNewSceneObjectName("bivector"),
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
            AnimationSpecs.CreateXyAnimatedVector3D(
                element.PositionToParametricCurve2D()
            );

        var animatedDirection =
            AnimationSpecs.CreateXyAnimatedVector3D(
                element.DirectionToParametricCurve2D()
            );

        var animatedNormal =
            AnimationSpecs.CreateXyAnimatedVector3D(
                element.NormalDirectionToParametricCurve2D()
            //element.DirectionToParametricCurve2D().GetNormalCurve()
            );

        var animatedPoint1 =
            AnimationSpecs.CreateAnimatedVector3D(
                t =>
                {
                    var el =
                        element.GetElement(t);

                    return (el.PositionToVector2D() - el.DirectionToVector2D(FlatStyle.Radius)).ToXyLinVector3D();
                }
            );

        var animatedPoint2 =
            AnimationSpecs.CreateAnimatedVector3D(
                t =>
                {
                    var el =
                        element.GetElement(t);

                    return (el.PositionToVector2D() + el.DirectionToVector2D(FlatStyle.Radius)).ToXyLinVector3D();
                }
            );

        MainSceneComposer.AddLineSegment(
            GrVisualLineSegment3D.CreateAnimated(
                GetNewSceneObjectName("lineSegment"),
                FlatStyle.GetLineVisualStyle(color),
                animatedPoint1,
                animatedPoint2
            )
        );

        if (FlatStyle.DrawPosition)
        {
            MainSceneComposer.AddPoint(
                GrVisualPoint3D.CreateAnimated(
                    GetNewSceneObjectName("point"),
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
            MainSceneComposer.AddVector(
                GrVisualVector3D.CreateAnimated(
                    GetNewSceneObjectName("vector"),
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
            MainSceneComposer.AddVector(
                GrVisualVector3D.CreateAnimated(
                    GetNewSceneObjectName("vector"),
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
        MainSceneComposer.AddLineSegment(
            GrVisualLineSegment3D.CreateStatic(
                GetNewSceneObjectName("lineSegment"),
                FlatStyle.GetLineVisualStyle(color),
                position - direction.SetLength(FlatStyle.Radius),
                position + direction.SetLength(FlatStyle.Radius)
            )
        );

        if (FlatStyle.DrawPosition)
        {
            MainSceneComposer.AddPoint(
                GrVisualPoint3D.CreateStatic(
                    GetNewSceneObjectName("point"),
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
            MainSceneComposer.AddVector(
                GrVisualVector3D.CreateStatic(
                    GetNewSceneObjectName("vector"),
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
            MainSceneComposer.AddBivector(
                GrVisualBivector3D.CreateStatic(
                    GetNewSceneObjectName("bivector"),
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
            AnimationSpecs.CreateAnimatedVector3D(position);

        var animatedDirection =
            AnimationSpecs.CreateAnimatedVector3D(direction);

        var animatedPoint1 =
            AnimationSpecs.CreateAnimatedVector3D(t =>
                position.GetPoint(t) - direction.GetPoint(t).SetLength(FlatStyle.Radius)
            );

        var animatedPoint2 =
            AnimationSpecs.CreateAnimatedVector3D(t =>
                position.GetPoint(t) + direction.GetPoint(t).SetLength(FlatStyle.Radius)
            );

        MainSceneComposer.AddLineSegment(
            GrVisualLineSegment3D.CreateAnimated(
                GetNewSceneObjectName("lineSegment"),
                FlatStyle.GetLineVisualStyle(color),
                animatedPoint1,
                animatedPoint2
            )
        );

        if (FlatStyle.DrawPosition)
        {
            MainSceneComposer.AddPoint(
                GrVisualPoint3D.CreateAnimated(
                    GetNewSceneObjectName("point"),
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
                AnimationSpecs.CreateAnimatedVector3D(t =>
                    direction.GetPoint(t).SetLength(FlatStyle.DirectionRadius)
                );

            MainSceneComposer.AddVector(
                GrVisualVector3D.CreateAnimated(
                    GetNewSceneObjectName("vector"),
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
            MainSceneComposer.AddBivector(
                GrVisualBivector3D.CreateAnimated(
                    GetNewSceneObjectName("bivector"),
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
            AnimationSpecs.CreateAnimatedVector3D(
                element.PositionToParametricCurve3D()
            );

        var animatedDirection =
            AnimationSpecs.CreateAnimatedVector3D(
                element.DirectionToParametricCurve3D()
            );

        var animatedPoint1 =
            AnimationSpecs.CreateAnimatedVector3D(
                t =>
                {
                    var el =
                        element.GetElement(t);

                    return el.PositionToVector3D() - el.DirectionToVector3D(FlatStyle.Radius);
                }
            );

        var animatedPoint2 =
            AnimationSpecs.CreateAnimatedVector3D(
                t =>
                {
                    var el =
                        element.GetElement(t);

                    return el.PositionToVector3D() + el.DirectionToVector3D(FlatStyle.Radius);
                }
            );

        MainSceneComposer.AddLineSegment(
            GrVisualLineSegment3D.CreateAnimated(
                GetNewSceneObjectName("lineSegment"),
                FlatStyle.GetLineVisualStyle(color),
                animatedPoint1,
                animatedPoint2
            )
        );

        if (FlatStyle.DrawPosition)
        {
            MainSceneComposer.AddPoint(
                GrVisualPoint3D.CreateAnimated(
                    GetNewSceneObjectName("point"),
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
            MainSceneComposer.AddVector(
                GrVisualVector3D.CreateAnimated(
                    GetNewSceneObjectName("vector"),
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
            MainSceneComposer.AddBivector(
                GrVisualBivector3D.CreateAnimated(
                    GetNewSceneObjectName("bivector"),
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
            MainSceneComposer.AddPoint(
                GrVisualPoint3D.CreateStatic(
                    GetNewSceneObjectName("point"),
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
            MainSceneComposer.AddBivector(
                GrVisualBivector3D.CreateStatic(
                    GetNewSceneObjectName("bivector"),
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
            MainSceneComposer.AddVector(
                GrVisualVector3D.CreateStatic(
                    GetNewSceneObjectName("vector"),
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
            AnimationSpecs.CreateXyAnimatedVector3D(position);

        var animatedNormal =
            AnimationSpecs.CreateAnimatedVector3D(
                direction
                    .ToParametricCurve3D(b =>
                        b.ToXyBivector3D().DirectionToUnitNormal3D(LinFloat64Vector3D.UnitSymmetric)
                    )
            );

        if (FlatStyle.DrawPosition)
        {
            MainSceneComposer.AddPoint(
                GrVisualPoint3D.CreateAnimated(
                    GetNewSceneObjectName("point"),
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
            MainSceneComposer.AddBivector(
                GrVisualBivector3D.CreateAnimated(
                    GetNewSceneObjectName("bivector"),
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
            MainSceneComposer.AddVector(
                GrVisualVector3D.CreateAnimated(
                    GetNewSceneObjectName("vector"),
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
            AnimationSpecs.CreateXyAnimatedVector3D(
                element.PositionToParametricCurve2D()
            );

        var animatedNormal =
            AnimationSpecs.CreateAnimatedVector3D(
                element
                    .DirectionToParametricBivector2D()
                    .XyDirectionToNormal3D(LinFloat64Vector3D.UnitSymmetric)
            );

        if (FlatStyle.DrawPosition)
        {
            MainSceneComposer.AddPoint(
                GrVisualPoint3D.CreateAnimated(
                    GetNewSceneObjectName("point"),
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
            MainSceneComposer.AddBivector(
                GrVisualBivector3D.CreateAnimated(
                    GetNewSceneObjectName("bivector"),
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
            MainSceneComposer.AddVector(
                GrVisualVector3D.CreateAnimated(
                    GetNewSceneObjectName("vector"),
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

        MainSceneComposer.AddCircleSurface(
            GrVisualCircleSurface3D.CreateStatic(
                GetNewSceneObjectName("disc"),
                FlatStyle.GetPlaneVisualStyle(color),
                position,
                normal,
                FlatStyle.Radius,
                false
            )
        );

        if (FlatStyle.DrawPosition)
        {
            MainSceneComposer.AddPoint(
                GrVisualPoint3D.CreateStatic(
                    GetNewSceneObjectName("point"),
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
            MainSceneComposer.AddBivector(
                GrVisualBivector3D.CreateStatic(
                    GetNewSceneObjectName("bivector"),
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
            MainSceneComposer.AddVector(
                GrVisualVector3D.CreateStatic(
                    GetNewSceneObjectName("vector"),
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
            AnimationSpecs.CreateAnimatedVector3D(position);

        var animatedNormal =
            AnimationSpecs.CreateAnimatedVector3D(
                direction.GetNormalVectorCurve()
            );

        MainSceneComposer.AddCircleSurface(
            GrVisualCircleSurface3D.CreateAnimated(
                GetNewSceneObjectName("disc"),
                FlatStyle.GetPlaneVisualStyle(color),
                animatedPosition,
                animatedNormal,
                FlatStyle.Radius,
                false
            )
        );

        if (FlatStyle.DrawPosition)
        {
            MainSceneComposer.AddPoint(
                GrVisualPoint3D.CreateAnimated(
                    GetNewSceneObjectName("point"),
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
            MainSceneComposer.AddBivector(
                GrVisualBivector3D.CreateAnimated(
                    GetNewSceneObjectName("bivector"),
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
            MainSceneComposer.AddVector(
                GrVisualVector3D.CreateAnimated(
                    GetNewSceneObjectName("vector"),
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
            AnimationSpecs.CreateAnimatedVector3D(
                element.PositionToParametricCurve3D()
            );

        var animatedNormal =
            AnimationSpecs.CreateAnimatedVector3D(
                element.NormalDirectionToParametricCurve3D()
            );

        MainSceneComposer.AddCircleSurface(
            GrVisualCircleSurface3D.CreateAnimated(
                GetNewSceneObjectName("disc"),
                FlatStyle.GetPlaneVisualStyle(color),
                animatedPosition,
                animatedNormal,
                FlatStyle.Radius,
                false
            )
        );

        if (FlatStyle.DrawPosition)
        {
            MainSceneComposer.AddPoint(
                GrVisualPoint3D.CreateAnimated(
                    GetNewSceneObjectName("point"),
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
            MainSceneComposer.AddBivector(
                GrVisualBivector3D.CreateAnimated(
                    GetNewSceneObjectName("bivector"),
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
            MainSceneComposer.AddVector(
                GrVisualVector3D.CreateAnimated(
                    GetNewSceneObjectName("vector"),
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
        MainSceneComposer.AddPoint(
            GrVisualPoint3D.CreateStatic(
                GetNewSceneObjectName("point"),
                RoundStyle.GetPointVisualStyle(color),
                center.ToXyLinVector3D()
            )
        );

        return this;
    }

    public CGaFloat64Visualizer DrawRoundPoint2D(Color color, IFloat64ParametricCurve2D center)
    {
        MainSceneComposer.AddPoint(
            GrVisualPoint3D.CreateAnimated(
                GetNewSceneObjectName("point"),
                RoundStyle.GetPointVisualStyle(color),
                AnimationSpecs.CreateAnimatedVector3D(center.ToXyParametricCurve3D())
            )
        );

        return this;
    }

    public CGaFloat64Visualizer DrawRoundPoint2D(Color color, CGaFloat64ParametricElement element)
    {
        var animatedPosition =
            AnimationSpecs.CreateXyAnimatedVector3D(
                element.PositionToParametricCurve2D()
            );

        MainSceneComposer.AddPoint(
            GrVisualPoint3D.CreateAnimated(
                GetNewSceneObjectName("point"),
                RoundStyle.GetPointVisualStyle(color),
                animatedPosition
            )
        );

        return this;
    }


    public CGaFloat64Visualizer DrawRoundPoint3D(Color color, LinFloat64Vector3D center)
    {
        MainSceneComposer.AddPoint(
            GrVisualPoint3D.CreateStatic(
                GetNewSceneObjectName("point"),
                RoundStyle.GetPointVisualStyle(color),
                center
            )
        );

        return this;
    }

    public CGaFloat64Visualizer DrawRoundPoint3D(Color color, IParametricCurve3D center)
    {
        MainSceneComposer.AddPoint(
            GrVisualPoint3D.CreateAnimated(
                GetNewSceneObjectName("point"),
                RoundStyle.GetPointVisualStyle(color),
                AnimationSpecs.CreateAnimatedVector3D(center)
            )
        );

        return this;
    }

    public CGaFloat64Visualizer DrawRoundPoint3D(Color color, CGaFloat64ParametricElement element)
    {
        var animatedPosition =
            AnimationSpecs.CreateAnimatedVector3D(
                element.PositionToParametricCurve3D()
            );

        MainSceneComposer.AddPoint(
            GrVisualPoint3D.CreateAnimated(
                GetNewSceneObjectName("point"),
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

        MainSceneComposer.AddPoint(
            GrVisualPoint3D.CreateStatic(
                GetNewSceneObjectName("point"),
                RoundStyle.GetPointPairVisualStyle(color),
                point1
            )
        );

        MainSceneComposer.AddPoint(
            GrVisualPoint3D.CreateStatic(
                GetNewSceneObjectName("point"),
                RoundStyle.GetPointPairVisualStyle(color),
                point2
            )
        );

        MainSceneComposer.AddLineSegment(
            GrVisualLineSegment3D.CreateStatic(
                GetNewSceneObjectName("line"),
                color.SetAlpha(RoundStyle.AuxGeometryColorAlpha).CreateDashedLineCurveStyle(5, 3, 16),
                point1,
                point2
            )
        );

        if (RoundStyle.DrawSphere)
        {
            MainSceneComposer.AddSphereSurface(
                GrVisualSphereSurface3D.CreateStatic(
                    GetNewSceneObjectName("sphere"),
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
            MainSceneComposer.AddPoint(
                GrVisualPoint3D.CreateStatic(
                    GetNewSceneObjectName("point"),
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
            MainSceneComposer.AddVector(
                GrVisualVector3D.CreateStatic(
                    GetNewSceneObjectName("vector"),
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
            MainSceneComposer.AddBivector(
                GrVisualBivector3D.CreateStatic(
                    GetNewSceneObjectName("bivector"),
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
            AnimationSpecs.CreateXyAnimatedVector3D(center);

        var animatedDirection =
            AnimationSpecs.CreateXyAnimatedVector3D(direction);

        var animatedRadius =
            AnimationSpecs.CreateAnimatedScalar(radius);

        var animatedPoint1 =
            AnimationSpecs.CreateXyAnimatedVector3D(t =>
                center.GetPoint(t) - direction.GetPoint(t).SetLength(radius.GetValue(t))
            );

        var animatedPoint2 =
            AnimationSpecs.CreateXyAnimatedVector3D(t =>
                center.GetPoint(t) + direction.GetPoint(t).SetLength(radius.GetValue(t))
            );

        MainSceneComposer.AddPoint(
            GrVisualPoint3D.CreateAnimated(
                GetNewSceneObjectName("point"),
                RoundStyle.GetPointPairVisualStyle(color),
                animatedPoint1
            )
        );

        MainSceneComposer.AddPoint(
            GrVisualPoint3D.CreateAnimated(
                GetNewSceneObjectName("point"),
                RoundStyle.GetPointPairVisualStyle(color),
                animatedPoint2
            )
        );

        MainSceneComposer.AddLineSegment(
            GrVisualLineSegment3D.CreateAnimated(
                GetNewSceneObjectName("line"),
                color.SetAlpha(RoundStyle.AuxGeometryColorAlpha).CreateDashedLineCurveStyle(5, 3, 16),
                animatedPoint1,
                animatedPoint2
            )
        );

        if (RoundStyle.DrawSphere)
        {
            MainSceneComposer.AddSphereSurface(
                GrVisualSphereSurface3D.CreateAnimated(
                    GetNewSceneObjectName("sphere"),
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
            MainSceneComposer.AddPoint(
                GrVisualPoint3D.CreateAnimated(
                    GetNewSceneObjectName("point"),
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
                AnimationSpecs.CreateXyAnimatedVector3D(
                    t =>
                        direction.GetPoint(t).SetLength(RoundStyle.DirectionRadius)
                );

            MainSceneComposer.AddVector(
                GrVisualVector3D.CreateAnimated(
                    GetNewSceneObjectName("vector"),
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
            MainSceneComposer.AddBivector(
                GrVisualBivector3D.CreateAnimated(
                    GetNewSceneObjectName("bivector"),
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
            AnimationSpecs.CreateXyAnimatedVector3D(
                element.PositionToParametricCurve2D()
            );

        var animatedDirection =
            AnimationSpecs.CreateXyAnimatedVector3D(
                element.DirectionToParametricCurve2D()
            );

        var animatedNormal =
            AnimationSpecs.CreateXyAnimatedVector3D(
                element.NormalDirectionToParametricCurve2D()
            );

        var animatedRadius =
            AnimationSpecs.CreateAnimatedScalar(
                element.RealRadiusToParametricScalar()
            );

        var animatedPoint1 =
            AnimationSpecs.CreateXyAnimatedVector3D(t =>
                {
                    var el = element.GetElement(t);

                    return el.PositionToVector2D() - el.DirectionToVector2D().SetLength(el.RealRadius);
                }
            );

        var animatedPoint2 =
            AnimationSpecs.CreateXyAnimatedVector3D(t =>
                {
                    var el = element.GetElement(t);

                    return el.PositionToVector2D() + el.DirectionToVector2D().SetLength(el.RealRadius);
                }
            );

        MainSceneComposer.AddPoint(
            GrVisualPoint3D.CreateAnimated(
                GetNewSceneObjectName("point"),
                RoundStyle.GetPointPairVisualStyle(color),
                animatedPoint1
            )
        );

        MainSceneComposer.AddPoint(
            GrVisualPoint3D.CreateAnimated(
                GetNewSceneObjectName("point"),
                RoundStyle.GetPointPairVisualStyle(color),
                animatedPoint2
            )
        );

        MainSceneComposer.AddLineSegment(
            GrVisualLineSegment3D.CreateAnimated(
                GetNewSceneObjectName("line"),
                color.SetAlpha(RoundStyle.AuxGeometryColorAlpha).CreateDashedLineCurveStyle(5, 3, 16),
                animatedPoint1,
                animatedPoint2
            )
        );

        if (RoundStyle.DrawSphere)
        {
            MainSceneComposer.AddSphereSurface(
                GrVisualSphereSurface3D.CreateAnimated(
                    GetNewSceneObjectName("sphere"),
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
            MainSceneComposer.AddPoint(
                GrVisualPoint3D.CreateAnimated(
                    GetNewSceneObjectName("point"),
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
            MainSceneComposer.AddVector(
                GrVisualVector3D.CreateAnimated(
                    GetNewSceneObjectName("vector"),
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
            MainSceneComposer.AddVector(
                GrVisualVector3D.CreateAnimated(
                    GetNewSceneObjectName("vector"),
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

        MainSceneComposer.AddPoint(
            GrVisualPoint3D.CreateStatic(
                GetNewSceneObjectName("point"),
                RoundStyle.GetPointPairVisualStyle(color),
                point1
            )
        );

        MainSceneComposer.AddPoint(
            GrVisualPoint3D.CreateStatic(
                GetNewSceneObjectName("point"),
                RoundStyle.GetPointPairVisualStyle(color),
                point2
            )
        );

        MainSceneComposer.AddLineSegment(
            GrVisualLineSegment3D.CreateStatic(
                GetNewSceneObjectName("line"),
                color.SetAlpha(RoundStyle.AuxGeometryColorAlpha).CreateDashedLineCurveStyle(5, 3, 16),
                point1,
                point2
            )
        );

        if (RoundStyle.DrawSphere)
        {
            MainSceneComposer.AddSphereSurface(
                GrVisualSphereSurface3D.CreateStatic(
                    GetNewSceneObjectName("sphere"),
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
            MainSceneComposer.AddPoint(
                GrVisualPoint3D.CreateStatic(
                    GetNewSceneObjectName("point"),
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
            MainSceneComposer.AddVector(
                GrVisualVector3D.CreateStatic(
                    GetNewSceneObjectName("vector"),
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
            MainSceneComposer.AddBivector(
                GrVisualBivector3D.CreateStatic(
                    GetNewSceneObjectName("bivector"),
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
            AnimationSpecs.CreateAnimatedVector3D(center);

        var animatedDirection =
            AnimationSpecs.CreateAnimatedVector3D(direction);

        var animatedRadius =
            AnimationSpecs.CreateAnimatedScalar(radius);

        var animatedPoint1 =
            AnimationSpecs.CreateAnimatedVector3D(t =>
                center.GetPoint(t) - direction.GetPoint(t).SetLength(radius.GetValue(t))
            );

        var animatedPoint2 =
            AnimationSpecs.CreateAnimatedVector3D(t =>
                center.GetPoint(t) + direction.GetPoint(t).SetLength(radius.GetValue(t))
            );

        MainSceneComposer.AddPoint(
            GrVisualPoint3D.CreateAnimated(
                GetNewSceneObjectName("point"),
                RoundStyle.GetPointPairVisualStyle(color),
                animatedPoint1
            )
        );

        MainSceneComposer.AddPoint(
            GrVisualPoint3D.CreateAnimated(
                GetNewSceneObjectName("point"),
                RoundStyle.GetPointPairVisualStyle(color),
                animatedPoint2
            )
        );

        MainSceneComposer.AddLineSegment(
            GrVisualLineSegment3D.CreateAnimated(
                GetNewSceneObjectName("line"),
                color.SetAlpha(RoundStyle.AuxGeometryColorAlpha).CreateDashedLineCurveStyle(5, 3, 16),
                animatedPoint1,
                animatedPoint2
            )
        );

        if (RoundStyle.DrawSphere)
        {
            MainSceneComposer.AddSphereSurface(
                GrVisualSphereSurface3D.CreateAnimated(
                    GetNewSceneObjectName("sphere"),
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
            MainSceneComposer.AddPoint(
                GrVisualPoint3D.CreateAnimated(
                    GetNewSceneObjectName("point"),
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
                AnimationSpecs.CreateAnimatedVector3D(
                    t =>
                        direction.GetPoint(t).SetLength(RoundStyle.DirectionRadius)
                );

            MainSceneComposer.AddVector(
                GrVisualVector3D.CreateAnimated(
                    GetNewSceneObjectName("vector"),
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
            MainSceneComposer.AddBivector(
                GrVisualBivector3D.CreateAnimated(
                    GetNewSceneObjectName("bivector"),
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
            AnimationSpecs.CreateAnimatedVector3D(
                element.PositionToParametricCurve3D()
            );

        var animatedDirection =
            AnimationSpecs.CreateAnimatedVector3D(
                element.DirectionToParametricCurve3D()
            );

        var animatedRadius =
            AnimationSpecs.CreateAnimatedScalar(
                element.RealRadiusToParametricScalar()
            );

        var animatedPoint1 =
            AnimationSpecs.CreateAnimatedVector3D(t =>
                {
                    var el = element.GetElement(t);

                    return el.PositionToVector3D() - el.DirectionToVector3D().SetLength(el.RealRadius);
                }
            );

        var animatedPoint2 =
            AnimationSpecs.CreateAnimatedVector3D(t =>
                {
                    var el = element.GetElement(t);

                    return el.PositionToVector3D() + el.DirectionToVector3D().SetLength(el.RealRadius);
                }
            );

        MainSceneComposer.AddPoint(
            GrVisualPoint3D.CreateAnimated(
                GetNewSceneObjectName("point"),
                RoundStyle.GetPointPairVisualStyle(color),
                animatedPoint1
            )
        );

        MainSceneComposer.AddPoint(
            GrVisualPoint3D.CreateAnimated(
                GetNewSceneObjectName("point"),
                RoundStyle.GetPointPairVisualStyle(color),
                animatedPoint2
            )
        );

        MainSceneComposer.AddLineSegment(
            GrVisualLineSegment3D.CreateAnimated(
                GetNewSceneObjectName("line"),
                color.SetAlpha(RoundStyle.AuxGeometryColorAlpha).CreateDashedLineCurveStyle(5, 3, 16),
                animatedPoint1,
                animatedPoint2
            )
        );

        if (RoundStyle.DrawSphere)
        {
            MainSceneComposer.AddSphereSurface(
                GrVisualSphereSurface3D.CreateAnimated(
                    GetNewSceneObjectName("sphere"),
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
            MainSceneComposer.AddPoint(
                GrVisualPoint3D.CreateAnimated(
                    GetNewSceneObjectName("point"),
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
            MainSceneComposer.AddVector(
                GrVisualVector3D.CreateAnimated(
                    GetNewSceneObjectName("vector"),
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
            MainSceneComposer.AddBivector(
                GrVisualBivector3D.CreateAnimated(
                    GetNewSceneObjectName("bivector"),
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

        MainSceneComposer.AddCircleCurve(
            GrVisualCircleCurve3D.CreateStatic(
                GetNewSceneObjectName("circle"),
                RoundStyle.GetCircleVisualStyle(color),
                center.ToXyLinVector3D(),
                normal,
                radius
            )
        );

        if (RoundStyle.DrawSphere)
        {
            MainSceneComposer.AddSphereSurface(
                GrVisualSphereSurface3D.CreateStatic(
                    GetNewSceneObjectName("sphere"),
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
            MainSceneComposer.AddPoint(
                GrVisualPoint3D.CreateStatic(
                    GetNewSceneObjectName("point"),
                    RoundStyle.GetPointVisualStyle(
                        color.SetAlpha(RoundStyle.AuxGeometryColorAlpha)
                    ),
                    center.ToXyLinVector3D()
                )
            );
        }

        if (RoundStyle.DrawPosition)
        {
            MainSceneComposer.AddPoint(
                GrVisualPoint3D.CreateStatic(
                    GetNewSceneObjectName("point"),
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
            MainSceneComposer.AddBivector(
                GrVisualBivector3D.CreateStatic(
                    GetNewSceneObjectName("bivector"),
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
            MainSceneComposer.AddVector(
                GrVisualVector3D.CreateStatic(
                    GetNewSceneObjectName("vector"),
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
            AnimationSpecs.CreateAnimatedVector3D(center.ToXyParametricCurve3D());

        var animatedNormal =
            AnimationSpecs.CreateAnimatedVector3D(
                direction
                .ToParametricCurve3D(b =>
                    b.ToXyBivector3D().DirectionToUnitNormal3D(LinFloat64Vector3D.UnitSymmetric)
                )
            );

        var animatedRadius =
            AnimationSpecs.CreateAnimatedScalar(radius);

        MainSceneComposer.AddCircleCurve(
            GrVisualCircleCurve3D.CreateAnimated(
                GetNewSceneObjectName("circle"),
                RoundStyle.GetCircleVisualStyle(color),
                animatedCenter,
                animatedNormal,
                animatedRadius
            )
        );

        if (RoundStyle.DrawSphere)
        {
            MainSceneComposer.AddSphereSurface(
                GrVisualSphereSurface3D.CreateAnimated(
                    GetNewSceneObjectName("sphere"),
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
            MainSceneComposer.AddPoint(
                GrVisualPoint3D.CreateAnimated(
                    GetNewSceneObjectName("point"),
                    RoundStyle.GetPointVisualStyle(
                        color.SetAlpha(RoundStyle.AuxGeometryColorAlpha)
                    ),
                    animatedCenter
                )
            );
        }

        if (RoundStyle.DrawPosition)
        {
            MainSceneComposer.AddPoint(
                GrVisualPoint3D.CreateAnimated(
                    GetNewSceneObjectName("point"),
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
            MainSceneComposer.AddBivector(
                GrVisualBivector3D.CreateAnimated(
                    GetNewSceneObjectName("bivector"),
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
            MainSceneComposer.AddVector(
                GrVisualVector3D.CreateAnimated(
                    GetNewSceneObjectName("vector"),
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
            AnimationSpecs.CreateXyAnimatedVector3D(
                element.PositionToParametricCurve2D()
            );

        var animatedNormal =
            AnimationSpecs.CreateAnimatedVector3D(
                element
                    .DirectionToParametricBivector2D()
                    .XyDirectionToNormal3D(LinFloat64Vector3D.UnitSymmetric)
            );

        var animatedRadius =
            AnimationSpecs.CreateAnimatedScalar(
                element.RealRadiusToParametricScalar()
            );

        MainSceneComposer.AddCircleCurve(
            GrVisualCircleCurve3D.CreateAnimated(
                GetNewSceneObjectName("circle"),
                RoundStyle.GetCircleVisualStyle(color),
                animatedPosition,
                animatedNormal,
                animatedRadius
            )
        );

        if (RoundStyle.DrawSphere)
        {
            MainSceneComposer.AddSphereSurface(
                GrVisualSphereSurface3D.CreateAnimated(
                    GetNewSceneObjectName("sphere"),
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
            MainSceneComposer.AddPoint(
                GrVisualPoint3D.CreateAnimated(
                    GetNewSceneObjectName("point"),
                    RoundStyle.GetPointVisualStyle(
                        color.SetAlpha(RoundStyle.AuxGeometryColorAlpha)
                    ),
                    animatedPosition
                )
            );
        }

        if (RoundStyle.DrawPosition)
        {
            MainSceneComposer.AddPoint(
                GrVisualPoint3D.CreateAnimated(
                    GetNewSceneObjectName("point"),
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
            MainSceneComposer.AddBivector(
                GrVisualBivector3D.CreateAnimated(
                    GetNewSceneObjectName("bivector"),
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
            MainSceneComposer.AddVector(
                GrVisualVector3D.CreateAnimated(
                    GetNewSceneObjectName("vector"),
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

        MainSceneComposer.AddCircleCurve(
            GrVisualCircleCurve3D.CreateStatic(
                GetNewSceneObjectName("circle"),
                RoundStyle.GetCircleVisualStyle(color),
                center,
                normal,
                radius
            )
        );

        if (RoundStyle.DrawSphere)
        {
            MainSceneComposer.AddSphereSurface(
                GrVisualSphereSurface3D.CreateStatic(
                    GetNewSceneObjectName("sphere"),
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
            MainSceneComposer.AddPoint(
                GrVisualPoint3D.CreateStatic(
                    GetNewSceneObjectName("point"),
                    RoundStyle.GetPointVisualStyle(
                        color.SetAlpha(RoundStyle.AuxGeometryColorAlpha)
                    ),
                    center
                )
            );
        }

        if (RoundStyle.DrawPosition)
        {
            MainSceneComposer.AddPoint(
                GrVisualPoint3D.CreateStatic(
                    GetNewSceneObjectName("point"),
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
            MainSceneComposer.AddBivector(
                GrVisualBivector3D.CreateStatic(
                    GetNewSceneObjectName("bivector"),
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
            MainSceneComposer.AddVector(
                GrVisualVector3D.CreateStatic(
                    GetNewSceneObjectName("vector"),
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
            AnimationSpecs.CreateAnimatedVector3D(center);

        var animatedNormal =
            AnimationSpecs.CreateAnimatedVector3D(direction.GetNormalVectorCurve());

        var animatedRadius =
            AnimationSpecs.CreateAnimatedScalar(radius);

        MainSceneComposer.AddCircleCurve(
            GrVisualCircleCurve3D.CreateAnimated(
                GetNewSceneObjectName("circle"),
                RoundStyle.GetCircleVisualStyle(color),
                animatedCenter,
                animatedNormal,
                animatedRadius
            )
        );

        if (RoundStyle.DrawSphere)
        {
            MainSceneComposer.AddSphereSurface(
                GrVisualSphereSurface3D.CreateAnimated(
                    GetNewSceneObjectName("sphere"),
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
            MainSceneComposer.AddPoint(
                GrVisualPoint3D.CreateAnimated(
                    GetNewSceneObjectName("point"),
                    RoundStyle.GetPointVisualStyle(
                        color.SetAlpha(RoundStyle.AuxGeometryColorAlpha)
                    ),
                    animatedCenter
                )
            );
        }

        if (RoundStyle.DrawPosition)
        {
            MainSceneComposer.AddPoint(
                GrVisualPoint3D.CreateAnimated(
                    GetNewSceneObjectName("point"),
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
            MainSceneComposer.AddBivector(
                GrVisualBivector3D.CreateAnimated(
                    GetNewSceneObjectName("bivector"),
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
            MainSceneComposer.AddVector(
                GrVisualVector3D.CreateAnimated(
                    GetNewSceneObjectName("vector"),
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
            AnimationSpecs.CreateAnimatedVector3D(
                element.PositionToParametricCurve3D()
            );

        var animatedNormal =
            AnimationSpecs.CreateAnimatedVector3D(
                element.NormalDirectionToParametricCurve3D()
            );

        var animatedRadius =
            AnimationSpecs.CreateAnimatedScalar(
                element.RealRadiusToParametricScalar()
            );

        MainSceneComposer.AddCircleCurve(
            GrVisualCircleCurve3D.CreateAnimated(
                GetNewSceneObjectName("circle"),
                RoundStyle.GetCircleVisualStyle(color),
                animatedPosition,
                animatedNormal,
                animatedRadius
            )
        );

        if (RoundStyle.DrawSphere)
        {
            MainSceneComposer.AddSphereSurface(
                GrVisualSphereSurface3D.CreateAnimated(
                    GetNewSceneObjectName("sphere"),
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
            MainSceneComposer.AddPoint(
                GrVisualPoint3D.CreateAnimated(
                    GetNewSceneObjectName("point"),
                    RoundStyle.GetPointVisualStyle(
                        color.SetAlpha(RoundStyle.AuxGeometryColorAlpha)
                    ),
                    animatedPosition
                )
            );
        }

        if (RoundStyle.DrawPosition)
        {
            MainSceneComposer.AddPoint(
                GrVisualPoint3D.CreateAnimated(
                    GetNewSceneObjectName("point"),
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
            MainSceneComposer.AddBivector(
                GrVisualBivector3D.CreateAnimated(
                    GetNewSceneObjectName("bivector"),
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
            MainSceneComposer.AddVector(
                GrVisualVector3D.CreateAnimated(
                    GetNewSceneObjectName("vector"),
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
        MainSceneComposer.AddSphereSurface(
            GrVisualSphereSurface3D.CreateStatic(
                GetNewSceneObjectName("sphere"),
                RoundStyle.GetSphereVisualStyle(color),
                center,
                radius
            )
        );

        if (RoundStyle.DrawCenter)
        {
            MainSceneComposer.AddPoint(
                GrVisualPoint3D.CreateStatic(
                    GetNewSceneObjectName("point"),
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
            AnimationSpecs.CreateAnimatedVector3D(center);

        var animatedRadius =
            AnimationSpecs.CreateAnimatedScalar(radius);

        MainSceneComposer.AddSphereSurface(
            GrVisualSphereSurface3D.CreateAnimated(
                GetNewSceneObjectName("sphere"),
                RoundStyle.GetSphereVisualStyle(color),
                animatedCenter,
                animatedRadius
            )
        );

        if (RoundStyle.DrawCenter)
        {
            MainSceneComposer.AddPoint(
                GrVisualPoint3D.CreateAnimated(
                    GetNewSceneObjectName("point"),
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
            AnimationSpecs.CreateAnimatedVector3D(
                element.PositionToParametricCurve3D()
            );

        var animatedRadius =
            AnimationSpecs.CreateAnimatedScalar(
                element.RealRadiusToParametricScalar()
            );

        MainSceneComposer.AddSphereSurface(
            GrVisualSphereSurface3D.CreateAnimated(
                GetNewSceneObjectName("sphere"),
                RoundStyle.GetSphereVisualStyle(color),
                animatedPosition,
                animatedRadius
            )
        );

        if (RoundStyle.DrawCenter)
        {
            MainSceneComposer.AddPoint(
                GrVisualPoint3D.CreateAnimated(
                    GetNewSceneObjectName("point"),
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