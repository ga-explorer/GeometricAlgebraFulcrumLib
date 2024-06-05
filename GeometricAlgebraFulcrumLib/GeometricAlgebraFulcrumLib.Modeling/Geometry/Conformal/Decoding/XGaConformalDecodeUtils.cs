using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Elements;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Decoding;

public static class XGaConformalDecodeUtils
{
    public static XGaConformalElement<T> DecodeElement<T>(this XGaConformalBlade<T> blade, XGaConformalElementSpecs<T> specs)
    {
        return blade.DecodeElement(
            blade.ConformalSpace.ZeroVectorBlade,
            specs
        );
    }

    public static XGaConformalElement<T> DecodeElement<T>(this XGaConformalBlade<T> blade, XGaConformalBlade<T> egaProbePoint, XGaConformalElementSpecs<T> specs)
    {
        if (specs.Encoding == XGaConformalElementEncoding.EGa)
            return specs.Kind switch
            {
                XGaConformalElementKind.Direction => 
                    blade.DecodeEGaDirection(),

                _ => throw new InvalidOperationException()
            };

        if (specs.Encoding == XGaConformalElementEncoding.PGa)
            return specs.Kind switch
            {
                XGaConformalElementKind.Flat => 
                    blade.DecodePGaFlat(egaProbePoint),

                _ => throw new InvalidOperationException()
            };

        if (specs.Encoding == XGaConformalElementEncoding.Opns)
            return specs.Kind switch
            {
                XGaConformalElementKind.Direction => 
                    blade.DecodeOpnsDirection(egaProbePoint),

                XGaConformalElementKind.Tangent => 
                    blade.DecodeOpnsTangent(egaProbePoint),

                XGaConformalElementKind.Flat => 
                    blade.DecodeOpnsFlat(egaProbePoint),

                XGaConformalElementKind.Round => 
                    blade.DecodeOpnsRound(egaProbePoint),

                _ => throw new InvalidOperationException()
            };
        
        if (specs.Encoding == XGaConformalElementEncoding.Ipns)
            return specs.Kind switch
            {
                XGaConformalElementKind.Direction => 
                    blade.DecodeIpnsDirection(egaProbePoint),

                XGaConformalElementKind.Tangent => 
                    blade.DecodeIpnsTangent(egaProbePoint),

                XGaConformalElementKind.Flat => 
                    blade.DecodeIpnsFlat(egaProbePoint),

                XGaConformalElementKind.Round => 
                    blade.DecodeIpnsRound(egaProbePoint),

                _ => throw new InvalidOperationException()
            };

        throw new InvalidOperationException();
    }
}