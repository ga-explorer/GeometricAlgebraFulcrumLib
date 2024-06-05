using GeometricAlgebraFulcrumLib.Core.Algebra;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Sequences.Periodic2D;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Meshes.PointsPath.Space2D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Meshes.PointsMesh;

public interface IPointsMesh2D : 
    IAlgebraicElement, 
    IPeriodicSequence2D<ILinFloat64Vector2D>
{
    PointsMeshSlicePointsPath2D GetSlicePathAt(int dimension, int index);
}