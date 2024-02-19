using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space1D.Angles;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space1D.Scalars;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space2D.Curves;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space3D.Curves;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space3D.Animations;

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
    public static GrVisualAnimatedScalar CreateAnimatedScalar(this GrVisualAnimationSpecs animationSpecs, IParametricScalar baseCurve)
    {
        return GrVisualAnimatedScalar.Create(
            animationSpecs,
            baseCurve
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedScalar CreateAnimatedScalar(this GrVisualAnimationSpecs animationSpecs, Float64ScalarRange baseParameterRange, IParametricScalar baseCurve)
    {
        return GrVisualAnimatedScalar.Create(
            animationSpecs,
            baseCurve,
            baseParameterRange
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedScalar CreateAnimatedScalar(this GrVisualAnimationSpecs animationSpecs, IParametricAngle baseCurve)
    {
        return GrVisualAnimatedScalar.Create(
            animationSpecs,
            baseCurve.ToRadianParametricScalar()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedScalar CreateAnimatedScalar(this GrVisualAnimationSpecs animationSpecs, Float64ScalarRange baseParameterRange, IParametricAngle baseCurve)
    {
        return GrVisualAnimatedScalar.Create(
            animationSpecs,
            baseCurve.ToRadianParametricScalar(),
            baseParameterRange
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector2D CreateAnimatedVector2D(this GrVisualAnimationSpecs animationSpecs, Float64Vector2D baseCurveValue)
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
    public static GrVisualAnimatedVector2D CreateAnimatedVector2D(this GrVisualAnimationSpecs animationSpecs, Float64ScalarRange baseParameterRange, Float64Vector2D baseCurveValue)
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
    public static GrVisualAnimatedVector2D CreateAnimatedVector2D(this GrVisualAnimationSpecs animationSpecs, Func<double, Float64Vector2D> baseCurveFunc)
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
    public static GrVisualAnimatedVector2D CreateAnimatedVector2D(this GrVisualAnimationSpecs animationSpecs, Float64ScalarRange baseParameterRange, Func<double, Float64Vector2D> baseCurveFunc)
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
    public static GrVisualAnimatedVector2D CreateAnimatedVector2D(this GrVisualAnimationSpecs animationSpecs, IParametricCurve2D baseCurve)
    {
        return GrVisualAnimatedVector2D.Create(
            animationSpecs,
            baseCurve
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector2D CreateAnimatedVector2D(this GrVisualAnimationSpecs animationSpecs, Float64ScalarRange baseParameterRange, IParametricCurve2D baseCurve)
    {
        return GrVisualAnimatedVector2D.Create(
            animationSpecs,
            baseCurve,
            baseParameterRange
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector3D CreateXyAnimatedVector3D(this GrVisualAnimationSpecs animationSpecs, Float64Vector2D baseCurveValue)
    {
        return GrVisualAnimatedVector3D.Create(
            animationSpecs,
            ConstantParametricCurve3D.Create(
                animationSpecs.FrameTimeRange, 
                baseCurveValue.ToXyVector3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector3D CreateAnimatedVector3D(this GrVisualAnimationSpecs animationSpecs, Float64Vector3D baseCurveValue)
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
    public static GrVisualAnimatedVector3D CreateXyAnimatedVector3D(this GrVisualAnimationSpecs animationSpecs, Float64ScalarRange baseParameterRange, Float64Vector2D baseCurveValue)
    {
        return GrVisualAnimatedVector3D.Create(
            animationSpecs,
            ConstantParametricCurve3D.Create(
                baseParameterRange, 
                baseCurveValue.ToXyVector3D()
            ),
            baseParameterRange
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector3D CreateAnimatedVector3D(this GrVisualAnimationSpecs animationSpecs, Float64ScalarRange baseParameterRange, Float64Vector3D baseCurveValue)
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
    public static GrVisualAnimatedVector3D CreateXyAnimatedVector3D(this GrVisualAnimationSpecs animationSpecs, Func<double, Float64Vector2D> baseCurveFunc)
    {
        return GrVisualAnimatedVector3D.Create(
            animationSpecs,
            ComputedParametricCurve3D.Create(
                animationSpecs.FrameTimeRange, 
                t => baseCurveFunc(t).ToXyVector3D()
            )
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector3D CreateAnimatedVector3D(this GrVisualAnimationSpecs animationSpecs, Func<double, Float64Vector3D> baseCurveFunc)
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
    public static GrVisualAnimatedVector3D CreateXyAnimatedVector3D(this GrVisualAnimationSpecs animationSpecs, Float64ScalarRange baseParameterRange, Func<double, Float64Vector2D> baseCurveFunc)
    {
        return GrVisualAnimatedVector3D.Create(
            animationSpecs,
            ComputedParametricCurve3D.Create(
                baseParameterRange, 
                t => baseCurveFunc(t).ToXyVector3D()
            ),
            baseParameterRange
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector3D CreateAnimatedVector3D(this GrVisualAnimationSpecs animationSpecs, Float64ScalarRange baseParameterRange, Func<double, Float64Vector3D> baseCurveFunc)
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
    public static GrVisualAnimatedVector3D CreateXyAnimatedVector3D(this GrVisualAnimationSpecs animationSpecs, IParametricCurve2D baseCurve)
    {
        return GrVisualAnimatedVector3D.Create(
            animationSpecs,
            ComputedParametricCurve3D.Create(
                animationSpecs.FrameTimeRange, 
                t => baseCurve.GetPoint(t).ToXyVector3D()
            )
        );
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector3D CreateXyAnimatedVector3D(this GrVisualAnimationSpecs animationSpecs, Float64ScalarRange baseParameterRange, IParametricCurve2D baseCurve)
    {
        return GrVisualAnimatedVector3D.Create(
            animationSpecs,
            ComputedParametricCurve3D.Create(
                baseParameterRange, 
                t => baseCurve.GetPoint(t).ToXyVector3D()
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