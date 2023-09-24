using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using DataStructuresLib.Collections;
using DataStructuresLib.Extensions;
using DataStructuresLib.Sequences.Periodic1D;
using DataStructuresLib.Sequences.Periodic2D;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Meshes.PointsMesh;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Meshes.PointsMesh.Space3D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space3D.Animations;

public class GrVisualAnimatedVectorMesh3D :
    GrVisualAnimatedGeometry,
    IPeriodicSequence2D<GrVisualAnimatedVector3D>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVectorMesh3D Create(GrVisualAnimationSpecs animationSpecs, int count1, int count2)
    {
        return new GrVisualAnimatedVectorMesh3D(
            animationSpecs, 
            count1, 
            count2
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVectorMesh3D Create(GrVisualAnimationSpecs animationSpecs, GrVisualAnimatedVector3D[,] dataArray)
    {
        return new GrVisualAnimatedVectorMesh3D(
            animationSpecs, 
            dataArray
        );
    }
    
    
    private readonly GrVisualAnimatedVector3D[,] _dataArray;


    public int Count1
        => _dataArray.GetLength(0);

    public int Count2
        => _dataArray.GetLength(1);

    public int Count 
        => _dataArray.Length;

    public GrVisualAnimatedVector3D this[int index]
    {
        get
        {
            var (index1, index2) = this.GetItemIndexPair(index);

            return _dataArray[index1, index2];
        }
        set
        {
            var (index1, index2) = this.GetItemIndexPair(index);

            if (value.FrameTimeRange != FrameTimeRange)
                throw new InvalidOperationException();

            _dataArray[index1, index2] = value;
        }
    }

    public GrVisualAnimatedVector3D this[int index1, int index2]
        => _dataArray[
            index1.Mod(Count1), 
            index2.Mod(Count2)
        ];

    public bool IsBasic
        => true;

    public bool IsOperator
        => false;
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private GrVisualAnimatedVectorMesh3D(GrVisualAnimationSpecs animationSpecs, int count1, int count2)
        : base(animationSpecs)
    {
        _dataArray = new GrVisualAnimatedVector3D[count1, count2];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private GrVisualAnimatedVectorMesh3D(GrVisualAnimationSpecs animationSpecs, GrVisualAnimatedVector3D[,] dataArray)
        : base(animationSpecs)
    {
        Debug.Assert(
            dataArray.GetItems(
                p => p.AnimationSpecs == animationSpecs
            ).All(p => p)
        );

        _dataArray = dataArray;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return _dataArray.GetItems(
            p => p
        ).All(p => p.IsValid() && p.FrameTimeRange == FrameTimeRange);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public PSeqSlice1D<GrVisualAnimatedVector3D> GetSliceAt(int dimension, int index)
    {
        return new PSeqSlice1D<GrVisualAnimatedVector3D>(this, dimension, index);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrVisualAnimatedVectorPath3D GetSlicePathAt(int dimension, int index)
    {
        var dataArray = 
            GetSliceAt(dimension, index).ToArray();

        return GrVisualAnimatedVectorPath3D.Create(AnimationSpecs, dataArray);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IPointsMesh3D GetPointsMesh(double time)
    {
        var pointArray = _dataArray.MapItems(
            p => (IFloat64Vector3D) p.GetPoint(time)
        );

        return new ArrayPointsMesh3D(pointArray);
    }
    
    public IEnumerator<GrVisualAnimatedVector3D> GetEnumerator()
    {
        for (var i2 = 0; i2 < Count2; i2++)
        for (var i1 = 0; i1 < Count1; i1++)
            yield return _dataArray[i1, i2];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}