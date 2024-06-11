using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Decoding;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Elements;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Versors;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Operations;

public static class CGaFloat64MappingUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Element MapUsing(this CGaFloat64Element element, IRGaFloat64Outermorphism outerMorphism)
    {
        return element
            .EncodeOpnsBlade()
            .MapUsing(outerMorphism)
            .DecodeOpnsElement();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Element MapUsing(this CGaFloat64Element element, CGaFloat64Versor versor)
    {
        return element
            .EncodeOpnsBlade()
            .MapUsing(versor)
            .DecodeOpnsElement();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade MapUsingMusicalIsomorphism(this CGaFloat64Blade blade)
    {
        return new CGaFloat64Blade(
            blade.GeometricSpace,
            blade.GeometricSpace.MusicalIsomorphism.OmMap(blade.InternalKVector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade MapUsing(this CGaFloat64Blade blade, IRGaFloat64Outermorphism outerMorphism)
    {
        return new CGaFloat64Blade(
            blade.GeometricSpace,
            outerMorphism.OmMap(blade.InternalKVector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade MapUsing(this CGaFloat64Blade blade, CGaFloat64Versor versor)
    {
        return versor.MapBlade(blade.InternalKVector);
    }

}