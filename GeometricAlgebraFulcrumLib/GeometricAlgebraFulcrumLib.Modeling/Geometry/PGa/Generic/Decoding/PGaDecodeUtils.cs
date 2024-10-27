using GeometricAlgebraFulcrumLib.Modeling.Geometry.PGa.Generic.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.PGa.Generic.Elements;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.PGa.Generic.Decoding;

public static class PGaDecodeUtils
{
    public static PGaElement<T> DecodeElement<T>(this PGaBlade<T> blade, PGaElementSpecs<T> specs)
    {
        return blade.DecodeElement(
            blade.GeometricSpace.ZeroVectorBlade,
            specs
        );
    }

    public static PGaElement<T> DecodeElement<T>(this PGaBlade<T> blade, PGaBlade<T> egaProbePoint, PGaElementSpecs<T> specs)
    {
        if (specs.Encoding == PGaElementEncoding.VGa)
            return specs.Kind switch
            {
                PGaElementKind.Ideal =>
                    blade.DecodeVGaDirection(),

                _ => throw new InvalidOperationException()
            };

        if (specs.Encoding == PGaElementEncoding.PGa)
            return specs.Kind switch
            {
                PGaElementKind.Ideal =>
                    blade.DecodeDirection(egaProbePoint),

                PGaElementKind.Euclidean =>
                    blade.DecodePGaElement(egaProbePoint),

                _ => throw new InvalidOperationException()
            };

        throw new InvalidOperationException();
    }
}