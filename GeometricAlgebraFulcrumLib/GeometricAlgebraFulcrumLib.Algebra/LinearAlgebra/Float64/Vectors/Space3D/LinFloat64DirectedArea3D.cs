using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

public sealed record LinFloat64DirectedArea3D(LinFloat64Vector3D Direction1, LinFloat64Vector3D Direction2) :
    IAlgebraicElement
{
    public LinFloat64Angle Angle
        => Direction1.GetAngle(Direction2);

    public LinFloat64Bivector3D Bivector
        => Direction1.VectorOp(Direction2);

    public LinFloat64Vector3D Normal
        => Direction1.VectorCross(Direction2);

    public LinFloat64Vector3D UnitNormal
        => Direction1.VectorUnitCross(Direction2);


    public LinFloat64DirectedArea3D(ILinFloat64Vector3D direction1)
        : this(direction1.ToLinVector3D(), direction1.GetNormal())
    {
    }

    public LinFloat64DirectedArea3D(ILinFloat64Vector3D direction1, ILinFloat64Vector3D direction2)
        : this(direction1.ToLinVector3D(), direction2.ToLinVector3D())
    {
    }


    public void Deconstruct(out LinFloat64Vector3D direction1, out LinFloat64Vector3D direction2)
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