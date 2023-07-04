using DataStructuresLib.Sequences.Periodic2D;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath;
using GeometricAlgebraFulcrumLib.MathBase.Graphics.Meshes.PointsPath.Space2D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.MathBase.Graphics.Meshes.PointsMesh
{
    public interface IPointsMesh2D : 
        IGeometricElement, 
        IPeriodicSequence2D<IFloat64Vector2D>
    {
        PointsMeshSlicePointsPath2D GetSlicePathAt(int dimension, int index);
    }
}