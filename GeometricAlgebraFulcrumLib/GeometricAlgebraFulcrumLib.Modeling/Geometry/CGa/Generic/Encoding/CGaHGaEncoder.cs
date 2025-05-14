using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Blades;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Encoding;

public sealed class CGaHGaEncoder<T> :
    CGaEncoderBase<T>
{
    internal CGaHGaEncoder(CGaGeometricSpace<T> geometricSpace)
        : base(geometricSpace)
    {
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Point(LinVector2D<T> egaPoint)
    {
        return Point(
            GeometricSpace.EncodeVGa.Vector(egaPoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Point(LinVector3D<T> egaPoint)
    {
        return Point(
            GeometricSpace.EncodeVGa.Vector(egaPoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Point(LinVector<T> egaPoint)
    {
        return Point(
            GeometricSpace.EncodeVGa.Vector(egaPoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Point(XGaVector<T> egaPoint)
    {
        return Point(
            GeometricSpace.EncodeVGa.Vector(egaPoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Point(CGaBlade<T> egaPoint)
    {
        Debug.Assert(
            egaPoint.IsVGaVector()
        );

        return GeometricSpace.Eo + egaPoint;
    }
}