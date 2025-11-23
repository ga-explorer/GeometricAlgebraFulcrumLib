using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Elements;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Blades;

public static class CGaFloat64BladeUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector Gp(this XGaFloat64Multivector mv, CGaFloat64Blade blade)
    {
        return mv.Gp(blade.InternalKVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade Op(this IEnumerable<CGaFloat64Blade> bladeList)
    {
        return new CGaFloat64Blade(
            bladeList.First().GeometricSpace,
            bladeList.Select(blade => blade.InternalKVector).Op().GetFirstKVectorPart()
        );
    }
}