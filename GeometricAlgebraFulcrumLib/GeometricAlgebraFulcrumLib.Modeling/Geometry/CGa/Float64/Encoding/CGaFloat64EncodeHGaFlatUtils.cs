using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Blades;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Encoding;

public static class CGaFloat64EncodeHGaFlatUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeHGaPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector2D egaPoint)
    {
        return cgaGeometricSpace.EncodeHGaPoint(
            cgaGeometricSpace.EncodeVGaVector(egaPoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeHGaPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector3D egaPoint)
    {
        return cgaGeometricSpace.EncodeHGaPoint(
            cgaGeometricSpace.EncodeVGaVector(egaPoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeHGaPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector egaPoint)
    {
        return cgaGeometricSpace.EncodeHGaPoint(
            cgaGeometricSpace.EncodeVGaVector(egaPoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeHGaPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, RGaFloat64Vector egaPoint)
    {
        return cgaGeometricSpace.EncodeHGaPoint(
            cgaGeometricSpace.EncodeVGaVector(egaPoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeHGaPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, CGaFloat64Blade egaPoint)
    {
        Debug.Assert(
            egaPoint.IsVGaVector()
        );

        return cgaGeometricSpace.Eo + egaPoint;
    }
}