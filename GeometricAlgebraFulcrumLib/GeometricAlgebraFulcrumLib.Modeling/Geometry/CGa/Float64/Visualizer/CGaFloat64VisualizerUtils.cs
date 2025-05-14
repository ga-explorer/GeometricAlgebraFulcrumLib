using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Elements;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Visualizer;

public static class CGaFloat64VisualizerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Visualizer DrawElement(this CGaFloat64Element element, Color color)
    {
        return element switch
        {
            CGaFloat64Direction direction =>
                element.Visualizer.DrawDirection(color, direction),

            CGaFloat64Tangent tangent =>
                element.Visualizer.DrawTangent(color, tangent),

            CGaFloat64Flat flat =>
                element.Visualizer.DrawFlat(color, flat),

            CGaFloat64Round round =>
                element.Visualizer.DrawRound(color, round),

            _ => throw new InvalidOperationException()

        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Visualizer DrawDirection(this CGaFloat64Direction element, Color color)
    {
        return element.Visualizer.DrawDirection(color, element);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Visualizer DrawTangent(this CGaFloat64Tangent element, Color color)
    {
        return element.Visualizer.DrawTangent(color, element);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Visualizer DrawFlat(this CGaFloat64Flat element, Color color)
    {
        return element.Visualizer.DrawFlat(color, element);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Visualizer DrawRound(this CGaFloat64Round element, Color color)
    {
        return element.Visualizer.DrawRound(color, element);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Visualizer DrawPGaFlat(this CGaFloat64Visualizer visualizer, CGaFloat64Blade blade, Color color)
    {
        return visualizer.DrawFlat(
            color,
            blade.DecodePGaFlat.Element()
        );
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static CGaFloat64Visualizer DrawPGaFlat(this CGaFloat64Visualizer visualizer, XGaConformalParametricBlade3D blade, Color color)
    //{
    //    return visualizer.DrawFlat(
    //        color,
    //        blade.DecodePGaFlat.Element()
    //    );
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Visualizer DrawPGaFlat(this CGaFloat64Blade blade, Color color)
    {
        return blade.Visualizer.DrawFlat(
            color,
            blade.DecodePGaFlat.Element()
        );
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static CGaFloat64Visualizer DrawPGaFlat(this XGaConformalParametricBlade2D blade, Color color)
    //{
    //    return blade.Visualizer.DrawFlat(
    //        color,
    //        blade.DecodePGaFlat.Element()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static CGaFloat64Visualizer DrawPGaFlat(this XGaConformalParametricBlade3D blade, Color color)
    //{
    //    return blade.Visualizer.DrawFlat(
    //        color,
    //        blade.DecodePGaFlat.Element()
    //    );
    //}


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Visualizer DrawOpnsBlade(this CGaFloat64Visualizer visualizer, CGaFloat64Blade blade, Color color)
    {
        var specs =
            blade.GetElementSpecs();

        return specs.Kind switch
        {
            CGaFloat64ElementKind.Direction =>
                visualizer.DrawOpnsDirection(blade, color),

            CGaFloat64ElementKind.Tangent =>
                visualizer.DrawOpnsTangent(blade, color),

            CGaFloat64ElementKind.Flat =>
                specs.Encoding == CGaFloat64ElementEncoding.Opns
                    ? visualizer.DrawOpnsFlat(blade, color)
                    : visualizer.DrawIpnsFlat(blade, color),

            CGaFloat64ElementKind.Round =>
                visualizer.DrawOpnsRound(blade, color),

            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Visualizer DrawOpnsBlade(this CGaFloat64Blade blade, Color color)
    {
        var specs =
            blade.GetElementSpecs();

        return specs.Kind switch
        {
            CGaFloat64ElementKind.Direction =>
                blade.DrawOpnsDirection(color),

            CGaFloat64ElementKind.Tangent =>
                blade.DrawOpnsTangent(color),

            CGaFloat64ElementKind.Flat =>
                specs.Encoding == CGaFloat64ElementEncoding.Opns
                    ? blade.DrawOpnsFlat(color)
                    : blade.DrawIpnsFlat(color),

            CGaFloat64ElementKind.Round =>
                blade.DrawOpnsRound(color),

            _ => throw new InvalidOperationException()
        };
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Visualizer DrawOpnsDirection(this CGaFloat64Visualizer visualizer, CGaFloat64Blade blade, Color color)
    {
        return visualizer.DrawDirection(
            color,
            blade.DecodeOpnsDirection.Element()
        );
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static CGaFloat64Visualizer DrawOpnsDirection(this CGaFloat64Visualizer visualizer, XGaConformalParametricBlade2D blade, Color color)
    //{
    //    return visualizer.DrawDirection(
    //        color,
    //        blade.DecodeOpnsDirection.Element()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static CGaFloat64Visualizer DrawOpnsDirection(this CGaFloat64Visualizer visualizer, XGaConformalParametricBlade3D blade, Color color)
    //{
    //    return visualizer.DrawDirection(
    //        color,
    //        blade.DecodeOpnsDirection.Element()
    //    );
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Visualizer DrawOpnsDirection(this CGaFloat64Blade blade, Color color)
    {
        return blade.Visualizer.DrawDirection(
            color,
            blade.DecodeOpnsDirection.Element()
        );
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static CGaFloat64Visualizer DrawOpnsDirection(this XGaConformalParametricBlade2D blade, Color color)
    //{
    //    return blade.Visualizer.DrawDirection(
    //        color,
    //        blade.DecodeOpnsDirection.Element()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static CGaFloat64Visualizer DrawOpnsDirection(this XGaConformalParametricBlade3D blade, Color color)
    //{
    //    return blade.Visualizer.DrawDirection(
    //        color,
    //        blade.DecodeOpnsDirection.Element()
    //    );
    //}


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Visualizer DrawOpnsTangent(this CGaFloat64Visualizer visualizer, CGaFloat64Blade blade, Color color)
    {
        return visualizer.DrawTangent(
            color,
            blade.DecodeOpnsTangent.Element()
        );
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static CGaFloat64Visualizer DrawOpnsTangent(this CGaFloat64Visualizer visualizer, XGaConformalParametricBlade2D blade, Color color)
    //{
    //    return visualizer.DrawTangent(
    //        color,
    //        blade.DecodeOpnsTangent.Element()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static CGaFloat64Visualizer DrawOpnsTangent(this CGaFloat64Visualizer visualizer, XGaConformalParametricBlade3D blade, Color color)
    //{
    //    return visualizer.DrawTangent(
    //        color,
    //        blade.DecodeOpnsTangent.Element()
    //    );
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Visualizer DrawOpnsTangent(this CGaFloat64Blade blade, Color color)
    {
        return blade.Visualizer.DrawTangent(
            color,
            blade.DecodeOpnsTangent.Element()
        );
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static CGaFloat64Visualizer DrawOpnsTangent(this XGaConformalParametricBlade2D blade, Color color)
    //{
    //    return blade.Visualizer.DrawTangent(
    //        color,
    //        blade.DecodeOpnsTangent.Element()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static CGaFloat64Visualizer DrawOpnsTangent(this XGaConformalParametricBlade3D blade, Color color)
    //{
    //    return blade.Visualizer.DrawTangent(
    //        color,
    //        blade.DecodeOpnsTangent.Element()
    //    );
    //}


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Visualizer DrawOpnsFlat(this CGaFloat64Visualizer visualizer, CGaFloat64Blade blade, Color color)
    {
        return visualizer.DrawFlat(
            color,
            blade.DecodeOpnsFlat.Element()
        );
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static CGaFloat64Visualizer DrawOpnsFlat(this CGaFloat64Visualizer visualizer, XGaConformalParametricBlade2D blade, Color color)
    //{
    //    return visualizer.DrawFlat(
    //        color,
    //        blade.DecodeOpnsFlat.Element()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static CGaFloat64Visualizer DrawOpnsFlat(this CGaFloat64Visualizer visualizer, XGaConformalParametricBlade3D blade, Color color)
    //{
    //    return visualizer.DrawFlat(
    //        color,
    //        blade.DecodeOpnsFlat.Element()
    //    );
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Visualizer DrawOpnsFlat(this CGaFloat64Blade blade, Color color)
    {
        return blade.Visualizer.DrawFlat(
            color,
            blade.DecodeOpnsFlat.Element()
        );
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static CGaFloat64Visualizer DrawOpnsFlat(this XGaConformalParametricBlade2D blade, Color color)
    //{
    //    return blade.Visualizer.DrawFlat(
    //        color,
    //        blade.DecodeOpnsFlat.Element()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static CGaFloat64Visualizer DrawOpnsFlat(this XGaConformalParametricBlade3D blade, Color color)
    //{
    //    return blade.Visualizer.DrawFlat(
    //        color,
    //        blade.DecodeOpnsFlat.Element()
    //    );
    //}


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Visualizer DrawOpnsRound(this CGaFloat64Visualizer visualizer, CGaFloat64Blade blade, Color color)
    {
        return visualizer.DrawRound(
            color,
            blade.DecodeOpnsRound.Element()
        );
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static CGaFloat64Visualizer DrawOpnsRound(this CGaFloat64Visualizer visualizer, XGaConformalParametricBlade2D blade, Color color)
    //{
    //    return visualizer.DrawRound(
    //        color,
    //        blade.DecodeOpnsRound.Element()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static CGaFloat64Visualizer DrawOpnsRound(this CGaFloat64Visualizer visualizer, XGaConformalParametricBlade3D blade, Color color)
    //{
    //    return visualizer.DrawRound(
    //        color,
    //        blade.DecodeOpnsRound.Element()
    //    );
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Visualizer DrawOpnsRound(this CGaFloat64Blade blade, Color color)
    {
        return blade.Visualizer.DrawRound(
            color,
            blade.DecodeOpnsRound.Element()
        );
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static CGaFloat64Visualizer DrawOpnsRound(this XGaConformalParametricBlade2D blade, Color color)
    //{
    //    return blade.Visualizer.DrawRound(
    //        color,
    //        blade.DecodeOpnsRound.Element()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static CGaFloat64Visualizer DrawOpnsRound(this XGaConformalParametricBlade3D blade, Color color)
    //{
    //    return blade.Visualizer.DrawRound(
    //        color,
    //        blade.DecodeOpnsRound.Element()
    //    );
    //}


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Visualizer DrawIpnsBlade(this CGaFloat64Visualizer visualizer, CGaFloat64Blade blade, Color color)
    {
        var specs =
            blade.GetElementSpecs();

        return specs.Kind switch
        {
            CGaFloat64ElementKind.Direction =>
                visualizer.DrawIpnsDirection(blade, color),

            CGaFloat64ElementKind.Tangent =>
                visualizer.DrawIpnsTangent(blade, color),

            CGaFloat64ElementKind.Flat =>
                specs.Encoding == CGaFloat64ElementEncoding.Opns
                    ? visualizer.DrawOpnsFlat(blade, color)
                    : visualizer.DrawIpnsFlat(blade, color),

            CGaFloat64ElementKind.Round =>
                visualizer.DrawIpnsRound(blade, color),

            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Visualizer DrawIpnsBlade(this CGaFloat64Blade blade, Color color)
    {
        var specs =
            blade.GetElementSpecs();

        return specs.Kind switch
        {
            CGaFloat64ElementKind.Direction =>
                blade.DrawIpnsDirection(color),

            CGaFloat64ElementKind.Tangent =>
                blade.DrawIpnsTangent(color),

            CGaFloat64ElementKind.Flat =>
                specs.Encoding == CGaFloat64ElementEncoding.Opns
                    ? blade.DrawOpnsFlat(color)
                    : blade.DrawIpnsFlat(color),

            CGaFloat64ElementKind.Round =>
                blade.DrawIpnsRound(color),

            _ => throw new InvalidOperationException()
        };
    }


    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static CGaFloat64Visualizer DrawBlade(this XGaConformalParametricBlade2D blade, Color color)
    //{
    //    if (blade.Specs.Encoding == CGaFloat64ElementEncoding.PGa)
    //    {
    //        return blade.Specs.Kind switch
    //        {
    //            CGaFloat64ElementKind.Flat =>
    //                blade.DrawPGaFlat(color),

    //            _ => blade.Visualizer
    //        };
    //    }

    //    if (blade.Specs.Encoding == CGaFloat64ElementEncoding.Opns)
    //    {
    //        return blade.Specs.Kind switch
    //        {
    //            CGaFloat64ElementKind.Direction =>
    //                blade.DrawOpnsDirection(color),

    //            CGaFloat64ElementKind.Tangent =>
    //                blade.DrawOpnsTangent(color),

    //            CGaFloat64ElementKind.Flat =>
    //                blade.DrawOpnsFlat(color),

    //            CGaFloat64ElementKind.Round =>
    //                blade.DrawOpnsRound(color),

    //            _ => blade.Visualizer
    //        };
    //    }

    //    if (blade.Specs.Encoding == CGaFloat64ElementEncoding.Ipns)
    //    {
    //        return blade.Specs.Kind switch
    //        {
    //            CGaFloat64ElementKind.Direction =>
    //                blade.DrawIpnsDirection(color),

    //            CGaFloat64ElementKind.Tangent =>
    //                blade.DrawIpnsTangent(color),

    //            CGaFloat64ElementKind.Flat =>
    //                blade.DrawIpnsFlat(color),

    //            CGaFloat64ElementKind.Round =>
    //                blade.DrawIpnsRound(color),

    //            _ => blade.Visualizer
    //        };
    //    }

    //    return blade.Visualizer;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static CGaFloat64Visualizer DrawBlade(this XGaConformalParametricBlade3D blade, Color color)
    //{
    //    if (blade.Specs.Encoding == CGaFloat64ElementEncoding.PGa)
    //    {
    //        return blade.Specs.Kind switch
    //        {
    //            CGaFloat64ElementKind.Flat =>
    //                blade.DrawPGaFlat(color),

    //            _ => blade.Visualizer
    //        };
    //    }

    //    if (blade.Specs.Encoding == CGaFloat64ElementEncoding.Opns)
    //    {
    //        return blade.Specs.Kind switch
    //        {
    //            CGaFloat64ElementKind.Direction =>
    //                blade.DrawOpnsDirection(color),

    //            CGaFloat64ElementKind.Tangent =>
    //                blade.DrawOpnsTangent(color),

    //            CGaFloat64ElementKind.Flat =>
    //                blade.DrawOpnsFlat(color),

    //            CGaFloat64ElementKind.Round =>
    //                blade.DrawOpnsRound(color),

    //            _ => blade.Visualizer
    //        };
    //    }

    //    if (blade.Specs.Encoding == CGaFloat64ElementEncoding.Ipns)
    //    {
    //        return blade.Specs.Kind switch
    //        {
    //            CGaFloat64ElementKind.Direction =>
    //                blade.DrawIpnsDirection(color),

    //            CGaFloat64ElementKind.Tangent =>
    //                blade.DrawIpnsTangent(color),

    //            CGaFloat64ElementKind.Flat =>
    //                blade.DrawIpnsFlat(color),

    //            CGaFloat64ElementKind.Round =>
    //                blade.DrawIpnsRound(color),

    //            _ => blade.Visualizer
    //        };
    //    }

    //    return blade.Visualizer;
    //}


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Visualizer DrawIpnsDirection(this CGaFloat64Visualizer visualizer, CGaFloat64Blade blade, Color color)
    {
        return visualizer.DrawDirection(
            color,
            blade.DecodeIpnsDirection.Element()
        );
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static CGaFloat64Visualizer DrawIpnsDirection(this CGaFloat64Visualizer visualizer, XGaConformalParametricBlade2D blade, Color color)
    //{
    //    return visualizer.DrawDirection(
    //        color,
    //        blade.DecodeIpnsDirection.Element()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static CGaFloat64Visualizer DrawIpnsDirection(this CGaFloat64Visualizer visualizer, XGaConformalParametricBlade3D blade, Color color)
    //{
    //    return visualizer.DrawDirection(
    //        color,
    //        blade.DecodeIpnsDirection.Element()
    //    );
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Visualizer DrawIpnsDirection(this CGaFloat64Blade blade, Color color)
    {
        return blade.Visualizer.DrawDirection(
            color,
            blade.DecodeIpnsDirection.Element()
        );
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static CGaFloat64Visualizer DrawIpnsDirection(this XGaConformalParametricBlade2D blade, Color color)
    //{
    //    return blade.Visualizer.DrawDirection(
    //        color,
    //        blade.DecodeIpnsDirection.Element()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static CGaFloat64Visualizer DrawIpnsDirection(this XGaConformalParametricBlade3D blade, Color color)
    //{
    //    return blade.Visualizer.DrawDirection(
    //        color,
    //        blade.DecodeIpnsDirection.Element()
    //    );
    //}


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Visualizer DrawIpnsTangent(this CGaFloat64Visualizer visualizer, CGaFloat64Blade blade, Color color)
    {
        return visualizer.DrawTangent(
            color,
            blade.DecodeIpnsTangent.Element()
        );
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static CGaFloat64Visualizer DrawIpnsTangent(this CGaFloat64Visualizer visualizer, XGaConformalParametricBlade2D blade, Color color)
    //{
    //    return visualizer.DrawTangent(
    //        color,
    //        blade.DecodeIpnsTangent.Element()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static CGaFloat64Visualizer DrawIpnsTangent(this CGaFloat64Visualizer visualizer, XGaConformalParametricBlade3D blade, Color color)
    //{
    //    return visualizer.DrawTangent(
    //        color,
    //        blade.DecodeIpnsTangent.Element()
    //    );
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Visualizer DrawIpnsTangent(this CGaFloat64Blade blade, Color color)
    {
        return blade.Visualizer.DrawTangent(
            color,
            blade.DecodeIpnsTangent.Element()
        );
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static CGaFloat64Visualizer DrawIpnsTangent(this XGaConformalParametricBlade2D blade, Color color)
    //{
    //    return blade.Visualizer.DrawTangent(
    //        color,
    //        blade.DecodeIpnsTangent.Element()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static CGaFloat64Visualizer DrawIpnsTangent(this XGaConformalParametricBlade3D blade, Color color)
    //{
    //    return blade.Visualizer.DrawTangent(
    //        color,
    //        blade.DecodeIpnsTangent.Element()
    //    );
    //}


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Visualizer DrawIpnsFlat(this CGaFloat64Visualizer visualizer, CGaFloat64Blade blade, Color color)
    {
        return visualizer.DrawFlat(
            color,
            blade.DecodeIpnsFlat.Element()
        );
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static CGaFloat64Visualizer DrawIpnsFlat(this CGaFloat64Visualizer visualizer, XGaConformalParametricBlade2D blade, Color color)
    //{
    //    return visualizer.DrawFlat(
    //        color,
    //        blade.DecodeIpnsFlat.Element()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static CGaFloat64Visualizer DrawIpnsFlat(this CGaFloat64Visualizer visualizer, XGaConformalParametricBlade3D blade, Color color)
    //{
    //    return visualizer.DrawFlat(
    //        color,
    //        blade.DecodeIpnsFlat.Element()
    //    );
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Visualizer DrawIpnsFlat(this CGaFloat64Blade blade, Color color)
    {
        return blade.Visualizer.DrawFlat(
            color,
            blade.DecodeIpnsFlat.Element()
        );
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static CGaFloat64Visualizer DrawIpnsFlat(this XGaConformalParametricBlade2D blade, Color color)
    //{
    //    return blade.Visualizer.DrawFlat(
    //        color,
    //        blade.DecodeIpnsFlat.Element()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static CGaFloat64Visualizer DrawIpnsFlat(this XGaConformalParametricBlade3D blade, Color color)
    //{
    //    return blade.Visualizer.DrawFlat(
    //        color,
    //        blade.DecodeIpnsFlat.Element()
    //    );
    //}


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Visualizer DrawIpnsRound(this CGaFloat64Visualizer visualizer, CGaFloat64Blade blade, Color color)
    {
        return visualizer.DrawRound(
            color,
            blade.DecodeIpnsRound.Element()
        );
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static CGaFloat64Visualizer DrawIpnsRound(this CGaFloat64Visualizer visualizer, XGaConformalParametricBlade2D blade, Color color)
    //{
    //    return visualizer.DrawRound(
    //        color,
    //        blade.DecodeIpnsRound.Element()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static CGaFloat64Visualizer DrawIpnsRound(this CGaFloat64Visualizer visualizer, XGaConformalParametricBlade3D blade, Color color)
    //{
    //    return visualizer.DrawRound(
    //        color,
    //        blade.DecodeIpnsRound.Element()
    //    );
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Visualizer DrawIpnsRound(this CGaFloat64Blade blade, Color color)
    {
        return blade.Visualizer.DrawRound(
            color,
            blade.DecodeIpnsRound.Element()
        );
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static CGaFloat64Visualizer DrawIpnsRound(this XGaConformalParametricBlade2D blade, Color color)
    //{
    //    return blade.Visualizer.DrawRound(
    //        color,
    //        blade.DecodeIpnsRound.Element()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static CGaFloat64Visualizer DrawIpnsRound(this XGaConformalParametricBlade3D blade, Color color)
    //{
    //    return blade.Visualizer.DrawRound(
    //        color,
    //        blade.DecodeIpnsRound.Element()
    //    );
    //}

}