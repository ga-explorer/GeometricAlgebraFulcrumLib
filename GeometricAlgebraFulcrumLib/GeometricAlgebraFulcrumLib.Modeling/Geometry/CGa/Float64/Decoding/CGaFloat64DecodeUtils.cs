using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Elements;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Decoding;

public static class CGaFloat64DecodeUtils
{
    public static CGaFloat64Element DecodeElement(this CGaFloat64Blade blade, CGaFloat64ElementSpecs specs)
    {
        return blade.DecodeElement(
            blade.GeometricSpace.ZeroVectorBlade,
            specs
        );
    }

    public static CGaFloat64Element DecodeElement(this CGaFloat64Blade blade, CGaFloat64Blade egaProbePoint, CGaFloat64ElementSpecs specs)
    {
        if (specs.Encoding == CGaFloat64ElementEncoding.VGa)
            return specs.Kind switch
            {
                CGaFloat64ElementKind.Direction =>
                    blade.DecodeVGaDirection(),

                _ => throw new InvalidOperationException()
            };

        if (specs.Encoding == CGaFloat64ElementEncoding.PGa)
            return specs.Kind switch
            {
                CGaFloat64ElementKind.Flat =>
                    blade.DecodePGaFlat(egaProbePoint),

                _ => throw new InvalidOperationException()
            };

        if (specs.Encoding == CGaFloat64ElementEncoding.Opns)
            return specs.Kind switch
            {
                CGaFloat64ElementKind.Direction =>
                    blade.DecodeOpnsDirection(egaProbePoint),

                CGaFloat64ElementKind.Tangent =>
                    blade.DecodeOpnsTangent(egaProbePoint),

                CGaFloat64ElementKind.Flat =>
                    blade.DecodeOpnsFlat(egaProbePoint),

                CGaFloat64ElementKind.Round =>
                    blade.DecodeOpnsRound(egaProbePoint),

                _ => throw new InvalidOperationException()
            };

        if (specs.Encoding == CGaFloat64ElementEncoding.Ipns)
            return specs.Kind switch
            {
                CGaFloat64ElementKind.Direction =>
                    blade.DecodeIpnsDirection(egaProbePoint),

                CGaFloat64ElementKind.Tangent =>
                    blade.DecodeIpnsTangent(egaProbePoint),

                CGaFloat64ElementKind.Flat =>
                    blade.DecodeIpnsFlat(egaProbePoint),

                CGaFloat64ElementKind.Round =>
                    blade.DecodeIpnsRound(egaProbePoint),

                _ => throw new InvalidOperationException()
            };

        throw new InvalidOperationException();
    }
}