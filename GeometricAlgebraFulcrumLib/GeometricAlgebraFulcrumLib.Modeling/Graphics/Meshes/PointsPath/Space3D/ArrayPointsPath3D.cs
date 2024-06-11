using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Sequences.Periodic1D;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PointsPath.Space3D;

/// <summary>
/// A path where points are directly stored in memory as an array
/// </summary>
public sealed class ArrayPointsPath3D : 
    PSeqArray1D<ILinFloat64Vector3D>, 
    IPointsPath3D
{
    public ArrayPointsPath3D(int pointsCount)
        : base(pointsCount.Repeat(LinFloat64Vector3D.Zero))
    {
    }

    public ArrayPointsPath3D(params ILinFloat64Vector3D[] pointsArray)
        : base(pointsArray)
    {
    }

    public ArrayPointsPath3D(IEnumerable<ILinFloat64Vector3D> pointsList)
        : base(pointsList)
    {
    }


    public bool IsValid()
    {
        return this.All(p => p.IsValid());
    }

    public IPointsPath3D MapPoints(Func<ILinFloat64Vector3D, ILinFloat64Vector3D> pointMapping)
    {
        return new ArrayPointsPath3D(
            DataArray.Select(pointMapping).ToArray()
        );
    }
}