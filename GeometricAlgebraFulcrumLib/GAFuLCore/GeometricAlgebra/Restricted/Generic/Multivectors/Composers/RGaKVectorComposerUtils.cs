using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Basis;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Dictionary;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;

public static class RGaKVectorComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaHigherKVector<T> HigherKVectorZero<T>(this RGaProcessor<T> processor, int grade)
    {
        return new RGaHigherKVector<T>(processor, grade);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaHigherKVector<T> HigherKVectorTerm<T>(this RGaProcessor<T> processor, ulong id)
    {
        var grade = id.Grade();

        return new RGaHigherKVector<T>(
            processor,

            grade,
            new SingleItemDictionary<ulong, T>(id, processor.ScalarProcessor.OneValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaHigherKVector<T> HigherKVectorTerm<T>(this RGaProcessor<T> processor, ulong id, T scalar)
    {
        var grade = id.Grade();

        return processor.ScalarProcessor.IsZero(scalar)
            ? new RGaHigherKVector<T>(processor, grade)
            : new RGaHigherKVector<T>(processor, grade, new SingleItemDictionary<ulong, T>(id, scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaHigherKVector<T> HigherKVectorTerm<T>(this RGaProcessor<T> processor, ulong id, IScalar<T> scalar)
    {
        return HigherKVectorTerm(processor, id, scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaHigherKVector<T> HigherKVectorTerm<T>(this RGaProcessor<T> processor, KeyValuePair<ulong, T> term)
    {
        var (id, scalar) = term;

        var grade = id.Grade();

        return processor.ScalarProcessor.IsZero(scalar)
            ? new RGaHigherKVector<T>(processor, grade)
            : new RGaHigherKVector<T>(processor, grade, new SingleItemDictionary<ulong, T>(id, scalar));
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaHigherKVector<T> HigherKVector<T>(this RGaProcessor<T> processor, int grade, IReadOnlyDictionary<ulong, T> basisScalarDictionary)
    {
        if (basisScalarDictionary.Count == 0 && basisScalarDictionary is not EmptyDictionary<ulong, T>)
            return processor.HigherKVectorZero(grade);

        if (basisScalarDictionary.Count == 1 && basisScalarDictionary is not SingleItemDictionary<ulong, T>)
            return processor.HigherKVectorTerm(basisScalarDictionary.First());

        return new RGaHigherKVector<T>(
            processor,
            grade,
            basisScalarDictionary
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaKVector<T> KVectorZero<T>(this RGaProcessor<T> processor, int grade)
    {
        if (grade < 0)
            throw new ArgumentOutOfRangeException(nameof(grade));

        return grade switch
        {
            0 => processor.ScalarZero,
            1 => processor.VectorZero,
            2 => processor.BivectorZero,
            _ => new RGaHigherKVector<T>(processor, grade)
        };
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaKVector<T> KVectorTerm<T>(this RGaProcessor<T> processor, KeyValuePair<ulong, T> term)
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
    public static RGaKVector<T> KVectorTerm<T>(this RGaProcessor<T> processor, ulong basisBlade)
    {
        return processor.KVectorTerm(
            new KeyValuePair<ulong, T>(basisBlade, processor.ScalarProcessor.OneValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaKVector<T> KVectorTerm<T>(this RGaProcessor<T> processor, ulong basisBlade, T scalar)
    {
        var grade = basisBlade.Grade();

        if (processor.ScalarProcessor.IsZero(scalar))
            return processor.KVectorZero(grade);

        return processor.KVectorTerm(

            new KeyValuePair<ulong, T>(basisBlade, scalar)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaKVector<T> KVectorTerm<T>(this RGaProcessor<T> processor, ulong basisBlade, IScalar<T> scalar)
    {
        return KVectorTerm(processor, basisBlade, scalar.ScalarValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaKVector<T> PseudoScalar<T>(this RGaProcessor<T> processor, int vSpaceDimensions)
    {
        var id = processor.GetBasisPseudoScalarId(vSpaceDimensions);

        return processor.KVectorTerm(

            new KeyValuePair<ulong, T>(id, processor.ScalarProcessor.OneValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaKVector<T> PseudoScalar<T>(this RGaProcessor<T> processor, int vSpaceDimensions, T scalarValue)
    {
        var id = processor.GetBasisPseudoScalarId(vSpaceDimensions);

        return processor.KVectorTerm(

            new KeyValuePair<ulong, T>(id, scalarValue)
        );
    }

    public static RGaKVector<T> PseudoScalarReverse<T>(this RGaProcessor<T> processor, int vSpaceDimensions)
    {
        var id =
            processor.GetBasisPseudoScalarId(vSpaceDimensions);

        var scalar =
            vSpaceDimensions.ReverseIsNegativeOfGrade()
                ? processor.ScalarProcessor.MinusOneValue
                : processor.ScalarProcessor.OneValue;

        return processor.KVectorTerm(

            new KeyValuePair<ulong, T>(id, scalar)
        );
    }

    public static RGaKVector<T> PseudoScalarConjugate<T>(this RGaProcessor<T> processor, int vSpaceDimensions)
    {
        var id =
            processor.GetBasisPseudoScalarId(vSpaceDimensions);

        var sign =
            processor.HermitianConjugateSign(id);

        if (sign.IsZero)
            throw new DivideByZeroException();

        var scalar = sign.ValueFromNumber(processor.ScalarProcessor);

        return processor.KVectorTerm(

            new KeyValuePair<ulong, T>(id, scalar)
        );
    }

    public static RGaKVector<T> PseudoScalarEInverse<T>(this RGaProcessor<T> processor, int vSpaceDimensions)
    {
        var id =
            processor.GetBasisPseudoScalarId(vSpaceDimensions);

        var sign =
            processor.EGpSquaredSign(id);

        var scalar = sign.ValueFromNumber(processor.ScalarProcessor);

        return processor.KVectorTerm(

            new KeyValuePair<ulong, T>(id, scalar)
        );
    }

    public static RGaKVector<T> PseudoScalarInverse<T>(this RGaProcessor<T> processor, int vSpaceDimensions)
    {
        var id =
            processor.GetBasisPseudoScalarId(vSpaceDimensions);

        var sign =
            processor.GpSquaredSign(id);

        if (sign.IsZero)
            throw new DivideByZeroException();

        var scalar = sign.ValueFromNumber(processor.ScalarProcessor);

        return processor.KVectorTerm(

            new KeyValuePair<ulong, T>(id, scalar)
        );
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaKVector<T> KVector<T>(this RGaProcessor<T> processor, int grade, IReadOnlyDictionary<ulong, T> basisScalarDictionary)
    {
        if (basisScalarDictionary.Count == 0 && basisScalarDictionary is not EmptyDictionary<ulong, T>)
            return processor.KVectorZero(grade);

        if (basisScalarDictionary.Count == 1 && basisScalarDictionary is not SingleItemDictionary<ulong, T>)
            return processor.KVectorTerm(basisScalarDictionary.First());

        return grade switch
        {
            0 => new RGaScalar<T>(processor, basisScalarDictionary),
            1 => new RGaVector<T>(processor, basisScalarDictionary),
            2 => new RGaBivector<T>(processor, basisScalarDictionary),
            _ => new RGaHigherKVector<T>(processor, grade, basisScalarDictionary)
        };
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaKVector<T> ToKVector<T>(this RGaBasisBlade basisBlade)
    {
        var processor = (RGaProcessor<T>)basisBlade.Metric;

        return processor.KVectorTerm(
            basisBlade.Id,
            processor.ScalarProcessor.OneValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaKVector<T> ToKVector<T>(this RGaSignedBasisBlade basisBlade)
    {
        var processor = (RGaProcessor<T>)basisBlade.Metric;

        return processor.KVectorTerm(
            basisBlade.Id,
            basisBlade.Sign.ValueFromNumber(processor.ScalarProcessor)
        );
    }


}