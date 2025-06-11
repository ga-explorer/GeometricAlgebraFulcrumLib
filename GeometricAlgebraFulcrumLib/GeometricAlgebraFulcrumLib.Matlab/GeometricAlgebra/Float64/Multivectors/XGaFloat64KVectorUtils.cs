using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Processors;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Multivectors;

public static class XGaFloat64KVectorUtils
{
    
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