using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Generic.Processors;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Converters.Generic;

public static class GaMultivectorConverterUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T2> Convert<T1, T2>(this XGaProcessor<T2> metric, Func<T1, T2> scalarMapping, XGaMultivector<T1> mv)
    {
        return mv switch
        {
            XGaScalar<T1> mv1 => metric.Convert(scalarMapping, mv1),
            XGaVector<T1> mv1 => metric.Convert(scalarMapping, mv1),
            XGaBivector<T1> mv1 => metric.Convert(scalarMapping, mv1),
            XGaHigherKVector<T1> mv1 => metric.Convert(scalarMapping, mv1),
            XGaGradedMultivector<T1> mv1 => metric.Convert(scalarMapping, mv1),
            _ => metric.Convert(scalarMapping, (XGaUniformMultivector<T1>)mv)
        };
    }
}