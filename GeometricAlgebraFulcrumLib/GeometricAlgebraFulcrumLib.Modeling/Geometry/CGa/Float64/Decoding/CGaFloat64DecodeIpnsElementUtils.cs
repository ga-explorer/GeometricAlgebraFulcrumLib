using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Elements;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Decoding;

public static class CGaFloat64DecodeIpnsElementUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Element DecodeIpnsElement(this CGaFloat64Blade cgaBlade)
    {
        var specs =
            cgaBlade.GetElementSpecs();

        return specs.Kind switch
        {
            CGaFloat64ElementKind.Direction =>
                cgaBlade.DecodeIpnsDirection(),

            CGaFloat64ElementKind.Tangent =>
                cgaBlade.DecodeIpnsTangent(),

            CGaFloat64ElementKind.Flat =>
                specs.Encoding == CGaFloat64ElementEncoding.Opns
                    ? cgaBlade.DecodeOpnsFlat()
                    : cgaBlade.DecodeIpnsFlat(),

            CGaFloat64ElementKind.Round =>
                cgaBlade.DecodeIpnsRound(),

            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeIpnsElementWeight(this CGaFloat64Blade cgaBlade)
    {
        var specs =
            cgaBlade.GetElementSpecs();

        return specs.Kind switch
        {
            CGaFloat64ElementKind.Direction =>
                cgaBlade.DecodeIpnsDirectionWeight(),

            CGaFloat64ElementKind.Tangent =>
                cgaBlade.DecodeIpnsTangentWeight(),

            CGaFloat64ElementKind.Flat =>
                specs.Encoding == CGaFloat64ElementEncoding.Opns
                    ? cgaBlade.DecodeOpnsFlatWeight()
                    : cgaBlade.DecodeIpnsFlatWeight(),

            CGaFloat64ElementKind.Round =>
                cgaBlade.DecodeIpnsRoundWeight(),

            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade DecodeIpnsElementVGaDirection(this CGaFloat64Blade cgaBlade)
    {
        var specs =
            cgaBlade.GetElementSpecs();

        return specs.Kind switch
        {
            CGaFloat64ElementKind.Direction =>
                cgaBlade.DecodeIpnsDirectionVGaDirection(),

            CGaFloat64ElementKind.Tangent =>
                cgaBlade.DecodeIpnsTangentVGaDirection(),

            CGaFloat64ElementKind.Flat =>
                specs.Encoding == CGaFloat64ElementEncoding.Opns
                    ? cgaBlade.DecodeOpnsFlatVGaDirection()
                    : cgaBlade.DecodeIpnsFlatVGaDirection(),

            CGaFloat64ElementKind.Round =>
                cgaBlade.DecodeIpnsRoundVGaDirection(),

            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade DecodeIpnsElementVGaDirectionNormal(this CGaFloat64Blade cgaBlade)
    {
        return cgaBlade.DecodeIpnsElementVGaDirection().VGaNormal();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade DecodeIpnsElementVGaPosition(this CGaFloat64Blade cgaBlade)
    {
        var specs =
            cgaBlade.GetElementSpecs();

        return specs.Kind switch
        {
            CGaFloat64ElementKind.Direction =>
                cgaBlade.GeometricSpace.ZeroVectorBlade,

            CGaFloat64ElementKind.Tangent =>
                cgaBlade.DecodeIpnsTangentVGaPosition(),

            CGaFloat64ElementKind.Flat =>
                specs.Encoding == CGaFloat64ElementEncoding.Opns
                    ? cgaBlade.DecodeOpnsFlatVGaPosition()
                    : cgaBlade.DecodeIpnsFlatVGaPosition(),

            CGaFloat64ElementKind.Round =>
                cgaBlade.DecodeIpnsRoundVGaCenter(),

            _ => throw new InvalidOperationException()
        };
    }
}