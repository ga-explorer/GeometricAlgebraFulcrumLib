using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Elements;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Encoding;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Decoding;

public static class CGaFloat64DecodeIpnsFlatUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DecodeIpnsLine2D(this CGaFloat64Blade ipnsFlat)
    {
        Debug.Assert(ipnsFlat.GeometricSpace.Is4D);

        return ipnsFlat.DecodeIpnsHyperPlane(
            ipnsFlat.GeometricSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DecodeIpnsLine2D(this CGaFloat64Blade ipnsFlat, LinFloat64Vector2D egaProbePoint)
    {
        return ipnsFlat.DecodeIpnsHyperPlane(
            ipnsFlat.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DecodeIpnsPlane3D(this CGaFloat64Blade ipnsFlat)
    {
        Debug.Assert(ipnsFlat.GeometricSpace.Is5D);

        return ipnsFlat.DecodeIpnsHyperPlane(
            ipnsFlat.GeometricSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DecodeIpnsPlane3D(this CGaFloat64Blade ipnsFlat, LinFloat64Vector3D egaProbePoint)
    {
        return ipnsFlat.DecodeIpnsHyperPlane(
            ipnsFlat.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DecodeIpnsHyperPlane(this CGaFloat64Blade ipnsFlat)
    {
        return ipnsFlat.DecodeIpnsHyperPlane(
            ipnsFlat.GeometricSpace.ZeroVectorBlade
        );
    }

    public static CGaFloat64Flat DecodeIpnsHyperPlane(this CGaFloat64Blade ipnsFlat, CGaFloat64Blade egaProbePoint)
    {
        var cgaGeometricSpace = ipnsFlat.GeometricSpace;

        var eoScalar = ipnsFlat[0] + ipnsFlat[1];

        // This is a point\hypersphere, not a hyperplane
        if (!eoScalar.IsNearZero())
            return new CGaFloat64Flat(
                cgaGeometricSpace,
                0,
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
                .GetVectorPart((int i) => i >= 2)
                .ToConformalBlade(ipnsFlat.GeometricSpace);
        //.DecodeIpnsHyperSphereVGaCenter(cgaGeometricSpace);

        var flat = new CGaFloat64Flat(
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
    public static CGaFloat64Flat DecodeIpnsFlat(this CGaFloat64Blade ipnsFlat, LinFloat64Vector2D egaProbePoint)
    {
        return ipnsFlat.DecodeIpnsFlat(
            ipnsFlat.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DecodeIpnsFlat(this CGaFloat64Blade ipnsFlat, LinFloat64Vector3D egaProbePoint)
    {
        return ipnsFlat.DecodeIpnsFlat(
            ipnsFlat.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DecodeIpnsFlat(this CGaFloat64Blade ipnsFlat, RGaFloat64Vector egaProbePoint)
    {
        return ipnsFlat.DecodeIpnsFlat(
            ipnsFlat.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DecodeIpnsFlat(this CGaFloat64Blade ipnsFlat)
    {
        return ipnsFlat.DecodeIpnsFlat(
            ipnsFlat.GeometricSpace.ZeroVectorBlade
        );
    }

    public static CGaFloat64Flat DecodeIpnsFlat(this CGaFloat64Blade ipnsFlat, CGaFloat64Blade egaProbePoint)
    {
        var cgaGeometricSpace = ipnsFlat.GeometricSpace;

        var ipnsProbePoint =
            egaProbePoint.VGaVectorToIpnsPoint();

        var position =
            ipnsProbePoint
                .Op(ipnsFlat)
                .Gp(ipnsFlat.Inverse())
                .GetVectorPart((int i) => i >= 2)
                .ToConformalBlade(ipnsFlat.GeometricSpace);
        //.DecodeIpnsHyperSphereVGaCenter(cgaGeometricSpace);

        var directionOpEi =
            cgaGeometricSpace.Ei.Lcp(ipnsFlat.CGaUnDual().Negative());

        var weight =
            ipnsProbePoint
                .Lcp(directionOpEi)
                .SpSquared()
                .SqrtOfAbs();

        var flat = new CGaFloat64Flat(
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
    //            egaProbePoint.GetPoint(t).EncodeVGaVectorBlade(blade.ConformalSpace)
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
    //            egaProbePoint.GetPoint(t).EncodeVGaVectorBlade(blade.ConformalSpace)
    //        )
    //    );
    //}


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade DecodeIpnsHyperPlaneVGaDirection(this CGaFloat64Blade ipnsFlat)
    {
        var cgaGeometricSpace = ipnsFlat.GeometricSpace;

        var eoScalar = ipnsFlat[0] + ipnsFlat[1];

        return eoScalar.IsNearZero()
            ? ipnsFlat.GetVGaVectorPart().VGaUnNormal()
            : cgaGeometricSpace.ZeroVectorBlade;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade DecodeIpnsFlatVGaDirection(this CGaFloat64Blade ipnsFlat)
    {
        return ipnsFlat.GeometricSpace.Ei
            .Lcp(ipnsFlat.CGaUnDual())
            .RemoveEi()
            .Negative()
            .DivideByNorm();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade DecodeIpnsHyperPlaneVGaNormalDirection(this CGaFloat64Blade ipnsFlat)
    {
        var cgaGeometricSpace = ipnsFlat.GeometricSpace;

        var eoScalar = ipnsFlat[0] + ipnsFlat[1];

        return eoScalar.IsNearZero()
            ? ipnsFlat.GetVGaVectorPart()
            : cgaGeometricSpace.ZeroVectorBlade;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade DecodeIpnsFlatVGaNormalDirection(this CGaFloat64Blade ipnsFlat)
    {
        return ipnsFlat.DecodeIpnsFlatVGaDirection().VGaNormal();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade DecodeIpnsLineVGaPosition(this CGaFloat64Blade ipnsFlat, LinFloat64Vector2D egaProbePoint)
    {
        return ipnsFlat.DecodeIpnsHyperPlaneVGaPosition(
            ipnsFlat.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade DecodeIpnsPlaneVGaPosition(this CGaFloat64Blade ipnsFlat, LinFloat64Vector3D egaProbePoint)
    {
        return ipnsFlat.DecodeIpnsHyperPlaneVGaPosition(
            ipnsFlat.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade DecodeIpnsHyperPlaneVGaPosition(this CGaFloat64Blade ipnsFlat, RGaFloat64Vector egaProbePoint)
    {
        return ipnsFlat.DecodeIpnsHyperPlaneVGaPosition(
            ipnsFlat.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade DecodeIpnsHyperPlaneVGaPosition(this CGaFloat64Blade ipnsFlat)
    {
        return ipnsFlat.DecodeIpnsHyperPlaneVGaPosition(
            ipnsFlat.GeometricSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade DecodeIpnsHyperPlaneVGaPosition(this CGaFloat64Blade ipnsFlat, CGaFloat64Blade egaProbePoint)
    {
        var cgaGeometricSpace = ipnsFlat.GeometricSpace;

        var eoScalar = ipnsFlat[0] + ipnsFlat[1];

        return eoScalar.IsNearZero()
            ? egaProbePoint
                .VGaVectorToIpnsPoint()
                .Op(ipnsFlat)
                .Gp(ipnsFlat.Inverse())
                .GetVectorPart((int i) => i >= 2)
                .ToConformalBlade(ipnsFlat.GeometricSpace)
            //.DecodeIpnsHyperSphereVGaCenter(cgaGeometricSpace)
            : cgaGeometricSpace.ZeroVectorBlade;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade DecodeIpnsFlatVGaPosition(this CGaFloat64Blade ipnsFlat)
    {
        return ipnsFlat.DecodeIpnsFlatVGaPosition(
            ipnsFlat.GeometricSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade DecodeIpnsFlatVGaPosition(this CGaFloat64Blade ipnsFlat, LinFloat64Vector2D egaProbePoint)
    {
        return ipnsFlat.DecodeIpnsFlatVGaPosition(
            ipnsFlat.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade DecodeIpnsFlatVGaPosition(this CGaFloat64Blade ipnsFlat, LinFloat64Vector3D egaProbePoint)
    {
        return ipnsFlat.DecodeIpnsFlatVGaPosition(
            ipnsFlat.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade DecodeIpnsFlatVGaPosition(this CGaFloat64Blade ipnsFlat, LinFloat64Vector egaProbePoint)
    {
        return ipnsFlat.DecodeIpnsFlatVGaPosition(
            ipnsFlat.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade DecodeIpnsFlatVGaPosition(this CGaFloat64Blade ipnsFlat, RGaFloat64Vector egaProbePoint)
    {
        return ipnsFlat.DecodeIpnsFlatVGaPosition(
            ipnsFlat.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade DecodeIpnsFlatVGaPosition(this CGaFloat64Blade ipnsFlat, CGaFloat64Blade egaProbePointBlade)
    {
        return egaProbePointBlade
            .VGaVectorToIpnsPoint()
            .Op(ipnsFlat)
            .Gp(ipnsFlat.Inverse())
            .VectorPartToConformalVGaBlade(ipnsFlat.GeometricSpace);
        //.DecodeIpnsHyperSphereVGaCenter(egaProbePointBlade.ConformalSpace);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeIpnsHyperPlaneWeight(this CGaFloat64Blade ipnsFlat)
    {
        var eoScalar = ipnsFlat[0] + ipnsFlat[1];

        return eoScalar.IsNearZero()
            ? ipnsFlat.GetVGaVectorPart().Norm()
            : 0d;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeIpnsFlatWeight(this CGaFloat64Blade ipnsFlat)
    {
        return ipnsFlat.DecodeIpnsFlatWeight(
            ipnsFlat.GeometricSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeIpnsFlatWeight(this CGaFloat64Blade ipnsFlat, LinFloat64Vector2D egaProbePoint)
    {
        return ipnsFlat.DecodeIpnsFlatWeight(
            ipnsFlat.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeIpnsFlatWeight(this CGaFloat64Blade ipnsFlat, LinFloat64Vector3D egaProbePoint)
    {
        return ipnsFlat.DecodeIpnsFlatWeight(
            ipnsFlat.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeIpnsFlatWeight(this CGaFloat64Blade ipnsFlat, LinFloat64Vector egaProbePoint)
    {
        return ipnsFlat.DecodeIpnsFlatWeight(
            ipnsFlat.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeIpnsFlatWeight(this CGaFloat64Blade ipnsFlat, RGaFloat64Vector egaProbePoint)
    {
        return ipnsFlat.DecodeIpnsFlatWeight(
            ipnsFlat.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeIpnsFlatWeight(this CGaFloat64Blade ipnsFlat, CGaFloat64Blade egaProbePoint)
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