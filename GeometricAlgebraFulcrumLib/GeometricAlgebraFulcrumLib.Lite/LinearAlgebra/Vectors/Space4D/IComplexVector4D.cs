using System.Numerics;
using DataStructuresLib.Basic;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space4D
{
    public interface IComplexVector4D : 
        IGeometricElement, 
        IQuad<Complex>
    {
        Complex X { get; }

        Complex Y { get; }

        Complex Z { get; }

        Complex W { get; }
    }
}