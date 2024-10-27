using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Elements;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Decoding;

public sealed class CGaBladeDecoder<T> :
    CGaBladeDecoderBase<T>
{
    private CGaVGaDirectionBladeDecoder<T>? _vgaDirectionDecoder;
    public CGaVGaDirectionBladeDecoder<T> VGaDirection 
        => _vgaDirectionDecoder ??= new CGaVGaDirectionBladeDecoder<T>(Blade);
    
    private CGaPGaFlatBladeDecoder<T>? _pgaFlatDecoder;
    public CGaPGaFlatBladeDecoder<T> PGaFlat 
        => _pgaFlatDecoder ??= new CGaPGaFlatBladeDecoder<T>(Blade);
    
    private CGaIpnsDirectionBladeDecoder<T>? _ipnsDirectionDecoder;
    public CGaIpnsDirectionBladeDecoder<T> IpnsDirection 
        => _ipnsDirectionDecoder ??= new CGaIpnsDirectionBladeDecoder<T>(Blade);
    
    private CGaIpnsTangentBladeDecoder<T>? _ipnsTangentDecoder;
    public CGaIpnsTangentBladeDecoder<T> IpnsTangent 
        => _ipnsTangentDecoder ??= new CGaIpnsTangentBladeDecoder<T>(Blade);
    
    private CGaIpnsFlatBladeDecoder<T>? _ipnsFlatDecoder;
    public CGaIpnsFlatBladeDecoder<T> IpnsFlat 
        => _ipnsFlatDecoder ??= new CGaIpnsFlatBladeDecoder<T>(Blade);

    private CGaIpnsRoundBladeDecoder<T>? _ipnsRoundDecoder;
    public CGaIpnsRoundBladeDecoder<T> IpnsRound 
        => _ipnsRoundDecoder ??= new CGaIpnsRoundBladeDecoder<T>(Blade);
    
    private CGaOpnsDirectionBladeDecoder<T>? _opnsDirectionDecoder;
    public CGaOpnsDirectionBladeDecoder<T> OpnsDirection 
        => _opnsDirectionDecoder ??= new CGaOpnsDirectionBladeDecoder<T>(Blade);
    
    private CGaOpnsTangentBladeDecoder<T>? _opnsTangentDecoder;
    public CGaOpnsTangentBladeDecoder<T> OpnsTangent 
        => _opnsTangentDecoder ??= new CGaOpnsTangentBladeDecoder<T>(Blade);
    
    private CGaOpnsFlatBladeDecoder<T>? _opnsFlatDecoder;
    public CGaOpnsFlatBladeDecoder<T> OpnsFlat 
        => _opnsFlatDecoder ??= new CGaOpnsFlatBladeDecoder<T>(Blade);

    private CGaOpnsRoundBladeDecoder<T>? _opnsRoundDecoder;
    public CGaOpnsRoundBladeDecoder<T> OpnsRound 
        => _opnsRoundDecoder ??= new CGaOpnsRoundBladeDecoder<T>(Blade);


    internal CGaBladeDecoder(CGaBlade<T> blade) 
        : base(blade)
    {
    }


    
    public CGaElement<T> Element(CGaElementSpecs<T> specs)
    {
        return Element(
            Blade.GeometricSpace.ZeroVectorBlade,
            specs
        );
    }

    public CGaElement<T> Element(CGaBlade<T> egaProbePoint, CGaElementSpecs<T> specs)
    {
        if (specs.Encoding == CGaElementEncoding.VGa)
            return specs.Kind switch
            {
                CGaElementKind.Direction =>
                    Blade.Decode.VGaDirection.Direction(),

                _ => throw new InvalidOperationException()
            };

        if (specs.Encoding == CGaElementEncoding.PGa)
            return specs.Kind switch
            {
                CGaElementKind.Flat =>
                    Blade.Decode.PGaFlat.Element(egaProbePoint),

                _ => throw new InvalidOperationException()
            };

        if (specs.Encoding == CGaElementEncoding.Opns)
            return specs.Kind switch
            {
                CGaElementKind.Direction =>
                    Blade.Decode.OpnsDirection.Element(egaProbePoint),

                CGaElementKind.Tangent =>
                    Blade.Decode.OpnsTangent.Element(egaProbePoint),

                CGaElementKind.Flat =>
                    Blade.Decode.OpnsFlat.Element(egaProbePoint),

                CGaElementKind.Round =>
                    Blade.Decode.OpnsRound.Element(egaProbePoint),

                _ => throw new InvalidOperationException()
            };

        if (specs.Encoding == CGaElementEncoding.Ipns)
            return specs.Kind switch
            {
                CGaElementKind.Direction =>
                    Blade.Decode.IpnsDirection.Element(egaProbePoint),

                CGaElementKind.Tangent =>
                    Blade.Decode.IpnsTangent.Element(egaProbePoint),

                CGaElementKind.Flat =>
                    Blade.Decode.IpnsFlat.Element(egaProbePoint),

                CGaElementKind.Round =>
                    Blade.Decode.IpnsRound.Element(egaProbePoint),

                _ => throw new InvalidOperationException()
            };

        throw new InvalidOperationException();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaElement<T> IpnsElement()
    {
        var specs =
            Blade.GetElementSpecs();

        return specs.Kind switch
        {
            CGaElementKind.Direction =>
                Blade.Decode.IpnsDirection.Element(),

            CGaElementKind.Tangent =>
                Blade.Decode.IpnsTangent.Element(),

            CGaElementKind.Flat =>
                specs.Encoding == CGaElementEncoding.Opns
                    ? Blade.Decode.OpnsFlat.Element()
                    : Blade.Decode.IpnsFlat.Element(),

            CGaElementKind.Round =>
                Blade.Decode.IpnsRound.Element(),

            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> IpnsElementWeight()
    {
        var specs =
            Blade.GetElementSpecs();

        return specs.Kind switch
        {
            CGaElementKind.Direction =>
                Blade.Decode.IpnsDirection.Weight(),

            CGaElementKind.Tangent =>
                Blade.Decode.IpnsTangent.Weight(),

            CGaElementKind.Flat =>
                specs.Encoding == CGaElementEncoding.Opns
                    ? Blade.Decode.OpnsFlat.Weight()
                    : Blade.Decode.IpnsFlat.Weight(),

            CGaElementKind.Round =>
                Blade.Decode.IpnsRound.Weight(),

            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> IpnsElementVGaDirection()
    {
        var specs =
            Blade.GetElementSpecs();

        return specs.Kind switch
        {
            CGaElementKind.Direction =>
                Blade.Decode.IpnsDirection.VGaDirectionAsBlade(),

            CGaElementKind.Tangent =>
                Blade.Decode.IpnsTangent.VGaDirection(),

            CGaElementKind.Flat =>
                specs.Encoding == CGaElementEncoding.Opns
                    ? Blade.Decode.OpnsFlat.VGaDirection()
                    : Blade.Decode.IpnsFlat.VGaDirection(),

            CGaElementKind.Round =>
                Blade.Decode.IpnsRound.VGaDirection(),

            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> IpnsElementVGaDirectionNormal()
    {
        return IpnsElementVGaDirection().VGaNormal();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> IpnsElementVGaPosition()
    {
        var specs =
            Blade.GetElementSpecs();

        return specs.Kind switch
        {
            CGaElementKind.Direction =>
                Blade.GeometricSpace.ZeroVectorBlade,

            CGaElementKind.Tangent =>
                Blade.Decode.IpnsTangent.VGaPosition(),

            CGaElementKind.Flat =>
                specs.Encoding == CGaElementEncoding.Opns
                    ? Blade.Decode.OpnsFlat.VGaPosition()
                    : Blade.Decode.IpnsFlat.VGaPosition(),

            CGaElementKind.Round =>
                Blade.Decode.IpnsRound.VGaCenter(),

            _ => throw new InvalidOperationException()
        };
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaElement<T> OpnsElement()
    {
        var specs =
            Blade.GetElementSpecs();

        return specs.Kind switch
        {
            CGaElementKind.Direction =>
                Blade.Decode.OpnsDirection.Element(),

            CGaElementKind.Tangent =>
                Blade.Decode.OpnsTangent.Element(),

            CGaElementKind.Flat =>
                specs.Encoding == CGaElementEncoding.Opns
                    ? Blade.Decode.OpnsFlat.Element()
                    : Blade.Decode.IpnsFlat.Element(),

            CGaElementKind.Round =>
                Blade.Decode.OpnsRound.Element(),

            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> OpnsElementWeight()
    {
        var specs =
            Blade.GetElementSpecs();

        return specs.Kind switch
        {
            CGaElementKind.Direction =>
                Blade.Decode.OpnsDirection.Weight(),

            CGaElementKind.Tangent =>
                Blade.Decode.OpnsTangent.Weight(),

            CGaElementKind.Flat =>
                specs.Encoding == CGaElementEncoding.Opns
                    ? Blade.Decode.OpnsFlat.Weight()
                    : Blade.Decode.IpnsFlat.Weight(),

            CGaElementKind.Round =>
                Blade.Decode.OpnsRound.Weight(),

            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> OpnsElementVGaDirectionAsBlade()
    {
        var specs =
            Blade.GetElementSpecs();

        return specs.Kind switch
        {
            CGaElementKind.Direction =>
                Blade.Decode.OpnsDirection.VGaDirectionAsBlade(),

            CGaElementKind.Tangent =>
                Blade.Decode.OpnsTangent.VGaDirection(),

            CGaElementKind.Flat =>
                specs.Encoding == CGaElementEncoding.Opns
                    ? Blade.Decode.OpnsFlat.VGaDirection()
                    : Blade.Decode.IpnsFlat.VGaDirection(),

            CGaElementKind.Round =>
                Blade.Decode.OpnsRound.VGaDirection(),

            _ => throw new InvalidOperationException()
        };
    }

}