using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Blades;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Encoding;

public static class XGaConformalEncodeHGaFlatUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeHGaPoint<T>(this XGaConformalSpace<T> conformalSpace, LinVector2D<T> egaPoint)
    {
        return conformalSpace.EncodeHGaPoint(
            conformalSpace.EncodeEGaVector(egaPoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeHGaPoint<T>(this XGaConformalSpace<T> conformalSpace, LinVector3D<T> egaPoint)
    {
        return conformalSpace.EncodeHGaPoint(
            conformalSpace.EncodeEGaVector(egaPoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeHGaPoint<T>(this XGaConformalSpace<T> conformalSpace, LinVector<T> egaPoint)
    {
        return conformalSpace.EncodeHGaPoint(
            conformalSpace.EncodeEGaVector(egaPoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeHGaPoint<T>(this XGaConformalSpace<T> conformalSpace, XGaVector<T> egaPoint)
    {
        return conformalSpace.EncodeHGaPoint(
            conformalSpace.EncodeEGaVector(egaPoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeHGaPoint<T>(this XGaConformalSpace<T> conformalSpace, XGaConformalBlade<T> egaPoint)
    {
        Debug.Assert(
            egaPoint.IsEGaVector()
        );

        return conformalSpace.Eo + egaPoint;
    }
}