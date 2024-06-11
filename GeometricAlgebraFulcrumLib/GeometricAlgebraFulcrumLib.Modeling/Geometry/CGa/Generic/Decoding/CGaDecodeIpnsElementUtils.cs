using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Elements;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Decoding;

public static class CGaDecodeIpnsElementUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaElement<T> DecodeIpnsElement<T>(this CGaBlade<T> cgaBlade)
    {
        var specs =
            cgaBlade.GetElementSpecs();

        return specs.Kind switch
        {
            CGaElementKind.Direction =>
                cgaBlade.DecodeIpnsDirection(),

            CGaElementKind.Tangent =>
                cgaBlade.DecodeIpnsTangent(),

            CGaElementKind.Flat =>
                specs.Encoding == CGaElementEncoding.Opns
                    ? cgaBlade.DecodeOpnsFlat()
                    : cgaBlade.DecodeIpnsFlat(),

            CGaElementKind.Round =>
                cgaBlade.DecodeIpnsRound(),

            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeIpnsElementWeight<T>(this CGaBlade<T> cgaBlade)
    {
        var specs =
            cgaBlade.GetElementSpecs();

        return specs.Kind switch
        {
            CGaElementKind.Direction =>
                cgaBlade.DecodeIpnsDirectionWeight(),

            CGaElementKind.Tangent =>
                cgaBlade.DecodeIpnsTangentWeight(),

            CGaElementKind.Flat =>
                specs.Encoding == CGaElementEncoding.Opns
                    ? cgaBlade.DecodeOpnsFlatWeight()
                    : cgaBlade.DecodeIpnsFlatWeight(),

            CGaElementKind.Round =>
                cgaBlade.DecodeIpnsRoundWeight(),

            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> DecodeIpnsElementVGaDirection<T>(this CGaBlade<T> cgaBlade)
    {
        var specs =
            cgaBlade.GetElementSpecs();

        return specs.Kind switch
        {
            CGaElementKind.Direction =>
                cgaBlade.DecodeIpnsDirectionVGaDirection(),

            CGaElementKind.Tangent =>
                cgaBlade.DecodeIpnsTangentVGaDirection(),

            CGaElementKind.Flat =>
                specs.Encoding == CGaElementEncoding.Opns
                    ? cgaBlade.DecodeOpnsFlatVGaDirection()
                    : cgaBlade.DecodeIpnsFlatVGaDirection(),

            CGaElementKind.Round =>
                cgaBlade.DecodeIpnsRoundVGaDirection(),

            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> DecodeIpnsElementVGaDirectionNormal<T>(this CGaBlade<T> cgaBlade)
    {
        return cgaBlade.DecodeIpnsElementVGaDirection().VGaNormal();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> DecodeIpnsElementVGaPosition<T>(this CGaBlade<T> cgaBlade)
    {
        var specs =
            cgaBlade.GetElementSpecs();

        return specs.Kind switch
        {
            CGaElementKind.Direction =>
                cgaBlade.GeometricSpace.ZeroVectorBlade,

            CGaElementKind.Tangent =>
                cgaBlade.DecodeIpnsTangentVGaPosition(),

            CGaElementKind.Flat =>
                specs.Encoding == CGaElementEncoding.Opns
                    ? cgaBlade.DecodeOpnsFlatVGaPosition()
                    : cgaBlade.DecodeIpnsFlatVGaPosition(),

            CGaElementKind.Round =>
                cgaBlade.DecodeIpnsRoundVGaCenter(),

            _ => throw new InvalidOperationException()
        };
    }
}