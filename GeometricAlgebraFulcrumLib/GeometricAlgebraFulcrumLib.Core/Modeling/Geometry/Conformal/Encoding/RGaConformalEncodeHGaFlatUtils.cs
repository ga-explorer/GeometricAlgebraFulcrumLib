using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Encoding;

public static class RGaConformalEncodeHGaFlatUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeHGaPoint(this RGaConformalSpace conformalSpace, LinFloat64Vector2D egaPoint)
    {
        return conformalSpace.EncodeHGaPoint(
            conformalSpace.EncodeEGaVector(egaPoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeHGaPoint(this RGaConformalSpace conformalSpace, LinFloat64Vector3D egaPoint)
    {
        return conformalSpace.EncodeHGaPoint(
            conformalSpace.EncodeEGaVector(egaPoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeHGaPoint(this RGaConformalSpace conformalSpace, LinFloat64Vector egaPoint)
    {
        return conformalSpace.EncodeHGaPoint(
            conformalSpace.EncodeEGaVector(egaPoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeHGaPoint(this RGaConformalSpace conformalSpace, RGaFloat64Vector egaPoint)
    {
        return conformalSpace.EncodeHGaPoint(
            conformalSpace.EncodeEGaVector(egaPoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeHGaPoint(this RGaConformalSpace conformalSpace, RGaConformalBlade egaPoint)
    {
        Debug.Assert(
            egaPoint.IsEGaVector()
        );

        return conformalSpace.Eo + egaPoint;
    }
}