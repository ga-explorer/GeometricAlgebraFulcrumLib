using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Decoding;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Elements;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Visualizer;

public static class RGaConformalVisualizerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalVisualizer DrawElement(this RGaConformalElement element, Color color)
    {
        return element switch
        {
            RGaConformalDirection direction =>
                element.Visualizer.DrawDirection(color, direction),

            RGaConformalTangent tangent =>
                element.Visualizer.DrawTangent(color, tangent),

            RGaConformalFlat flat =>
                element.Visualizer.DrawFlat(color, flat),

            RGaConformalRound round =>
                element.Visualizer.DrawRound(color, round),

            _ => throw new InvalidOperationException()

        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalVisualizer DrawDirection(this RGaConformalDirection element, Color color)
    {
        return element.Visualizer.DrawDirection(color, element);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalVisualizer DrawTangent(this RGaConformalTangent element, Color color)
    {
        return element.Visualizer.DrawTangent(color, element);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalVisualizer DrawFlat(this RGaConformalFlat element, Color color)
    {
        return element.Visualizer.DrawFlat(color, element);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalVisualizer DrawRound(this RGaConformalRound element, Color color)
    {
        return element.Visualizer.DrawRound(color, element);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalVisualizer DrawPGaFlat(this RGaConformalVisualizer visualizer, RGaConformalBlade blade, Color color)
    {
        return visualizer.DrawFlat(
            color,
            blade.DecodePGaFlat()
        );
    }
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalVisualizer DrawPGaFlat(this RGaConformalVisualizer visualizer, RGaConformalParametricBlade3D blade, Color color)
    //{
    //    return visualizer.DrawFlat(
    //        color,
    //        blade.DecodePGaFlat()
    //    );
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalVisualizer DrawPGaFlat(this RGaConformalBlade blade, Color color)
    {
        return blade.Visualizer.DrawFlat(
            color,
            blade.DecodePGaFlat()
        );
    }
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalVisualizer DrawPGaFlat(this RGaConformalParametricBlade2D blade, Color color)
    //{
    //    return blade.Visualizer.DrawFlat(
    //        color,
    //        blade.DecodePGaFlat()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalVisualizer DrawPGaFlat(this RGaConformalParametricBlade3D blade, Color color)
    //{
    //    return blade.Visualizer.DrawFlat(
    //        color,
    //        blade.DecodePGaFlat()
    //    );
    //}

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalVisualizer DrawOpnsBlade(this RGaConformalVisualizer visualizer, RGaConformalBlade blade, Color color)
    {
        var specs =
            blade.GetElementSpecs();

        return specs.Kind switch
        {
            RGaConformalElementKind.Direction => 
                visualizer.DrawOpnsDirection(blade, color),

            RGaConformalElementKind.Tangent => 
                visualizer.DrawOpnsTangent(blade, color),

            RGaConformalElementKind.Flat =>
                specs.Encoding == RGaConformalElementEncoding.Opns
                    ? visualizer.DrawOpnsFlat(blade, color)
                    : visualizer.DrawIpnsFlat(blade, color),

            RGaConformalElementKind.Round =>
                visualizer.DrawOpnsRound(blade, color),

            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalVisualizer DrawOpnsBlade(this RGaConformalBlade blade, Color color)
    {
        var specs =
            blade.GetElementSpecs();

        return specs.Kind switch
        {
            RGaConformalElementKind.Direction => 
                blade.DrawOpnsDirection(color),

            RGaConformalElementKind.Tangent => 
                blade.DrawOpnsTangent(color),

            RGaConformalElementKind.Flat =>
                specs.Encoding == RGaConformalElementEncoding.Opns
                    ? blade.DrawOpnsFlat(color)
                    : blade.DrawIpnsFlat(color),

            RGaConformalElementKind.Round =>
                blade.DrawOpnsRound(color),

            _ => throw new InvalidOperationException()
        };
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalVisualizer DrawOpnsDirection(this RGaConformalVisualizer visualizer, RGaConformalBlade blade, Color color)
    {
        return visualizer.DrawDirection(
            color,
            blade.DecodeOpnsDirection()
        );
    }
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalVisualizer DrawOpnsDirection(this RGaConformalVisualizer visualizer, RGaConformalParametricBlade2D blade, Color color)
    //{
    //    return visualizer.DrawDirection(
    //        color,
    //        blade.DecodeOpnsDirection()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalVisualizer DrawOpnsDirection(this RGaConformalVisualizer visualizer, RGaConformalParametricBlade3D blade, Color color)
    //{
    //    return visualizer.DrawDirection(
    //        color,
    //        blade.DecodeOpnsDirection()
    //    );
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalVisualizer DrawOpnsDirection(this RGaConformalBlade blade, Color color)
    {
        return blade.Visualizer.DrawDirection(
            color,
            blade.DecodeOpnsDirection()
        );
    }
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalVisualizer DrawOpnsDirection(this RGaConformalParametricBlade2D blade, Color color)
    //{
    //    return blade.Visualizer.DrawDirection(
    //        color,
    //        blade.DecodeOpnsDirection()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalVisualizer DrawOpnsDirection(this RGaConformalParametricBlade3D blade, Color color)
    //{
    //    return blade.Visualizer.DrawDirection(
    //        color,
    //        blade.DecodeOpnsDirection()
    //    );
    //}

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalVisualizer DrawOpnsTangent(this RGaConformalVisualizer visualizer, RGaConformalBlade blade, Color color)
    {
        return visualizer.DrawTangent(
            color,
            blade.DecodeOpnsTangent()
        );
    }
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalVisualizer DrawOpnsTangent(this RGaConformalVisualizer visualizer, RGaConformalParametricBlade2D blade, Color color)
    //{
    //    return visualizer.DrawTangent(
    //        color,
    //        blade.DecodeOpnsTangent()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalVisualizer DrawOpnsTangent(this RGaConformalVisualizer visualizer, RGaConformalParametricBlade3D blade, Color color)
    //{
    //    return visualizer.DrawTangent(
    //        color,
    //        blade.DecodeOpnsTangent()
    //    );
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalVisualizer DrawOpnsTangent(this RGaConformalBlade blade, Color color)
    {
        return blade.Visualizer.DrawTangent(
            color,
            blade.DecodeOpnsTangent()
        );
    }
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalVisualizer DrawOpnsTangent(this RGaConformalParametricBlade2D blade, Color color)
    //{
    //    return blade.Visualizer.DrawTangent(
    //        color,
    //        blade.DecodeOpnsTangent()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalVisualizer DrawOpnsTangent(this RGaConformalParametricBlade3D blade, Color color)
    //{
    //    return blade.Visualizer.DrawTangent(
    //        color,
    //        blade.DecodeOpnsTangent()
    //    );
    //}

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalVisualizer DrawOpnsFlat(this RGaConformalVisualizer visualizer, RGaConformalBlade blade, Color color)
    {
        return visualizer.DrawFlat(
            color,
            blade.DecodeOpnsFlat()
        );
    }
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalVisualizer DrawOpnsFlat(this RGaConformalVisualizer visualizer, RGaConformalParametricBlade2D blade, Color color)
    //{
    //    return visualizer.DrawFlat(
    //        color,
    //        blade.DecodeOpnsFlat()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalVisualizer DrawOpnsFlat(this RGaConformalVisualizer visualizer, RGaConformalParametricBlade3D blade, Color color)
    //{
    //    return visualizer.DrawFlat(
    //        color,
    //        blade.DecodeOpnsFlat()
    //    );
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalVisualizer DrawOpnsFlat(this RGaConformalBlade blade, Color color)
    {
        return blade.Visualizer.DrawFlat(
            color,
            blade.DecodeOpnsFlat()
        );
    }
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalVisualizer DrawOpnsFlat(this RGaConformalParametricBlade2D blade, Color color)
    //{
    //    return blade.Visualizer.DrawFlat(
    //        color,
    //        blade.DecodeOpnsFlat()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalVisualizer DrawOpnsFlat(this RGaConformalParametricBlade3D blade, Color color)
    //{
    //    return blade.Visualizer.DrawFlat(
    //        color,
    //        blade.DecodeOpnsFlat()
    //    );
    //}

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalVisualizer DrawOpnsRound(this RGaConformalVisualizer visualizer, RGaConformalBlade blade, Color color)
    {
        return visualizer.DrawRound(
            color,
            blade.DecodeOpnsRound()
        );
    }
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalVisualizer DrawOpnsRound(this RGaConformalVisualizer visualizer, RGaConformalParametricBlade2D blade, Color color)
    //{
    //    return visualizer.DrawRound(
    //        color,
    //        blade.DecodeOpnsRound()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalVisualizer DrawOpnsRound(this RGaConformalVisualizer visualizer, RGaConformalParametricBlade3D blade, Color color)
    //{
    //    return visualizer.DrawRound(
    //        color,
    //        blade.DecodeOpnsRound()
    //    );
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalVisualizer DrawOpnsRound(this RGaConformalBlade blade, Color color)
    {
        return blade.Visualizer.DrawRound(
            color,
            blade.DecodeOpnsRound()
        );
    }
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalVisualizer DrawOpnsRound(this RGaConformalParametricBlade2D blade, Color color)
    //{
    //    return blade.Visualizer.DrawRound(
    //        color,
    //        blade.DecodeOpnsRound()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalVisualizer DrawOpnsRound(this RGaConformalParametricBlade3D blade, Color color)
    //{
    //    return blade.Visualizer.DrawRound(
    //        color,
    //        blade.DecodeOpnsRound()
    //    );
    //}

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalVisualizer DrawIpnsBlade(this RGaConformalVisualizer visualizer, RGaConformalBlade blade, Color color)
    {
        var specs =
            blade.GetElementSpecs();

        return specs.Kind switch
        {
            RGaConformalElementKind.Direction => 
                visualizer.DrawIpnsDirection(blade, color),

            RGaConformalElementKind.Tangent => 
                visualizer.DrawIpnsTangent(blade, color),

            RGaConformalElementKind.Flat =>
                specs.Encoding == RGaConformalElementEncoding.Opns
                    ? visualizer.DrawOpnsFlat(blade, color)
                    : visualizer.DrawIpnsFlat(blade, color),

            RGaConformalElementKind.Round =>
                visualizer.DrawIpnsRound(blade, color),

            _ => throw new InvalidOperationException()
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalVisualizer DrawIpnsBlade(this RGaConformalBlade blade, Color color)
    {
        var specs =
            blade.GetElementSpecs();

        return specs.Kind switch
        {
            RGaConformalElementKind.Direction => 
                blade.DrawIpnsDirection(color),

            RGaConformalElementKind.Tangent => 
                blade.DrawIpnsTangent(color),

            RGaConformalElementKind.Flat =>
                specs.Encoding == RGaConformalElementEncoding.Opns
                    ? blade.DrawOpnsFlat(color)
                    : blade.DrawIpnsFlat(color),

            RGaConformalElementKind.Round =>
                blade.DrawIpnsRound(color),

            _ => throw new InvalidOperationException()
        };
    }
    
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalVisualizer DrawBlade(this RGaConformalParametricBlade2D blade, Color color)
    //{
    //    if (blade.Specs.Encoding == RGaConformalElementEncoding.PGa)
    //    {
    //        return blade.Specs.Kind switch
    //        {
    //            RGaConformalElementKind.Flat =>
    //                blade.DrawPGaFlat(color),

    //            _ => blade.Visualizer
    //        };
    //    }
        
    //    if (blade.Specs.Encoding == RGaConformalElementEncoding.Opns)
    //    {
    //        return blade.Specs.Kind switch
    //        {
    //            RGaConformalElementKind.Direction =>
    //                blade.DrawOpnsDirection(color),

    //            RGaConformalElementKind.Tangent =>
    //                blade.DrawOpnsTangent(color),

    //            RGaConformalElementKind.Flat =>
    //                blade.DrawOpnsFlat(color),

    //            RGaConformalElementKind.Round =>
    //                blade.DrawOpnsRound(color),

    //            _ => blade.Visualizer
    //        };
    //    }

    //    if (blade.Specs.Encoding == RGaConformalElementEncoding.Ipns)
    //    {
    //        return blade.Specs.Kind switch
    //        {
    //            RGaConformalElementKind.Direction =>
    //                blade.DrawIpnsDirection(color),

    //            RGaConformalElementKind.Tangent =>
    //                blade.DrawIpnsTangent(color),

    //            RGaConformalElementKind.Flat =>
    //                blade.DrawIpnsFlat(color),

    //            RGaConformalElementKind.Round =>
    //                blade.DrawIpnsRound(color),

    //            _ => blade.Visualizer
    //        };
    //    }

    //    return blade.Visualizer;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalVisualizer DrawBlade(this RGaConformalParametricBlade3D blade, Color color)
    //{
    //    if (blade.Specs.Encoding == RGaConformalElementEncoding.PGa)
    //    {
    //        return blade.Specs.Kind switch
    //        {
    //            RGaConformalElementKind.Flat =>
    //                blade.DrawPGaFlat(color),

    //            _ => blade.Visualizer
    //        };
    //    }
        
    //    if (blade.Specs.Encoding == RGaConformalElementEncoding.Opns)
    //    {
    //        return blade.Specs.Kind switch
    //        {
    //            RGaConformalElementKind.Direction =>
    //                blade.DrawOpnsDirection(color),

    //            RGaConformalElementKind.Tangent =>
    //                blade.DrawOpnsTangent(color),

    //            RGaConformalElementKind.Flat =>
    //                blade.DrawOpnsFlat(color),

    //            RGaConformalElementKind.Round =>
    //                blade.DrawOpnsRound(color),

    //            _ => blade.Visualizer
    //        };
    //    }

    //    if (blade.Specs.Encoding == RGaConformalElementEncoding.Ipns)
    //    {
    //        return blade.Specs.Kind switch
    //        {
    //            RGaConformalElementKind.Direction =>
    //                blade.DrawIpnsDirection(color),

    //            RGaConformalElementKind.Tangent =>
    //                blade.DrawIpnsTangent(color),

    //            RGaConformalElementKind.Flat =>
    //                blade.DrawIpnsFlat(color),

    //            RGaConformalElementKind.Round =>
    //                blade.DrawIpnsRound(color),

    //            _ => blade.Visualizer
    //        };
    //    }

    //    return blade.Visualizer;
    //}

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalVisualizer DrawIpnsDirection(this RGaConformalVisualizer visualizer, RGaConformalBlade blade, Color color)
    {
        return visualizer.DrawDirection(
            color,
            blade.DecodeIpnsDirection()
        );
    }
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalVisualizer DrawIpnsDirection(this RGaConformalVisualizer visualizer, RGaConformalParametricBlade2D blade, Color color)
    //{
    //    return visualizer.DrawDirection(
    //        color,
    //        blade.DecodeIpnsDirection()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalVisualizer DrawIpnsDirection(this RGaConformalVisualizer visualizer, RGaConformalParametricBlade3D blade, Color color)
    //{
    //    return visualizer.DrawDirection(
    //        color,
    //        blade.DecodeIpnsDirection()
    //    );
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalVisualizer DrawIpnsDirection(this RGaConformalBlade blade, Color color)
    {
        return blade.Visualizer.DrawDirection(
            color,
            blade.DecodeIpnsDirection()
        );
    }
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalVisualizer DrawIpnsDirection(this RGaConformalParametricBlade2D blade, Color color)
    //{
    //    return blade.Visualizer.DrawDirection(
    //        color,
    //        blade.DecodeIpnsDirection()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalVisualizer DrawIpnsDirection(this RGaConformalParametricBlade3D blade, Color color)
    //{
    //    return blade.Visualizer.DrawDirection(
    //        color,
    //        blade.DecodeIpnsDirection()
    //    );
    //}

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalVisualizer DrawIpnsTangent(this RGaConformalVisualizer visualizer, RGaConformalBlade blade, Color color)
    {
        return visualizer.DrawTangent(
            color,
            blade.DecodeIpnsTangent()
        );
    }
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalVisualizer DrawIpnsTangent(this RGaConformalVisualizer visualizer, RGaConformalParametricBlade2D blade, Color color)
    //{
    //    return visualizer.DrawTangent(
    //        color,
    //        blade.DecodeIpnsTangent()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalVisualizer DrawIpnsTangent(this RGaConformalVisualizer visualizer, RGaConformalParametricBlade3D blade, Color color)
    //{
    //    return visualizer.DrawTangent(
    //        color,
    //        blade.DecodeIpnsTangent()
    //    );
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalVisualizer DrawIpnsTangent(this RGaConformalBlade blade, Color color)
    {
        return blade.Visualizer.DrawTangent(
            color,
            blade.DecodeIpnsTangent()
        );
    }
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalVisualizer DrawIpnsTangent(this RGaConformalParametricBlade2D blade, Color color)
    //{
    //    return blade.Visualizer.DrawTangent(
    //        color,
    //        blade.DecodeIpnsTangent()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalVisualizer DrawIpnsTangent(this RGaConformalParametricBlade3D blade, Color color)
    //{
    //    return blade.Visualizer.DrawTangent(
    //        color,
    //        blade.DecodeIpnsTangent()
    //    );
    //}

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalVisualizer DrawIpnsFlat(this RGaConformalVisualizer visualizer, RGaConformalBlade blade, Color color)
    {
        return visualizer.DrawFlat(
            color,
            blade.DecodeIpnsFlat()
        );
    }
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalVisualizer DrawIpnsFlat(this RGaConformalVisualizer visualizer, RGaConformalParametricBlade2D blade, Color color)
    //{
    //    return visualizer.DrawFlat(
    //        color,
    //        blade.DecodeIpnsFlat()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalVisualizer DrawIpnsFlat(this RGaConformalVisualizer visualizer, RGaConformalParametricBlade3D blade, Color color)
    //{
    //    return visualizer.DrawFlat(
    //        color,
    //        blade.DecodeIpnsFlat()
    //    );
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalVisualizer DrawIpnsFlat(this RGaConformalBlade blade, Color color)
    {
        return blade.Visualizer.DrawFlat(
            color,
            blade.DecodeIpnsFlat()
        );
    }
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalVisualizer DrawIpnsFlat(this RGaConformalParametricBlade2D blade, Color color)
    //{
    //    return blade.Visualizer.DrawFlat(
    //        color,
    //        blade.DecodeIpnsFlat()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalVisualizer DrawIpnsFlat(this RGaConformalParametricBlade3D blade, Color color)
    //{
    //    return blade.Visualizer.DrawFlat(
    //        color,
    //        blade.DecodeIpnsFlat()
    //    );
    //}

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalVisualizer DrawIpnsRound(this RGaConformalVisualizer visualizer, RGaConformalBlade blade, Color color)
    {
        return visualizer.DrawRound(
            color,
            blade.DecodeIpnsRound()
        );
    }
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalVisualizer DrawIpnsRound(this RGaConformalVisualizer visualizer, RGaConformalParametricBlade2D blade, Color color)
    //{
    //    return visualizer.DrawRound(
    //        color,
    //        blade.DecodeIpnsRound()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalVisualizer DrawIpnsRound(this RGaConformalVisualizer visualizer, RGaConformalParametricBlade3D blade, Color color)
    //{
    //    return visualizer.DrawRound(
    //        color,
    //        blade.DecodeIpnsRound()
    //    );
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalVisualizer DrawIpnsRound(this RGaConformalBlade blade, Color color)
    {
        return blade.Visualizer.DrawRound(
            color,
            blade.DecodeIpnsRound()
        );
    }
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalVisualizer DrawIpnsRound(this RGaConformalParametricBlade2D blade, Color color)
    //{
    //    return blade.Visualizer.DrawRound(
    //        color,
    //        blade.DecodeIpnsRound()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalVisualizer DrawIpnsRound(this RGaConformalParametricBlade3D blade, Color color)
    //{
    //    return blade.Visualizer.DrawRound(
    //        color,
    //        blade.DecodeIpnsRound()
    //    );
    //}

}