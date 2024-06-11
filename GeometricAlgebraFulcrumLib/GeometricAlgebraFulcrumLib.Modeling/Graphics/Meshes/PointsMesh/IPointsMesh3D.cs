using GeometricAlgebraFulcrumLib.Algebra;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PointsPath.Space3D;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Sequences.Periodic2D;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PointsMesh;

public interface IPointsMesh3D : 
    IAlgebraicElement,
    IPeriodicSequence2D<ILinFloat64Vector3D>
{
    PointsMeshSlicePointsPath3D GetSlicePathAt(int dimension, int index);
}