using System.Collections;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PathsMesh;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PointsPath.Space2D;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Sequences.Periodic1D;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PointsMesh.Space2D;

public sealed class PathsMeshAsPointsMesh2D
    : IPointsMesh2D
{
    public IPathsMesh2D BaseMesh { get; }

    public int Count
        => BaseMesh.MeshPointsCount;

    public ILinFloat64Vector2D this[int index]
    {
        get
        {
            index = index.Mod(Count);
            var index1 = index % Count1;
            var index2 = (index - index1) / Count1;

            return BaseMesh[index2][index1];
        }
    }

    public int Count1
        => BaseMesh.PathPointsCount;

    public int Count2
        => BaseMesh.Count;

    public ILinFloat64Vector2D this[int index1, int index2]
        => BaseMesh[index2][index1];

    public bool IsBasic
        => true;

    public bool IsOperator
        => false;


    public PathsMeshAsPointsMesh2D(IPathsMesh2D baseMesh)
    {
        BaseMesh = baseMesh;
    }


    public PSeqSlice1D<ILinFloat64Vector2D> GetSliceAt(int dimension, int index)
    {
        return new PointsMeshSlicePointsPath2D(this, dimension, index);
    }

    public PointsMeshSlicePointsPath2D GetSlicePathAt(int dimension, int index)
    {
        return new PointsMeshSlicePointsPath2D(this, dimension, index);
    }

    public IEnumerator<ILinFloat64Vector2D> GetEnumerator()
    {
        return BaseMesh.SelectMany(p => p).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public bool IsValid()
    {
        throw new NotImplementedException();
    }
}