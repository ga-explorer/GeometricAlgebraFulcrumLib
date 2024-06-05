using GeometricAlgebraFulcrumLib.Utilities.Structures.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Meshes.PointsPath.Space3D;

public class ConstantPointsPath3D
    : PSeqConstant1D<ILinFloat64Vector3D>, IPointsPath3D
{
    public ConstantPointsPath3D(int count)
        : base(count, LinFloat64Vector3D.Create(0, 0, 0))
    {
    }

    public ConstantPointsPath3D(int count, double x, double y, double z)
        : base(count, LinFloat64Vector3D.Create(x, y, z))
    {
    }

    public ConstantPointsPath3D(int count, ILinFloat64Vector3D value)
        : base(count, value)
    {
    }


    public bool IsValid()
    {
        return Count >= 2 && Value.IsValid();
    }

    public IPointsPath3D MapPoints(Func<ILinFloat64Vector3D, ILinFloat64Vector3D> pointMapping)
    {
        return new ConstantPointsPath3D(Count, pointMapping(Value));
    }
}