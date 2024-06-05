using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Elements;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Decoding;

public static class XGaConformalDecodeOpnsElementUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalElement<T> DecodeOpnsElement<T>(this XGaConformalBlade<T> cgaBlade)
    {
        var specs =
            cgaBlade.GetElementSpecs();

        return specs.Kind switch
        {
            XGaConformalElementKind.Direction =>
                cgaBlade.DecodeOpnsDirection(),

            XGaConformalElementKind.Tangent =>
                cgaBlade.DecodeOpnsTangent(),

            XGaConformalElementKind.Flat =>
                specs.Encoding == XGaConformalElementEncoding.Opns
                    ? cgaBlade.DecodeOpnsFlat()
                    : cgaBlade.DecodeIpnsFlat(),

            XGaConformalElementKind.Round =>
                cgaBlade.DecodeOpnsRound(),

            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeOpnsElementWeight<T>(this XGaConformalBlade<T> cgaBlade)
    {
        var specs =
            cgaBlade.GetElementSpecs();

        return specs.Kind switch
        {
            XGaConformalElementKind.Direction =>
                cgaBlade.DecodeOpnsDirectionWeight(),

            XGaConformalElementKind.Tangent =>
                cgaBlade.DecodeOpnsTangentWeight(),

            XGaConformalElementKind.Flat =>
                specs.Encoding == XGaConformalElementEncoding.Opns
                    ? cgaBlade.DecodeOpnsFlatWeight()
                    : cgaBlade.DecodeIpnsFlatWeight(),

            XGaConformalElementKind.Round =>
                cgaBlade.DecodeOpnsRoundWeight(),

            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> DecodeOpnsElementEGaDirection<T>(this XGaConformalBlade<T> cgaBlade)
    {
        var specs =
            cgaBlade.GetElementSpecs();

        return specs.Kind switch
        {
            XGaConformalElementKind.Direction =>
                cgaBlade.DecodeOpnsDirectionEGaDirection(),

            XGaConformalElementKind.Tangent =>
                cgaBlade.DecodeOpnsTangentEGaDirection(),

            XGaConformalElementKind.Flat =>
                specs.Encoding == XGaConformalElementEncoding.Opns
                    ? cgaBlade.DecodeOpnsFlatEGaDirection()
                    : cgaBlade.DecodeIpnsFlatEGaDirection(),

            XGaConformalElementKind.Round =>
                cgaBlade.DecodeOpnsRoundEGaDirection(),

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