using DataStructuresLib.Sequences.Periodic2D;
using NumericalGeometryLib.BasicMath.Tuples;
using GraphicsComposerLib.Geometry.Meshes.PointsPath.Space3D;

namespace GraphicsComposerLib.Geometry.Meshes.PointsMesh
{
    public interface IPointsMesh3D
        : IPeriodicSequence2D<ITuple3D>
    {
        PointsMeshSlicePointsPath3D GetSlicePathAt(int dimension, int index);
    }
}