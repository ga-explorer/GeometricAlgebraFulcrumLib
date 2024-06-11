using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PointsPath;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PointsPath.Space3D;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Sequences.Periodic1D;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Animations;

public class GrVisualAnimatedVectorPath3D :
    GrVisualAnimatedGeometry,
    IPeriodicSequence1D<GrVisualAnimatedVector3D>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVectorPath3D Create(GrVisualAnimationSpecs animationSpecs, int count)
    {
        return new GrVisualAnimatedVectorPath3D(
            animationSpecs, 
            count
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVectorPath3D Create(GrVisualAnimationSpecs animationSpecs, GrVisualAnimatedVector3D[] dataArray)
    {
        return new GrVisualAnimatedVectorPath3D(
            animationSpecs, 
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
            if (value.FrameTimeRange != FrameTimeRange || !value.IsValid())
                throw new InvalidOperationException();

            _dataArray[index.Mod(Count)] = value;
        }
    }
    
    public bool IsBasic
        => true;

    public bool IsOperator
        => false;
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private GrVisualAnimatedVectorPath3D(GrVisualAnimationSpecs animationSpecs, int count)
        : base(animationSpecs)
    {
        _dataArray = new GrVisualAnimatedVector3D[count];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private GrVisualAnimatedVectorPath3D(GrVisualAnimationSpecs animationSpecs, GrVisualAnimatedVector3D[] dataArray)
        : base(animationSpecs)
    {
        Debug.Assert(
           dataArray.All(p => 
                p.IsValid() && 
                p.AnimationSpecs == animationSpecs
            )
        );

        _dataArray = dataArray;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return FrameTimeRange.IsValid() &&
               FrameTimeRange.MinValue >= 0 &&
               _dataArray.All(p => 
                   p.IsValid() && 
                   p.FrameTimeRange == FrameTimeRange
               );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IPointsPath3D GetPointsPath(double time)
    {
        var pointArray = _dataArray.MapItems(
            p => (ILinFloat64Vector3D) p.GetPoint(time)
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