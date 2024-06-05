using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Elements;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Decoding;

public static class RGaConformalDecodeIpnsElementUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalElement DecodeIpnsElement(this RGaConformalBlade cgaBlade)
    {
        var specs =
            cgaBlade.GetElementSpecs();

        return specs.Kind switch
        {
            RGaConformalElementKind.Direction =>
                cgaBlade.DecodeIpnsDirection(),

            RGaConformalElementKind.Tangent =>
                cgaBlade.DecodeIpnsTangent(),

            RGaConformalElementKind.Flat =>
                specs.Encoding == RGaConformalElementEncoding.Opns
                    ? cgaBlade.DecodeOpnsFlat()
                    : cgaBlade.DecodeIpnsFlat(),

            RGaConformalElementKind.Round =>
                cgaBlade.DecodeIpnsRound(),

            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeIpnsElementWeight(this RGaConformalBlade cgaBlade)
    {
        var specs =
            cgaBlade.GetElementSpecs();

        return specs.Kind switch
        {
            RGaConformalElementKind.Direction =>
                cgaBlade.DecodeIpnsDirectionWeight(),

            RGaConformalElementKind.Tangent =>
                cgaBlade.DecodeIpnsTangentWeight(),

            RGaConformalElementKind.Flat =>
                specs.Encoding == RGaConformalElementEncoding.Opns
                    ? cgaBlade.DecodeOpnsFlatWeight()
                    : cgaBlade.DecodeIpnsFlatWeight(),

            RGaConformalElementKind.Round =>
                cgaBlade.DecodeIpnsRoundWeight(),

            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade DecodeIpnsElementEGaDirection(this RGaConformalBlade cgaBlade)
    {
        var specs =
            cgaBlade.GetElementSpecs();

        return specs.Kind switch
        {
            RGaConformalElementKind.Direction =>
                cgaBlade.DecodeIpnsDirectionEGaDirection(),

            RGaConformalElementKind.Tangent =>
                cgaBlade.DecodeIpnsTangentEGaDirection(),

            RGaConformalElementKind.Flat =>
                specs.Encoding == RGaConformalElementEncoding.Opns
                    ? cgaBlade.DecodeOpnsFlatEGaDirection()
                    : cgaBlade.DecodeIpnsFlatEGaDirection(),

            RGaConformalElementKind.Round =>
                cgaBlade.DecodeIpnsRoundEGaDirection(),

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