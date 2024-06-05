using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Elements;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Decoding;

public static class XGaConformalDecodeIpnsElementUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalElement<T> DecodeIpnsElement<T>(this XGaConformalBlade<T> cgaBlade)
    {
        var specs =
            cgaBlade.GetElementSpecs();

        return specs.Kind switch
        {
            XGaConformalElementKind.Direction =>
                cgaBlade.DecodeIpnsDirection(),

            XGaConformalElementKind.Tangent =>
                cgaBlade.DecodeIpnsTangent(),

            XGaConformalElementKind.Flat =>
                specs.Encoding == XGaConformalElementEncoding.Opns
                    ? cgaBlade.DecodeOpnsFlat()
                    : cgaBlade.DecodeIpnsFlat(),

            XGaConformalElementKind.Round =>
                cgaBlade.DecodeIpnsRound(),

            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeIpnsElementWeight<T>(this XGaConformalBlade<T> cgaBlade)
    {
        var specs =
            cgaBlade.GetElementSpecs();

        return specs.Kind switch
        {
            XGaConformalElementKind.Direction =>
                cgaBlade.DecodeIpnsDirectionWeight(),

            XGaConformalElementKind.Tangent =>
                cgaBlade.DecodeIpnsTangentWeight(),

            XGaConformalElementKind.Flat =>
                specs.Encoding == XGaConformalElementEncoding.Opns
                    ? cgaBlade.DecodeOpnsFlatWeight()
                    : cgaBlade.DecodeIpnsFlatWeight(),

            XGaConformalElementKind.Round =>
                cgaBlade.DecodeIpnsRoundWeight(),

            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> DecodeIpnsElementEGaDirection<T>(this XGaConformalBlade<T> cgaBlade)
    {
        var specs =
            cgaBlade.GetElementSpecs();

        return specs.Kind switch
        {
            XGaConformalElementKind.Direction =>
                cgaBlade.DecodeIpnsDirectionEGaDirection(),

            XGaConformalElementKind.Tangent =>
                cgaBlade.DecodeIpnsTangentEGaDirection(),

            XGaConformalElementKind.Flat =>
                specs.Encoding == XGaConformalElementEncoding.Opns
                    ? cgaBlade.DecodeOpnsFlatEGaDirection()
                    : cgaBlade.DecodeIpnsFlatEGaDirection(),

            XGaConformalElementKind.Round =>
                cgaBlade.DecodeIpnsRoundEGaDirection(),

            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> DecodeIpnsElementEGaDirectionNormal<T>(this XGaConformalBlade<T> cgaBlade)
    {
        return cgaBlade.DecodeIpnsElementEGaDirection().EGaNormal();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> DecodeIpnsElementEGaPosition<T>(this XGaConformalBlade<T> cgaBlade)
    {
        var specs =
            cgaBlade.GetElementSpecs();

        return specs.Kind switch
        {
            XGaConformalElementKind.Direction =>
                cgaBlade.ConformalSpace.ZeroVectorBlade,

            XGaConformalElementKind.Tangent =>
                cgaBlade.DecodeIpnsTangentEGaPosition(),

            XGaConformalElementKind.Flat =>
                specs.Encoding == XGaConformalElementEncoding.Opns
                    ? cgaBlade.DecodeOpnsFlatEGaPosition()
                    : cgaBlade.DecodeIpnsFlatEGaPosition(),

            XGaConformalElementKind.Round =>
                cgaBlade.DecodeIpnsRoundEGaCenter(),

            _ => throw new InvalidOperationException()
        };
    }
}