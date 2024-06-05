using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Angles;
using GeometricAlgebraFulcrumLib.Core.Algebra;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;

public sealed record LinDirectedArea3D<T>(LinVector3D<T> Direction1, LinVector3D<T> Direction2) :
    IAlgebraicElement
{
    public LinPolarAngle<T> Angle
        => Direction1.GetAngle(Direction2);

    public LinBivector3D<T> Bivector
        => Direction1.VectorOp(Direction2);

    public LinVector3D<T> Normal
        => Direction1.VectorCross(Direction2);

    public LinVector3D<T> UnitNormal
        => Direction1.VectorUnitCross(Direction2);


    public LinDirectedArea3D(ILinVector3D<T> direction1)
        : this(direction1.ToVector3D(), direction1.GetNormal())
    {
    }

    public LinDirectedArea3D(ILinVector3D<T> direction1, ILinVector3D<T> direction2)
        : this(direction1.ToVector3D(), direction2.ToVector3D())
    {
    }


    public void Deconstruct(out LinVector3D<T> direction1, out LinVector3D<T> direction2)
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