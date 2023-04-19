using DataStructuresLib.Sequences.Periodic2D;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;
using GraphicsComposerLib.Geometry.Meshes.PointsPath.Space2D;

namespace GraphicsComposerLib.Geometry.Meshes.PointsMesh
{
    public interface IPointsMesh2D
        : IPeriodicSequence2D<IFloat64Tuple2D>
    {
        PointsMeshSlicePointsPath2D GetSlicePathAt(int dimension, int index);
    }
}