using GeometricAlgebraFulcrumLib.Algebra;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Sequences.Periodic1D;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PointsPath;

/// <summary>
/// This interface represents a poly-line in 2D.
/// https://en.wikipedia.org/wiki/Polygonal_chain
/// </summary>
public interface IPointsPath2D : 
    IAlgebraicElement, 
    IPeriodicSequence1D<ILinFloat64Vector2D>
{
    IPointsPath2D MapPoints(Func<ILinFloat64Vector2D, ILinFloat64Vector2D> pointMapping);
}