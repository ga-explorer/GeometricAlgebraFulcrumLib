using GeometricAlgebraFulcrumLib.Algebra;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PointsPath.Space2D;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Sequences.Periodic2D;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PointsMesh;

public interface IPointsMesh2D : 
    IAlgebraicElement, 
    IPeriodicSequence2D<ILinFloat64Vector2D>
{
    PointsMeshSlicePointsPath2D GetSlicePathAt(int dimension, int index);
}