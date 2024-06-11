using GeometricAlgebraFulcrumLib.Algebra;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Sequences.Periodic1D;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PointsPath;

/// <summary>
/// This interface represents a poly-line in 3D.
/// https://en.wikipedia.org/wiki/Polygonal_chain
/// </summary>
public interface IPointsPath3D : 
    IAlgebraicElement, 
    IPeriodicSequence1D<ILinFloat64Vector3D>
{
    IPointsPath3D MapPoints(Func<ILinFloat64Vector3D, ILinFloat64Vector3D> pointMapping);
}