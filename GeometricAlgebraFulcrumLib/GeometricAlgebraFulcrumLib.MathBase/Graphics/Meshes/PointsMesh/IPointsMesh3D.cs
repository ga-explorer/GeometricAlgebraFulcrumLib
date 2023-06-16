using DataStructuresLib.Sequences.Periodic2D;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath;
using GeometricAlgebraFulcrumLib.MathBase.Graphics.Meshes.PointsPath.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.MathBase.Graphics.Meshes.PointsMesh
{
    public interface IPointsMesh3D : 
        IGeometricElement,
        IPeriodicSequence2D<IFloat64Tuple3D>
    {
        PointsMeshSlicePointsPath3D GetSlicePathAt(int dimension, int index);
    }
}