using DataStructuresLib.Basic;

namespace EuclideanGeometryLib.BasicMath.Tuples
{
    public interface ITuple2D : 
        IGeometricElement, 
        IPair<double>
    {
        double X { get; }

        double Y { get; }
    }
}