using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using DataStructuresLib.Dictionary;
using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Processors;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Multivectors.Composers
{
    public static class XGaFloat64BivectorComposerUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Dictionary<IIndexSet, double> CreateBivectorDictionary(this IReadOnlyDictionary<IndexPair, double> inputDictionary)
        {
            var basisScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<double>();

            foreach (var (key, value) in inputDictionary)
                basisScalarDictionary.Add(key.IndexPairToIndexSet(), value);

            return basisScalarDictionary;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Dictionary<IIndexSet, double> CreateBivectorDictionary(this IReadOnlyDictionary<Int32Pair, double> inputDictionary)
        {
            var basisScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<double>();

            foreach (var (key, value) in inputDictionary)
                basisScalarDictionary.Add(key.IndexPairToIndexSet(), value);

            return basisScalarDictionary;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Bivector CreateZeroBivector(this XGaFloat64Processor processor)
        {
            return new XGaFloat64Bivector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Bivector CreateBivector(this XGaFloat64Processor processor, IReadOnlyDictionary<IndexPair, double> basisScalarDictionary)
        {
            return new XGaFloat64Bivector(
                processor,
                basisScalarDictionary.CreateBivectorDictionary()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Bivector CreateBivector(this XGaFloat64Processor processor, IReadOnlyDictionary<Int32Pair, double> basisScalarDictionary)
        {
            return new XGaFloat64Bivector(
                processor,
                basisScalarDictionary.CreateBivectorDictionary()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Bivector CreateBivector(this XGaFloat64Processor processor, IReadOnlyDictionary<IIndexSet, double> basisScalarDictionary)
        {
            if (basisScalarDictionary.Count == 0 && basisScalarDictionary is not EmptyDictionary<IIndexSet, double>)
                return processor.CreateZeroBivector();

            if (basisScalarDictionary.Count == 1 && basisScalarDictionary is not SingleItemDictionary<IIndexSet, double>)
                return processor.CreateBivector(basisScalarDictionary.First());

            return new XGaFloat64Bivector(
                processor,
                basisScalarDictionary
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Bivector CreateBivector(this XGaFloat64Processor processor, int index1, int index2)
        {
            if (index1 < 0 || index1 >= index2)
                throw new InvalidOperationException();

            return new XGaFloat64Bivector(
                processor,
                new SingleItemDictionary<IIndexSet, double>(
                    IndexSetUtils.IndexPairToIndexSet(index1, index2),
                    1d
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Bivector CreateBivector(this XGaFloat64Processor processor, int index1, int index2, double scalar)
        {
            if (index1 < 0 || index1 >= index2)
                throw new InvalidOperationException();

            if (scalar.IsZero())
                return new XGaFloat64Bivector(processor);

            return new XGaFloat64Bivector(
                processor,
                new SingleItemDictionary<IIndexSet, double>(
                    IndexSetUtils.IndexPairToIndexSet(index1, index2),
                    scalar
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Bivector CreateBivector(this XGaFloat64Processor processor, IPair<int> indexPair)
        {
            return new XGaFloat64Bivector(
                processor,

                new SingleItemDictionary<IIndexSet, double>(
                    indexPair.IndexPairToIndexSet(),
                    1d
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Bivector CreateBivector(this XGaFloat64Processor processor, IPair<int> indexPair, double scalar)
        {
            if (scalar.IsZero())
                return new XGaFloat64Bivector(processor);

            return new XGaFloat64Bivector(
                processor,

                new SingleItemDictionary<IIndexSet, double>(
                    indexPair.IndexPairToIndexSet(),
                    scalar
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Bivector CreateBivector(this XGaFloat64Processor processor, KeyValuePair<Int32Pair, double> indexScalarPair)
        {
            return processor.CreateBivector(
                indexScalarPair.Key, indexScalarPair.Value);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Bivector CreateBivector(this XGaFloat64Processor processor, KeyValuePair<IIndexSet, double> indexScalarPair)
        {
            return processor.CreateBivector(indexScalarPair.Key, indexScalarPair.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Bivector CreateBivector(this XGaFloat64Processor processor, IIndexSet basisBlade)
        {
            return new XGaFloat64Bivector(
                processor,
                new SingleItemDictionary<IIndexSet, double>(basisBlade, 1d)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Bivector CreateBivector(this XGaFloat64Processor processor, IIndexSet basisBlade, double scalar)
        {
            if (scalar.IsZero())
                return new XGaFloat64Bivector(processor);

            return new XGaFloat64Bivector(
                processor,
                new SingleItemDictionary<IIndexSet, double>(basisBlade, scalar)
            );
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Bivector CreateBivector2D(this XGaFloat64Processor processor, double scalar01)
        {
            return processor
                .CreateComposer()
                .SetTerm(3, scalar01)
                .GetBivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Bivector CreateBivector3D(this XGaFloat64Processor processor, double scalar01, double scalar02, double scalar12)
        {
            return processor
                .CreateComposer()
                .SetTerm(3, scalar01)
                .SetTerm(5, scalar02)
                .SetTerm(6, scalar12)
                .GetBivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Bivector CreateBivector3D(this XGaFloat64Processor processor, Float64Bivector3D bivector)
        {
            return processor
                .CreateComposer()
                .SetTerm(3, bivector.Xy)
                .SetTerm(5, bivector.Xz)
                .SetTerm(6, bivector.Yz)
                .GetBivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Bivector ToBivector(this Float64Bivector3D bivector)
        {
            return XGaFloat64Processor.Euclidean
                .CreateComposer()
                .SetTerm(3, bivector.Xy)
                .SetTerm(5, bivector.Xz)
                .SetTerm(6, bivector.Yz)
                .GetBivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Bivector ToBivector(this Float64Bivector3D bivector, XGaFloat64Processor processor)
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