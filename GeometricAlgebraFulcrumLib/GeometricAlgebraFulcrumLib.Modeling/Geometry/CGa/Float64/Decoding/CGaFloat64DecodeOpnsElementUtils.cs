using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Elements;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Decoding;

public static class CGaFloat64DecodeOpnsElementUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Element DecodeOpnsElement(this CGaFloat64Blade cgaBlade)
    {
        var specs =
            cgaBlade.GetElementSpecs();

        return specs.Kind switch
        {
            CGaFloat64ElementKind.Direction =>
                cgaBlade.DecodeOpnsDirection(),

            CGaFloat64ElementKind.Tangent =>
                cgaBlade.DecodeOpnsTangent(),

            CGaFloat64ElementKind.Flat =>
                specs.Encoding == CGaFloat64ElementEncoding.Opns
                    ? cgaBlade.DecodeOpnsFlat()
                    : cgaBlade.DecodeIpnsFlat(),

            CGaFloat64ElementKind.Round =>
                cgaBlade.DecodeOpnsRound(),

            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeOpnsElementWeight(this CGaFloat64Blade cgaBlade)
    {
        var specs =
            cgaBlade.GetElementSpecs();

        return specs.Kind switch
        {
            CGaFloat64ElementKind.Direction =>
                cgaBlade.DecodeOpnsDirectionWeight(),

            CGaFloat64ElementKind.Tangent =>
                cgaBlade.DecodeOpnsTangentWeight(),

            CGaFloat64ElementKind.Flat =>
                specs.Encoding == CGaFloat64ElementEncoding.Opns
                    ? cgaBlade.DecodeOpnsFlatWeight()
                    : cgaBlade.DecodeIpnsFlatWeight(),

            CGaFloat64ElementKind.Round =>
                cgaBlade.DecodeOpnsRoundWeight(),

            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade DecodeOpnsElementVGaDirection(this CGaFloat64Blade cgaBlade)
    {
        var specs =
            cgaBlade.GetElementSpecs();

        return specs.Kind switch
        {
            CGaFloat64ElementKind.Direction =>
                cgaBlade.DecodeOpnsDirectionVGaDirection(),

            CGaFloat64ElementKind.Tangent =>
                cgaBlade.DecodeOpnsTangentVGaDirection(),

            CGaFloat64ElementKind.Flat =>
                specs.Encoding == CGaFloat64ElementEncoding.Opns
                    ? cgaBlade.DecodeOpnsFlatVGaDirection()
                    : cgaBlade.DecodeIpnsFlatVGaDirection(),

            CGaFloat64ElementKind.Round =>
                cgaBlade.DecodeOpnsRoundVGaDirection(),

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