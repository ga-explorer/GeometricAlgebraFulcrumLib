using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Elements;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Decoding;

public static class CGaDecodeOpnsElementUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaElement<T> DecodeOpnsElement<T>(this CGaBlade<T> cgaBlade)
    {
        var specs =
            cgaBlade.GetElementSpecs();

        return specs.Kind switch
        {
            CGaElementKind.Direction =>
                cgaBlade.DecodeOpnsDirection(),

            CGaElementKind.Tangent =>
                cgaBlade.DecodeOpnsTangent(),

            CGaElementKind.Flat =>
                specs.Encoding == CGaElementEncoding.Opns
                    ? cgaBlade.DecodeOpnsFlat()
                    : cgaBlade.DecodeIpnsFlat(),

            CGaElementKind.Round =>
                cgaBlade.DecodeOpnsRound(),

            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeOpnsElementWeight<T>(this CGaBlade<T> cgaBlade)
    {
        var specs =
            cgaBlade.GetElementSpecs();

        return specs.Kind switch
        {
            CGaElementKind.Direction =>
                cgaBlade.DecodeOpnsDirectionWeight(),

            CGaElementKind.Tangent =>
                cgaBlade.DecodeOpnsTangentWeight(),

            CGaElementKind.Flat =>
                specs.Encoding == CGaElementEncoding.Opns
                    ? cgaBlade.DecodeOpnsFlatWeight()
                    : cgaBlade.DecodeIpnsFlatWeight(),

            CGaElementKind.Round =>
                cgaBlade.DecodeOpnsRoundWeight(),

            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> DecodeOpnsElementVGaDirection<T>(this CGaBlade<T> cgaBlade)
    {
        var specs =
            cgaBlade.GetElementSpecs();

        return specs.Kind switch
        {
            CGaElementKind.Direction =>
                cgaBlade.DecodeOpnsDirectionVGaDirection(),

            CGaElementKind.Tangent =>
                cgaBlade.DecodeOpnsTangentVGaDirection(),

            CGaElementKind.Flat =>
                specs.Encoding == CGaElementEncoding.Opns
                    ? cgaBlade.DecodeOpnsFlatVGaDirection()
                    : cgaBlade.DecodeIpnsFlatVGaDirection(),

            CGaElementKind.Round =>
                cgaBlade.DecodeOpnsRoundVGaDirection(),

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