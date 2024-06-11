using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Blades;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Encoding;

public static class CGaEncodeHGaFlatUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeHGaPoint<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector2D<T> egaPoint)
    {
        return cgaGeometricSpace.EncodeHGaPoint(
            cgaGeometricSpace.EncodeVGaVector(egaPoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeHGaPoint<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector3D<T> egaPoint)
    {
        return cgaGeometricSpace.EncodeHGaPoint(
            cgaGeometricSpace.EncodeVGaVector(egaPoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeHGaPoint<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector<T> egaPoint)
    {
        return cgaGeometricSpace.EncodeHGaPoint(
            cgaGeometricSpace.EncodeVGaVector(egaPoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeHGaPoint<T>(this CGaGeometricSpace<T> cgaGeometricSpace, XGaVector<T> egaPoint)
    {
        return cgaGeometricSpace.EncodeHGaPoint(
            cgaGeometricSpace.EncodeVGaVector(egaPoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeHGaPoint<T>(this CGaGeometricSpace<T> cgaGeometricSpace, CGaBlade<T> egaPoint)
    {
        Debug.Assert(
            egaPoint.IsVGaVector()
        );

        return cgaGeometricSpace.Eo + egaPoint;
    }
}