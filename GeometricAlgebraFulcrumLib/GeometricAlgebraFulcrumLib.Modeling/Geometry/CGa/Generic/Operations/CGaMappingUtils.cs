using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Elements;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Versors;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Operations;

public static class CGaMappingUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaElement<T> MapUsing<T>(this CGaElement<T> element, IXGaOutermorphism<T> outerMorphism)
    {
        return element
            .EncodeOpnsBlade()
            .MapUsing(outerMorphism)
            .Decode.OpnsElement();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaElement<T> MapUsing<T>(this CGaElement<T> element, CGaVersor<T> versor)
    {
        return element
            .EncodeOpnsBlade()
            .MapUsing(versor)
            .Decode.OpnsElement();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> MapUsingMusicalIsomorphism<T>(this CGaBlade<T> blade)
    {
        return new CGaBlade<T>(
            blade.GeometricSpace,
            blade.GeometricSpace.MusicalIsomorphism.OmMap(blade.InternalKVector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> MapUsing<T>(this CGaBlade<T> blade, IXGaOutermorphism<T> outerMorphism)
    {
        return new CGaBlade<T>(
            blade.GeometricSpace,
            outerMorphism.OmMap(blade.InternalKVector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> MapUsing<T>(this CGaBlade<T> blade, CGaVersor<T> versor)
    {
        return versor.MapBlade(blade.InternalKVector);
    }

}