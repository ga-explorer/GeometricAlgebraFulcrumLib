using DataStructuresLib.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.MathBase.Graphics.Meshes.PointsPath
{
    /// <summary>
    /// This interface represents a poly-line in 2D.
    /// https://en.wikipedia.org/wiki/Polygonal_chain
    /// </summary>
    public interface IPointsPath2D : 
        IGeometricElement, 
        IPeriodicSequence1D<IFloat64Vector2D>
    {
        IPointsPath2D MapPoints(Func<IFloat64Vector2D, IFloat64Vector2D> pointMapping);
    }
}