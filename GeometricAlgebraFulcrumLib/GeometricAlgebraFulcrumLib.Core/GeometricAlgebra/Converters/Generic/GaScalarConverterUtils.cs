using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Generic.Processors;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Converters.Generic;

public static class GaScalarConverterUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T2> Convert<T1, T2>(this XGaProcessor<T2> metric, Func<T1, T2> scalarMapping, XGaScalar<T1> mv)
    {
        return new XGaScalar<T2>(
            metric,
            scalarMapping(mv.ScalarValue)
        );
    }


}