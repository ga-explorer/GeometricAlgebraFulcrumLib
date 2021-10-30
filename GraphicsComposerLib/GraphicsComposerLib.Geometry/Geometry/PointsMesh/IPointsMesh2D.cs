using DataStructuresLib.Sequences.Periodic2D;
using EuclideanGeometryLib.BasicMath.Tuples;
using GraphicsComposerLib.Geometry.Geometry.PointsPath.Space2D;

namespace GraphicsComposerLib.Geometry.Geometry.PointsMesh
{
    public interface IPointsMesh2D
        : IPeriodicSequence2D<ITuple2D>
    {
        PointsMeshSlicePointsPath2D GetSlicePathAt(int dimension, int index);
    }
}