using System.Numerics;
using DataStructuresLib.Basic;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D
{
    public interface IComplexVector2D : 
        IGeometricElement, 
        IPair<Complex>
    {
        Complex X { get; }

        Complex Y { get; }
    }
}