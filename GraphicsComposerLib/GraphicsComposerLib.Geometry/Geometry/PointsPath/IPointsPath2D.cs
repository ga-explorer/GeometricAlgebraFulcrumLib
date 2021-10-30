using DataStructuresLib.Sequences.Periodic1D;
using EuclideanGeometryLib.BasicMath.Tuples;

namespace GraphicsComposerLib.Geometry.Geometry.PointsPath
{
    /// <summary>
    /// This interface represents a poly-line in 3D.
    /// https://en.wikipedia.org/wiki/Polygonal_chain
    /// </summary>
    public interface IPointsPath2D 
        : IPeriodicSequence1D<ITuple2D>
    {
    }
}