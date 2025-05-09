using System.Collections;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PointsPath.Space2D;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PointsMesh.Space2D;

public class PlanarPointsMesh2D
    : IPointsMesh2D
{
    public ILinFloat64Vector2D Origin { get; set; }

    public ILinFloat64Vector2D Direction1 { get; set; }

    public ILinFloat64Vector2D Direction2 { get; set; }

    public IPeriodicSequence1D<double> Parameters1 { get; }

    public IPeriodicSequence1D<double> Parameters2 { get; }

    public int Count
        => Parameters1.Count * Parameters2.Count;

    public int Count1
        => Parameters1.Count;

    public int Count2
        => Parameters2.Count;

    public ILinFloat64Vector2D this[int index]
    {
        get
        {
            index = index.Mod(Count);
            var index1 = index % Count1;
            var index2 = (index - index1) / Count1;

            return this[index1, index2];
        }
    }

    public ILinFloat64Vector2D this[int index1, int index2]
    {
        get
        {
            var t1 = Parameters1[index1];
            var t2 = Parameters2[index2];

            return LinFloat64Vector2D.Create(Origin.X + t1 * Direction1.X + t2 * Direction2.X,
                Origin.Y + t1 * Direction1.Y + t2 * Direction2.Y);
        }
    }

    public bool IsBasic
        => true;

    public bool IsOperator
        => false;


    public PlanarPointsMesh2D(IPeriodicSequence1D<double> parameters1, IPeriodicSequence1D<double> parameters2)
    {
        Parameters1 = parameters1;
        Parameters2 = parameters2;

        Origin = LinFloat64Vector2D.Create((Float64Scalar)0, 0);
        Direction1 = LinFloat64Vector2D.Create((Float64Scalar)1, 0);
        Direction2 = LinFloat64Vector2D.Create((Float64Scalar)0, 1);
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
        for (var i2 = 0; i2 < Count2; i2++)
        for (var i1 = 0; i1 < Count1; i1++)
            yield return this[i1, i2];
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