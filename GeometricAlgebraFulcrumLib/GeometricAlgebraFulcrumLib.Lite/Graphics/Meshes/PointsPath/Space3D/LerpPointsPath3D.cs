using System.Collections;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Meshes.PathsMesh.Space3D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Meshes.PointsPath.Space3D;

public sealed class LerpPointsPath3D 
    : IPointsPath3D
{
    public LerpPathsMesh3D BaseMesh { get; }

    public IPointsPath3D Path1 
        => BaseMesh.Path1;

    public IPointsPath3D Path2 
        => BaseMesh.Path2;

    public double ParamValue { get; set; }

    public int Count 
        => Path1.Count;

    public IFloat64Vector3D this[int index] 
        => ParamValue.Lerp(
            Path1[index], 
            Path2[index]
        );

    public Pair<IFloat64Vector3D> this[int index1, int index2] 
        => new Pair<IFloat64Vector3D>(
            this[index1],
            this[index2]
        );

    public bool IsBasic 
        => true;

    public bool IsOperator 
        => false;


    public LerpPointsPath3D(LerpPathsMesh3D baseMesh)
    {
        BaseMesh = baseMesh;
        ParamValue = 0.5d;
    }

    public LerpPointsPath3D(LerpPathsMesh3D baseMesh, double paramValue)
    {
        BaseMesh = baseMesh;
        ParamValue = paramValue;
    }

        
    public bool IsValid()
    {
        return ParamValue.IsValid() && 
               Path1.IsValid() &&
               Path2.IsValid();
    }
        
    public IPointsPath3D MapPoints(Func<IFloat64Vector3D, IFloat64Vector3D> pointMapping)
    {
        return new ArrayPointsPath3D(this.Select(pointMapping));
    }

    public IEnumerator<IFloat64Vector3D> GetEnumerator()
    {
        return Enumerable
            .Range(0, Count)
            .Select(i => this[i])
            .GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return Enumerable
            .Range(0, Count)
            .Select(i => this[i])
            .GetEnumerator();
    }
}