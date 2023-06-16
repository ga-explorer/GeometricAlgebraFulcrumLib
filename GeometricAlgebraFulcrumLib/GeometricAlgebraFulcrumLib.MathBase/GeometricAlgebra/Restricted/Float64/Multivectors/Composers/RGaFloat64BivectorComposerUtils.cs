using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using DataStructuresLib.Dictionary;
using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Multivectors.Composers
{
    public static class RGaFloat64BivectorComposerUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Dictionary<ulong, double> CreateBivectorDictionary(this IReadOnlyDictionary<IndexPair, double> inputDictionary)
        {
            var basisScalarDictionary = new Dictionary<ulong, double>();

            foreach (var (indexPair, value) in inputDictionary)
                basisScalarDictionary.Add(
                    1UL << indexPair.Item1 | 1UL << indexPair.Item2,
                    value
                );

            return basisScalarDictionary;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Dictionary<ulong, double> CreateBivectorDictionary(this IReadOnlyDictionary<Int32Pair, double> inputDictionary)
        {
            var basisScalarDictionary = new Dictionary<ulong, double>();

            foreach (var (indexPair, value) in inputDictionary)
                basisScalarDictionary.Add(
                    1UL << indexPair.Item1 | 1UL << indexPair.Item2,
                    value
                );

            return basisScalarDictionary;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Bivector CreateZeroBivector(this RGaFloat64Processor metric)
        {
            return new RGaFloat64Bivector(metric);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Bivector CreateBivector(this RGaFloat64Processor metric, IReadOnlyDictionary<IndexPair, double> basisScalarDictionary)
        {
            return new RGaFloat64Bivector(
                metric,
                basisScalarDictionary.CreateBivectorDictionary()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Bivector CreateBivector(this RGaFloat64Processor metric, IReadOnlyDictionary<Int32Pair, double> basisScalarDictionary)
        {
            return new RGaFloat64Bivector(
                metric,
                basisScalarDictionary.CreateBivectorDictionary()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Bivector CreateBivector(this RGaFloat64Processor metric, IReadOnlyDictionary<ulong, double> basisScalarDictionary)
        {
            if (basisScalarDictionary.Count == 0 && basisScalarDictionary is not EmptyDictionary<ulong, double>)
                return metric.CreateZeroBivector();

            if (basisScalarDictionary.Count == 1 && basisScalarDictionary is not SingleItemDictionary<ulong, double>)
                return metric.CreateBivector(basisScalarDictionary.First());

            return new RGaFloat64Bivector(
                metric,

                basisScalarDictionary
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Bivector CreateBivector(this RGaFloat64Processor metric, int index1, int index2)
        {
            if (index1 < 0 || index1 >= index2 || index2 >= 64)
                throw new InvalidOperationException();

            return new RGaFloat64Bivector(
                metric,
                new SingleItemDictionary<ulong, double>(
                    1UL << index1 | 1UL << index2,
                    1d
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Bivector CreateBivector(this RGaFloat64Processor metric, int index1, int index2, double scalar)
        {
            if (index1 < 0 || index1 >= index2 || index2 >= 64)
                throw new InvalidOperationException();

            if (scalar.IsZero())
                return new RGaFloat64Bivector(metric);

            return new RGaFloat64Bivector(
                metric,
                new SingleItemDictionary<ulong, double>(
                    1UL << index1 | 1UL << index2,
                    scalar
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Bivector CreateBivector(this RGaFloat64Processor metric, IPair<int> indexPair)
        {
            var index1 = indexPair.Item1;
            var index2 = indexPair.Item2;

            if (index1 < 0 || index1 >= index2 || index2 >= 64)
                throw new InvalidOperationException();

            return new RGaFloat64Bivector(
                metric,
                new SingleItemDictionary<ulong, double>(
                    1UL << index1 | 1UL << index2,
                    1d
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Bivector CreateBivector(this RGaFloat64Processor metric, IPair<int> indexPair, double scalar)
        {
            var index1 = indexPair.Item1;
            var index2 = indexPair.Item2;

            if (index1 < 0 || index1 >= index2 || index2 >= 64)
                throw new InvalidOperationException();

            if (scalar.IsZero())
                return new RGaFloat64Bivector(metric);

            return new RGaFloat64Bivector(
                metric,
                new SingleItemDictionary<ulong, double>(
                    1UL << index1 | 1UL << index2,
                    scalar
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Bivector CreateBivector(this RGaFloat64Processor metric, KeyValuePair<Int32Pair, double> indexScalarPair)
        {
            return metric.CreateBivector(
                indexScalarPair.Key,
                indexScalarPair.Value
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Bivector CreateBivector(this RGaFloat64Processor metric, KeyValuePair<ulong, double> indexScalarPair)
        {
            return metric.CreateBivector(indexScalarPair.Key, indexScalarPair.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Bivector CreateBivector(this RGaFloat64Processor metric, ulong basisBlade)
        {
            return new RGaFloat64Bivector(
                metric,
                new SingleItemDictionary<ulong, double>(basisBlade, 1d)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Bivector CreateBivector(this RGaFloat64Processor metric, ulong basisBlade, double scalar)
        {
            if (scalar.IsZero())
                return new RGaFloat64Bivector(metric);

            return new RGaFloat64Bivector(
                metric,

                new SingleItemDictionary<ulong, double>(basisBlade, scalar)
            );
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Bivector CreateBivector2D(this RGaFloat64Processor processor, double scalar01)
        {
            return processor
                .CreateComposer()
                .SetTerm(3, scalar01)
                .GetBivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Bivector CreateBivector3D(this RGaFloat64Processor processor, double scalar01, double scalar02, double scalar12)
        {
            return processor
                .CreateComposer()
                .SetTerm(3, scalar01)
                .SetTerm(5, scalar02)
                .SetTerm(6, scalar12)
                .GetBivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Bivector CreateBivector3D(this RGaFloat64Processor processor, Float64Bivector3D bivector)
        {
            return processor
                .CreateComposer()
                .SetTerm(3, bivector.Xy)
                .SetTerm(5, bivector.Xz)
                .SetTerm(6, bivector.Yz)
                .GetBivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Bivector ToBivector(this Float64Bivector3D bivector)
        {
            return RGaFloat64Processor
                .Euclidean
                .CreateComposer()
                .SetTerm(3, bivector.Xy)
                .SetTerm(5, bivector.Xz)
                .SetTerm(6, bivector.Yz)
                .GetBivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Bivector ToBivector(this Float64Bivector3D bivector, RGaFloat64Processor processor)
        {
            return processor
                .CreateComposer()
                .SetTerm(3, bivector.Xy)
                .SetTerm(5, bivector.Xz)
                .SetTerm(6, bivector.Yz)
                .GetBivector();
        }
    }
}