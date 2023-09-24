using System.Runtime.CompilerServices;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Elements;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space3D.Animations;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space3D.Basic;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space3D.Curves;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space3D.Surfaces;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space1D.Scalars;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space3D.Bivectors;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space3D.Curves;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space2D.Bivectors;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space2D.Curves;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space3D.Trivectors;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Visualizer;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space3D.Styles;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using WebComposerLib.Colors;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Visualizer;

public class RGaConformalVisualizer :
    GeometryVisualizer
{
    public RGaConformalSpace ConformalSpace { get; }

    public RGaConformalVisualizerDirectionStyle DirectionStyle { get; }

    public RGaConformalVisualizerTangentStyle TangentStyle { get; }

    public RGaConformalVisualizerFlatStyle FlatStyle { get; }

    public RGaConformalVisualizerRoundStyle RoundStyle { get; }


    internal RGaConformalVisualizer(RGaConformalSpace conformalSpace)
    {
        ConformalSpace = conformalSpace;

        DirectionStyle = new RGaConformalVisualizerDirectionStyle(this, 0.08);
        TangentStyle = new RGaConformalVisualizerTangentStyle(this, 0.08);
        FlatStyle = new RGaConformalVisualizerFlatStyle(this, 0.08, 6);
        RoundStyle = new RGaConformalVisualizerRoundStyle(this, 0.08);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected IReadOnlyList<int> GetInvalidFrameIndices(RGaConformalParametricElement element, RGaConformalElementKind kind, int egaDirectionGrade)
    {
        return AnimationSpecs
            .GetFrameIndexValuePairs(element.GetElement)
            .Where(indexElement => 
                indexElement.Value.Specs.Kind != kind ||
                indexElement.Value.Specs.EGaDirectionGrade != egaDirectionGrade
            ).SelectToImmutableArray(
                indexElement => indexElement.Key
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected IReadOnlyList<int> GetDirectionPointInvalidFrameIndices(RGaConformalParametricElement element)
    {
        return GetInvalidFrameIndices(
            element,
            RGaConformalElementKind.Direction,
            0
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected IReadOnlyList<int> GetDirectionLineInvalidFrameIndices(RGaConformalParametricElement element)
    {
        return GetInvalidFrameIndices(
            element,
            RGaConformalElementKind.Direction,
            1
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected IReadOnlyList<int> GetDirectionPlaneInvalidFrameIndices(RGaConformalParametricElement element)
    {
        return GetInvalidFrameIndices(
            element,
            RGaConformalElementKind.Direction,
            2
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected IReadOnlyList<int> GetDirectionVolumeInvalidFrameIndices(RGaConformalParametricElement element)
    {
        return GetInvalidFrameIndices(
            element,
            RGaConformalElementKind.Direction,
            3
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected IReadOnlyList<int> GetTangentPointInvalidFrameIndices(RGaConformalParametricElement element)
    {
        return GetInvalidFrameIndices(
            element,
            RGaConformalElementKind.Tangent,
            0
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected IReadOnlyList<int> GetTangentLineInvalidFrameIndices(RGaConformalParametricElement element)
    {
        return GetInvalidFrameIndices(
            element,
            RGaConformalElementKind.Tangent,
            1
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected IReadOnlyList<int> GetTangentPlaneInvalidFrameIndices(RGaConformalParametricElement element)
    {
        return GetInvalidFrameIndices(
            element,
            RGaConformalElementKind.Tangent,
            2
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected IReadOnlyList<int> GetTangentVolumeInvalidFrameIndices(RGaConformalParametricElement element)
    {
        return GetInvalidFrameIndices(
            element,
            RGaConformalElementKind.Tangent,
            3
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected IReadOnlyList<int> GetFlatPointInvalidFrameIndices(RGaConformalParametricElement element)
    {
        return GetInvalidFrameIndices(
            element,
            RGaConformalElementKind.Flat,
            0
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected IReadOnlyList<int> GetFlatLineInvalidFrameIndices(RGaConformalParametricElement element)
    {
        return GetInvalidFrameIndices(
            element,
            RGaConformalElementKind.Flat,
            1
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected IReadOnlyList<int> GetFlatPlaneInvalidFrameIndices(RGaConformalParametricElement element)
    {
        return GetInvalidFrameIndices(
            element,
            RGaConformalElementKind.Flat,
            2
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected IReadOnlyList<int> GetFlatVolumeInvalidFrameIndices(RGaConformalParametricElement element)
    {
        return GetInvalidFrameIndices(
            element,
            RGaConformalElementKind.Flat,
            3
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected IReadOnlyList<int> GetRoundPointInvalidFrameIndices(RGaConformalParametricElement element)
    {
        return GetInvalidFrameIndices(
            element,
            RGaConformalElementKind.Round,
            0
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected IReadOnlyList<int> GetRoundPointPairInvalidFrameIndices(RGaConformalParametricElement element)
    {
        return GetInvalidFrameIndices(
            element,
            RGaConformalElementKind.Round,
            1
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected IReadOnlyList<int> GetRoundCircleInvalidFrameIndices(RGaConformalParametricElement element)
    {
        return GetInvalidFrameIndices(
            element,
            RGaConformalElementKind.Round,
            2
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected IReadOnlyList<int> GetRoundSphereInvalidFrameIndices(RGaConformalParametricElement element)
    {
        return GetInvalidFrameIndices(
            element,
            RGaConformalElementKind.Round,
            3
        );
    }


    public override RGaConformalVisualizer BeginDrawing2D(string title, IReadOnlyDictionary<string, string> laTeXDictionary)
    {
        base.BeginDrawing2D(title, laTeXDictionary);

        DirectionStyle.SetStyle(0.08);
        TangentStyle.SetStyle(0.08);
        FlatStyle.SetStyle(0.08, 6);
        RoundStyle.SetStyle(0.08);

        return this;
    }

    public override RGaConformalVisualizer BeginDrawing3D(string title, IReadOnlyDictionary<string, string> laTeXDictionary)
    {
        base.BeginDrawing3D(title, laTeXDictionary);

        DirectionStyle.SetStyle(0.08);
        TangentStyle.SetStyle(0.08);
        FlatStyle.SetStyle(0.08, 6);
        RoundStyle.SetStyle(0.08);

        return this;
    }


    public RGaConformalVisualizer SetDirectionStyle(double thickness, bool drawPosition = true, double directionRadius = 1, double normalDirectionRadius = 1, double auxGeometryColorAlpha = 0.5)
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

    public RGaConformalVisualizer SetTangentStyle(double thickness, bool drawPosition = true, double directionRadius = 1, double normalDirectionRadius = 1, double auxGeometryColorAlpha = 0.5)
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

    public RGaConformalVisualizer SetFlatStyle(double thickness, double radius, bool drawPosition = true, double directionRadius = 1, double normalDirectionRadius = 1, double auxGeometryColorAlpha = 0.5)
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

    public RGaConformalVisualizer SetRoundStyle(double thickness, bool drawCenter = true, bool drawSphere = true, double directionRadius = 1, double normalDirectionRadius = 1, double auxGeometryColorAlpha = 0.5)
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
    
    
    public RGaConformalVisualizer DrawDirectionPoint2D(Color color, Float64Vector2D position)
    {
        MainSceneComposer.AddPoint(
            GrVisualPoint3D.CreateStatic(
                GetNewSceneObjectName("point"),
                DirectionStyle.GetPointVisualStyle(color),
                position.ToXyVector3D()
            )
        );

        return this;
    }
    
    public RGaConformalVisualizer DrawDirectionPoint2D(Color color, IParametricCurve2D position)
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
    
    public RGaConformalVisualizer DrawDirectionPoint2D(Color color, RGaConformalParametricElement element)
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


    public RGaConformalVisualizer DrawDirectionPoint3D(Color color, Float64Vector3D position)
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
    
    public RGaConformalVisualizer DrawDirectionPoint3D(Color color, IParametricCurve3D position)
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
    
    public RGaConformalVisualizer DrawDirectionPoint3D(Color color, RGaConformalParametricElement element)
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


    public RGaConformalVisualizer DrawDirectionLine2D(Color color, Float64Vector2D position, Float64Vector2D direction)
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
                    position.ToXyVector3D()
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
                    position.ToXyVector3D(),
                    direction.SetLength(DirectionStyle.DirectionRadius).ToXyVector3D()
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
                    position.ToXyVector3D(),
                    direction.ToXyVector3D(),
                    DirectionStyle.NormalDirectionRadius
                )
            );
        }
        
        return this;
    }
    
    public RGaConformalVisualizer DrawDirectionLine2D(Color color, IParametricCurve2D position, IParametricCurve2D direction)
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
                    direction.GetPoint(t).SetLength(DirectionStyle.DirectionRadius).ToXyVector3D()
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
    
    public RGaConformalVisualizer DrawDirectionLine2D(Color color, RGaConformalParametricElement element)
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


    public RGaConformalVisualizer DrawDirectionLine3D(Color color, Float64Vector3D position, Float64Vector3D direction)
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
    
    public RGaConformalVisualizer DrawDirectionLine3D(Color color, IParametricCurve3D position, IParametricCurve3D direction)
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
    
    public RGaConformalVisualizer DrawDirectionLine3D(Color color, RGaConformalParametricElement element)
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


    public RGaConformalVisualizer DrawDirectionPlane2D(Color color, Float64Vector2D position, Float64Bivector2D direction)
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
                    position.ToXyVector3D()
                )
            );
        }

        var normal = 
            direction.ToXyBivector3D().Dual3D();

        if (DirectionStyle.DrawDirection)
        {
            MainSceneComposer.AddBivector(
                GrVisualBivector3D.CreateStatic(
                    GetNewSceneObjectName("bivector"),
                    DirectionStyle.GetVectorVisualStyle(
                        color.SetAlpha(DirectionStyle.AuxGeometryColorAlpha),
                        DirectionStyle.Thickness
                    ),
                    position.ToXyVector3D(),
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
                    position.ToXyVector3D(),
                    normal.SetLength(DirectionStyle.NormalDirectionRadius)
                )
            );
        }

        return this;
    }
    
    public RGaConformalVisualizer DrawDirectionPlane2D(Color color, IParametricCurve2D position, IParametricBivector2D direction)
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
                        b.ToXyBivector3D().Dual3D()
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
    
    public RGaConformalVisualizer DrawDirectionPlane2D(Color color, RGaConformalParametricElement element)
    {
        var animatedPosition =
            AnimationSpecs.CreateXyAnimatedVector3D(
                element.PositionToParametricCurve2D()
            );
        
        var animatedNormal =
            AnimationSpecs.CreateAnimatedVector3D(
                element
                    .DirectionToParametricBivector2D()
                    .XyDual3D()
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


    public RGaConformalVisualizer DrawDirectionPlane3D(Color color, Float64Vector3D position, Float64Bivector3D direction)
    {
        var normal = direction.Dual3D();
        
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
    
    public RGaConformalVisualizer DrawDirectionPlane3D(Color color, IParametricCurve3D position, IParametricBivector3D direction)
    {
        var animatedPosition =
            AnimationSpecs.CreateAnimatedVector3D(position);
        
        var animatedNormal = 
            AnimationSpecs.CreateAnimatedVector3D(
                direction.GetDualVectorCurve()
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
    
    public RGaConformalVisualizer DrawDirectionPlane3D(Color color, RGaConformalParametricElement element)
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


    public RGaConformalVisualizer DrawDirection(Color color, RGaConformalDirection element)
    {
        if (ConformalSpace.Is4D)
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
    
    
    public RGaConformalVisualizer DrawTangentPoint2D(Color color, Float64Vector2D position)
    {
        MainSceneComposer.AddPoint(
            GrVisualPoint3D.CreateStatic(
                GetNewSceneObjectName("point"),
                TangentStyle.GetPointVisualStyle(color),
                position.ToXyVector3D()
            )
        );

        return this;
    }
    
    public RGaConformalVisualizer DrawTangentPoint2D(Color color, IParametricCurve2D position)
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
    
    public RGaConformalVisualizer DrawTangentPoint2D(Color color, RGaConformalParametricElement element)
    {
        var invalidFrameIndices = 
            GetTangentPointInvalidFrameIndices(element);
        
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


    public RGaConformalVisualizer DrawTangentPoint3D(Color color, Float64Vector3D position)
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
    
    public RGaConformalVisualizer DrawTangentPoint3D(Color color, IParametricCurve3D position)
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
    
    public RGaConformalVisualizer DrawTangentPoint3D(Color color, RGaConformalParametricElement element)
    {
        var invalidFrameIndices = 
            GetTangentPointInvalidFrameIndices(element);
        
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


    public RGaConformalVisualizer DrawTangentLine2D(Color color, Float64Vector2D position, Float64Vector2D direction)
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
                    position.ToXyVector3D()
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
                    position.ToXyVector3D(),
                    direction.SetLength(TangentStyle.DirectionRadius).ToXyVector3D()
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
                    position.ToXyVector3D(),
                    direction.ToXyVector3D(),
                    TangentStyle.NormalDirectionRadius
                )
            );
        }
        
        return this;
    }
    
    public RGaConformalVisualizer DrawTangentLine2D(Color color, IParametricCurve2D position, IParametricCurve2D direction)
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
                    direction.GetPoint(t).SetLength(TangentStyle.DirectionRadius).ToXyVector3D()
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
    
    public RGaConformalVisualizer DrawTangentLine2D(Color color, RGaConformalParametricElement element)
    {
        var invalidFrameIndices = 
            GetTangentLineInvalidFrameIndices(element);
        
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


    public RGaConformalVisualizer DrawTangentLine3D(Color color, Float64Vector3D position, Float64Vector3D direction)
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
    
    public RGaConformalVisualizer DrawTangentLine3D(Color color, IParametricCurve3D position, IParametricCurve3D direction)
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
    
    public RGaConformalVisualizer DrawTangentLine3D(Color color, RGaConformalParametricElement element)
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


    public RGaConformalVisualizer DrawTangentPlane2D(Color color, Float64Vector2D position, Float64Bivector2D direction)
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
                    position.ToXyVector3D()
                )
            );
        }

        var normal = 
            direction.ToXyBivector3D().Dual3D();

        if (TangentStyle.DrawDirection)
        {
            MainSceneComposer.AddBivector(
                GrVisualBivector3D.CreateStatic(
                    GetNewSceneObjectName("bivector"),
                    TangentStyle.GetVectorVisualStyle(
                        color.SetAlpha(TangentStyle.AuxGeometryColorAlpha),
                        TangentStyle.Thickness
                    ),
                    position.ToXyVector3D(),
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
                    position.ToXyVector3D(),
                    normal.SetLength(TangentStyle.NormalDirectionRadius)
                )
            );
        }

        return this;
    }
    
    public RGaConformalVisualizer DrawTangentPlane2D(Color color, IParametricCurve2D position, IParametricBivector2D direction)
    {
        var animatedPosition =
            AnimationSpecs.CreateXyAnimatedVector3D(position);
        
        var animatedNormal = 
            AnimationSpecs.CreateAnimatedVector3D(
                direction
                    .ToParametricCurve3D(b => 
                        b.ToXyBivector3D().Dual3D()
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
    
    public RGaConformalVisualizer DrawTangentPlane2D(Color color, RGaConformalParametricElement element)
    {
        var animatedPosition =
            AnimationSpecs.CreateXyAnimatedVector3D(
                element.PositionToParametricCurve2D()
            );
        
        var animatedNormal =
            AnimationSpecs.CreateAnimatedVector3D(
                element
                    .DirectionToParametricBivector2D()
                    .XyDual3D()
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


    public RGaConformalVisualizer DrawTangentPlane3D(Color color, Float64Vector3D position, Float64Bivector3D direction)
    {
        var normal = direction.Dual3D();

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
    
    public RGaConformalVisualizer DrawTangentPlane3D(Color color, IParametricCurve3D position, IParametricBivector3D direction)
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
                direction.GetDualVectorCurve()
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
    
    public RGaConformalVisualizer DrawTangentPlane3D(Color color, RGaConformalParametricElement element)
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


    public RGaConformalVisualizer DrawTangent(Color color, RGaConformalTangent element)
    {
        if (ConformalSpace.Is4D)
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
    
    
    public RGaConformalVisualizer DrawFlatPoint2D(Color color, Float64Vector2D position)
    {
        MainSceneComposer.AddPoint(
            GrVisualPoint3D.CreateStatic(
                GetNewSceneObjectName("point"),
                FlatStyle.GetPointVisualStyle(color),
                position.ToXyVector3D()
            )
        );

        return this;
    }
    
    public RGaConformalVisualizer DrawFlatPoint2D(Color color, IParametricCurve2D position)
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
    
    public RGaConformalVisualizer DrawFlatPoint2D(Color color, RGaConformalParametricElement element)
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


    public RGaConformalVisualizer DrawFlatPoint3D(Color color, Float64Vector3D position)
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
    
    public RGaConformalVisualizer DrawFlatPoint3D(Color color, IParametricCurve3D position)
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
    
    public RGaConformalVisualizer DrawFlatPoint3D(Color color, RGaConformalParametricElement element)
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


    public RGaConformalVisualizer DrawFlatLine2D(Color color, Float64Vector2D position, Float64Vector2D direction)
    {
        MainSceneComposer.AddLineSegment(
            GrVisualLineSegment3D.CreateStatic(
                GetNewSceneObjectName("lineSegment"),
                FlatStyle.GetLineVisualStyle(color),
                (position - direction.SetLength(FlatStyle.Radius)).ToXyVector3D(),
                (position + direction.SetLength(FlatStyle.Radius)).ToXyVector3D()
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
                    position.ToXyVector3D()
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
                    position.ToXyVector3D(),
                    direction.SetLength(FlatStyle.DirectionRadius).ToXyVector3D()
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
                    position.ToXyVector3D(),
                    direction.ToXyVector3D(),
                    FlatStyle.NormalDirectionRadius
                )
            );
        }
        
        return this;
    }
    
    public RGaConformalVisualizer DrawFlatLine2D(Color color, IParametricCurve2D position, IParametricCurve2D direction)
    {
        var animatedPosition =
            AnimationSpecs.CreateXyAnimatedVector3D(position);

        var animatedDirection =
            AnimationSpecs.CreateXyAnimatedVector3D(direction);

        var animatedPoint1 =
            AnimationSpecs.CreateAnimatedVector3D(t => 
                (position.GetPoint(t) - direction.GetPoint(t).SetLength(FlatStyle.Radius)).ToXyVector3D()
            );

        var animatedPoint2 =
            AnimationSpecs.CreateAnimatedVector3D(t => 
                (position.GetPoint(t) + direction.GetPoint(t).SetLength(FlatStyle.Radius)).ToXyVector3D()
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
                    direction.GetPoint(t).SetLength(FlatStyle.DirectionRadius).ToXyVector3D()
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
    
    public RGaConformalVisualizer DrawFlatLine2D(Color color, RGaConformalParametricElement element)
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

                    return (el.PositionToVector2D() - el.DirectionToVector2D(FlatStyle.Radius)).ToXyVector3D();
                }
            );
            
        var animatedPoint2 =
            AnimationSpecs.CreateAnimatedVector3D(
                t =>
                {
                    var el = 
                        element.GetElement(t);

                    return (el.PositionToVector2D() + el.DirectionToVector2D(FlatStyle.Radius)).ToXyVector3D();
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


    public RGaConformalVisualizer DrawFlatLine3D(Color color, Float64Vector3D position, Float64Vector3D direction)
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
    
    public RGaConformalVisualizer DrawFlatLine3D(Color color, IParametricCurve3D position, IParametricCurve3D direction)
    {
        var animatedPosition =
            AnimationSpecs.CreateAnimatedVector3D(position);

        var animatedDirection =
            AnimationSpecs.CreateAnimatedVector3D(direction);

        var animatedPoint1 =
            AnimationSpecs.CreateAnimatedVector3D(t => 
                (position.GetPoint(t) - direction.GetPoint(t).SetLength(FlatStyle.Radius))
            );

        var animatedPoint2 =
            AnimationSpecs.CreateAnimatedVector3D(t => 
                (position.GetPoint(t) + direction.GetPoint(t).SetLength(FlatStyle.Radius))
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
    
    public RGaConformalVisualizer DrawFlatLine3D(Color color, RGaConformalParametricElement element)
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


    public RGaConformalVisualizer DrawFlatPlane2D(Color color, Float64Vector2D position, Float64Bivector2D direction)
    {
        var normal = 
            direction.ToXyBivector3D().Dual3D();
        
        if (FlatStyle.DrawPosition)
        {
            MainSceneComposer.AddPoint(
                GrVisualPoint3D.CreateStatic(
                    GetNewSceneObjectName("point"),
                    FlatStyle.GetPointVisualStyle(
                        color.SetAlpha(FlatStyle.AuxGeometryColorAlpha),
                        FlatStyle.Thickness * 1.5
                    ),
                    position.ToXyVector3D()
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
                    position.ToXyVector3D(),
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
                    position.ToXyVector3D(),
                    normal.SetLength(FlatStyle.NormalDirectionRadius)
                )
            );
        }

        return this;
    }
    
    public RGaConformalVisualizer DrawFlatPlane2D(Color color, IParametricCurve2D position, IParametricBivector2D direction)
    {
        var animatedPosition =
            AnimationSpecs.CreateXyAnimatedVector3D(position);
        
        var animatedNormal = 
            AnimationSpecs.CreateAnimatedVector3D(
                direction
                    .ToParametricCurve3D(b => 
                        b.ToXyBivector3D().Dual3D()
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
    
    public RGaConformalVisualizer DrawFlatPlane2D(Color color, RGaConformalParametricElement element)
    {
        var animatedPosition =
            AnimationSpecs.CreateXyAnimatedVector3D(
                element.PositionToParametricCurve2D()
            );
        
        var animatedNormal =
            AnimationSpecs.CreateAnimatedVector3D(
                element
                    .DirectionToParametricBivector2D()
                    .XyDual3D()
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


    public RGaConformalVisualizer DrawFlatPlane3D(Color color, Float64Vector3D position, Float64Bivector3D direction)
    {
        var normal = direction.Dual3D();

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
    
    public RGaConformalVisualizer DrawFlatPlane3D(Color color, IParametricCurve3D position, IParametricBivector3D direction)
    {
        var animatedPosition =
            AnimationSpecs.CreateAnimatedVector3D(position);
        
        var animatedNormal = 
            AnimationSpecs.CreateAnimatedVector3D(
                direction.GetDualVectorCurve()
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
    
    public RGaConformalVisualizer DrawFlatPlane3D(Color color, RGaConformalParametricElement element)
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


    public RGaConformalVisualizer DrawFlat(Color color, RGaConformalFlat element)
    {
        if (ConformalSpace.Is4D)
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
    
    
    public RGaConformalVisualizer DrawRoundPoint2D(Color color, Float64Vector2D center)
    {
        MainSceneComposer.AddPoint(
            GrVisualPoint3D.CreateStatic(
                GetNewSceneObjectName("point"),
                RoundStyle.GetPointVisualStyle(color),
                center.ToXyVector3D()
            )
        );

        return this;
    }
    
    public RGaConformalVisualizer DrawRoundPoint2D(Color color, IParametricCurve2D center)
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
    
    public RGaConformalVisualizer DrawRoundPoint2D(Color color, RGaConformalParametricElement element)
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


    public RGaConformalVisualizer DrawRoundPoint3D(Color color, Float64Vector3D center)
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
    
    public RGaConformalVisualizer DrawRoundPoint3D(Color color, IParametricCurve3D center)
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
    
    public RGaConformalVisualizer DrawRoundPoint3D(Color color, RGaConformalParametricElement element)
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


    public RGaConformalVisualizer DrawRoundPointPair2D(Color color, Float64Vector2D center, Float64Vector2D direction, double radius)
    {
        var point1 = (center - direction.SetLength(radius)).ToXyVector3D();
        var point2 = (center + direction.SetLength(radius)).ToXyVector3D();

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
                    center.ToXyVector3D(),
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
                    center.ToXyVector3D()
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
                    center.ToXyVector3D(),
                    direction.SetLength(RoundStyle.DirectionRadius).ToXyVector3D()
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
                    center.ToXyVector3D(),
                    direction.ToXyVector3D(),
                    RoundStyle.NormalDirectionRadius
                )
            );
        }
        
        return this;
    }
    
    public RGaConformalVisualizer DrawRoundPointPair2D(Color color, IParametricCurve2D center, IParametricCurve2D direction, IParametricScalar radius)
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
    
    public RGaConformalVisualizer DrawRoundPointPair2D(Color color, RGaConformalParametricElement element)
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
    

    public RGaConformalVisualizer DrawRoundPointPair3D(Color color, Float64Vector3D center, Float64Vector3D direction, double radius)
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
    
    public RGaConformalVisualizer DrawRoundPointPair3D(Color color, IParametricCurve3D center, IParametricCurve3D direction, IParametricScalar radius)
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
    
    public RGaConformalVisualizer DrawRoundPointPair3D(Color color, RGaConformalParametricElement element)
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
    

    public RGaConformalVisualizer DrawRoundCircle2D(Color color, Float64Vector2D center, Float64Bivector2D direction, double radius)
    {
        var normal = 
            Float64Bivector3D.Create(direction.Xy, 0 , 0).Dual3D();

        MainSceneComposer.AddCircle(
            GrVisualCircleCurve3D.CreateStatic(
                GetNewSceneObjectName("circle"),
                RoundStyle.GetCircleVisualStyle(color),
                center.ToXyVector3D(),
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
                    center.ToXyVector3D(),
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
                    center.ToXyVector3D()
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
                    center.ToXyVector3D()
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
                    center.ToXyVector3D(),
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
                    center.ToXyVector3D(),
                    normal
                )
            );
        }

        return this;
    }
    
    public RGaConformalVisualizer DrawRoundCircle2D(Color color, IParametricCurve2D center, IParametricBivector2D direction, IParametricScalar radius)
    {
        var animatedCenter = 
            AnimationSpecs.CreateAnimatedVector3D(center.ToXyParametricCurve3D());

        var animatedNormal = 
            AnimationSpecs.CreateAnimatedVector3D(
                direction
                .ToParametricCurve3D(b => 
                    Float64Bivector3D.Create(b.Xy, 0, 0).Dual3D()
                )
            );
        
        var animatedRadius = 
            AnimationSpecs.CreateAnimatedScalar(radius);

        MainSceneComposer.AddCircle(
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
    
    public RGaConformalVisualizer DrawRoundCircle2D(Color color, RGaConformalParametricElement element)
    {
        var animatedPosition =
            AnimationSpecs.CreateXyAnimatedVector3D(
                element.PositionToParametricCurve2D()
            );
        
        var animatedNormal =
            AnimationSpecs.CreateAnimatedVector3D(
                element
                    .DirectionToParametricBivector2D()
                    .XyDual3D()
            );
        
        var animatedRadius = 
            AnimationSpecs.CreateAnimatedScalar(
                element.RealRadiusToParametricScalar()
            );

        MainSceneComposer.AddCircle(
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


    public RGaConformalVisualizer DrawRoundCircle3D(Color color, Float64Vector3D center, Float64Bivector3D direction, double radius)
    {
        var normal = direction.Dual3D();

        MainSceneComposer.AddCircle(
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
    
    public RGaConformalVisualizer DrawRoundCircle3D(Color color, IParametricCurve3D center, IParametricBivector3D direction, IParametricScalar radius)
    {
        var animatedCenter = 
            AnimationSpecs.CreateAnimatedVector3D(center);

        var animatedNormal = 
            AnimationSpecs.CreateAnimatedVector3D(direction.GetDualVectorCurve());
        
        var animatedRadius = 
            AnimationSpecs.CreateAnimatedScalar(radius);

        MainSceneComposer.AddCircle(
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
    
    public RGaConformalVisualizer DrawRoundCircle3D(Color color, RGaConformalParametricElement element)
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

        MainSceneComposer.AddCircle(
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


    public RGaConformalVisualizer DrawRoundSphere3D(Color color, Float64Vector3D center, Float64Trivector3D direction, double radius)
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
    
    public RGaConformalVisualizer DrawRoundSphere3D(Color color, IParametricCurve3D center, IParametricTrivector3D direction, IParametricScalar radius)
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
    
    public RGaConformalVisualizer DrawRoundSphere3D(Color color, RGaConformalParametricElement element)
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


    public RGaConformalVisualizer DrawRound(Color color, RGaConformalRound element)
    {
        if (ConformalSpace.Is4D)
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
    

    public RGaConformalVisualizer DrawElement(Color color, RGaConformalElement element)
    {
        return element switch
        {
            RGaConformalDirection direction => DrawDirection(color, direction),
            RGaConformalTangent tangent => DrawTangent(color, tangent),
            RGaConformalFlat flat => DrawFlat(color, flat),
            RGaConformalRound round => DrawRound(color, round),
            _ => this
        };
    }
    

}