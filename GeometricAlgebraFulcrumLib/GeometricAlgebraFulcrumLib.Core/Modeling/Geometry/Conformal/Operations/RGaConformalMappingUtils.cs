using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Decoding;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Elements;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Versors;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Operations;

public static class RGaConformalMappingUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalElement MapUsing(this RGaConformalElement element, IRGaFloat64Outermorphism outerMorphism)
    {
        return element
            .EncodeOpnsBlade()
            .MapUsing(outerMorphism)
            .DecodeOpnsElement();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalElement MapUsing(this RGaConformalElement element, RGaConformalVersor versor)
    {
        return element
            .EncodeOpnsBlade()
            .MapUsing(versor)
            .DecodeOpnsElement();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade MapUsingMusicalIsomorphism(this RGaConformalBlade blade)
    {
        return new RGaConformalBlade(
            blade.ConformalSpace,
            blade.ConformalSpace.MusicalIsomorphism.OmMap(blade.InternalKVector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade MapUsing(this RGaConformalBlade blade, IRGaFloat64Outermorphism outerMorphism)
    {
        return new RGaConformalBlade(
            blade.ConformalSpace,
            outerMorphism.OmMap(blade.InternalKVector)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade MapUsing(this RGaConformalBlade blade, RGaConformalVersor versor)
    {
        return versor.MapBlade(blade.InternalKVector);
    }

}