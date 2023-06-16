using System.Numerics;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space4D
{
    public interface IComplexTuple4D : 
        IGeometricElement, 
        IQuad<Complex>
    {
        Complex X { get; }

        Complex Y { get; }

        Complex Z { get; }

        Complex W { get; }
    }
}