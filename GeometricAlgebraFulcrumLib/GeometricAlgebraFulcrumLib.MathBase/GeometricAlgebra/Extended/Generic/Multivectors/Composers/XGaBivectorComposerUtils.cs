using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using DataStructuresLib.Dictionary;
using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Euclidean3D;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Processors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors.Composers
{
    public static class XGaBivectorComposerUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Dictionary<IIndexSet, T> CreateBivectorDictionary<T>(this IReadOnlyDictionary<IndexPair, T> inputDictionary)
        {
            var basisScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<T>();

            foreach (var (key, value) in inputDictionary)
                basisScalarDictionary.Add(key.IndexPairToIndexSet(), value);

            return basisScalarDictionary;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Dictionary<IIndexSet, T> CreateBivectorDictionary<T>(this IReadOnlyDictionary<Int32Pair, T> inputDictionary)
        {
            var basisScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<T>();

            foreach (var (key, value) in inputDictionary)
                basisScalarDictionary.Add(key.IndexPairToIndexSet(), value);

            return basisScalarDictionary;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaBivector<T> CreateZeroBivector<T>(this XGaProcessor<T> processor)
        {
            return new XGaBivector<T>(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaBivector<T> CreateBivector<T>(this XGaProcessor<T> processor, IReadOnlyDictionary<IndexPair, T> basisScalarDictionary)
        {
            return new XGaBivector<T>(
                processor,
                basisScalarDictionary.CreateBivectorDictionary()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaBivector<T> CreateBivector<T>(this XGaProcessor<T> processor, IReadOnlyDictionary<Int32Pair, T> basisScalarDictionary)
        {
            return new XGaBivector<T>(
                processor,
                basisScalarDictionary.CreateBivectorDictionary()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaBivector<T> CreateBivector<T>(this XGaProcessor<T> processor, IReadOnlyDictionary<IIndexSet, T> basisScalarDictionary)
        {
            if (basisScalarDictionary.Count == 0 && basisScalarDictionary is not EmptyDictionary<IIndexSet, T>)
                return processor.CreateZeroBivector();

            if (basisScalarDictionary.Count == 1 && basisScalarDictionary is not SingleItemDictionary<IIndexSet, T>)
                return processor.CreateBivector(basisScalarDictionary.First());

            return new XGaBivector<T>(
                processor,
                basisScalarDictionary
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaBivector<T> CreateBivector<T>(this XGaProcessor<T> processor, IPair<int> indexPair)
        {
            var index1 = indexPair.Item1;
            var index2 = indexPair.Item2;

            if (index1 < 0 || index1 >= index2)
                throw new InvalidOperationException();

            return new XGaBivector<T>(
                processor,

                new SingleItemDictionary<IIndexSet, T>(
                    indexPair.IndexPairToIndexSet(),
                    processor.ScalarProcessor.ScalarOne
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaBivector<T> CreateBivector<T>(this XGaProcessor<T> processor, IPair<int> indexPair, T scalar)
        {
            var index1 = indexPair.Item1;
            var index2 = indexPair.Item2;

            if (index1 < 0 || index1 >= index2)
                throw new InvalidOperationException();

            if (processor.ScalarProcessor.IsZero(scalar))
                return new XGaBivector<T>(processor);

            return new XGaBivector<T>(
                processor,

                new SingleItemDictionary<IIndexSet, T>(
                    indexPair.IndexPairToIndexSet(),
                    scalar
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaBivector<T> CreateBivector<T>(this XGaProcessor<T> processor, int index1, int index2)
        {
            if (index1 < 0 || index1 >= index2)
                throw new InvalidOperationException();

            return new XGaBivector<T>(
                processor,

                new SingleItemDictionary<IIndexSet, T>(
                    IndexSetUtils.IndexPairToIndexSet(index1, index2),
                    processor.ScalarProcessor.ScalarOne
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaBivector<T> CreateBivector<T>(this XGaProcessor<T> processor, int index1, int index2, T scalar)
        {
            if (index1 < 0 || index1 >= index2)
                throw new InvalidOperationException();

            if (processor.ScalarProcessor.IsZero(scalar))
                return new XGaBivector<T>(processor);

            return new XGaBivector<T>(
                processor,

                new SingleItemDictionary<IIndexSet, T>(
                    IndexSetUtils.IndexPairToIndexSet(index1, index2),
                    scalar
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaBivector<T> CreateBivector<T>(this XGaProcessor<T> processor, KeyValuePair<Int32Pair, T> indexScalarPair)
        {
            return processor.CreateBivector(

                indexScalarPair.Key,
                indexScalarPair.Value
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaBivector<T> CreateBivector<T>(this XGaProcessor<T> processor, KeyValuePair<IIndexSet, T> indexScalarPair)
        {
            return processor.CreateBivector(indexScalarPair.Key, indexScalarPair.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaBivector<T> CreateBivector<T>(this XGaProcessor<T> processor, IIndexSet basisBlade)
        {
            return new XGaBivector<T>(
                processor,

                new SingleItemDictionary<IIndexSet, T>(basisBlade, processor.ScalarProcessor.ScalarOne)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaBivector<T> CreateBivector<T>(this XGaProcessor<T> processor, IIndexSet basisBlade, T scalar)
        {
            if (processor.ScalarProcessor.IsZero(scalar))
                return new XGaBivector<T>(processor);

            return new XGaBivector<T>(
                processor,

                new SingleItemDictionary<IIndexSet, T>(basisBlade, scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaBivector<T> CreateBivector<T>(this XGaProcessor<T> processor, IIndexSet basisBlade, Scalar<T> scalar)
        {
            if (scalar.IsZero())
                return new XGaBivector<T>(processor);

            return new XGaBivector<T>(
                processor,
                new SingleItemDictionary<IIndexSet, T>(basisBlade, scalar.ScalarValue)
            );
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaBivector<T> CreateBivector2D<T>(this XGaProcessor<T> processor, double scalar01)
        {
            return processor
                .CreateComposer()
                .SetTerm(3, scalar01)
                .GetBivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaBivector<T> CreateBivector3D<T>(this XGaProcessor<T> processor, double scalar01, double scalar02, double scalar12)
        {
            return processor
                .CreateComposer()
                .SetTerm(3, scalar01)
                .SetTerm(5, scalar02)
                .SetTerm(6, scalar12)
                .GetBivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaBivector<T> CreateBivector3D<T>(this XGaProcessor<T> processor, T scalar01, T scalar02, T scalar12)
        {
            return processor
                .CreateComposer()
                .SetTerm(3, scalar01)
                .SetTerm(5, scalar02)
                .SetTerm(6, scalar12)
                .GetBivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaBivector<T> CreateBivector3D<T>(this XGaProcessor<T> processor, EGa3Bivector bivector)
        {
            return processor
                .CreateComposer()
                .SetTerm(3, bivector.Xy)
                .SetTerm(5, bivector.Xz)
                .SetTerm(6, bivector.Yz)
                .GetBivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaBivector<T> ToBivector<T>(this EGa3Bivector bivector, XGaProcessor<T> processor)
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