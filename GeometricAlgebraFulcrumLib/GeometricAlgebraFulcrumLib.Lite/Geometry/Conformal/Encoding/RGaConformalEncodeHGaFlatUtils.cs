using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.SpaceND;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Encoding;

public static class RGaConformalEncodeHGaFlatUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeHGaPoint(this RGaConformalSpace conformalSpace, Float64Vector2D egaPoint)
    {
        return conformalSpace.EncodeHGaPoint(
            conformalSpace.EncodeEGaVector(egaPoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeHGaPoint(this RGaConformalSpace conformalSpace, Float64Vector3D egaPoint)
    {
        return conformalSpace.EncodeHGaPoint(
            conformalSpace.EncodeEGaVector(egaPoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeHGaPoint(this RGaConformalSpace conformalSpace, Float64Vector egaPoint)
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