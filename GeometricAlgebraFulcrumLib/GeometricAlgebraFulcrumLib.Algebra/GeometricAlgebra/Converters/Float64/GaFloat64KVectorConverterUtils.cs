using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Converters.Float64;

public static class GaFloat64KVectorConverterUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> Convert<T>(this XGaProcessor<T> metric, Func<double, T> scalarMapping, XGaFloat64KVector mv)
    {
        return mv switch
        {
            XGaFloat64Scalar mv1 => metric.Convert(scalarMapping, mv1),
            XGaFloat64Vector mv1 => metric.Convert(scalarMapping, mv1),
            XGaFloat64Bivector mv1 => metric.Convert(scalarMapping, mv1),
            _ => metric.Convert(scalarMapping, (XGaFloat64HigherKVector)mv)
        };
    }
}