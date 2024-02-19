using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Versors;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Encoding;

public static class RGaConformalEncodeVersorUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalVersor EncodeEGaRotation(this RGaConformalSpace conformalSpace, Float64PlanarAngle angle, double bivectorXy = 1d)
    {
        Debug.Assert(conformalSpace.Is4D);

        var scalar = Math.Cos(angle / 2);
        var bivectorNorm = bivectorXy.Abs();
        var bivectorScalar = Math.Sin(angle / 2) / bivectorNorm;

        return conformalSpace.ConformalProcessor
            .CreateComposer()
            .SetScalarTerm(scalar)
            .SetBivectorTerm(2, 3, bivectorScalar * bivectorXy)
            .GetSimpleMultivector()
            .ToConformalCGaVersor(conformalSpace);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalVersor EncodeEGaRotation(this RGaConformalSpace conformalSpace, Float64PlanarAngle angle, Float64Bivector2D bivector)
    {
        Debug.Assert(conformalSpace.Is4D);

        var scalar = Math.Cos(angle / 2);
        var bivectorScalar = Math.Sin(angle / 2) / bivector.Norm();

        return conformalSpace.ConformalProcessor
            .CreateComposer()
            .SetScalarTerm(scalar)
            .SetBivectorTerm(2, 3, bivectorScalar * bivector.Xy)
            .GetSimpleMultivector()
            .ToConformalCGaVersor(conformalSpace);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalVersor EncodeEGaRotation(this RGaConformalSpace conformalSpace, Float64PlanarAngle angle, double bivectorXy, double bivectorXz, double bivectorYz)
    {
        var scalar = Math.Cos(angle / 2);
        var bivectorNorm = (bivectorXy * bivectorXy + bivectorXz * bivectorXz + bivectorYz * bivectorYz).Sqrt();
        var bivectorScalar = Math.Sin(angle / 2) / bivectorNorm;

        return conformalSpace.ConformalProcessor
            .CreateComposer()
            .SetScalarTerm(scalar)
            .SetBivectorTerm(2, 3, bivectorScalar * bivectorXy)
            .SetBivectorTerm(2, 4, bivectorScalar * bivectorXz)
            .SetBivectorTerm(3, 4, bivectorScalar * bivectorYz)
            .GetSimpleMultivector()
            .ToConformalCGaVersor(conformalSpace);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalVersor EncodeEGaRotation(this RGaConformalSpace conformalSpace, Float64PlanarAngle angle, Float64Bivector3D bivector)
    {
        Debug.Assert(conformalSpace.Is5D);

        var scalar = Math.Cos(angle / 2);
        var bivectorScalar = Math.Sin(angle / 2) / bivector.Norm();

        return conformalSpace.ConformalProcessor
            .CreateComposer()
            .SetScalarTerm(scalar)
            .SetBivectorTerm(2, 3, bivectorScalar * bivector.Xy)
            .SetBivectorTerm(2, 4, bivectorScalar * bivector.Xz)
            .SetBivectorTerm(3, 4, bivectorScalar * bivector.Yz)
            .GetSimpleMultivector()
            .ToConformalCGaVersor(conformalSpace);
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalVersor EncodeCGaRotation(this RGaConformalSpace conformalSpace, Float64PlanarAngle angle, Float64Vector2D egaAxisPoint)
    {
        Debug.Assert(conformalSpace.Is4D);

        var bivector = 
            conformalSpace.EncodeOpnsFlatPoint(
                egaAxisPoint
            ).InternalBivector;

        return conformalSpace.EncodeCGaRotation(angle, bivector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalVersor EncodeCGaRotation(this RGaConformalSpace conformalSpace, Float64PlanarAngle angle, Float64Vector3D egaAxisPoint, Float64Vector3D egaAxisVector)
    {
        Debug.Assert(conformalSpace.Is5D);

        var bivector = 
            conformalSpace.EncodeIpnsFlatLine(
                egaAxisPoint,
                egaAxisVector
            ).InternalBivector;

        return conformalSpace.EncodeCGaRotation(angle, bivector);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalVersor EncodeCGaRotation(this RGaConformalSpace conformalSpace, Float64PlanarAngle angle, RGaFloat64Bivector bivector)
    {
        var halfAngle = angle.GetHalfAngleInPositiveRange();

        return (halfAngle.Cos().Value + halfAngle.Sin().Value / bivector.Norm().Scalar() * bivector).ToConformalCGaVersor(conformalSpace);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalVersor EncodePGaTranslation(this RGaConformalSpace conformalSpace, RGaFloat64Vector egaVector)
    {
        Debug.Assert(conformalSpace.IsValidEGaElement(egaVector));

        return new RGaConformalVersor(
            conformalSpace,
            1 - 0.5d * conformalSpace.EoVector.Op(egaVector)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalVersor EncodeCGaTranslation(this RGaConformalSpace conformalSpace, double vectorX, double vectorY)
    {
        Debug.Assert(conformalSpace.Is4D);

        var vector = Float64Vector2D.Create(vectorX, vectorY);

        return conformalSpace.EncodeCGaTranslation(
            conformalSpace.EncodeEGaVectorAsVector(vector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalVersor EncodeCGaTranslation(this RGaConformalSpace conformalSpace, Float64Vector2D vector)
    {
        Debug.Assert(conformalSpace.Is4D);

        return conformalSpace.EncodeCGaTranslation(
            conformalSpace.EncodeEGaVectorAsVector(vector)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalVersor EncodeCGaTranslation(this RGaConformalSpace conformalSpace, double vectorX, double vectorY, double vectorZ)
    {
        Debug.Assert(conformalSpace.Is5D);

        var vector = Float64Vector3D.Create(vectorX, vectorY, vectorZ);

        return conformalSpace.EncodeCGaTranslation(
            conformalSpace.EncodeEGaVectorAsVector(vector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalVersor EncodeCGaTranslation(this RGaConformalSpace conformalSpace, Float64Vector3D vector)
    {
        Debug.Assert(conformalSpace.Is5D);

        return conformalSpace.EncodeCGaTranslation(
            conformalSpace.EncodeEGaVectorAsVector(vector)
            //vector.ToRGaFloat64Vector()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalVersor EncodeCGaTranslation(this RGaConformalSpace conformalSpace, RGaFloat64Vector egaVector)
    {
        Debug.Assert(conformalSpace.IsValidEGaElement(egaVector));

        return (1 + 0.5d * conformalSpace.EiVector.Op(egaVector))
            .ToConformalCGaVersor(conformalSpace);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalVersor EncodeCGaScaling(this RGaConformalSpace conformalSpace, double scalingFactor)
    {
        var g = 0.5 * scalingFactor.LogE();

        return (Math.Cosh(g) + Math.Sinh(g) * conformalSpace.EoiBivector).ToConformalCGaVersor(conformalSpace);
    }

}