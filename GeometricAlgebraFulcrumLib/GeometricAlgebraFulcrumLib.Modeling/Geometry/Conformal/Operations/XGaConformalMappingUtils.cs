using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Decoding;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Elements;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Versors;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Operations;

public static class XGaConformalMappingUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalElement<T> MapUsing<T>(this XGaConformalElement<T> element, IXGaOutermorphism<T> outerMorphism)
    {
        return element
            .EncodeOpnsBlade()
            .MapUsing(outerMorphism)
            .DecodeOpnsElement();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalElement<T> MapUsing<T>(this XGaConformalElement<T> element, XGaConformalVersor<T> versor)
    {
        return element
            .EncodeOpnsBlade()
            .MapUsing(versor)
            .DecodeOpnsElement();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> MapUsingMusicalIsomorphism<T>(this XGaConformalBlade<T> blade)
    {
        return new XGaConformalBlade<T>(
            blade.ConformalSpace,
            blade.ConformalSpace.MusicalIsomorphism.OmMap(blade.InternalKVector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> MapUsing<T>(this XGaConformalBlade<T> blade, IXGaOutermorphism<T> outerMorphism)
    {
        return new XGaConformalBlade<T>(
            blade.ConformalSpace,
            outerMorphism.OmMap(blade.InternalKVector)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> MapUsing<T>(this XGaConformalBlade<T> blade, XGaConformalVersor<T> versor)
    {
        return versor.MapBlade(blade.InternalKVector);
    }

}