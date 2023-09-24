using DataStructuresLib.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Meshes.PointsPath
{
    /// <summary>
    /// This interface represents a poly-line in 3D.
    /// https://en.wikipedia.org/wiki/Polygonal_chain
    /// </summary>
    public interface IPointsPath3D : 
        IGeometricElement, 
        IPeriodicSequence1D<IFloat64Vector3D>
    {
        IPointsPath3D MapPoints(Func<IFloat64Vector3D, IFloat64Vector3D> pointMapping);
    }
}