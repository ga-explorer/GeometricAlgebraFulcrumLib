using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Blades;

public static class RGaConformalBladeUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<RGaConformalBlade> GetBasisBladesEGa(this RGaConformalSpace conformalSpace)
    {
        return (1UL << (conformalSpace.VSpaceDimensions - 2))
            .GetRange()
            .OrderBy(id => id.Grade())
            .ThenBy(id => id)
            .Select(id =>
                conformalSpace
                    .ConformalProcessor
                    .CreateTermKVector(id << 2)
                    .ToConformalBlade(conformalSpace)
            );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<RGaConformalBlade> GetBasisBladesPGa(this RGaConformalSpace conformalSpace)
    {
        return (1UL << (conformalSpace.VSpaceDimensions - 1))
            .GetRange()
            .OrderBy(id => id.Grade())
            .ThenBy(id => id)
            .Select(id => 
                conformalSpace
                    .LaTeXBasisMapInverse
                    .OmMapBasisBlade(id)
                    .ToConformalBlade(conformalSpace)
            );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<RGaConformalBlade> GetBasisBladesCGa(this RGaConformalSpace conformalSpace)
    {
        return conformalSpace
            .GaSpaceDimensions
            .GetRange()
            .OrderBy(id => id.Grade())
            .ThenBy(id => id)
            .Select(id => 
                conformalSpace
                    .LaTeXBasisMapInverse
                    .OmMapBasisBlade(id)
                    .ToConformalBlade(conformalSpace)
            );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<RGaConformalBlade> GetBasisBladesCGaInf(this RGaConformalSpace conformalSpace)
    {
        return (1UL << (conformalSpace.VSpaceDimensions - 1))
            .GetRange(id => id << 1)
            .OrderBy(id => id.Grade())
            .ThenBy(id => id)
            .Select(id => 
                conformalSpace
                    .LaTeXBasisMapInverse
                    .OmMapBasisBlade(id)
                    .ToConformalBlade(conformalSpace)
            );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector Gp(this RGaFloat64Multivector mv, RGaConformalBlade blade)
    {
        return mv.Gp(blade.InternalKVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade Op(this IEnumerable<RGaConformalBlade> bladeList)
    {
        return new RGaConformalBlade(
            bladeList.First().ConformalSpace,
            bladeList.Select(blade => blade.InternalKVector).Op().GetFirstKVectorPart()
        );
    }
}