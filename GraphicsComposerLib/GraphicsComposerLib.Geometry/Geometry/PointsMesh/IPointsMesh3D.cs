using DataStructuresLib.Sequences.Periodic2D;
using EuclideanGeometryLib.BasicMath.Tuples;
using GraphicsComposerLib.Geometry.Geometry.PointsPath.Space3D;

namespace GraphicsComposerLib.Geometry.Geometry.PointsMesh
{
    public interface IPointsMesh3D
        : IPeriodicSequence2D<ITuple3D>
    {
        PointsMeshSlicePointsPath3D GetSlicePathAt(int dimension, int index);
    }
}