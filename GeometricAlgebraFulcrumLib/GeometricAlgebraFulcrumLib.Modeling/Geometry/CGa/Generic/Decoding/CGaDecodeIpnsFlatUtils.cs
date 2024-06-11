using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Elements;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Encoding;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Decoding;

public static class CGaDecodeIpnsFlatUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DecodeIpnsLine2D<T>(this CGaBlade<T> ipnsFlat)
    {
        Debug.Assert(ipnsFlat.GeometricSpace.Is4D);

        return ipnsFlat.DecodeIpnsHyperPlane(
            ipnsFlat.GeometricSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DecodeIpnsLine2D<T>(this CGaBlade<T> ipnsFlat, LinVector2D<T> egaProbePoint)
    {
        return ipnsFlat.DecodeIpnsHyperPlane(
            ipnsFlat.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DecodeIpnsPlane3D<T>(this CGaBlade<T> ipnsFlat)
    {
        Debug.Assert(ipnsFlat.GeometricSpace.Is5D);

        return ipnsFlat.DecodeIpnsHyperPlane(
            ipnsFlat.GeometricSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DecodeIpnsPlane3D<T>(this CGaBlade<T> ipnsFlat, LinVector3D<T> egaProbePoint)
    {
        return ipnsFlat.DecodeIpnsHyperPlane(
            ipnsFlat.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DecodeIpnsHyperPlane<T>(this CGaBlade<T> ipnsFlat)
    {
        return ipnsFlat.DecodeIpnsHyperPlane(
            ipnsFlat.GeometricSpace.ZeroVectorBlade
        );
    }

    public static CGaFlat<T> DecodeIpnsHyperPlane<T>(this CGaBlade<T> ipnsFlat, CGaBlade<T> egaProbePoint)
    {
        var cgaGeometricSpace = ipnsFlat.GeometricSpace;

        var eoScalar = ipnsFlat[0] + ipnsFlat[1];

        // This is a point\hypersphere, not a hyperplane
        if (!eoScalar.IsNearZero())
            return new CGaFlat<T>(
                cgaGeometricSpace,
                cgaGeometricSpace.ScalarProcessor.Zero,
                cgaGeometricSpace.ZeroVectorBlade,
                cgaGeometricSpace.ZeroVectorBlade
            );

        var normal = ipnsFlat.GetVGaVectorPart();
        var weight = normal.Norm();

        // Hyperplane has zero normal
        if (weight.IsNearZero())
            throw new InvalidOperationException();

        var ipnsProbePoint =
            egaProbePoint.VGaVectorToIpnsPoint();

        var position =
            ipnsProbePoint
                .Op(ipnsFlat)
                .Gp(ipnsFlat.Inverse())
                .GetVectorPart()
                .GetVectorPart(i => i >= 2)
                .ToConformalBlade(ipnsFlat.GeometricSpace);
        //.DecodeIpnsHyperSphereVGaCenter(cgaGeometricSpace);

        var flat = new CGaFlat<T>(
            cgaGeometricSpace,
            weight,
            position,
            normal.VGaUnNormal()
        );

        Debug.Assert(
            flat.PositionToIpnsPoint().Lcp(ipnsFlat).IsNearZero()
        );

        return flat;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DecodeIpnsFlat<T>(this CGaBlade<T> ipnsFlat, LinVector2D<T> egaProbePoint)
    {
        return ipnsFlat.DecodeIpnsFlat(
            ipnsFlat.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DecodeIpnsFlat<T>(this CGaBlade<T> ipnsFlat, LinVector3D<T> egaProbePoint)
    {
        return ipnsFlat.DecodeIpnsFlat(
            ipnsFlat.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DecodeIpnsFlat<T>(this CGaBlade<T> ipnsFlat, XGaVector<T> egaProbePoint)
    {
        return ipnsFlat.DecodeIpnsFlat(
            ipnsFlat.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DecodeIpnsFlat<T>(this CGaBlade<T> ipnsFlat)
    {
        return ipnsFlat.DecodeIpnsFlat(
            ipnsFlat.GeometricSpace.ZeroVectorBlade
        );
    }

    public static CGaFlat<T> DecodeIpnsFlat<T>(this CGaBlade<T> ipnsFlat, CGaBlade<T> egaProbePoint)
    {
        var cgaGeometricSpace = ipnsFlat.GeometricSpace;

        var ipnsProbePoint =
            egaProbePoint.VGaVectorToIpnsPoint();

        var position =
            ipnsProbePoint
                .Op(ipnsFlat)
                .Gp(ipnsFlat.Inverse())
                .GetVectorPart()
                .GetVectorPart(i => i >= 2)
                .ToConformalBlade(ipnsFlat.GeometricSpace);
        //.DecodeIpnsHyperSphereVGaCenter(cgaGeometricSpace);

        var directionOpEi =
            cgaGeometricSpace.Ei.Lcp(ipnsFlat.CGaUnDual().Negative());

        var weight =
            ipnsProbePoint
                .Lcp(directionOpEi)
                .SpSquared()
                .SqrtOfAbs();

        var flat = new CGaFlat<T>(
            cgaGeometricSpace,
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
    //            egaProbePoint.GetPoint(t).EncodeVGaVectorBlade(blade.ConformalSpace)
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
    //            egaProbePoint.GetPoint(t).EncodeVGaVectorBlade(blade.ConformalSpace)
    //        )
    //    );
    //}


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> DecodeIpnsHyperPlaneVGaDirection<T>(this CGaBlade<T> ipnsFlat)
    {
        var cgaGeometricSpace = ipnsFlat.GeometricSpace;

        var eoScalar = ipnsFlat[0] + ipnsFlat[1];

        return eoScalar.IsNearZero()
            ? ipnsFlat.GetVGaVectorPart().VGaUnNormal()
            : cgaGeometricSpace.ZeroVectorBlade;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> DecodeIpnsFlatVGaDirection<T>(this CGaBlade<T> ipnsFlat)
    {
        return ipnsFlat.GeometricSpace.Ei
            .Lcp(ipnsFlat.CGaUnDual())
            .RemoveEi()
            .Negative()
            .DivideByNorm();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> DecodeIpnsHyperPlaneVGaNormalDirection<T>(this CGaBlade<T> ipnsFlat)
    {
        var cgaGeometricSpace = ipnsFlat.GeometricSpace;

        var eoScalar = ipnsFlat[0] + ipnsFlat[1];

        return eoScalar.IsNearZero()
            ? ipnsFlat.GetVGaVectorPart()
            : cgaGeometricSpace.ZeroVectorBlade;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> DecodeIpnsFlatVGaNormalDirection<T>(this CGaBlade<T> ipnsFlat)
    {
        return ipnsFlat.DecodeIpnsFlatVGaDirection().VGaNormal();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> DecodeIpnsLineVGaPosition<T>(this CGaBlade<T> ipnsFlat, LinVector2D<T> egaProbePoint)
    {
        return ipnsFlat.DecodeIpnsHyperPlaneVGaPosition(
            ipnsFlat.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> DecodeIpnsPlaneVGaPosition<T>(this CGaBlade<T> ipnsFlat, LinVector3D<T> egaProbePoint)
    {
        return ipnsFlat.DecodeIpnsHyperPlaneVGaPosition(
            ipnsFlat.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> DecodeIpnsHyperPlaneVGaPosition<T>(this CGaBlade<T> ipnsFlat, XGaVector<T> egaProbePoint)
    {
        return ipnsFlat.DecodeIpnsHyperPlaneVGaPosition(
            ipnsFlat.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> DecodeIpnsHyperPlaneVGaPosition<T>(this CGaBlade<T> ipnsFlat)
    {
        return ipnsFlat.DecodeIpnsHyperPlaneVGaPosition(
            ipnsFlat.GeometricSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> DecodeIpnsHyperPlaneVGaPosition<T>(this CGaBlade<T> ipnsFlat, CGaBlade<T> egaProbePoint)
    {
        var cgaGeometricSpace = ipnsFlat.GeometricSpace;

        var eoScalar = ipnsFlat[0] + ipnsFlat[1];

        return eoScalar.IsNearZero()
            ? egaProbePoint
                .VGaVectorToIpnsPoint()
                .Op(ipnsFlat)
                .Gp(ipnsFlat.Inverse())
                .GetVectorPart()
                .GetVectorPart(i => i >= 2)
                .ToConformalBlade(ipnsFlat.GeometricSpace)
            //.DecodeIpnsHyperSphereVGaCenter(cgaGeometricSpace)
            : cgaGeometricSpace.ZeroVectorBlade;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> DecodeIpnsFlatVGaPosition<T>(this CGaBlade<T> ipnsFlat)
    {
        return ipnsFlat.DecodeIpnsFlatVGaPosition(
            ipnsFlat.GeometricSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> DecodeIpnsFlatVGaPosition<T>(this CGaBlade<T> ipnsFlat, LinVector2D<T> egaProbePoint)
    {
        return ipnsFlat.DecodeIpnsFlatVGaPosition(
            ipnsFlat.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> DecodeIpnsFlatVGaPosition<T>(this CGaBlade<T> ipnsFlat, LinVector3D<T> egaProbePoint)
    {
        return ipnsFlat.DecodeIpnsFlatVGaPosition(
            ipnsFlat.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> DecodeIpnsFlatVGaPosition<T>(this CGaBlade<T> ipnsFlat, LinVector<T> egaProbePoint)
    {
        return ipnsFlat.DecodeIpnsFlatVGaPosition(
            ipnsFlat.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> DecodeIpnsFlatVGaPosition<T>(this CGaBlade<T> ipnsFlat, XGaVector<T> egaProbePoint)
    {
        return ipnsFlat.DecodeIpnsFlatVGaPosition(
            ipnsFlat.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> DecodeIpnsFlatVGaPosition<T>(this CGaBlade<T> ipnsFlat, CGaBlade<T> egaProbePointBlade)
    {
        return egaProbePointBlade
            .VGaVectorToIpnsPoint()
            .Op(ipnsFlat)
            .Gp(ipnsFlat.Inverse())
            .VectorPartToConformalVGaBlade(ipnsFlat.GeometricSpace);
        //.DecodeIpnsHyperSphereVGaCenter(egaProbePointBlade.ConformalSpace);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeIpnsHyperPlaneWeight<T>(this CGaBlade<T> ipnsFlat)
    {
        var eoScalar = ipnsFlat[0] + ipnsFlat[1];

        return eoScalar.IsNearZero()
            ? ipnsFlat.GetVGaVectorPart().Norm()
            : ipnsFlat.GeometricSpace.ScalarProcessor.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeIpnsFlatWeight<T>(this CGaBlade<T> ipnsFlat)
    {
        return ipnsFlat.DecodeIpnsFlatWeight(
            ipnsFlat.GeometricSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeIpnsFlatWeight<T>(this CGaBlade<T> ipnsFlat, LinVector2D<T> egaProbePoint)
    {
        return ipnsFlat.DecodeIpnsFlatWeight(
            ipnsFlat.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeIpnsFlatWeight<T>(this CGaBlade<T> ipnsFlat, LinVector3D<T> egaProbePoint)
    {
        return ipnsFlat.DecodeIpnsFlatWeight(
            ipnsFlat.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeIpnsFlatWeight<T>(this CGaBlade<T> ipnsFlat, LinVector<T> egaProbePoint)
    {
        return ipnsFlat.DecodeIpnsFlatWeight(
            ipnsFlat.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeIpnsFlatWeight<T>(this CGaBlade<T> ipnsFlat, XGaVector<T> egaProbePoint)
    {
        return ipnsFlat.DecodeIpnsFlatWeight(
            ipnsFlat.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeIpnsFlatWeight<T>(this CGaBlade<T> ipnsFlat, CGaBlade<T> egaProbePoint)
    {
        var ipnsProbePoint =
            egaProbePoint.VGaVectorToIpnsPoint();

        var directionOpEi =
            ipnsFlat.GeometricSpace.Ei.Lcp(ipnsFlat.CGaUnDual().Negative());

        return ipnsProbePoint
            .Lcp(directionOpEi)
            .SpSquared()
            .SqrtOfAbs();
    }


}