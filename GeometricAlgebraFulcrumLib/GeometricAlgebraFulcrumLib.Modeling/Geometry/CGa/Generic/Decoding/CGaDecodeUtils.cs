using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Elements;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Decoding;

public static class CGaDecodeUtils
{
    public static CGaElement<T> DecodeElement<T>(this CGaBlade<T> blade, CGaElementSpecs<T> specs)
    {
        return blade.DecodeElement(
            blade.GeometricSpace.ZeroVectorBlade,
            specs
        );
    }

    public static CGaElement<T> DecodeElement<T>(this CGaBlade<T> blade, CGaBlade<T> egaProbePoint, CGaElementSpecs<T> specs)
    {
        if (specs.Encoding == CGaElementEncoding.VGa)
            return specs.Kind switch
            {
                CGaElementKind.Direction =>
                    blade.DecodeVGaDirection(),

                _ => throw new InvalidOperationException()
            };

        if (specs.Encoding == CGaElementEncoding.PGa)
            return specs.Kind switch
            {
                CGaElementKind.Flat =>
                    blade.DecodePGaFlat(egaProbePoint),

                _ => throw new InvalidOperationException()
            };

        if (specs.Encoding == CGaElementEncoding.Opns)
            return specs.Kind switch
            {
                CGaElementKind.Direction =>
                    blade.DecodeOpnsDirection(egaProbePoint),

                CGaElementKind.Tangent =>
                    blade.DecodeOpnsTangent(egaProbePoint),

                CGaElementKind.Flat =>
                    blade.DecodeOpnsFlat(egaProbePoint),

                CGaElementKind.Round =>
                    blade.DecodeOpnsRound(egaProbePoint),

                _ => throw new InvalidOperationException()
            };

        if (specs.Encoding == CGaElementEncoding.Ipns)
            return specs.Kind switch
            {
                CGaElementKind.Direction =>
                    blade.DecodeIpnsDirection(egaProbePoint),

                CGaElementKind.Tangent =>
                    blade.DecodeIpnsTangent(egaProbePoint),

                CGaElementKind.Flat =>
                    blade.DecodeIpnsFlat(egaProbePoint),

                CGaElementKind.Round =>
                    blade.DecodeIpnsRound(egaProbePoint),

                _ => throw new InvalidOperationException()
            };

        throw new InvalidOperationException();
    }
}