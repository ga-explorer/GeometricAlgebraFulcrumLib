using GeometricAlgebraFulcrumLib.MathBase.BasicMath;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;

public sealed record Float64DirectedArea3D(Float64Vector3D Direction1, Float64Vector3D Direction2) :
    IGeometricElement
{
    public Float64PlanarAngle Angle
        => Direction1.GetAngle(Direction2);

    public Float64Bivector3D Bivector
        => Direction1.VectorOp(Direction2);

    public Float64Vector3D Normal
        => Direction1.VectorCross(Direction2);

    public Float64Vector3D UnitNormal
        => Direction1.VectorUnitCross(Direction2);


    public Float64DirectedArea3D(IFloat64Vector3D direction1)
        : this(direction1.ToVector3D(), direction1.GetNormal())
    {
    }

    public Float64DirectedArea3D(IFloat64Vector3D direction1, IFloat64Vector3D direction2)
        : this(direction1.ToVector3D(), direction2.ToVector3D())
    {
    }


    public void Deconstruct(out Float64Vector3D direction1, out Float64Vector3D direction2)
    {
        direction1 = Direction1;
        direction2 = Direction2;
    }


    public bool IsValid()
    {
        return Direction1.IsValid() &&
               Direction2.IsValid();
    }
}