using GeometricAlgebraFulcrumLib.Modeling.Geometry.Projective.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Projective.Elements;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Projective.Decoding;

public static class XGaProjectiveDecodeUtils
{
    public static XGaProjectiveElement<T> DecodeElement<T>(this XGaProjectiveBlade<T> blade, XGaProjectiveElementSpecs<T> specs)
    {
        return blade.DecodeElement(
            blade.ProjectiveSpace.ZeroVectorBlade,
            specs
        );
    }

    public static XGaProjectiveElement<T> DecodeElement<T>(this XGaProjectiveBlade<T> blade, XGaProjectiveBlade<T> egaProbePoint, XGaProjectiveElementSpecs<T> specs)
    {
        if (specs.Encoding == XGaProjectiveElementEncoding.EGa)
            return specs.Kind switch
            {
                XGaProjectiveElementKind.Ideal => 
                    blade.DecodeEGaDirection(),

                _ => throw new InvalidOperationException()
            };

        if (specs.Encoding == XGaProjectiveElementEncoding.PGa)
            return specs.Kind switch
            {
                XGaProjectiveElementKind.Ideal => 
                    blade.DecodeDirection(egaProbePoint),

                XGaProjectiveElementKind.Euclidean => 
                    blade.DecodePGaElement(egaProbePoint),

                _ => throw new InvalidOperationException()
            };
        
        throw new InvalidOperationException();
    }
}