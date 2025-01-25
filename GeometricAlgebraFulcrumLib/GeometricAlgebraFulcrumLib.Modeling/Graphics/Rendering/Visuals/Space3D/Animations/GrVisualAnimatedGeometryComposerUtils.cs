using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space1D.Angles;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space1D.Scalars;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space2D.Curves;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space3D.Curves;
using GeometricAlgebraFulcrumLib.Modeling.Signals;
using GeometricAlgebraFulcrumLib.Modeling.Temporal.Float64.Scalars;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Animations;

public static class GrVisualAnimatedGeometryComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedScalar CreateAnimatedScalar(this Float64SamplingSpecs samplingSpecs, double baseCurveValue)
    {
        return GrVisualAnimatedScalar.Create(
            TemporalFloat64Scalar.Constant(
                baseCurveValue,
                samplingSpecs.MinTime, 
                samplingSpecs.MaxTime
            ), 
            samplingSpecs
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedScalar CreateAnimatedScalar(this Float64SamplingSpecs samplingSpecs, Func<double, double> baseCurveFunc)
    {
        return GrVisualAnimatedScalar.Create(
            TemporalFloat64Scalar.Computed(
                baseCurveFunc,
                samplingSpecs.MinTime, 
                samplingSpecs.MaxTime
            ), 
            samplingSpecs
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedScalar CreateAnimatedScalar(this Float64SamplingSpecs samplingSpecs, TemporalFloat64Scalar baseCurve)
    {
        return GrVisualAnimatedScalar.Create(baseCurve, samplingSpecs);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedScalar CreateAnimatedScalar(this Float64SamplingSpecs samplingSpecs, IFloat64ParametricScalar baseCurve)
    {
        return GrVisualAnimatedScalar.Create(baseCurve.ToTemporalScalar(), samplingSpecs);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedScalar CreateAnimatedScalar(this Float64SamplingSpecs samplingSpecs, IParametricPolarAngle baseCurve)
    {
        return GrVisualAnimatedScalar.Create(baseCurve.ToTemporalScalar(), samplingSpecs);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector2D CreateAnimatedVector2D(this Float64SamplingSpecs samplingSpecs, LinFloat64Vector2D baseCurveValue)
    {
        return GrVisualAnimatedVector2D.Create(
            samplingSpecs,
            ConstantParametricCurve2D.Create(
                samplingSpecs.TimeRange, 
                baseCurveValue
            )
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector2D CreateAnimatedVector2D(this Float64SamplingSpecs samplingSpecs, Float64ScalarRange baseParameterRange, LinFloat64Vector2D baseCurveValue)
    {
        return GrVisualAnimatedVector2D.Create(
            samplingSpecs,
            ConstantParametricCurve2D.Create(
                baseParameterRange, 
                baseCurveValue
            ),
            baseParameterRange
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector2D CreateAnimatedVector2D(this Float64SamplingSpecs samplingSpecs, Func<double, LinFloat64Vector2D> baseCurveFunc)
    {
        return GrVisualAnimatedVector2D.Create(
            samplingSpecs,
            ComputedParametricCurve2D.Create(
                samplingSpecs.TimeRange, 
                baseCurveFunc
            )
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector2D CreateAnimatedVector2D(this Float64SamplingSpecs samplingSpecs, Float64ScalarRange baseParameterRange, Func<double, LinFloat64Vector2D> baseCurveFunc)
    {
        return GrVisualAnimatedVector2D.Create(
            samplingSpecs,
            ComputedParametricCurve2D.Create(
                baseParameterRange, 
                baseCurveFunc
            ),
            baseParameterRange
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector2D CreateAnimatedVector2D(this Float64SamplingSpecs samplingSpecs, IFloat64ParametricCurve2D baseCurve)
    {
        return GrVisualAnimatedVector2D.Create(
            samplingSpecs,
            baseCurve
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector2D CreateAnimatedVector2D(this Float64SamplingSpecs samplingSpecs, Float64ScalarRange baseParameterRange, IFloat64ParametricCurve2D baseCurve)
    {
        return GrVisualAnimatedVector2D.Create(
            samplingSpecs,
            baseCurve,
            baseParameterRange
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector3D CreateXyAnimatedVector3D(this Float64SamplingSpecs samplingSpecs, LinFloat64Vector2D baseCurveValue)
    {
        return GrVisualAnimatedVector3D.Create(
            samplingSpecs,
            ConstantParametricCurve3D.Create(
                samplingSpecs.TimeRange, 
                baseCurveValue.ToXyLinVector3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector3D CreateAnimatedVector3D(this Float64SamplingSpecs samplingSpecs, LinFloat64Vector3D baseCurveValue)
    {
        return GrVisualAnimatedVector3D.Create(
            samplingSpecs,
            ConstantParametricCurve3D.Create(
                samplingSpecs.TimeRange, 
                baseCurveValue
            )
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector3D CreateXyAnimatedVector3D(this Float64SamplingSpecs samplingSpecs, Float64ScalarRange baseParameterRange, LinFloat64Vector2D baseCurveValue)
    {
        return GrVisualAnimatedVector3D.Create(
            samplingSpecs,
            ConstantParametricCurve3D.Create(
                baseParameterRange, 
                baseCurveValue.ToXyLinVector3D()
            ),
            baseParameterRange
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector3D CreateAnimatedVector3D(this Float64SamplingSpecs samplingSpecs, Float64ScalarRange baseParameterRange, LinFloat64Vector3D baseCurveValue)
    {
        return GrVisualAnimatedVector3D.Create(
            samplingSpecs,
            ConstantParametricCurve3D.Create(
                baseParameterRange, 
                baseCurveValue
            ),
            baseParameterRange
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector3D CreateXyAnimatedVector3D(this Float64SamplingSpecs samplingSpecs, Func<double, LinFloat64Vector2D> baseCurveFunc)
    {
        return GrVisualAnimatedVector3D.Create(
            samplingSpecs,
            ComputedParametricCurve3D.Create(
                samplingSpecs.TimeRange, 
                t => baseCurveFunc(t).ToXyLinVector3D()
            )
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector3D CreateAnimatedVector3D(this Float64SamplingSpecs samplingSpecs, Func<double, LinFloat64Vector3D> baseCurveFunc)
    {
        return GrVisualAnimatedVector3D.Create(
            samplingSpecs,
            ComputedParametricCurve3D.Create(
                samplingSpecs.TimeRange, 
                baseCurveFunc
            )
        );
    }
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static GrVisualAnimatedVector3D CreateAnimatedVector3D(this GrVisualAnimationSpecs samplingSpecs, Func<double, Float64Vector3D> baseCurveFunc)
    //{
    //    var vector = GrVisualAnimatedVector3D.Create(
    //        samplingSpecs,
    //        ComputedParametricCurve3D.Create(
    //            samplingSpecs.FrameTimeRange, 
    //            baseCurveFunc
    //        )
    //    );

    //    vector.SetInvalidFrameIndices(invalidFrameIndices);

    //    return vector;
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector3D CreateXyAnimatedVector3D(this Float64SamplingSpecs samplingSpecs, Float64ScalarRange baseParameterRange, Func<double, LinFloat64Vector2D> baseCurveFunc)
    {
        return GrVisualAnimatedVector3D.Create(
            samplingSpecs,
            ComputedParametricCurve3D.Create(
                baseParameterRange, 
                t => baseCurveFunc(t).ToXyLinVector3D()
            ),
            baseParameterRange
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector3D CreateAnimatedVector3D(this Float64SamplingSpecs samplingSpecs, Float64ScalarRange baseParameterRange, Func<double, LinFloat64Vector3D> baseCurveFunc)
    {
        return GrVisualAnimatedVector3D.Create(
            samplingSpecs,
            ComputedParametricCurve3D.Create(
                baseParameterRange, 
                baseCurveFunc
            ),
            baseParameterRange
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector3D CreateXyAnimatedVector3D(this Float64SamplingSpecs samplingSpecs, IFloat64ParametricCurve2D baseCurve)
    {
        return GrVisualAnimatedVector3D.Create(
            samplingSpecs,
            ComputedParametricCurve3D.Create(
                samplingSpecs.TimeRange, 
                t => baseCurve.GetPoint(t).ToXyLinVector3D()
            )
        );
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector3D CreateXyAnimatedVector3D(this Float64SamplingSpecs samplingSpecs, Float64ScalarRange baseParameterRange, IFloat64ParametricCurve2D baseCurve)
    {
        return GrVisualAnimatedVector3D.Create(
            samplingSpecs,
            ComputedParametricCurve3D.Create(
                baseParameterRange, 
                t => baseCurve.GetPoint(t).ToXyLinVector3D()
            ),
            baseParameterRange
        );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector3D CreateAnimatedVector3D(this Float64SamplingSpecs samplingSpecs, IParametricCurve3D baseCurve)
    {
        return GrVisualAnimatedVector3D.Create(
            samplingSpecs,
            baseCurve
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector3D CreateAnimatedVector3D(this Float64SamplingSpecs samplingSpecs, Float64ScalarRange baseParameterRange, IParametricCurve3D baseCurve)
    {
        return GrVisualAnimatedVector3D.Create(
            samplingSpecs,
            baseCurve,
            baseParameterRange
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