using System.Numerics;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D
{
    public interface IComplexTuple2D : IGeometricElement, IPair<Complex>
    {
        Complex X { get; }

        Complex Y { get; }
    }
}