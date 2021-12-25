using System.Numerics;
using DataStructuresLib.Basic;

namespace NumericalGeometryLib.BasicMath.Tuples
{
    public interface IComplexTuple4D : IGeometricElement, IQuad<Complex>
    {
        Complex X { get; }

        Complex Y { get; }

        Complex Z { get; }

        Complex W { get; }
    }
}