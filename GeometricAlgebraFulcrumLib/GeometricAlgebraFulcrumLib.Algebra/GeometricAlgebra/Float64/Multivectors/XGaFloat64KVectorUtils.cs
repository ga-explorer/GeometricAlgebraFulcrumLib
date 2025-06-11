using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;

public static class XGaFloat64KVectorUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector Op(this IEnumerable<XGaFloat64Vector> mvList, XGaFloat64Processor processor)
    {
        XGaFloat64KVector blade = processor.ScalarOne;

        foreach (var vector in mvList)
        {
            var newBlade = blade.Op(vector);

            if (newBlade.IsZero)
                return processor.ScalarZero;

            blade = newBlade;
        }

        return blade;
    }
    
    public static XGaFloat64KVector SpanToBlade(this IEnumerable<XGaFloat64Vector> mvList, XGaFloat64Processor processor)
    {
        XGaFloat64KVector blade = processor.ScalarOne;

        foreach (var vector in mvList)
        {
            var newBlade = blade.Op(vector);

            if (newBlade.IsNearZero())
                continue;

            blade = newBlade;
        }

        return blade;
    }

}