using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Versors;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Encoding;

public static class CGaFloat64EncodeVersorUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Versor EncodeVGaRotation(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Angle angle, double bivectorXy = 1d)
    {
        Debug.Assert(cgaGeometricSpace.Is4D);

        var (halfAngleCos, halfAngleSin) =
            angle.HalfPolarAngle();

        var scalar = halfAngleCos;
        var bivectorNorm = bivectorXy.Abs();
        var bivectorScalar = halfAngleSin / bivectorNorm;

        return cgaGeometricSpace.ConformalProcessor
            .CreateComposer()
            .SetScalarTerm(scalar)
            .SetBivectorTerm(2, 3, bivectorScalar * bivectorXy)
            .GetSimpleMultivector()
            .ToConformalCGaVersor(cgaGeometricSpace);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Versor EncodeVGaRotation(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Angle angle, LinFloat64Bivector2D bivector)
    {
        Debug.Assert(cgaGeometricSpace.Is4D);

        var (halfAngleCos, halfAngleSin) =
            angle.HalfPolarAngle();

        var scalar = halfAngleCos;
        var bivectorScalar = halfAngleSin / bivector.Norm();

        return cgaGeometricSpace.ConformalProcessor
            .CreateComposer()
            .SetScalarTerm(scalar)
            .SetBivectorTerm(2, 3, bivectorScalar * bivector.Xy)
            .GetSimpleMultivector()
            .ToConformalCGaVersor(cgaGeometricSpace);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Versor EncodeVGaRotation(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Angle angle, double bivectorXy, double bivectorXz, double bivectorYz)
    {
        var (halfAngleCos, halfAngleSin) =
            angle.HalfPolarAngle();

        var scalar = halfAngleCos;
        var bivectorNorm = (bivectorXy * bivectorXy + bivectorXz * bivectorXz + bivectorYz * bivectorYz).Sqrt();
        var bivectorScalar = halfAngleSin / bivectorNorm;

        return cgaGeometricSpace.ConformalProcessor
            .CreateComposer()
            .SetScalarTerm(scalar)
            .SetBivectorTerm(2, 3, bivectorScalar * bivectorXy)
            .SetBivectorTerm(2, 4, bivectorScalar * bivectorXz)
            .SetBivectorTerm(3, 4, bivectorScalar * bivectorYz)
            .GetSimpleMultivector()
            .ToConformalCGaVersor(cgaGeometricSpace);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Versor EncodeVGaRotation(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Angle angle, LinFloat64Bivector3D bivector)
    {
        Debug.Assert(cgaGeometricSpace.Is5D);

        var (halfAngleCos, halfAngleSin) =
            angle.HalfPolarAngle();

        var scalar = halfAngleCos;
        var bivectorScalar = halfAngleSin / bivector.Norm();

        return cgaGeometricSpace.ConformalProcessor
            .CreateComposer()
            .SetScalarTerm(scalar)
            .SetBivectorTerm(2, 3, bivectorScalar * bivector.Xy)
            .SetBivectorTerm(2, 4, bivectorScalar * bivector.Xz)
            .SetBivectorTerm(3, 4, bivectorScalar * bivector.Yz)
            .GetSimpleMultivector()
            .ToConformalCGaVersor(cgaGeometricSpace);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Versor EncodePGaRotation(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Angle angle, LinFloat64Vector2D egaAxisPoint)
    {
        Debug.Assert(cgaGeometricSpace.Is4D);

        var (halfAngleCos, halfAngleSin) = angle.HalfPolarAngle();

        var pgaAxis =
            cgaGeometricSpace.EncodePGaPoint(egaAxisPoint);

        return new CGaFloat64Versor(
            cgaGeometricSpace,
            halfAngleCos + halfAngleSin * pgaAxis.InternalKVector
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Versor EncodePGaRotation(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Angle angle, LinFloat64Vector3D egaAxisPoint, LinFloat64Vector3D egaAxisDirection)
    {
        Debug.Assert(cgaGeometricSpace.Is5D);

        var (halfAngleCos, halfAngleSin) = angle.HalfPolarAngle();
        var pgaAxis = cgaGeometricSpace.EncodePGaLine(egaAxisPoint, egaAxisDirection);

        return new CGaFloat64Versor(
            cgaGeometricSpace,
            halfAngleCos + halfAngleSin * pgaAxis.InternalKVector
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Versor EncodeCGaRotation(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Angle angle, LinFloat64Vector2D egaAxisPoint)
    {
        Debug.Assert(cgaGeometricSpace.Is4D);

        var bivector =
            cgaGeometricSpace.EncodeOpnsFlatPoint(
                egaAxisPoint
            ).InternalBivector;

        return cgaGeometricSpace.EncodeCGaRotation(angle, bivector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Versor EncodeCGaRotation(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Angle angle, LinFloat64Vector3D egaAxisPoint, LinFloat64Vector3D egaAxisVector)
    {
        Debug.Assert(cgaGeometricSpace.Is5D);

        var bivector =
            cgaGeometricSpace.EncodeIpnsFlatLine(
                egaAxisPoint,
                egaAxisVector
            ).InternalBivector;

        return cgaGeometricSpace.EncodeCGaRotation(angle, bivector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Versor EncodeCGaRotation(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Angle angle, RGaFloat64Bivector bivector)
    {
        var (halfAngleCos, halfAngleSin) = angle.HalfPolarAngle();

        return (halfAngleCos + halfAngleSin / bivector.Norm() * bivector).ToConformalCGaVersor(cgaGeometricSpace);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Versor EncodePGaTranslation(this CGaFloat64GeometricSpace cgaGeometricSpace, RGaFloat64Vector egaVector)
    {
        Debug.Assert(cgaGeometricSpace.IsValidVGaElement(egaVector));

        return new CGaFloat64Versor(
            cgaGeometricSpace,
            1 - 0.5d * cgaGeometricSpace.EoVector.Op(egaVector)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Versor EncodeCGaTranslation(this CGaFloat64GeometricSpace cgaGeometricSpace, double vectorX, double vectorY)
    {
        Debug.Assert(cgaGeometricSpace.Is4D);

        var vector = LinFloat64Vector2D.Create(vectorX, vectorY);

        return cgaGeometricSpace.EncodeCGaTranslation(
            cgaGeometricSpace.EncodeVGaVectorAsRGaVector(vector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Versor EncodeCGaTranslation(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector2D vector)
    {
        Debug.Assert(cgaGeometricSpace.Is4D);

        return cgaGeometricSpace.EncodeCGaTranslation(
            cgaGeometricSpace.EncodeVGaVectorAsRGaVector(vector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Versor EncodeCGaTranslation(this CGaFloat64GeometricSpace cgaGeometricSpace, double vectorX, double vectorY, double vectorZ)
    {
        Debug.Assert(cgaGeometricSpace.Is5D);

        var vector = LinFloat64Vector3D.Create(vectorX, vectorY, vectorZ);

        return cgaGeometricSpace.EncodeCGaTranslation(
            cgaGeometricSpace.EncodeVGaVectorAsRGaVector(vector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Versor EncodeCGaTranslation(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector3D vector)
    {
        Debug.Assert(cgaGeometricSpace.Is5D);

        return cgaGeometricSpace.EncodeCGaTranslation(
            cgaGeometricSpace.EncodeVGaVectorAsRGaVector(vector)
        //vector.ToRGaFloat64Vector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Versor EncodeCGaTranslation(this CGaFloat64GeometricSpace cgaGeometricSpace, RGaFloat64Vector egaVector)
    {
        Debug.Assert(cgaGeometricSpace.IsValidVGaElement(egaVector));

        return (1 + 0.5d * cgaGeometricSpace.EiVector.Op(egaVector))
            .ToConformalCGaVersor(cgaGeometricSpace);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Versor EncodeCGaScaling(this CGaFloat64GeometricSpace cgaGeometricSpace, double scalingFactor)
    {
        var g = 0.5 * scalingFactor.LogE();

        return (Math.Cosh(g) + Math.Sinh(g) * cgaGeometricSpace.EoiBivector).ToConformalCGaVersor(cgaGeometricSpace);
    }

}