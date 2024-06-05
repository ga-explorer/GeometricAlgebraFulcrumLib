using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Elements;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Encoding;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Decoding;

public static class XGaConformalDecodeIpnsFlatUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalFlat<T> DecodeIpnsLine2D<T>(this XGaConformalBlade<T> ipnsFlat)
    {
        Debug.Assert(ipnsFlat.ConformalSpace.Is4D);

        return ipnsFlat.DecodeIpnsHyperPlane(
            ipnsFlat.ConformalSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalFlat<T> DecodeIpnsLine2D<T>(this XGaConformalBlade<T> ipnsFlat, LinVector2D<T> egaProbePoint)
    {
        return ipnsFlat.DecodeIpnsHyperPlane(
            ipnsFlat.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalFlat<T> DecodeIpnsPlane3D<T>(this XGaConformalBlade<T> ipnsFlat)
    {
        Debug.Assert(ipnsFlat.ConformalSpace.Is5D);

        return ipnsFlat.DecodeIpnsHyperPlane(
            ipnsFlat.ConformalSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalFlat<T> DecodeIpnsPlane3D<T>(this XGaConformalBlade<T> ipnsFlat, LinVector3D<T> egaProbePoint)
    {
        return ipnsFlat.DecodeIpnsHyperPlane(
            ipnsFlat.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalFlat<T> DecodeIpnsHyperPlane<T>(this XGaConformalBlade<T> ipnsFlat)
    {
        return ipnsFlat.DecodeIpnsHyperPlane(
            ipnsFlat.ConformalSpace.ZeroVectorBlade
        );
    }

    public static XGaConformalFlat<T> DecodeIpnsHyperPlane<T>(this XGaConformalBlade<T> ipnsFlat, XGaConformalBlade<T> egaProbePoint)
    {
        var conformalSpace = ipnsFlat.ConformalSpace;

        var eoScalar = ipnsFlat[0] + ipnsFlat[1];

        // This is a point\hypersphere, not a hyperplane
        if (!eoScalar.IsNearZero())
            return new XGaConformalFlat<T>(
                conformalSpace,
                conformalSpace.ScalarProcessor.Zero,
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
                .GetVectorPart()
                .GetVectorPart(i => i >= 2)
                .ToConformalBlade(ipnsFlat.ConformalSpace);
                //.DecodeIpnsHyperSphereEGaCenter(conformalSpace);

        var flat = new XGaConformalFlat<T>(
            conformalSpace,
            weight,
            position,
            normal.EGaUnNormal()
        );

        Debug.Assert(
            flat.PositionToIpnsPoint().Lcp(ipnsFlat).IsNearZero()
        );

        return flat;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalFlat<T> DecodeIpnsFlat<T>(this XGaConformalBlade<T> ipnsFlat, LinVector2D<T> egaProbePoint)
    {
        return ipnsFlat.DecodeIpnsFlat(
            ipnsFlat.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalFlat<T> DecodeIpnsFlat<T>(this XGaConformalBlade<T> ipnsFlat, LinVector3D<T> egaProbePoint)
    {
        return ipnsFlat.DecodeIpnsFlat(
            ipnsFlat.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalFlat<T> DecodeIpnsFlat<T>(this XGaConformalBlade<T> ipnsFlat, XGaVector<T> egaProbePoint)
    {
        return ipnsFlat.DecodeIpnsFlat(
            ipnsFlat.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalFlat<T> DecodeIpnsFlat<T>(this XGaConformalBlade<T> ipnsFlat)
    {
        return ipnsFlat.DecodeIpnsFlat(
            ipnsFlat.ConformalSpace.ZeroVectorBlade
        );
    }

    public static XGaConformalFlat<T> DecodeIpnsFlat<T>(this XGaConformalBlade<T> ipnsFlat, XGaConformalBlade<T> egaProbePoint)
    {
        var conformalSpace = ipnsFlat.ConformalSpace;

        var ipnsProbePoint =
            egaProbePoint.EGaVectorToIpnsPoint();

        var position =
            ipnsProbePoint
                .Op(ipnsFlat)
                .Gp(ipnsFlat.Inverse())
                .GetVectorPart()
                .GetVectorPart(i => i >= 2)
                .ToConformalBlade(ipnsFlat.ConformalSpace);
                //.DecodeIpnsHyperSphereEGaCenter(conformalSpace);

        var directionOpEi =
            conformalSpace.Ei.Lcp(ipnsFlat.CGaUnDual().Negative());

        var weight =
            ipnsProbePoint
                .Lcp(directionOpEi)
                .SpSquared()
                .SqrtOfAbs();

        var flat = new XGaConformalFlat<T>(
            conformalSpace,
            weight,
            position,
            directionOpEi.RemoveEi()
        );

        Debug.Assert(
            weight.IsZero() ||
            flat.PositionToIpnsPoint().Lcp(ipnsFlat).IsNearZero()
        );

        return flat;
    }
    
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaConformalParametricElement<T> DecodeIpnsFlat<T>(this XGaConformalParametricBlade2D blade)
    //{
    //    if (!blade.Specs.IsIpnsFlat)
    //        throw new InvalidOperationException();

    //    return XGaConformalParametricElement<T>.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeIpnsFlat()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaConformalParametricElement<T> DecodeIpnsFlat<T>(this XGaConformalParametricBlade3D blade)
    //{
    //    if (!blade.Specs.IsIpnsFlat)
    //        throw new InvalidOperationException();

    //    return XGaConformalParametricElement<T>.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeIpnsFlat()
    //    );
    //}
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaConformalParametricElement<T> DecodeIpnsFlat<T>(this XGaConformalParametricBlade2D blade, IParametricCurve2D egaProbePoint)
    //{
    //    if (!blade.Specs.IsIpnsFlat)
    //        throw new InvalidOperationException();

    //    return XGaConformalParametricElement<T>.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeIpnsFlat(
    //            egaProbePoint.GetPoint(t).EncodeEGaVectorBlade(blade.ConformalSpace)
    //        )
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaConformalParametricElement<T> DecodeIpnsFlat<T>(this XGaConformalParametricBlade3D blade, IParametricCurve3D egaProbePoint)
    //{
    //    if (!blade.Specs.IsIpnsFlat)
    //        throw new InvalidOperationException();

    //    return XGaConformalParametricElement<T>.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeIpnsFlat(
    //            egaProbePoint.GetPoint(t).EncodeEGaVectorBlade(blade.ConformalSpace)
    //        )
    //    );
    //}


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> DecodeIpnsHyperPlaneEGaDirection<T>(this XGaConformalBlade<T> ipnsFlat)
    {
        var conformalSpace = ipnsFlat.ConformalSpace;

        var eoScalar = ipnsFlat[0] + ipnsFlat[1];

        return eoScalar.IsNearZero()
            ? ipnsFlat.GetEGaVectorPart().EGaUnNormal()
            : conformalSpace.ZeroVectorBlade;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> DecodeIpnsFlatEGaDirection<T>(this XGaConformalBlade<T> ipnsFlat)
    {
        return ipnsFlat.ConformalSpace.Ei
            .Lcp(ipnsFlat.CGaUnDual())
            .RemoveEi()
            .Negative()
            .DivideByNorm();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> DecodeIpnsHyperPlaneEGaNormalDirection<T>(this XGaConformalBlade<T> ipnsFlat)
    {
        var conformalSpace = ipnsFlat.ConformalSpace;

        var eoScalar = ipnsFlat[0] + ipnsFlat[1];

        return eoScalar.IsNearZero()
            ? ipnsFlat.GetEGaVectorPart()
            : conformalSpace.ZeroVectorBlade;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> DecodeIpnsFlatEGaNormalDirection<T>(this XGaConformalBlade<T> ipnsFlat)
    {
        return ipnsFlat.DecodeIpnsFlatEGaDirection().EGaNormal();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> DecodeIpnsLineEGaPosition<T>(this XGaConformalBlade<T> ipnsFlat, LinVector2D<T> egaProbePoint)
    {
        return ipnsFlat.DecodeIpnsHyperPlaneEGaPosition(
            ipnsFlat.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> DecodeIpnsPlaneEGaPosition<T>(this XGaConformalBlade<T> ipnsFlat, LinVector3D<T> egaProbePoint)
    {
        return ipnsFlat.DecodeIpnsHyperPlaneEGaPosition(
            ipnsFlat.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> DecodeIpnsHyperPlaneEGaPosition<T>(this XGaConformalBlade<T> ipnsFlat, XGaVector<T> egaProbePoint)
    {
        return ipnsFlat.DecodeIpnsHyperPlaneEGaPosition(
            ipnsFlat.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> DecodeIpnsHyperPlaneEGaPosition<T>(this XGaConformalBlade<T> ipnsFlat)
    {
        return ipnsFlat.DecodeIpnsHyperPlaneEGaPosition(
            ipnsFlat.ConformalSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> DecodeIpnsHyperPlaneEGaPosition<T>(this XGaConformalBlade<T> ipnsFlat, XGaConformalBlade<T> egaProbePoint)
    {
        var conformalSpace = ipnsFlat.ConformalSpace;

        var eoScalar = ipnsFlat[0] + ipnsFlat[1];

        return eoScalar.IsNearZero()
            ? egaProbePoint
                .EGaVectorToIpnsPoint()
                .Op(ipnsFlat)
                .Gp(ipnsFlat.Inverse())
                .GetVectorPart()
                .GetVectorPart(i => i >= 2)
                .ToConformalBlade(ipnsFlat.ConformalSpace)
                //.DecodeIpnsHyperSphereEGaCenter(conformalSpace)
            : conformalSpace.ZeroVectorBlade;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> DecodeIpnsFlatEGaPosition<T>(this XGaConformalBlade<T> ipnsFlat)
    {
        return ipnsFlat.DecodeIpnsFlatEGaPosition(
            ipnsFlat.ConformalSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> DecodeIpnsFlatEGaPosition<T>(this XGaConformalBlade<T> ipnsFlat, LinVector2D<T> egaProbePoint)
    {
        return ipnsFlat.DecodeIpnsFlatEGaPosition(
            ipnsFlat.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> DecodeIpnsFlatEGaPosition<T>(this XGaConformalBlade<T> ipnsFlat, LinVector3D<T> egaProbePoint)
    {
        return ipnsFlat.DecodeIpnsFlatEGaPosition(
            ipnsFlat.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> DecodeIpnsFlatEGaPosition<T>(this XGaConformalBlade<T> ipnsFlat, LinVector<T> egaProbePoint)
    {
        return ipnsFlat.DecodeIpnsFlatEGaPosition(
            ipnsFlat.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> DecodeIpnsFlatEGaPosition<T>(this XGaConformalBlade<T> ipnsFlat, XGaVector<T> egaProbePoint)
    {
        return ipnsFlat.DecodeIpnsFlatEGaPosition(
            ipnsFlat.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> DecodeIpnsFlatEGaPosition<T>(this XGaConformalBlade<T> ipnsFlat, XGaConformalBlade<T> egaProbePointBlade)
    {
        return egaProbePointBlade
            .EGaVectorToIpnsPoint()
            .Op(ipnsFlat)
            .Gp(ipnsFlat.Inverse())
            .VectorPartToConformalEGaBlade(ipnsFlat.ConformalSpace);
        //.DecodeIpnsHyperSphereEGaCenter(egaProbePointBlade.ConformalSpace);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeIpnsHyperPlaneWeight<T>(this XGaConformalBlade<T> ipnsFlat)
    {
        var eoScalar = ipnsFlat[0] + ipnsFlat[1];

        return eoScalar.IsNearZero()
            ? ipnsFlat.GetEGaVectorPart().Norm()
            : ipnsFlat.ConformalSpace.ScalarProcessor.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeIpnsFlatWeight<T>(this XGaConformalBlade<T> ipnsFlat)
    {
        return ipnsFlat.DecodeIpnsFlatWeight(
            ipnsFlat.ConformalSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeIpnsFlatWeight<T>(this XGaConformalBlade<T> ipnsFlat, LinVector2D<T> egaProbePoint)
    {
        return ipnsFlat.DecodeIpnsFlatWeight(
            ipnsFlat.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeIpnsFlatWeight<T>(this XGaConformalBlade<T> ipnsFlat, LinVector3D<T> egaProbePoint)
    {
        return ipnsFlat.DecodeIpnsFlatWeight(
            ipnsFlat.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeIpnsFlatWeight<T>(this XGaConformalBlade<T> ipnsFlat, LinVector<T> egaProbePoint)
    {
        return ipnsFlat.DecodeIpnsFlatWeight(
            ipnsFlat.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeIpnsFlatWeight<T>(this XGaConformalBlade<T> ipnsFlat, XGaVector<T> egaProbePoint)
    {
        return ipnsFlat.DecodeIpnsFlatWeight(
            ipnsFlat.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeIpnsFlatWeight<T>(this XGaConformalBlade<T> ipnsFlat, XGaConformalBlade<T> egaProbePoint)
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