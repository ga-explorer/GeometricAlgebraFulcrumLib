using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space1D.Angles;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space1D.Scalars;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space2D.Curves;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space3D.Curves;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Animations;

public static class GrVisualAnimatedGeometryComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedScalar CreateAnimatedScalar(this GrVisualAnimationSpecs animationSpecs, double baseCurveValue)
    {
        return GrVisualAnimatedScalar.Create(
            animationSpecs,
            ConstantParametricScalar.Create(
                animationSpecs.FrameTimeRange, 
                baseCurveValue
            )
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedScalar CreateAnimatedScalar(this GrVisualAnimationSpecs animationSpecs, Float64ScalarRange baseParameterRange, double baseCurveValue)
    {
        return GrVisualAnimatedScalar.Create(
            animationSpecs,
            ConstantParametricScalar.Create(
                baseParameterRange, 
                baseCurveValue
            ),
            baseParameterRange
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedScalar CreateAnimatedScalar(this GrVisualAnimationSpecs animationSpecs, Func<double, double> baseCurveFunc)
    {
        return GrVisualAnimatedScalar.Create(
            animationSpecs,
            ComputedParametricScalar.Create(
                animationSpecs.FrameTimeRange, 
                baseCurveFunc
            )
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedScalar CreateAnimatedScalar(this GrVisualAnimationSpecs animationSpecs, Float64ScalarRange baseParameterRange, Func<double, double> baseCurveFunc)
    {
        return GrVisualAnimatedScalar.Create(
            animationSpecs,
            ComputedParametricScalar.Create(
                baseParameterRange, 
                baseCurveFunc
            ),
            baseParameterRange
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedScalar CreateAnimatedScalar(this GrVisualAnimationSpecs animationSpecs, IFloat64ParametricScalar baseCurve)
    {
        return GrVisualAnimatedScalar.Create(
            animationSpecs,
            baseCurve
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedScalar CreateAnimatedScalar(this GrVisualAnimationSpecs animationSpecs, Float64ScalarRange baseParameterRange, IFloat64ParametricScalar baseCurve)
    {
        return GrVisualAnimatedScalar.Create(
            animationSpecs,
            baseCurve,
            baseParameterRange
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedScalar CreateAnimatedScalar(this GrVisualAnimationSpecs animationSpecs, IParametricPolarAngle baseCurve)
    {
        return GrVisualAnimatedScalar.Create(
            animationSpecs,
            baseCurve.ToRadianParametricScalar()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedScalar CreateAnimatedScalar(this GrVisualAnimationSpecs animationSpecs, Float64ScalarRange baseParameterRange, IParametricPolarAngle baseCurve)
    {
        return GrVisualAnimatedScalar.Create(
            animationSpecs,
            baseCurve.ToRadianParametricScalar(),
            baseParameterRange
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector2D CreateAnimatedVector2D(this GrVisualAnimationSpecs animationSpecs, LinFloat64Vector2D baseCurveValue)
    {
        return GrVisualAnimatedVector2D.Create(
            animationSpecs,
            ConstantParametricCurve2D.Create(
                animationSpecs.FrameTimeRange, 
                baseCurveValue
            )
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector2D CreateAnimatedVector2D(this GrVisualAnimationSpecs animationSpecs, Float64ScalarRange baseParameterRange, LinFloat64Vector2D baseCurveValue)
    {
        return GrVisualAnimatedVector2D.Create(
            animationSpecs,
            ConstantParametricCurve2D.Create(
                baseParameterRange, 
                baseCurveValue
            ),
            baseParameterRange
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector2D CreateAnimatedVector2D(this GrVisualAnimationSpecs animationSpecs, Func<double, LinFloat64Vector2D> baseCurveFunc)
    {
        return GrVisualAnimatedVector2D.Create(
            animationSpecs,
            ComputedParametricCurve2D.Create(
                animationSpecs.FrameTimeRange, 
                baseCurveFunc
            )
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector2D CreateAnimatedVector2D(this GrVisualAnimationSpecs animationSpecs, Float64ScalarRange baseParameterRange, Func<double, LinFloat64Vector2D> baseCurveFunc)
    {
        return GrVisualAnimatedVector2D.Create(
            animationSpecs,
            ComputedParametricCurve2D.Create(
                baseParameterRange, 
                baseCurveFunc
            ),
            baseParameterRange
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector2D CreateAnimatedVector2D(this GrVisualAnimationSpecs animationSpecs, IFloat64ParametricCurve2D baseCurve)
    {
        return GrVisualAnimatedVector2D.Create(
            animationSpecs,
            baseCurve
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector2D CreateAnimatedVector2D(this GrVisualAnimationSpecs animationSpecs, Float64ScalarRange baseParameterRange, IFloat64ParametricCurve2D baseCurve)
    {
        return GrVisualAnimatedVector2D.Create(
            animationSpecs,
            baseCurve,
            baseParameterRange
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector3D CreateXyAnimatedVector3D(this GrVisualAnimationSpecs animationSpecs, LinFloat64Vector2D baseCurveValue)
    {
        return GrVisualAnimatedVector3D.Create(
            animationSpecs,
            ConstantParametricCurve3D.Create(
                animationSpecs.FrameTimeRange, 
                baseCurveValue.ToXyLinVector3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector3D CreateAnimatedVector3D(this GrVisualAnimationSpecs animationSpecs, LinFloat64Vector3D baseCurveValue)
    {
        return GrVisualAnimatedVector3D.Create(
            animationSpecs,
            ConstantParametricCurve3D.Create(
                animationSpecs.FrameTimeRange, 
                baseCurveValue
            )
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector3D CreateXyAnimatedVector3D(this GrVisualAnimationSpecs animationSpecs, Float64ScalarRange baseParameterRange, LinFloat64Vector2D baseCurveValue)
    {
        return GrVisualAnimatedVector3D.Create(
            animationSpecs,
            ConstantParametricCurve3D.Create(
                baseParameterRange, 
                baseCurveValue.ToXyLinVector3D()
            ),
            baseParameterRange
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector3D CreateAnimatedVector3D(this GrVisualAnimationSpecs animationSpecs, Float64ScalarRange baseParameterRange, LinFloat64Vector3D baseCurveValue)
    {
        return GrVisualAnimatedVector3D.Create(
            animationSpecs,
            ConstantParametricCurve3D.Create(
                baseParameterRange, 
                baseCurveValue
            ),
            baseParameterRange
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector3D CreateXyAnimatedVector3D(this GrVisualAnimationSpecs animationSpecs, Func<double, LinFloat64Vector2D> baseCurveFunc)
    {
        return GrVisualAnimatedVector3D.Create(
            animationSpecs,
            ComputedParametricCurve3D.Create(
                animationSpecs.FrameTimeRange, 
                t => baseCurveFunc(t).ToXyLinVector3D()
            )
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector3D CreateAnimatedVector3D(this GrVisualAnimationSpecs animationSpecs, Func<double, LinFloat64Vector3D> baseCurveFunc)
    {
        return GrVisualAnimatedVector3D.Create(
            animationSpecs,
            ComputedParametricCurve3D.Create(
                animationSpecs.FrameTimeRange, 
                baseCurveFunc
            )
        );
    }
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static GrVisualAnimatedVector3D CreateAnimatedVector3D(this GrVisualAnimationSpecs animationSpecs, Func<double, Float64Vector3D> baseCurveFunc)
    //{
    //    var vector = GrVisualAnimatedVector3D.Create(
    //        animationSpecs,
    //        ComputedParametricCurve3D.Create(
    //            animationSpecs.FrameTimeRange, 
    //            baseCurveFunc
    //        )
    //    );

    //    vector.SetInvalidFrameIndices(invalidFrameIndices);

    //    return vector;
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector3D CreateXyAnimatedVector3D(this GrVisualAnimationSpecs animationSpecs, Float64ScalarRange baseParameterRange, Func<double, LinFloat64Vector2D> baseCurveFunc)
    {
        return GrVisualAnimatedVector3D.Create(
            animationSpecs,
            ComputedParametricCurve3D.Create(
                baseParameterRange, 
                t => baseCurveFunc(t).ToXyLinVector3D()
            ),
            baseParameterRange
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector3D CreateAnimatedVector3D(this GrVisualAnimationSpecs animationSpecs, Float64ScalarRange baseParameterRange, Func<double, LinFloat64Vector3D> baseCurveFunc)
    {
        return GrVisualAnimatedVector3D.Create(
            animationSpecs,
            ComputedParametricCurve3D.Create(
                baseParameterRange, 
                baseCurveFunc
            ),
            baseParameterRange
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector3D CreateXyAnimatedVector3D(this GrVisualAnimationSpecs animationSpecs, IFloat64ParametricCurve2D baseCurve)
    {
        return GrVisualAnimatedVector3D.Create(
            animationSpecs,
            ComputedParametricCurve3D.Create(
                animationSpecs.FrameTimeRange, 
                t => baseCurve.GetPoint(t).ToXyLinVector3D()
            )
        );
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector3D CreateXyAnimatedVector3D(this GrVisualAnimationSpecs animationSpecs, Float64ScalarRange baseParameterRange, IFloat64ParametricCurve2D baseCurve)
    {
        return GrVisualAnimatedVector3D.Create(
            animationSpecs,
            ComputedParametricCurve3D.Create(
                baseParameterRange, 
                t => baseCurve.GetPoint(t).ToXyLinVector3D()
            ),
            baseParameterRange
        );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector3D CreateAnimatedVector3D(this GrVisualAnimationSpecs animationSpecs, IParametricCurve3D baseCurve)
    {
        return GrVisualAnimatedVector3D.Create(
            animationSpecs,
            baseCurve
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector3D CreateAnimatedVector3D(this GrVisualAnimationSpecs animationSpecs, Float64ScalarRange baseParameterRange, IParametricCurve3D baseCurve)
    {
        return GrVisualAnimatedVector3D.Create(
            animationSpecs,
            baseCurve,
            baseParameterRange
        );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVectorMesh3D CreateAnimatedVectorMesh3D(this GrVisualAnimationSpecs animationSpecs, int count1, int count2)
    {
        return GrVisualAnimatedVectorMesh3D.Create(
            animationSpecs, 
            count1, 
            count2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVectorMesh3D CreateAnimatedVectorMesh3D(this GrVisualAnimationSpecs animationSpecs, GrVisualAnimatedVector3D[,] dataArray)
    {
        return GrVisualAnimatedVectorMesh3D.Create(
            animationSpecs, 
            dataArray
        );
    }
}