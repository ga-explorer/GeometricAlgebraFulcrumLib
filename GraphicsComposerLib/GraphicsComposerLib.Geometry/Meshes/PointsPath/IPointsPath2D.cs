using DataStructuresLib.Sequences.Periodic1D;
using NumericalGeometryLib.BasicMath.Tuples;

namespace GraphicsComposerLib.Geometry.Meshes.PointsPath
{
    /// <summary>
    /// This interface represents a poly-line in 2D.
    /// https://en.wikipedia.org/wiki/Polygonal_chain
    /// </summary>
    public interface IPointsPath2D 
        : IPeriodicSequence1D<ITuple2D>
    {
    }
}