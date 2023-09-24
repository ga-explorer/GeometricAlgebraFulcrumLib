using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.SpaceND;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Encoding;

public static class RGaConformalEncodeIpnsRoundUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeIpnsRoundCircle(this RGaConformalSpace conformalSpace, double radiusSquared, double centerX, double centerY)
    {
        Debug.Assert(conformalSpace.Is4D);

        return conformalSpace.EncodeIpnsRoundHyperSphere(
            radiusSquared,
            Float64Vector2D.Create(centerX, centerY).ToRGaFloat64Vector()
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeIpnsRoundCircle(this RGaConformalSpace conformalSpace, double radiusSquared, Float64Vector2D center)
    {
        Debug.Assert(conformalSpace.Is4D);

        return conformalSpace.EncodeIpnsRoundHyperSphere(
            radiusSquared, 
            center.ToRGaFloat64Vector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeIpnsRealRoundCircle(this RGaConformalSpace conformalSpace, double radius, double centerX, double centerY)
    {
        Debug.Assert(conformalSpace.Is4D);

        return conformalSpace.EncodeIpnsRealRoundHyperSphere(
            radius, 
            Float64Vector2D.Create(centerX, centerY).ToRGaFloat64Vector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeIpnsRealRoundCircle(this RGaConformalSpace conformalSpace, double radius, Float64Vector2D center)
    {
        Debug.Assert(conformalSpace.Is4D);

        return conformalSpace.EncodeIpnsRealRoundHyperSphere(
            radius, 
            center.ToRGaFloat64Vector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeIpnsImaginaryRoundCircle(this RGaConformalSpace conformalSpace, double radius, double centerX, double centerY)
    {
        Debug.Assert(conformalSpace.Is4D);

        return conformalSpace.EncodeIpnsImaginaryRoundHyperSphere(
            radius, 
            Float64Vector2D.Create(centerX, centerY).ToRGaFloat64Vector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeIpnsImaginaryRoundCircle(this RGaConformalSpace conformalSpace, double radius, Float64Vector2D center)
    {
        Debug.Assert(conformalSpace.Is4D);

        return conformalSpace.EncodeIpnsImaginaryRoundHyperSphere(
            radius, 
            center.ToRGaFloat64Vector()
        );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeIpnsRoundCircle(this RGaConformalSpace conformalSpace, double radiusSquared, Float64Vector2D egaCenter, Float64Bivector2D egaBivector)
    {
        return conformalSpace.EncodeIpnsRoundCircle(
            radiusSquared, 
            egaCenter.ToRGaFloat64Vector(),
            egaBivector.ToRGaFloat64Bivector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeIpnsRoundCircle(this RGaConformalSpace conformalSpace, double radiusSquared, Float64Vector3D egaCenter, Float64Bivector3D egaBivector)
    {
        return conformalSpace.EncodeIpnsRoundCircle(
            radiusSquared, 
            egaCenter.ToRGaFloat64Vector(),
            egaBivector.ToRGaFloat64Bivector()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeIpnsRealRoundCircle(this RGaConformalSpace conformalSpace, double radius, Float64Vector3D egaCenter, Float64Bivector3D egaBivector)
    {
        return conformalSpace.EncodeIpnsRealRoundCircle(
            radius, 
            egaCenter.ToRGaFloat64Vector(),
            egaBivector.ToRGaFloat64Bivector()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeIpnsImaginaryRoundCircle(this RGaConformalSpace conformalSpace, double radius, Float64Vector3D egaCenter, Float64Bivector3D egaBivector)
    {
        return conformalSpace.EncodeIpnsImaginaryRoundCircle(
            radius, 
            egaCenter.ToRGaFloat64Vector(),
            egaBivector.ToRGaFloat64Bivector()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeIpnsRoundCircle(this RGaConformalSpace conformalSpace, double radiusSquared, Float64Vector3D egaCenter, Float64Vector3D egaNormalVector)
    {
        return conformalSpace.EncodeIpnsRoundCircle(
            radiusSquared, 
            egaCenter.ToRGaFloat64Vector(),
            egaNormalVector.UnDual3D().ToRGaFloat64Bivector()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeIpnsRealRoundCircle(this RGaConformalSpace conformalSpace, double radius, Float64Vector3D egaCenter, Float64Vector3D egaNormalVector)
    {
        return conformalSpace.EncodeIpnsRealRoundCircle(
            radius, 
            egaCenter.ToRGaFloat64Vector(),
            egaNormalVector.UnDual3D().ToRGaFloat64Bivector()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeIpnsImaginaryRoundCircle(this RGaConformalSpace conformalSpace, double radius, Float64Vector3D egaCenter, Float64Vector3D egaNormalVector)
    {
        return conformalSpace.EncodeIpnsImaginaryRoundCircle(
            radius, 
            egaCenter.ToRGaFloat64Vector(),
            egaNormalVector.UnDual3D().ToRGaFloat64Bivector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeIpnsRoundSphere(this RGaConformalSpace conformalSpace, double radiusSquared, double centerX, double centerY, double centerZ)
    {
        return conformalSpace.EncodeIpnsRoundHyperSphere(
            radiusSquared, 
            Float64Vector3D.Create(centerX, centerY, centerZ).ToRGaFloat64Vector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeIpnsRoundSphere(this RGaConformalSpace conformalSpace, double radiusSquared, Float64Vector3D center)
    {
        Debug.Assert(conformalSpace.Is5D);

        return conformalSpace.EncodeIpnsRoundHyperSphere(
            radiusSquared, 
            center.ToRGaFloat64Vector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeIpnsRealRoundSphere(this RGaConformalSpace conformalSpace, double radius, double centerX, double centerY, double centerZ)
    {
        Debug.Assert(conformalSpace.Is5D);

        return conformalSpace.EncodeIpnsRealRoundHyperSphere(
            radius, 
            Float64Vector3D.Create(centerX, centerY, centerZ).ToRGaFloat64Vector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeIpnsRealRoundSphere(this RGaConformalSpace conformalSpace, double radius, Float64Vector3D center)
    {
        Debug.Assert(conformalSpace.Is5D);

        return conformalSpace.EncodeIpnsRealRoundHyperSphere(
            radius, 
            center.ToRGaFloat64Vector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeIpnsImaginaryRoundSphere(this RGaConformalSpace conformalSpace, double radius, double centerX, double centerY, double centerZ)
    {
        Debug.Assert(conformalSpace.Is5D);

        return conformalSpace.EncodeIpnsImaginaryRoundHyperSphere(
            radius, 
            Float64Vector3D.Create(centerX, centerY, centerZ).ToRGaFloat64Vector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeIpnsImaginaryRoundSphere(this RGaConformalSpace conformalSpace, double radius, Float64Vector3D center)
    {
        Debug.Assert(conformalSpace.Is5D);

        return conformalSpace.EncodeIpnsImaginaryRoundHyperSphere(
            radius, 
            center.ToRGaFloat64Vector()
        );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeIpnsRoundPoint(this RGaConformalSpace conformalSpace, double pointX, double pointY)
    {
        var p = 
            conformalSpace.EncodeEGaVectorAsVector(pointX, pointY);

        var pNormSquared = 
            pointX * pointX + 
            pointY * pointY;

        var kVector = 
            conformalSpace.EoVector +
            p +
            0.5d * pNormSquared * conformalSpace.EiVector;

        return new RGaConformalBlade(conformalSpace, kVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeIpnsRoundPoint(this RGaConformalSpace conformalSpace, Float64Vector2D egaPoint)
    {
        var p = 
            conformalSpace.EncodeEGaVectorAsVector(egaPoint);

        var kVector = 
            conformalSpace.EoVector +
            p +
            0.5d * egaPoint.NormSquared() * conformalSpace.EiVector;

        return new RGaConformalBlade(conformalSpace, kVector);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeIpnsRoundPoint(this RGaConformalSpace conformalSpace, double pointX, double pointY, double pointZ)
    {
        var p = 
            conformalSpace.EncodeEGaVectorAsVector(pointX, pointY, pointZ);

        var pNormSquared = 
            pointX * pointX + 
            pointY * pointY + 
            pointZ * pointZ;

        var kVector = 
            conformalSpace.EoVector +
            p +
            0.5d * pNormSquared * conformalSpace.EiVector;

        return new RGaConformalBlade(conformalSpace, kVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeIpnsRoundPoint(this RGaConformalSpace conformalSpace, Float64Vector3D egaPoint)
    {
        var p = 
            conformalSpace.EncodeEGaVectorAsVector(egaPoint);

        var kVector = 
            conformalSpace.EoVector +
            p +
            0.5d * egaPoint.NormSquared().Value * conformalSpace.EiVector;

        return new RGaConformalBlade(conformalSpace, kVector);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeIpnsRoundPoint(this RGaConformalSpace conformalSpace, Float64Vector egaPoint)
    {
        var p = 
            conformalSpace.EncodeEGaVectorAsVector(egaPoint);

        var kVector = 
            conformalSpace.EoVector +
            p +
            0.5d * egaPoint.ENormSquared() * conformalSpace.EiVector;

        return new RGaConformalBlade(conformalSpace, kVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeIpnsRoundPoint(this RGaConformalSpace conformalSpace, RGaFloat64Vector egaPoint)
    {
        var p = 
            conformalSpace.EncodeEGaVectorAsVector(egaPoint);

        var kVector = 
            conformalSpace.EoVector +
            p +
            0.5d * egaPoint.NormSquared() * conformalSpace.EiVector;

        return new RGaConformalBlade(conformalSpace, kVector);
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeIpnsRoundPointPair(this RGaConformalSpace conformalSpace, double radiusSquared, RGaFloat64Vector egaCenter, RGaFloat64Vector egaDirection)
    {
        var direction = 
            egaDirection
                .EncodeEGaBlade(conformalSpace)
                .EGaDual()
                .GradeInvolution();

        return (conformalSpace.Eo - 0.5 * radiusSquared * conformalSpace.Ei)
            .Op(direction)
            .TranslateBy(egaCenter);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeIpnsRealRoundPointPair(this RGaConformalSpace conformalSpace, double radius, RGaFloat64Vector egaCenter, RGaFloat64Vector egaDirection)
    {
        return conformalSpace.EncodeIpnsRoundPointPair(
            radius * radius, 
            egaCenter, 
            egaDirection
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeIpnsImaginaryRoundPointPair(this RGaConformalSpace conformalSpace, double radius, RGaFloat64Vector egaCenter, RGaFloat64Vector egaDirection)
    {
        return conformalSpace.EncodeIpnsRoundPointPair(
            -radius * radius, 
            egaCenter, 
            egaDirection
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeIpnsRoundCircle(this RGaConformalSpace conformalSpace, double radiusSquared, RGaFloat64Vector egaCenter, RGaFloat64Bivector egaDirection)
    {
        var direction = 
            egaDirection
                .EncodeEGaBlade(conformalSpace)
                .EGaDual()
                .GradeInvolution();

        return (conformalSpace.Eo - 0.5 * radiusSquared * conformalSpace.Ei)
            .Op(direction)
            .TranslateBy(egaCenter);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeIpnsRealRoundCircle(this RGaConformalSpace conformalSpace, double radius, RGaFloat64Vector egaCenter, RGaFloat64Bivector egaDirection)
    {
        return conformalSpace.EncodeIpnsRoundCircle(
            radius * radius, 
            egaCenter, 
            egaDirection
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeIpnsImaginaryRoundCircle(this RGaConformalSpace conformalSpace, double radius, RGaFloat64Vector egaCenter, RGaFloat64Bivector egaDirection)
    {
        return conformalSpace.EncodeIpnsRoundCircle(
            -radius * radius, 
            egaCenter, 
            egaDirection
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeIpnsRealRoundUnitHyperSphere(this RGaConformalSpace conformalSpace)
    {
        return conformalSpace.Eo - 0.5d * conformalSpace.Ei;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeIpnsImaginaryRoundUnitHyperSphere(this RGaConformalSpace conformalSpace)
    {
        return conformalSpace.Eo + 0.5d * conformalSpace.Ei;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeIpnsRoundHyperSphere(this RGaConformalSpace conformalSpace, double radiusSquared)
    {
        return conformalSpace.Eo - 0.5d * radiusSquared * conformalSpace.Ei;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeIpnsRoundHyperSphere(this RGaConformalSpace conformalSpace, double radiusSquared, RGaFloat64Vector egaCenter)
    {
        var c = conformalSpace.EncodeIpnsRoundPoint(egaCenter);

        return c - 0.5d * radiusSquared * conformalSpace.Ei;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeIpnsRealRoundHyperSphere(this RGaConformalSpace conformalSpace, double radius)
    {
        return conformalSpace.Eo - 0.5d * radius * radius * conformalSpace.Ei;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeIpnsRealRoundHyperSphere(this RGaConformalSpace conformalSpace, double radius, RGaFloat64Vector egaCenter)
    {
        var c = conformalSpace.EncodeIpnsRoundPoint(egaCenter);

        return c - 0.5d * radius * radius * conformalSpace.Ei;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeIpnsImaginaryRoundHyperSphere(this RGaConformalSpace conformalSpace, double radius)
    {
        return conformalSpace.Eo + 0.5d * radius * radius * conformalSpace.Ei;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeIpnsImaginaryRoundHyperSphere(this RGaConformalSpace conformalSpace, double radius, RGaFloat64Vector egaCenter)
    {
        var c = conformalSpace.EncodeIpnsRoundPoint(egaCenter);

        return c + 0.5d * radius * radius * conformalSpace.Ei;
    }

}