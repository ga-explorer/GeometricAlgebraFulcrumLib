using DataStructuresLib.Basic;

namespace NumericalGeometryLib.BasicMath.Tuples
{
    public interface IFloat64Tuple3D : 
        IGeometricElement, 
        ITriplet<double>
    {
        double X { get; }

        double Y { get; }

        double Z { get; }
    }
}