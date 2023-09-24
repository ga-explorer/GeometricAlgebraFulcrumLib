using DataStructuresLib.Dictionary;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic
{
    public static class RGaMetricUtils
    {
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsValidMultivectorDictionary<T>(this RGaMetric metric, IReadOnlyDictionary<int, RGaKVector<T>> gradeKVectorDictionary)
        {
            return gradeKVectorDictionary.Count switch
            {
                0 => gradeKVectorDictionary is EmptyDictionary<int, RGaKVector<T>>,

                1 => gradeKVectorDictionary is SingleItemDictionary<int, RGaKVector<T>> dict &&
                     dict.Key >= 0 &&
                     dict.Value.Metric.HasSameSignature(metric) &&
                     dict.Value.IsValid(),

                _ => gradeKVectorDictionary.All(p =>
                    p.Key >= 0 &&
                    p.Value.Metric.HasSameSignature(metric) &&
                    p.Value.IsValid()
                )
            };
        }

    }
}
