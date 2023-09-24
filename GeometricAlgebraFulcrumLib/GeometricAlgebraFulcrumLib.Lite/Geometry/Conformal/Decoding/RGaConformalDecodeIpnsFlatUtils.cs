using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Elements;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Encoding;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.SpaceND;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Decoding;

public static class RGaConformalDecodeIpnsFlatUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalFlat DecodeIpnsLine2D(this RGaConformalBlade ipnsFlat)
    {
        Debug.Assert(ipnsFlat.ConformalSpace.Is4D);

        return ipnsFlat.DecodeIpnsHyperPlane(
            ipnsFlat.ConformalSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalFlat DecodeIpnsLine2D(this RGaConformalBlade ipnsFlat, Float64Vector2D egaProbePoint)
    {
        return ipnsFlat.DecodeIpnsHyperPlane(
            ipnsFlat.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalFlat DecodeIpnsPlane3D(this RGaConformalBlade ipnsFlat)
    {
        Debug.Assert(ipnsFlat.ConformalSpace.Is5D);

        return ipnsFlat.DecodeIpnsHyperPlane(
            ipnsFlat.ConformalSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalFlat DecodeIpnsPlane3D(this RGaConformalBlade ipnsFlat, Float64Vector3D egaProbePoint)
    {
        return ipnsFlat.DecodeIpnsHyperPlane(
            ipnsFlat.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalFlat DecodeIpnsHyperPlane(this RGaConformalBlade ipnsFlat)
    {
        return ipnsFlat.DecodeIpnsHyperPlane(
            ipnsFlat.ConformalSpace.ZeroVectorBlade
        );
    }

    public static RGaConformalFlat DecodeIpnsHyperPlane(this RGaConformalBlade ipnsFlat, RGaConformalBlade egaProbePoint)
    {
        var conformalSpace = ipnsFlat.ConformalSpace;

        var eoScalar = ipnsFlat[0] + ipnsFlat[1];

        // This is a point\hypersphere, not a hyperplane
        if (!eoScalar.IsNearZero())
            return new RGaConformalFlat(
                conformalSpace,
                0,
                conformalSpace.ZeroVectorBlade,
                conformalSpace.ZeroVectorBlade
            );

        var normal = ipnsFlat.GetEGaVectorPart();
        var weight = normal.Norm();

        // Hyperplane has zero normal
        if (weight.IsNearZero())
            throw new InvalidOperationException();

        var ipnsProbePoint =
            egaProbePoint.EGaVectorToIpnsPoint();

        var position =
            ipnsProbePoint
                .Op(ipnsFlat)
                .Gp(ipnsFlat.Inverse())
                .GetVectorPart((int i) => i >= 2)
                .ToConformalBlade(ipnsFlat.ConformalSpace);
                //.DecodeIpnsHyperSphereEGaCenter(conformalSpace);

        var flat = new RGaConformalFlat(
            conformalSpace,
            weight,
            position,
            normal.EGaUnNormal()
        );

        Debug.Assert(
            flat.IpnsFlatPosition.Lcp(ipnsFlat).IsNearZero()
        );

        return flat;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalFlat DecodeIpnsFlat(this RGaConformalBlade ipnsFlat, Float64Vector2D egaProbePoint)
    {
        return ipnsFlat.DecodeIpnsFlat(
            ipnsFlat.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalFlat DecodeIpnsFlat(this RGaConformalBlade ipnsFlat, Float64Vector3D egaProbePoint)
    {
        return ipnsFlat.DecodeIpnsFlat(
            ipnsFlat.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalFlat DecodeIpnsFlat(this RGaConformalBlade ipnsFlat, RGaFloat64Vector egaProbePoint)
    {
        return ipnsFlat.DecodeIpnsFlat(
            ipnsFlat.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalFlat DecodeIpnsFlat(this RGaConformalBlade ipnsFlat)
    {
        return ipnsFlat.DecodeIpnsFlat(
            ipnsFlat.ConformalSpace.ZeroVectorBlade
        );
    }

    public static RGaConformalFlat DecodeIpnsFlat(this RGaConformalBlade ipnsFlat, RGaConformalBlade egaProbePoint)
    {
        var conformalSpace = ipnsFlat.ConformalSpace;

        var ipnsProbePoint =
            egaProbePoint.EGaVectorToIpnsPoint();

        var position =
            ipnsProbePoint
                .Op(ipnsFlat)
                .Gp(ipnsFlat.Inverse())
                .GetVectorPart((int i) => i >= 2)
                .ToConformalBlade(ipnsFlat.ConformalSpace);
                //.DecodeIpnsHyperSphereEGaCenter(conformalSpace);

        var directionOpEi =
            conformalSpace.Ei.Lcp(ipnsFlat.CGaUnDual().Negative());

        var weight =
            ipnsProbePoint
                .Lcp(directionOpEi)
                .SpSquared()
                .SqrtOfAbs();

        var flat = new RGaConformalFlat(
            conformalSpace,
            weight,
            position,
            directionOpEi.RemoveEi()
        );

        Debug.Assert(
            weight.IsZero() ||
            flat.IpnsFlatPosition.Lcp(ipnsFlat).IsNearZero()
        );

        return flat;
    }
    
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalParametricElement DecodeIpnsFlat(this RGaConformalParametricBlade2D blade)
    //{
    //    if (!blade.Specs.IsIpnsFlat)
    //        throw new InvalidOperationException();

    //    return RGaConformalParametricElement.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeIpnsFlat()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalParametricElement DecodeIpnsFlat(this RGaConformalParametricBlade3D blade)
    //{
    //    if (!blade.Specs.IsIpnsFlat)
    //        throw new InvalidOperationException();

    //    return RGaConformalParametricElement.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeIpnsFlat()
    //    );
    //}
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalParametricElement DecodeIpnsFlat(this RGaConformalParametricBlade2D blade, IParametricCurve2D egaProbePoint)
    //{
    //    if (!blade.Specs.IsIpnsFlat)
    //        throw new InvalidOperationException();

    //    return RGaConformalParametricElement.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeIpnsFlat(
    //            egaProbePoint.GetPoint(t).EncodeEGaVectorBlade(blade.ConformalSpace)
    //        )
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalParametricElement DecodeIpnsFlat(this RGaConformalParametricBlade3D blade, IParametricCurve3D egaProbePoint)
    //{
    //    if (!blade.Specs.IsIpnsFlat)
    //        throw new InvalidOperationException();

    //    return RGaConformalParametricElement.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeIpnsFlat(
    //            egaProbePoint.GetPoint(t).EncodeEGaVectorBlade(blade.ConformalSpace)
    //        )
    //    );
    //}


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade DecodeIpnsHyperPlaneEGaDirection(this RGaConformalBlade ipnsFlat)
    {
        var conformalSpace = ipnsFlat.ConformalSpace;

        var eoScalar = ipnsFlat[0] + ipnsFlat[1];

        return eoScalar.IsNearZero()
            ? ipnsFlat.GetEGaVectorPart().EGaUnNormal()
            : conformalSpace.ZeroVectorBlade;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade DecodeIpnsFlatEGaDirection(this RGaConformalBlade ipnsFlat)
    {
        return ipnsFlat.ConformalSpace.Ei
            .Lcp(ipnsFlat.CGaUnDual())
            .RemoveEi()
            .Negative()
            .DivideByNorm();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade DecodeIpnsHyperPlaneEGaNormalDirection(this RGaConformalBlade ipnsFlat)
    {
        var conformalSpace = ipnsFlat.ConformalSpace;

        var eoScalar = ipnsFlat[0] + ipnsFlat[1];

        return eoScalar.IsNearZero()
            ? ipnsFlat.GetEGaVectorPart()
            : conformalSpace.ZeroVectorBlade;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade DecodeIpnsFlatEGaNormalDirection(this RGaConformalBlade ipnsFlat)
    {
        return ipnsFlat.DecodeIpnsFlatEGaDirection().EGaNormal();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade DecodeIpnsLineEGaPosition(this RGaConformalBlade ipnsFlat, Float64Vector2D egaProbePoint)
    {
        return ipnsFlat.DecodeIpnsHyperPlaneEGaPosition(
            ipnsFlat.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade DecodeIpnsPlaneEGaPosition(this RGaConformalBlade ipnsFlat, Float64Vector3D egaProbePoint)
    {
        return ipnsFlat.DecodeIpnsHyperPlaneEGaPosition(
            ipnsFlat.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade DecodeIpnsHyperPlaneEGaPosition(this RGaConformalBlade ipnsFlat, RGaFloat64Vector egaProbePoint)
    {
        return ipnsFlat.DecodeIpnsHyperPlaneEGaPosition(
            ipnsFlat.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade DecodeIpnsHyperPlaneEGaPosition(this RGaConformalBlade ipnsFlat)
    {
        return ipnsFlat.DecodeIpnsHyperPlaneEGaPosition(
            ipnsFlat.ConformalSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade DecodeIpnsHyperPlaneEGaPosition(this RGaConformalBlade ipnsFlat, RGaConformalBlade egaProbePoint)
    {
        var conformalSpace = ipnsFlat.ConformalSpace;

        var eoScalar = ipnsFlat[0] + ipnsFlat[1];

        return eoScalar.IsNearZero()
            ? egaProbePoint
                .EGaVectorToIpnsPoint()
                .Op(ipnsFlat)
                .Gp(ipnsFlat.Inverse())
                .GetVectorPart((int i) => i >= 2)
                .ToConformalBlade(ipnsFlat.ConformalSpace)
                //.DecodeIpnsHyperSphereEGaCenter(conformalSpace)
            : conformalSpace.ZeroVectorBlade;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade DecodeIpnsFlatEGaPosition(this RGaConformalBlade ipnsFlat)
    {
        return ipnsFlat.DecodeIpnsFlatEGaPosition(
            ipnsFlat.ConformalSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade DecodeIpnsFlatEGaPosition(this RGaConformalBlade ipnsFlat, Float64Vector2D egaProbePoint)
    {
        return ipnsFlat.DecodeIpnsFlatEGaPosition(
            ipnsFlat.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade DecodeIpnsFlatEGaPosition(this RGaConformalBlade ipnsFlat, Float64Vector3D egaProbePoint)
    {
        return ipnsFlat.DecodeIpnsFlatEGaPosition(
            ipnsFlat.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade DecodeIpnsFlatEGaPosition(this RGaConformalBlade ipnsFlat, Float64Vector egaProbePoint)
    {
        return ipnsFlat.DecodeIpnsFlatEGaPosition(
            ipnsFlat.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade DecodeIpnsFlatEGaPosition(this RGaConformalBlade ipnsFlat, RGaFloat64Vector egaProbePoint)
    {
        return ipnsFlat.DecodeIpnsFlatEGaPosition(
            ipnsFlat.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade DecodeIpnsFlatEGaPosition(this RGaConformalBlade ipnsFlat, RGaConformalBlade egaProbePointBlade)
    {
        return egaProbePointBlade
            .EGaVectorToIpnsPoint()
            .Op(ipnsFlat)
            .Gp(ipnsFlat.Inverse())
            .VectorPartToConformalEGaBlade(ipnsFlat.ConformalSpace);
        //.DecodeIpnsHyperSphereEGaCenter(egaProbePointBlade.ConformalSpace);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeIpnsHyperPlaneWeight(this RGaConformalBlade ipnsFlat)
    {
        var eoScalar = ipnsFlat[0] + ipnsFlat[1];

        return eoScalar.IsNearZero()
            ? ipnsFlat.GetEGaVectorPart().Norm()
            : 0d;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeIpnsFlatWeight(this RGaConformalBlade ipnsFlat)
    {
        return ipnsFlat.DecodeIpnsFlatWeight(
            ipnsFlat.ConformalSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeIpnsFlatWeight(this RGaConformalBlade ipnsFlat, Float64Vector2D egaProbePoint)
    {
        return ipnsFlat.DecodeIpnsFlatWeight(
            ipnsFlat.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeIpnsFlatWeight(this RGaConformalBlade ipnsFlat, Float64Vector3D egaProbePoint)
    {
        return ipnsFlat.DecodeIpnsFlatWeight(
            ipnsFlat.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeIpnsFlatWeight(this RGaConformalBlade ipnsFlat, Float64Vector egaProbePoint)
    {
        return ipnsFlat.DecodeIpnsFlatWeight(
            ipnsFlat.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeIpnsFlatWeight(this RGaConformalBlade ipnsFlat, RGaFloat64Vector egaProbePoint)
    {
        return ipnsFlat.DecodeIpnsFlatWeight(
            ipnsFlat.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeIpnsFlatWeight(this RGaConformalBlade ipnsFlat, RGaConformalBlade egaProbePoint)
    {
        var ipnsProbePoint =
            egaProbePoint.EGaVectorToIpnsPoint();

        var directionOpEi =
            ipnsFlat.ConformalSpace.Ei.Lcp(ipnsFlat.CGaUnDual().Negative());

        return ipnsProbePoint
            .Lcp(directionOpEi)
            .SpSquared()
            .SqrtOfAbs();
    }


}