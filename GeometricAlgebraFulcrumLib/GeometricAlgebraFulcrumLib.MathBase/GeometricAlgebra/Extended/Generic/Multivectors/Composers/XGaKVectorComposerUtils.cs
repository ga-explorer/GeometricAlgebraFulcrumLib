using System.Runtime.CompilerServices;
using DataStructuresLib.Dictionary;
using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Basis;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors.Composers
{
    public static class XGaKVectorComposerUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaHigherKVector<T> CreateZeroHigherKVector<T>(this XGaProcessor<T> processor, int grade)
        {
            return new XGaHigherKVector<T>(processor, grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaHigherKVector<T> CreateHigherKVector<T>(this XGaProcessor<T> processor, IIndexSet id)
        {
            var grade = id.Count;

            return new XGaHigherKVector<T>(
                processor,
                grade,
                new SingleItemDictionary<IIndexSet, T>(id, processor.ScalarProcessor.ScalarOne)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaHigherKVector<T> CreateHigherKVector<T>(this XGaProcessor<T> processor, IIndexSet id, T scalar)
        {
            var grade = id.Count;

            return processor.ScalarProcessor.IsZero(scalar)
                ? new XGaHigherKVector<T>(processor, grade)
                : new XGaHigherKVector<T>(processor, grade, new SingleItemDictionary<IIndexSet, T>(id, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaHigherKVector<T> CreateHigherKVector<T>(this XGaProcessor<T> processor, KeyValuePair<IIndexSet, T> term)
        {
            var (id, scalar) = term;

            var grade = id.Count;

            return processor.ScalarProcessor.IsZero(scalar)
                ? new XGaHigherKVector<T>(processor, grade)
                : new XGaHigherKVector<T>(processor, grade, new SingleItemDictionary<IIndexSet, T>(id, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaHigherKVector<T> CreateHigherKVector<T>(this XGaProcessor<T> processor, int grade, IReadOnlyDictionary<IIndexSet, T> basisScalarDictionary)
        {
            if (basisScalarDictionary.Count == 0 && basisScalarDictionary is not EmptyDictionary<IIndexSet, T>)
                return processor.CreateZeroHigherKVector(grade);

            if (basisScalarDictionary.Count == 1 && basisScalarDictionary is not SingleItemDictionary<IIndexSet, T>)
                return processor.CreateHigherKVector(basisScalarDictionary.First());

            return new XGaHigherKVector<T>(
                processor,

                grade,
                basisScalarDictionary
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaKVector<T> CreateZeroKVector<T>(this XGaProcessor<T> processor, int grade)
        {
            if (grade < 0)
                throw new ArgumentOutOfRangeException(nameof(grade));

            return grade switch
            {
                0 => processor.CreateZeroScalar(),
                1 => processor.CreateZeroVector(),
                2 => processor.CreateZeroBivector(),
                _ => new XGaHigherKVector<T>(processor, grade)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaKVector<T> CreateKVector<T>(this XGaProcessor<T> processor, KeyValuePair<IIndexSet, T> term)
        {
            var grade = term.Key.Count;

            return grade switch
            {
                0 => new XGaScalar<T>(processor, term.Value),
                1 => new XGaVector<T>(processor, term),
                2 => new XGaBivector<T>(processor, term),
                _ => new XGaHigherKVector<T>(processor, term)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaKVector<T> CreateKVector<T>(this XGaProcessor<T> processor, int grade, IReadOnlyDictionary<IIndexSet, T> basisScalarDictionary)
        {
            if (basisScalarDictionary.Count == 0 && basisScalarDictionary is not EmptyDictionary<IIndexSet, T>)
                return processor.CreateZeroKVector(grade);

            if (basisScalarDictionary.Count == 1 && basisScalarDictionary is not SingleItemDictionary<IIndexSet, T>)
                return processor.CreateKVector(basisScalarDictionary.First());

            return grade switch
            {
                0 => new XGaScalar<T>(processor, basisScalarDictionary),
                1 => new XGaVector<T>(processor, basisScalarDictionary),
                2 => new XGaBivector<T>(processor, basisScalarDictionary),
                _ => new XGaHigherKVector<T>(processor, grade, basisScalarDictionary)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaKVector<T> CreateKVector<T>(this XGaProcessor<T> processor, IIndexSet basisBlade)
        {
            return processor.CreateKVector(

                new KeyValuePair<IIndexSet, T>(basisBlade, processor.ScalarProcessor.ScalarOne)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaKVector<T> CreateKVector<T>(this XGaProcessor<T> processor, IIndexSet basisBlade, T scalar)
        {
            var grade = basisBlade.Count;

            if (processor.ScalarProcessor.IsZero(scalar))
                return processor.CreateZeroKVector(grade);

            return processor.CreateKVector(

                new KeyValuePair<IIndexSet, T>(basisBlade, scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaKVector<T> CreatePseudoScalar<T>(this XGaProcessor<T> processor, int vSpaceDimensions)
        {
            var id = processor.GetBasisPseudoScalarId(vSpaceDimensions);

            return processor.CreateKVector(

                new KeyValuePair<IIndexSet, T>(id, processor.ScalarProcessor.ScalarOne)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaKVector<T> CreatePseudoScalar<T>(this XGaProcessor<T> processor, int vSpaceDimensions, T scalarValue)
        {
            var id = processor.GetBasisPseudoScalarId(vSpaceDimensions);

            return processor.CreateKVector(

                new KeyValuePair<IIndexSet, T>(id, scalarValue)
            );
        }

        public static XGaKVector<T> CreatePseudoScalarReverse<T>(this XGaProcessor<T> processor, int vSpaceDimensions)
        {
            var id =
                processor.GetBasisPseudoScalarId(vSpaceDimensions);

            var scalar =
                vSpaceDimensions.ReverseIsNegativeOfGrade()
                    ? processor.ScalarProcessor.ScalarMinusOne
                    : processor.ScalarProcessor.ScalarOne;

            return processor.CreateKVector(

                new KeyValuePair<IIndexSet, T>(id, scalar)
            );
        }

        public static XGaKVector<T> CreatePseudoScalarConjugate<T>(this XGaProcessor<T> processor, int vSpaceDimensions)
        {
            var id =
                processor.GetBasisPseudoScalarId(vSpaceDimensions);

            var sign =
                processor.ConjugateSign(id);

            if (sign.IsZero)
                throw new DivideByZeroException();

            var scalar = sign.ToScalarValue(processor.ScalarProcessor);

            return processor.CreateKVector(

                new KeyValuePair<IIndexSet, T>(id, scalar)
            );
        }

        public static XGaKVector<T> CreatePseudoScalarEInverse<T>(this XGaProcessor<T> processor, int vSpaceDimensions)
        {
            var id =
                processor.GetBasisPseudoScalarId(vSpaceDimensions);

            var sign =
                processor.EGpSquaredSign(id);

            var scalar = sign.ToScalarValue(processor.ScalarProcessor);

            return processor.CreateKVector(

                new KeyValuePair<IIndexSet, T>(id, scalar)
            );
        }

        public static XGaKVector<T> CreatePseudoScalarInverse<T>(this XGaProcessor<T> processor, int vSpaceDimensions)
        {
            var id =
                processor.GetBasisPseudoScalarId(vSpaceDimensions);

            var sign =
                processor.GpSquaredSign(id);

            if (sign.IsZero)
                throw new DivideByZeroException();

            var scalar = sign.ToScalarValue(processor.ScalarProcessor);

            return processor.CreateKVector(

                new KeyValuePair<IIndexSet, T>(id, scalar)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaKVector<T> ToKVector<T>(this XGaBasisBlade basisBlade, XGaProcessor<T> processor)
        {
            return processor.CreateKVector(
                basisBlade.Id,
                processor.ScalarProcessor.ScalarOne
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaKVector<T> ToKVector<T>(this XGaSignedBasisBlade basisBlade, XGaProcessor<T> processor)
        {
            return processor.CreateKVector(
                basisBlade.Id,
                basisBlade.Sign.ToScalarValue(processor.ScalarProcessor)
            );
        }

    }
}