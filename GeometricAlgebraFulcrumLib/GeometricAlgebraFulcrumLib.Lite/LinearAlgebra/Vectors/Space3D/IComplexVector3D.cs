using System.Numerics;
using DataStructuresLib.Basic;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D
{
    public interface IComplexVector3D : 
        IGeometricElement, 
        ITriplet<Complex>
    {
        Complex X { get; }

        Complex Y { get; }

        Complex Z { get; }
    }
}