using System.Runtime.CompilerServices;
using DataStructuresLib.Dictionary;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Basis;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Processors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors.Composers
{
    public static class RGaKVectorComposerUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaHigherKVector<T> CreateZeroHigherKVector<T>(this RGaProcessor<T> processor, int grade)
        {
            return new RGaHigherKVector<T>(processor, grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaHigherKVector<T> CreateHigherKVector<T>(this RGaProcessor<T> processor, ulong id)
        {
            var grade = id.Grade();

            return new RGaHigherKVector<T>(
                processor,

                grade,
                new SingleItemDictionary<ulong, T>(id, processor.ScalarProcessor.ScalarOne)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaHigherKVector<T> CreateHigherKVector<T>(this RGaProcessor<T> processor, ulong id, T scalar)
        {
            var grade = id.Grade();

            return processor.ScalarProcessor.IsZero(scalar)
                ? new RGaHigherKVector<T>(processor, grade)
                : new RGaHigherKVector<T>(processor, grade, new SingleItemDictionary<ulong, T>(id, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaHigherKVector<T> CreateHigherKVector<T>(this RGaProcessor<T> processor, KeyValuePair<ulong, T> term)
        {
            var (id, scalar) = term;

            var grade = id.Grade();

            return processor.ScalarProcessor.IsZero(scalar)
                ? new RGaHigherKVector<T>(processor, grade)
                : new RGaHigherKVector<T>(processor, grade, new SingleItemDictionary<ulong, T>(id, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaHigherKVector<T> CreateHigherKVector<T>(this RGaProcessor<T> processor, int grade, IReadOnlyDictionary<ulong, T> basisScalarDictionary)
        {
            if (basisScalarDictionary.Count == 0 && basisScalarDictionary is not EmptyDictionary<ulong, T>)
                return processor.CreateZeroHigherKVector(grade);

            if (basisScalarDictionary.Count == 1 && basisScalarDictionary is not SingleItemDictionary<ulong, T>)
                return processor.CreateHigherKVector(basisScalarDictionary.First());

            return new RGaHigherKVector<T>(
                processor,
                grade,
                basisScalarDictionary
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaKVector<T> CreateZeroKVector<T>(this RGaProcessor<T> processor, int grade)
        {
            if (grade < 0)
                throw new ArgumentOutOfRangeException(nameof(grade));

            return grade switch
            {
                0 => processor.CreateZeroScalar(),
                1 => processor.CreateZeroVector(),
                2 => processor.CreateZeroBivector(),
                _ => new RGaHigherKVector<T>(processor, grade)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaKVector<T> CreateKVector<T>(this RGaProcessor<T> processor, KeyValuePair<ulong, T> term)
        {
            var grade = term.Key.Grade();

            return grade switch
            {
                0 => new RGaScalar<T>(processor, term.Value),
                1 => new RGaVector<T>(processor, term),
                2 => new RGaBivector<T>(processor, term),
                _ => new RGaHigherKVector<T>(processor, term)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaKVector<T> CreateKVector<T>(this RGaProcessor<T> processor, int grade, IReadOnlyDictionary<ulong, T> basisScalarDictionary)
        {
            if (basisScalarDictionary.Count == 0 && basisScalarDictionary is not EmptyDictionary<ulong, T>)
                return processor.CreateZeroKVector(grade);

            if (basisScalarDictionary.Count == 1 && basisScalarDictionary is not SingleItemDictionary<ulong, T>)
                return processor.CreateKVector(basisScalarDictionary.First());

            return grade switch
            {
                0 => new RGaScalar<T>(processor, basisScalarDictionary),
                1 => new RGaVector<T>(processor, basisScalarDictionary),
                2 => new RGaBivector<T>(processor, basisScalarDictionary),
                _ => new RGaHigherKVector<T>(processor, grade, basisScalarDictionary)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaKVector<T> CreateKVector<T>(this RGaProcessor<T> processor, ulong basisBlade)
        {
            return processor.CreateKVector(
                new KeyValuePair<ulong, T>(basisBlade, processor.ScalarProcessor.ScalarOne)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaKVector<T> CreateKVector<T>(this RGaProcessor<T> processor, ulong basisBlade, T scalar)
        {
            var grade = basisBlade.Grade();

            if (processor.ScalarProcessor.IsZero(scalar))
                return processor.CreateZeroKVector(grade);

            return processor.CreateKVector(

                new KeyValuePair<ulong, T>(basisBlade, scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaKVector<T> CreatePseudoScalar<T>(this RGaProcessor<T> processor, int vSpaceDimensions)
        {
            var id = processor.GetBasisPseudoScalarId(vSpaceDimensions);

            return processor.CreateKVector(

                new KeyValuePair<ulong, T>(id, processor.ScalarProcessor.ScalarOne)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaKVector<T> CreatePseudoScalar<T>(this RGaProcessor<T> processor, int vSpaceDimensions, T scalarValue)
        {
            var id = processor.GetBasisPseudoScalarId(vSpaceDimensions);

            return processor.CreateKVector(

                new KeyValuePair<ulong, T>(id, scalarValue)
            );
        }

        public static RGaKVector<T> CreatePseudoScalarReverse<T>(this RGaProcessor<T> processor, int vSpaceDimensions)
        {
            var id =
                processor.GetBasisPseudoScalarId(vSpaceDimensions);

            var scalar =
                vSpaceDimensions.ReverseIsNegativeOfGrade()
                    ? processor.ScalarProcessor.ScalarMinusOne
                    : processor.ScalarProcessor.ScalarOne;

            return processor.CreateKVector(

                new KeyValuePair<ulong, T>(id, scalar)
            );
        }

        public static RGaKVector<T> CreatePseudoScalarConjugate<T>(this RGaProcessor<T> processor, int vSpaceDimensions)
        {
            var id =
                processor.GetBasisPseudoScalarId(vSpaceDimensions);

            var sign =
                processor.ConjugateSign(id);

            if (sign.IsZero)
                throw new DivideByZeroException();

            var scalar = sign.ToScalarValue(processor.ScalarProcessor);

            return processor.CreateKVector(

                new KeyValuePair<ulong, T>(id, scalar)
            );
        }

        public static RGaKVector<T> CreatePseudoScalarEInverse<T>(this RGaProcessor<T> processor, int vSpaceDimensions)
        {
            var id =
                processor.GetBasisPseudoScalarId(vSpaceDimensions);

            var sign =
                processor.EGpSquaredSign(id);

            var scalar = sign.ToScalarValue(processor.ScalarProcessor);

            return processor.CreateKVector(

                new KeyValuePair<ulong, T>(id, scalar)
            );
        }

        public static RGaKVector<T> CreatePseudoScalarInverse<T>(this RGaProcessor<T> processor, int vSpaceDimensions)
        {
            var id =
                processor.GetBasisPseudoScalarId(vSpaceDimensions);

            var sign =
                processor.GpSquaredSign(id);

            if (sign.IsZero)
                throw new DivideByZeroException();

            var scalar = sign.ToScalarValue(processor.ScalarProcessor);

            return processor.CreateKVector(

                new KeyValuePair<ulong, T>(id, scalar)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaKVector<T> ToKVector<T>(this RGaBasisBlade basisBlade)
        {
            var processor = (RGaProcessor<T>)basisBlade.Metric;

            return processor.CreateKVector(
                basisBlade.Id,
                processor.ScalarProcessor.ScalarOne
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaKVector<T> ToKVector<T>(this RGaSignedBasisBlade basisBlade)
        {
            var processor = (RGaProcessor<T>)basisBlade.Metric;

            return processor.CreateKVector(
                basisBlade.Id,
                basisBlade.Sign.ToScalarValue(processor.ScalarProcessor)
            );
        }


    }
}