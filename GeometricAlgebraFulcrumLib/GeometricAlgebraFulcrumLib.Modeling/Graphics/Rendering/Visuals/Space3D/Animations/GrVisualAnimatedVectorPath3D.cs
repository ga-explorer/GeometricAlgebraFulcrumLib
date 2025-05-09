using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PointsPath;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PointsPath.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Signals;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Animations;

public class GrVisualAnimatedVectorPath3D :
    GrVisualAnimatedGeometry,
    IPeriodicSequence1D<GrVisualAnimatedVector3D>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVectorPath3D Create(Float64SamplingSpecs samplingSpecs, int count)
    {
        return new GrVisualAnimatedVectorPath3D(
            samplingSpecs, 
            count
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVectorPath3D Create(Float64SamplingSpecs samplingSpecs, GrVisualAnimatedVector3D[] dataArray)
    {
        return new GrVisualAnimatedVectorPath3D(
            samplingSpecs, 
            dataArray
        );
    }
    

    private readonly GrVisualAnimatedVector3D[] _dataArray;

    
    public int Count 
        => _dataArray.Length;

    public GrVisualAnimatedVector3D this[int index]
    {
        get => _dataArray[index.Mod(Count)];
        set
        {
            if (((GrVisualAnimatedGeometry)value).TimeRange != TimeRange || !value.IsValid())
                throw new InvalidOperationException();

            _dataArray[index.Mod(Count)] = value;
        }
    }
    
    public bool IsBasic
        => true;

    public bool IsOperator
        => false;
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private GrVisualAnimatedVectorPath3D(Float64SamplingSpecs samplingSpecs, int count)
        : base(samplingSpecs)
    {
        _dataArray = new GrVisualAnimatedVector3D[count];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private GrVisualAnimatedVectorPath3D(Float64SamplingSpecs samplingSpecs, GrVisualAnimatedVector3D[] dataArray)
        : base(samplingSpecs)
    {
        Debug.Assert(
           dataArray.All(p => 
                p.IsValid() && 
                p.SamplingSpecs == samplingSpecs
            )
        );

        _dataArray = dataArray;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return TimeRange.IsValid() &&
               TimeRange.MinValue >= 0 &&
               _dataArray.All(p => 
                   p.IsValid() && 
                   ((GrVisualAnimatedGeometry)p).TimeRange == TimeRange
               );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IPointsPath3D GetPointsPath(double time)
    {
        var pointArray = _dataArray.MapItems(
            p => (ILinFloat64Vector3D) p.GetValue(time)
        );

        return new ArrayPointsPath3D(pointArray);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<GrVisualAnimatedVector3D> GetEnumerator()
    {
        return ((IEnumerable<GrVisualAnimatedVector3D>)_dataArray).GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}