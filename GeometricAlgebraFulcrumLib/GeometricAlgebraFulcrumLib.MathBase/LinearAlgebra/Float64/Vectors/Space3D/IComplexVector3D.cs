using System.Numerics;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D
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