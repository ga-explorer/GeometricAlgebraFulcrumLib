using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using DataStructuresLib.Extensions;
using DataStructuresLib.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Borders;
using GeometricAlgebraFulcrumLib.MathBase.Graphics.Meshes.PointsPath;
using GeometricAlgebraFulcrumLib.MathBase.Graphics.Meshes.PointsPath.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;

namespace GraphicsComposerLib.Rendering.Visuals.Space3D.Animations;

public class GrVisualAnimatedVectorPath3D :
    GrVisualAnimatedGeometry,
    IPeriodicSequence1D<GrVisualAnimatedVector3D>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVectorPath3D Create(Float64Range1D timeRange, int count)
    {
        return new GrVisualAnimatedVectorPath3D(timeRange, count);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVectorPath3D Create(Float64Range1D timeRange, GrVisualAnimatedVector3D[] dataArray)
    {
        return new GrVisualAnimatedVectorPath3D(timeRange, dataArray);
    }


    private readonly GrVisualAnimatedVector3D[] _dataArray;

    
    public int Count 
        => _dataArray.Length;

    public GrVisualAnimatedVector3D this[int index]
    {
        get => _dataArray[index.Mod(Count)];
        set
        {
            if (value.TimeRange != TimeRange || !value.IsValid())
                throw new InvalidOperationException();

            _dataArray[index.Mod(Count)] = value;
        }
    }
    
    public bool IsBasic
        => true;

    public bool IsOperator
        => false;
    
    public override Float64Range1D TimeRange { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private GrVisualAnimatedVectorPath3D(Float64Range1D timeRange, int count)
    {
        Debug.Assert(
            timeRange.IsValid() &&
            timeRange.MinValue >= 0
        );

        TimeRange = timeRange;
        _dataArray = new GrVisualAnimatedVector3D[count];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private GrVisualAnimatedVectorPath3D(Float64Range1D timeRange, GrVisualAnimatedVector3D[] dataArray)
    {
        Debug.Assert(
            timeRange.IsValid() &&
            timeRange.MinValue >= 0 &&
            dataArray.All(p => 
                p.IsValid() && 
                p.TimeRange == timeRange
            )
        );

        TimeRange = timeRange;
        _dataArray = dataArray;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return TimeRange.IsValid() &&
               TimeRange.MinValue >= 0 &&
               _dataArray.All(p => 
                   p.IsValid() && 
                   p.TimeRange == TimeRange
               );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IPointsPath3D GetPointsPath(double time)
    {
        var pointArray = _dataArray.MapItems(
            p => (IFloat64Tuple3D) p.GetPoint(time)
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