using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Converters.Generic;

public static class GaKVectorConverterUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T2> Convert<T1, T2>(this XGaProcessor<T2> metric, Func<T1, T2> scalarMapping, XGaKVector<T1> mv)
    {
        return mv switch
        {
            XGaScalar<T1> mv1 => metric.Convert(scalarMapping, mv1),
            XGaVector<T1> mv1 => metric.Convert(scalarMapping, mv1),
            XGaBivector<T1> mv1 => metric.Convert(scalarMapping, mv1),
            _ => metric.Convert(scalarMapping, (XGaHigherKVector<T1>)mv)
        };
    }
}