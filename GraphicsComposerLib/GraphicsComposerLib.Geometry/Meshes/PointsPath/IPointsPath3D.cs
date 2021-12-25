using DataStructuresLib.Sequences.Periodic1D;
using NumericalGeometryLib.BasicMath.Tuples;

namespace GraphicsComposerLib.Geometry.Meshes.PointsPath
{
    /// <summary>
    /// This interface represents a poly-line in 3D.
    /// https://en.wikipedia.org/wiki/Polygonal_chain
    /// </summary>
    public interface IPointsPath3D 
        : IPeriodicSequence1D<ITuple3D>
    {
    }
}