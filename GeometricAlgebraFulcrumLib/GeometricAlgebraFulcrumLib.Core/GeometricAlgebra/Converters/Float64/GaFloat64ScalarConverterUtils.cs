using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Generic.Processors;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Converters.Float64;

public static class GaFloat64ScalarConverterUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> Convert<T>(this XGaProcessor<T> metric, Func<double, T> scalarMapping, XGaFloat64Scalar mv)
    {
        return new XGaScalar<T>(
            metric,
            scalarMapping(mv.ScalarValue)
        );
    }
}