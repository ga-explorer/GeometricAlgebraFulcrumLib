using DataStructuresLib.Sequences.Periodic2D;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Meshes.PointsPath.Space2D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Meshes.PointsMesh
{
    public interface IPointsMesh2D : 
        IGeometricElement, 
        IPeriodicSequence2D<IFloat64Vector2D>
    {
        PointsMeshSlicePointsPath2D GetSlicePathAt(int dimension, int index);
    }
}