using DataStructuresLib.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Meshes.PointsPath.Space3D;

public class ConstantPointsPath3D
    : PSeqConstant1D<IFloat64Vector3D>, IPointsPath3D
{
    public ConstantPointsPath3D(int count)
        : base(count, Float64Vector3D.Create(0, 0, 0))
    {
    }

    public ConstantPointsPath3D(int count, double x, double y, double z)
        : base(count, Float64Vector3D.Create(x, y, z))
    {
    }

    public ConstantPointsPath3D(int count, IFloat64Vector3D value)
        : base(count, value)
    {
    }


    public bool IsValid()
    {
        return Count >= 2 && Value.IsValid();
    }

    public IPointsPath3D MapPoints(Func<IFloat64Vector3D, IFloat64Vector3D> pointMapping)
    {
        return new ConstantPointsPath3D(Count, pointMapping(Value));
    }
}