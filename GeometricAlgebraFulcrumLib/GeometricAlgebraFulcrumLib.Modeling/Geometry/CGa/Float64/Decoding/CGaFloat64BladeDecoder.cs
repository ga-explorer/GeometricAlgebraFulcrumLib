using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Elements;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Decoding;

public sealed class CGaFloat64BladeDecoder :
    CGaFloat64BladeDecoderBase
{
    private CGaFloat64VGaDirectionBladeDecoder? _vgaDirectionDecoder;
    public CGaFloat64VGaDirectionBladeDecoder VGaDirection 
        => _vgaDirectionDecoder ??= new CGaFloat64VGaDirectionBladeDecoder(Blade);
    
    private CGaFloat64PGaFlatBladeDecoder? _pgaFlatDecoder;
    public CGaFloat64PGaFlatBladeDecoder PGaFlat 
        => _pgaFlatDecoder ??= new CGaFloat64PGaFlatBladeDecoder(Blade);
    
    private CGaFloat64IpnsDirectionBladeDecoder? _ipnsDirectionDecoder;
    public CGaFloat64IpnsDirectionBladeDecoder IpnsDirection 
        => _ipnsDirectionDecoder ??= new CGaFloat64IpnsDirectionBladeDecoder(Blade);
    
    private CGaFloat64IpnsTangentBladeDecoder? _ipnsTangentDecoder;
    public CGaFloat64IpnsTangentBladeDecoder IpnsTangent 
        => _ipnsTangentDecoder ??= new CGaFloat64IpnsTangentBladeDecoder(Blade);
    
    private CGaFloat64IpnsFlatBladeDecoder? _ipnsFlatDecoder;
    public CGaFloat64IpnsFlatBladeDecoder IpnsFlat 
        => _ipnsFlatDecoder ??= new CGaFloat64IpnsFlatBladeDecoder(Blade);

    private CGaFloat64IpnsRoundBladeDecoder? _ipnsRoundDecoder;
    public CGaFloat64IpnsRoundBladeDecoder IpnsRound 
        => _ipnsRoundDecoder ??= new CGaFloat64IpnsRoundBladeDecoder(Blade);
    
    private CGaFloat64OpnsDirectionBladeDecoder? _opnsDirectionDecoder;
    public CGaFloat64OpnsDirectionBladeDecoder OpnsDirection 
        => _opnsDirectionDecoder ??= new CGaFloat64OpnsDirectionBladeDecoder(Blade);
    
    private CGaFloat64OpnsTangentBladeDecoder? _opnsTangentDecoder;
    public CGaFloat64OpnsTangentBladeDecoder OpnsTangent 
        => _opnsTangentDecoder ??= new CGaFloat64OpnsTangentBladeDecoder(Blade);
    
    private CGaFloat64OpnsFlatBladeDecoder? _opnsFlatDecoder;
    public CGaFloat64OpnsFlatBladeDecoder OpnsFlat 
        => _opnsFlatDecoder ??= new CGaFloat64OpnsFlatBladeDecoder(Blade);

    private CGaFloat64OpnsRoundBladeDecoder? _opnsRoundDecoder;
    public CGaFloat64OpnsRoundBladeDecoder OpnsRound 
        => _opnsRoundDecoder ??= new CGaFloat64OpnsRoundBladeDecoder(Blade);


    internal CGaFloat64BladeDecoder(CGaFloat64Blade blade) 
        : base(blade)
    {
    }


    
    public CGaFloat64Element Element(CGaFloat64ElementSpecs specs)
    {
        return Element(
            Blade.GeometricSpace.ZeroVectorBlade,
            specs
        );
    }

    public CGaFloat64Element Element(CGaFloat64Blade egaProbePoint, CGaFloat64ElementSpecs specs)
    {
        if (specs.Encoding == CGaFloat64ElementEncoding.VGa)
            return specs.Kind switch
            {
                CGaFloat64ElementKind.Direction =>
                    Blade.DecodeVGaDirection.Element(),

                _ => throw new InvalidOperationException()
            };

        if (specs.Encoding == CGaFloat64ElementEncoding.PGa)
            return specs.Kind switch
            {
                CGaFloat64ElementKind.Flat =>
                    Blade.DecodePGaFlat.Element(egaProbePoint),

                _ => throw new InvalidOperationException()
            };

        if (specs.Encoding == CGaFloat64ElementEncoding.Opns)
            return specs.Kind switch
            {
                CGaFloat64ElementKind.Direction =>
                    Blade.DecodeOpnsDirection.Element(egaProbePoint),

                CGaFloat64ElementKind.Tangent =>
                    Blade.DecodeOpnsTangent.Element(egaProbePoint),

                CGaFloat64ElementKind.Flat =>
                    Blade.DecodeOpnsFlat.Element(egaProbePoint),

                CGaFloat64ElementKind.Round =>
                    Blade.DecodeOpnsRound.Element(egaProbePoint),

                _ => throw new InvalidOperationException()
            };

        if (specs.Encoding == CGaFloat64ElementEncoding.Ipns)
            return specs.Kind switch
            {
                CGaFloat64ElementKind.Direction =>
                    Blade.DecodeIpnsDirection.Element(egaProbePoint),

                CGaFloat64ElementKind.Tangent =>
                    Blade.DecodeIpnsTangent.Element(egaProbePoint),

                CGaFloat64ElementKind.Flat =>
                    Blade.DecodeIpnsFlat.Element(egaProbePoint),

                CGaFloat64ElementKind.Round =>
                    Blade.DecodeIpnsRound.Element(egaProbePoint),

                _ => throw new InvalidOperationException()
            };

        throw new InvalidOperationException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Element IpnsElement()
    {
        var specs =
            Blade.GetElementSpecs();

        return specs.Kind switch
        {
            CGaFloat64ElementKind.Direction =>
                Blade.DecodeIpnsDirection.Element(),

            CGaFloat64ElementKind.Tangent =>
                Blade.DecodeIpnsTangent.Element(),

            CGaFloat64ElementKind.Flat =>
                specs.Encoding == CGaFloat64ElementEncoding.Opns
                    ? Blade.DecodeOpnsFlat.Element()
                    : Blade.DecodeIpnsFlat.Element(),

            CGaFloat64ElementKind.Round =>
                Blade.DecodeIpnsRound.Element(),

            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double IpnsElementWeight()
    {
        var specs =
            Blade.GetElementSpecs();

        return specs.Kind switch
        {
            CGaFloat64ElementKind.Direction =>
                Blade.DecodeIpnsDirection.Weight(),

            CGaFloat64ElementKind.Tangent =>
                Blade.DecodeIpnsTangent.Weight(),

            CGaFloat64ElementKind.Flat =>
                specs.Encoding == CGaFloat64ElementEncoding.Opns
                    ? Blade.DecodeOpnsFlat.Weight()
                    : Blade.DecodeIpnsFlat.Weight(),

            CGaFloat64ElementKind.Round =>
                Blade.DecodeIpnsRound.Weight(),

            _ => throw new InvalidOperationException()
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade IpnsElementVGaDirection()
    {
        var specs =
            Blade.GetElementSpecs();

        return specs.Kind switch
        {
            CGaFloat64ElementKind.Direction =>
                Blade.Decode.IpnsDirection.VGaDirection(),

            CGaFloat64ElementKind.Tangent =>
                Blade.Decode.IpnsTangent.VGaDirection(),

            CGaFloat64ElementKind.Flat =>
                specs.Encoding == CGaFloat64ElementEncoding.Opns
                    ? Blade.Decode.OpnsFlat.VGaDirection()
                    : Blade.Decode.IpnsFlat.VGaDirection(),

            CGaFloat64ElementKind.Round =>
                Blade.Decode.IpnsRound.VGaDirection(),

            _ => throw new InvalidOperationException()
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade IpnsElementVGaDirectionNormal()
    {
        return IpnsElementVGaDirection().VGaNormal();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade IpnsElementVGaPosition()
    {
        var specs =
            Blade.GetElementSpecs();

        return specs.Kind switch
        {
            CGaFloat64ElementKind.Direction =>
                Blade.GeometricSpace.ZeroVectorBlade,

            CGaFloat64ElementKind.Tangent =>
                Blade.Decode.IpnsTangent.VGaPosition(),

            CGaFloat64ElementKind.Flat =>
                specs.Encoding == CGaFloat64ElementEncoding.Opns
                    ? Blade.Decode.OpnsFlat.VGaPosition()
                    : Blade.Decode.IpnsFlat.VGaPosition(),

            CGaFloat64ElementKind.Round =>
                Blade.Decode.IpnsRound.VGaCenter(),

            _ => throw new InvalidOperationException()
        };
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Element OpnsElement()
    {
        var specs =
            Blade.GetElementSpecs();

        return specs.Kind switch
        {
            CGaFloat64ElementKind.Direction =>
                Blade.DecodeOpnsDirection.Element(),

            CGaFloat64ElementKind.Tangent =>
                Blade.DecodeOpnsTangent.Element(),

            CGaFloat64ElementKind.Flat =>
                specs.Encoding == CGaFloat64ElementEncoding.Opns
                    ? Blade.DecodeOpnsFlat.Element()
                    : Blade.DecodeIpnsFlat.Element(),

            CGaFloat64ElementKind.Round =>
                Blade.DecodeOpnsRound.Element(),

            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double OpnsElementWeight()
    {
        var specs =
            Blade.GetElementSpecs();

        return specs.Kind switch
        {
            CGaFloat64ElementKind.Direction =>
                Blade.DecodeOpnsDirection.Weight(),

            CGaFloat64ElementKind.Tangent =>
                Blade.DecodeOpnsTangent.Weight(),

            CGaFloat64ElementKind.Flat =>
                specs.Encoding == CGaFloat64ElementEncoding.Opns
                    ? Blade.DecodeOpnsFlat.Weight()
                    : Blade.DecodeIpnsFlat.Weight(),

            CGaFloat64ElementKind.Round =>
                Blade.DecodeOpnsRound.Weight(),

            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade OpnsElementVGaDirection()
    {
        var specs =
            Blade.GetElementSpecs();

        return specs.Kind switch
        {
            CGaFloat64ElementKind.Direction =>
                Blade.DecodeOpnsDirection.VGaDirection(),

            CGaFloat64ElementKind.Tangent =>
                Blade.DecodeOpnsTangent.VGaDirection(),

            CGaFloat64ElementKind.Flat =>
                specs.Encoding == CGaFloat64ElementEncoding.Opns
                    ? Blade.DecodeOpnsFlat.VGaDirection()
                    : Blade.DecodeIpnsFlat.VGaDirection(),

            CGaFloat64ElementKind.Round =>
                Blade.DecodeOpnsRound.VGaDirection(),

            _ => throw new InvalidOperationException()
        };
    }

    
}