using DataStructuresLib.Sequences.Periodic2D;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Meshes.PointsPath.Space3D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Meshes.PointsMesh
{
    public interface IPointsMesh3D : 
        IGeometricElement,
        IPeriodicSequence2D<IFloat64Vector3D>
    {
        PointsMeshSlicePointsPath3D GetSlicePathAt(int dimension, int index);
    }
}