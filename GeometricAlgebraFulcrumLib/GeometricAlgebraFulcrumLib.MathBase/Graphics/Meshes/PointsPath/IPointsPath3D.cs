using DataStructuresLib.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.MathBase.Graphics.Meshes.PointsPath
{
    /// <summary>
    /// This interface represents a poly-line in 3D.
    /// https://en.wikipedia.org/wiki/Polygonal_chain
    /// </summary>
    public interface IPointsPath3D : 
        IGeometricElement, 
        IPeriodicSequence1D<IFloat64Tuple3D>
    {
        IPointsPath3D MapPoints(Func<IFloat64Tuple3D, IFloat64Tuple3D> pointMapping);
    }
}