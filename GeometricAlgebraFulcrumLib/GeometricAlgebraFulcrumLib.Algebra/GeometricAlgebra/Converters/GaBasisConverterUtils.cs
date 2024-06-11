using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Converters;

public static class GaBasisConverterUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaBasisBlade Convert(this RGaFloat64Processor metric, XGaBasisBlade basisBlade)
    {
        return new RGaBasisBlade(
            metric,
            basisBlade.Id.ToUInt64()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBasisBlade Convert(this XGaMetric metric, RGaBasisBlade basisBlade)
    {
        return new XGaBasisBlade(
            metric,
            basisBlade.Id.BitPatternToUInt64IndexSet()
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaSignedBasisBlade Convert(this RGaFloat64Processor metric, XGaSignedBasisBlade basisBlade)
    {
        return new RGaSignedBasisBlade(
            metric,
            basisBlade.Id.ToUInt64(),
            basisBlade.Sign
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaSignedBasisBlade Convert(this XGaMetric metric, RGaSignedBasisBlade basisBlade)
    {
        return new XGaSignedBasisBlade(
            metric,
            basisBlade.Id.BitPatternToUInt64IndexSet(),
            basisBlade.Sign
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IRGaSignedBasisBlade Convert(this RGaFloat64Processor metric, IXGaSignedBasisBlade basisBlade)
    {
        return basisBlade.IsPositive
            ? new RGaBasisBlade(
                metric,
                basisBlade.Id.ToUInt64()
            )
            : new RGaSignedBasisBlade(
                metric,
                basisBlade.Id.ToUInt64(),
                basisBlade.Sign
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IXGaSignedBasisBlade Convert(this XGaMetric metric, IRGaSignedBasisBlade basisBlade)
    {
        return basisBlade.IsPositive
            ? new XGaBasisBlade(
                metric,
                basisBlade.Id.BitPatternToUInt64IndexSet()
            )
            : new XGaSignedBasisBlade(
                metric,
                basisBlade.Id.BitPatternToUInt64IndexSet(),
                basisBlade.Sign
            );
    }
}