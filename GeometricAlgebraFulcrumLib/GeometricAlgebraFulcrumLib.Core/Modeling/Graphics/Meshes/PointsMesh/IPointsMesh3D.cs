using GeometricAlgebraFulcrumLib.Core.Algebra;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Sequences.Periodic2D;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Meshes.PointsPath.Space3D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Meshes.PointsMesh;

public interface IPointsMesh3D : 
    IAlgebraicElement,
    IPeriodicSequence2D<ILinFloat64Vector3D>
{
    PointsMeshSlicePointsPath3D GetSlicePathAt(int dimension, int index);
}