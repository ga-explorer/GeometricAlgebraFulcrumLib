using System.Numerics;
using DataStructuresLib.Basic;

namespace GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples
{
    public interface IComplexTuple3D : IGeometricElement, ITriplet<Complex>
    {
        Complex X { get; }

        Complex Y { get; }

        Complex Z { get; }
    }
}