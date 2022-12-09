using DataStructuresLib.Basic;

namespace NumericalGeometryLib.BasicMath.Tuples
{
    public interface IFloat64Tuple4D : 
        IGeometricElement, 
        IQuad<double>
    {
        double X { get; }

        double Y { get; }

        double Z { get; }

        double W { get; }
    }
}