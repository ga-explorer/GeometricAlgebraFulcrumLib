using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Elements;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Decoding;

public static class RGaConformalDecodeUtils
{
    public static RGaConformalElement DecodeElement(this RGaConformalBlade blade, RGaConformalElementSpecs specs)
    {
        return blade.DecodeElement(
            blade.ConformalSpace.ZeroVectorBlade,
            specs
        );
    }

    public static RGaConformalElement DecodeElement(this RGaConformalBlade blade, RGaConformalBlade egaProbePoint, RGaConformalElementSpecs specs)
    {
        if (specs.Encoding == RGaConformalElementEncoding.EGa)
            return specs.Kind switch
            {
                RGaConformalElementKind.Direction => 
                    blade.DecodeEGaDirection(),

                _ => throw new InvalidOperationException()
            };

        if (specs.Encoding == RGaConformalElementEncoding.PGa)
            return specs.Kind switch
            {
                RGaConformalElementKind.Flat => 
                    blade.DecodePGaFlat(egaProbePoint),

                _ => throw new InvalidOperationException()
            };

        if (specs.Encoding == RGaConformalElementEncoding.Opns)
            return specs.Kind switch
            {
                RGaConformalElementKind.Direction => 
                    blade.DecodeOpnsDirection(egaProbePoint),

                RGaConformalElementKind.Tangent => 
                    blade.DecodeOpnsTangent(egaProbePoint),

                RGaConformalElementKind.Flat => 
                    blade.DecodeOpnsFlat(egaProbePoint),

                RGaConformalElementKind.Round => 
                    blade.DecodeOpnsRound(egaProbePoint),

                _ => throw new InvalidOperationException()
            };
        
        if (specs.Encoding == RGaConformalElementEncoding.Ipns)
            return specs.Kind switch
            {
                RGaConformalElementKind.Direction => 
                    blade.DecodeIpnsDirection(egaProbePoint),

                RGaConformalElementKind.Tangent => 
                    blade.DecodeIpnsTangent(egaProbePoint),

                RGaConformalElementKind.Flat => 
                    blade.DecodeIpnsFlat(egaProbePoint),

                RGaConformalElementKind.Round => 
                    blade.DecodeIpnsRound(egaProbePoint),

                _ => throw new InvalidOperationException()
            };

        throw new InvalidOperationException();
    }
}