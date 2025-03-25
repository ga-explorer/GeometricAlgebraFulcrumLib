using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Signals;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64.Angles;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Basic;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Basic;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Animations;

public static class GrVisualAnimatedGeometryComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedScalar CreateAnimatedScalar(this Float64SamplingSpecs samplingSpecs, double baseCurveValue)
    {
        return GrVisualAnimatedScalar.Create(
            Float64ScalarSignal.FiniteConstant(
                samplingSpecs.MinTime, 
                samplingSpecs.MaxTime,
                baseCurveValue
            ), 
            samplingSpecs
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedScalar CreateAnimatedScalar(this Float64SamplingSpecs samplingSpecs, Func<double, double> baseCurveFunc)
    {
        return GrVisualAnimatedScalar.Create(
            Float64ScalarSignal.FiniteComputed(samplingSpecs.MinTime, 
                samplingSpecs.MaxTime, baseCurveFunc), 
            samplingSpecs
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedScalar CreateAnimatedScalar(this Float64SamplingSpecs samplingSpecs, Float64ScalarSignal baseCurve)
    {
        return GrVisualAnimatedScalar.Create(baseCurve, samplingSpecs);
    }
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static GrVisualAnimatedScalar CreateAnimatedScalar(this Float64SamplingSpecs samplingSpecs, Float64ScalarSignal baseCurve)
    //{
    //    return GrVisualAnimatedScalar.Create(baseCurve, samplingSpecs);
    //}
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedScalar CreateAnimatedScalar(this Float64SamplingSpecs samplingSpecs, LinFloat64PolarAngleTimeSignal baseCurve)
    {
        return GrVisualAnimatedScalar.Create(baseCurve.RadiansToTimeSignal(), samplingSpecs);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector2D CreateAnimatedVector2D(this Float64SamplingSpecs samplingSpecs, LinFloat64Vector2D baseCurveValue)
    {
        return GrVisualAnimatedVector2D.Create(Float64ConstantPath2D.Finite(
                samplingSpecs.TimeRange, 
                baseCurveValue
            ), samplingSpecs);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector2D CreateAnimatedVector2D(this Float64SamplingSpecs samplingSpecs, Float64ScalarRange baseTimeRange, LinFloat64Vector2D baseCurveValue)
    {
        return GrVisualAnimatedVector2D.Create(
            Float64ConstantPath2D.Finite(
                baseTimeRange, 
                baseCurveValue
            ),
            samplingSpecs
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector2D CreateAnimatedVector2D(this Float64SamplingSpecs samplingSpecs, Func<double, LinFloat64Vector2D> baseCurveFunc)
    {
        return GrVisualAnimatedVector2D.Create(Float64ComputedPath2D.Finite(
                samplingSpecs.TimeRange, 
                baseCurveFunc
            ), samplingSpecs);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector2D CreateAnimatedVector2D(this Float64SamplingSpecs samplingSpecs, Float64ScalarRange baseTimeRange, Func<double, LinFloat64Vector2D> baseCurveFunc)
    {
        return GrVisualAnimatedVector2D.Create(
            Float64ComputedPath2D.Finite(
                baseTimeRange, 
                baseCurveFunc
            ),
            samplingSpecs
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector2D CreateAnimatedVector2D(this Float64SamplingSpecs samplingSpecs, Float64Path2D baseCurve)
    {
        return GrVisualAnimatedVector2D.Create(baseCurve, samplingSpecs);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector2D CreateAnimatedVector2D(this Float64Path2D baseCurve, Float64SamplingSpecs samplingSpecs)
    {
        return GrVisualAnimatedVector2D.Create(baseCurve, samplingSpecs);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector3D CreateXyAnimatedVector3D(this Float64SamplingSpecs samplingSpecs, LinFloat64Vector2D baseCurveValue)
    {
        return GrVisualAnimatedVector3D.Create(
            Float64ConstantPath3D.Finite(
                samplingSpecs.TimeRange, 
                baseCurveValue.ToXyLinVector3D()
            ), 
            samplingSpecs
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector3D CreateAnimatedVector3D(this Float64SamplingSpecs samplingSpecs, LinFloat64Vector3D baseCurveValue)
    {
        return GrVisualAnimatedVector3D.Create(
            Float64ConstantPath3D.Finite(
                samplingSpecs.TimeRange, 
                baseCurveValue
            ), 
            samplingSpecs
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector3D CreateXyAnimatedVector3D(this Float64SamplingSpecs samplingSpecs, Float64ScalarRange baseTimeRange, LinFloat64Vector2D baseCurveValue)
    {
        return GrVisualAnimatedVector3D.Create(
            Float64ConstantPath3D.Finite(
                baseTimeRange, 
                baseCurveValue.ToXyLinVector3D()
            ),
            samplingSpecs
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector3D CreateAnimatedVector3D(this Float64SamplingSpecs samplingSpecs, Float64ScalarRange baseTimeRange, LinFloat64Vector3D baseCurveValue)
    {
        return GrVisualAnimatedVector3D.Create(
            Float64ConstantPath3D.Finite(
                baseTimeRange, 
                baseCurveValue
            ),
            samplingSpecs
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector3D CreateXyAnimatedVector3D(this Float64SamplingSpecs samplingSpecs, Func<double, LinFloat64Vector2D> baseCurveFunc)
    {
        return GrVisualAnimatedVector3D.Create(
            Float64ComputedPath3D.Finite(
                samplingSpecs.TimeRange, 
                t => baseCurveFunc(t).ToXyLinVector3D()
            ), 
            samplingSpecs
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector3D CreateAnimatedVector3D(this Float64SamplingSpecs samplingSpecs, Func<double, LinFloat64Vector3D> baseCurveFunc)
    {
        return GrVisualAnimatedVector3D.Create(
            Float64ComputedPath3D.Finite(
                samplingSpecs.TimeRange, 
                baseCurveFunc
            ), 
            samplingSpecs
        );
    }
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static GrVisualAnimatedVector3D CreateAnimatedVector3D(this GrVisualAnimationSpecs samplingSpecs, Func<double, Float64Vector3D> baseCurveFunc)
    //{
    //    var vector = GrVisualAnimatedVector3D.Create(
    //        samplingSpecs,
    //        Float64ComputedPointPath3D.Finite(
    //            samplingSpecs.FrameTimeRange, 
    //            baseCurveFunc
    //        )
    //    );

    //    vector.SetInvalidFrameIndices(invalidFrameIndices);

    //    return vector;
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector3D CreateXyAnimatedVector3D(this Float64SamplingSpecs samplingSpecs, Float64ScalarRange baseTimeRange, Func<double, LinFloat64Vector2D> baseCurveFunc)
    {
        return GrVisualAnimatedVector3D.Create(
            Float64ComputedPath3D.Finite(
                baseTimeRange, 
                t => baseCurveFunc(t).ToXyLinVector3D()
            ),
            samplingSpecs
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector3D CreateAnimatedVector3D(this Float64SamplingSpecs samplingSpecs, Float64ScalarRange baseTimeRange, Func<double, LinFloat64Vector3D> baseCurveFunc)
    {
        return GrVisualAnimatedVector3D.Create(
            Float64ComputedPath3D.Finite(
                baseTimeRange, 
                baseCurveFunc
            ),
            samplingSpecs
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector3D CreateXyAnimatedVector3D(this Float64SamplingSpecs samplingSpecs, Float64Path2D baseCurve)
    {
        return GrVisualAnimatedVector3D.Create(Float64ComputedPath3D.Finite(
                samplingSpecs.TimeRange, 
                t => baseCurve.GetValue(t).ToXyLinVector3D()
            ), samplingSpecs);
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector3D CreateXyAnimatedVector3D(this Float64SamplingSpecs samplingSpecs, Float64ScalarRange baseTimeRange, Float64Path2D baseCurve)
    {
        return GrVisualAnimatedVector3D.Create(
            Float64ComputedPath3D.Finite(
                baseTimeRange, 
                t => baseCurve.GetValue(t).ToXyLinVector3D()
            ),
            samplingSpecs
        );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector3D CreateAnimatedVector3D(this Float64SamplingSpecs samplingSpecs, Float64Path3D baseCurve)
    {
        return GrVisualAnimatedVector3D.Create(baseCurve, samplingSpecs);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector3D CreateAnimatedVector3D(this Float64SamplingSpecs samplingSpecs, Float64ScalarRange baseTimeRange, Float64Path3D baseCurve)
    {
        return GrVisualAnimatedVector3D.Create(
            baseCurve,
            samplingSpecs
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVectorMesh3D CreateAnimatedVectorMesh3D(this Float64SamplingSpecs samplingSpecs, int count1, int count2)
    {
        return GrVisualAnimatedVectorMesh3D.Create(
            samplingSpecs, 
            count1, 
            count2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVectorMesh3D CreateAnimatedVectorMesh3D(this Float64SamplingSpecs samplingSpecs, GrVisualAnimatedVector3D[,] dataArray)
    {
        return GrVisualAnimatedVectorMesh3D.Create(
            samplingSpecs, 
            dataArray
        );
    }
}