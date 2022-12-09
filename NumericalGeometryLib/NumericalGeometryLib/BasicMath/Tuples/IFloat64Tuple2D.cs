using DataStructuresLib.Basic;

namespace NumericalGeometryLib.BasicMath.Tuples
{
    public interface IFloat64Tuple2D : 
        IGeometricElement, 
        IPair<double>
    {
        double X { get; }

        double Y { get; }
    }
}