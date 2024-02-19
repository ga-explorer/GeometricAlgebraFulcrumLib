using System.Collections;
using DataStructuresLib.Basic;
using DataStructuresLib.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Meshes.PointsPath.Space3D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Meshes.PointsMesh.Space3D;

public class PlanarPointsMesh3D
    : IPointsMesh3D
{
    public IFloat64Vector3D Origin { get; set; }

    public IFloat64Vector3D Direction1 { get; set; }

    public IFloat64Vector3D Direction2 { get; set; }

    public IPeriodicSequence1D<double> Parameters1 { get; }

    public IPeriodicSequence1D<double> Parameters2 { get; }

    public int Count 
        => Parameters1.Count * Parameters2.Count;

    public int Count1 
        => Parameters1.Count;

    public int Count2 
        => Parameters2.Count;

    public IFloat64Vector3D this[int index]
    {
        get
        {
            index = index.Mod(Count);
            var index1 = index % Count1;
            var index2 = (index - index1) / Count1;

            return this[index1, index2];
        }
    }

    public IFloat64Vector3D this[int index1, int index2]
    {
        get
        {
            var t1 = Parameters1[index1];
            var t2 = Parameters2[index2];

            return Float64Vector3D.Create(Origin.X + t1 * Direction1.X + t2 * Direction2.X,
                Origin.Y + t1 * Direction1.Y + t2 * Direction2.Y,
                Origin.Z + t1 * Direction1.Z + t2 * Direction2.Z);
        }
    }

    public bool IsBasic 
        => true;

    public bool IsOperator 
        => false;


    public PlanarPointsMesh3D(IPeriodicSequence1D<double> parameters1, IPeriodicSequence1D<double> parameters2)
    {
        Parameters1 = parameters1;
        Parameters2 = parameters2;

        Origin = Float64Vector3D.Create(0, 0, 0);
        Direction1 = Float64Vector3D.Create(1, 0, 0);
        Direction2 = Float64Vector3D.Create(0, 1, 0);
    }
        
    public PlanarPointsMesh3D(IPeriodicSequence1D<double> parameters1, IPeriodicSequence1D<double> parameters2, IFloat64Vector3D origin, IFloat64Vector3D direction1, IFloat64Vector3D direction2)
    {
        Parameters1 = parameters1;
        Parameters2 = parameters2;

        Origin = origin;
        Direction1 = direction1;
        Direction2 = direction2;
    }


    public PSeqSlice1D<IFloat64Vector3D> GetSliceAt(int dimension, int index)
    {
        return new PointsMeshSlicePointsPath3D(this, dimension, index);
    }

    public PointsMeshSlicePointsPath3D GetSlicePathAt(int dimension, int index)
    {
        return new PointsMeshSlicePointsPath3D(this, dimension, index);
    }

    public IEnumerator<IFloat64Vector3D> GetEnumerator()
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