using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Elements;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Decoding;

public static class RGaConformalDecodeOpnsElementUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalElement DecodeOpnsElement(this RGaConformalBlade cgaBlade)
    {
        var specs =
            cgaBlade.GetElementSpecs();

        return specs.Kind switch
        {
            RGaConformalElementKind.Direction =>
                cgaBlade.DecodeOpnsDirection(),

            RGaConformalElementKind.Tangent =>
                cgaBlade.DecodeOpnsTangent(),

            RGaConformalElementKind.Flat =>
                specs.Encoding == RGaConformalElementEncoding.Opns
                    ? cgaBlade.DecodeOpnsFlat()
                    : cgaBlade.DecodeIpnsFlat(),

            RGaConformalElementKind.Round =>
                cgaBlade.DecodeOpnsRound(),

            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeOpnsElementWeight(this RGaConformalBlade cgaBlade)
    {
        var specs =
            cgaBlade.GetElementSpecs();

        return specs.Kind switch
        {
            RGaConformalElementKind.Direction =>
                cgaBlade.DecodeOpnsDirectionWeight(),

            RGaConformalElementKind.Tangent =>
                cgaBlade.DecodeOpnsTangentWeight(),

            RGaConformalElementKind.Flat =>
                specs.Encoding == RGaConformalElementEncoding.Opns
                    ? cgaBlade.DecodeOpnsFlatWeight()
                    : cgaBlade.DecodeIpnsFlatWeight(),

            RGaConformalElementKind.Round =>
                cgaBlade.DecodeOpnsRoundWeight(),

            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade DecodeOpnsElementEGaDirection(this RGaConformalBlade cgaBlade)
    {
        var specs =
            cgaBlade.GetElementSpecs();

        return specs.Kind switch
        {
            RGaConformalElementKind.Direction =>
                cgaBlade.DecodeOpnsDirectionEGaDirection(),

            RGaConformalElementKind.Tangent =>
                cgaBlade.DecodeOpnsTangentEGaDirection(),

            RGaConformalElementKind.Flat =>
                specs.Encoding == RGaConformalElementEncoding.Opns
                    ? cgaBlade.DecodeOpnsFlatEGaDirection()
                    : cgaBlade.DecodeIpnsFlatEGaDirection(),

            RGaConformalElementKind.Round =>
                cgaBlade.DecodeOpnsRoundEGaDirection(),

            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade DecodeIpnsElementEGaDirectionNormal(this RGaConformalBlade cgaBlade)
    {
        return cgaBlade.DecodeIpnsElementEGaDirection().EGaNormal();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade DecodeIpnsElementEGaPosition(this RGaConformalBlade cgaBlade)
    {
        var specs =
            cgaBlade.GetElementSpecs();

        return specs.Kind switch
        {
            RGaConformalElementKind.Direction =>
                cgaBlade.ConformalSpace.ZeroVectorBlade,

            RGaConformalElementKind.Tangent =>
                cgaBlade.DecodeIpnsTangentEGaPosition(),

            RGaConformalElementKind.Flat =>
                specs.Encoding == RGaConformalElementEncoding.Opns
                    ? cgaBlade.DecodeOpnsFlatEGaPosition()
                    : cgaBlade.DecodeIpnsFlatEGaPosition(),

            RGaConformalElementKind.Round =>
                cgaBlade.DecodeIpnsRoundEGaCenter(),

            _ => throw new InvalidOperationException()
        };
    }
}