using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Core.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Core.Structures.Dictionary;
using GeometricAlgebraFulcrumLib.Core.Structures.IndexSets;

// ReSharper disable InconsistentNaming

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Generic.Multivectors.Composers;

public static class XGaKVectorComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaHigherKVector<T> HigherKVectorZero<T>(this XGaProcessor<T> processor, int grade)
    {
        return new XGaHigherKVector<T>(processor, grade);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaHigherKVector<T> HigherKVectorTerm<T>(this XGaProcessor<T> processor, IndexSet id)
    {
        var grade = id.Count;

        return new XGaHigherKVector<T>(
            processor,
            grade,
            new SingleItemDictionary<IndexSet, T>(id, processor.ScalarProcessor.OneValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaHigherKVector<T> HigherKVectorTerm<T>(this XGaProcessor<T> processor, IndexSet id, T scalar)
    {
        var grade = id.Count;

        return processor.ScalarProcessor.IsZero(scalar)
            ? new XGaHigherKVector<T>(processor, grade)
            : new XGaHigherKVector<T>(processor, grade, new SingleItemDictionary<IndexSet, T>(id, scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaHigherKVector<T> HigherKVectorTerm<T>(this XGaProcessor<T> processor, IndexSet id, IScalar<T> scalar)
    {
        return processor.HigherKVectorTerm(id, scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaHigherKVector<T> HigherKVectorTerm<T>(this XGaProcessor<T> processor, KeyValuePair<IndexSet, T> term)
    {
        var (id, scalar) = term;

        var grade = id.Count;

        return processor.ScalarProcessor.IsZero(scalar)
            ? new XGaHigherKVector<T>(processor, grade)
            : new XGaHigherKVector<T>(processor, grade, new SingleItemDictionary<IndexSet, T>(id, scalar));
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaHigherKVector<T> HigherKVector<T>(this XGaProcessor<T> processor, int grade, IReadOnlyDictionary<IndexSet, T> basisScalarDictionary)
    {
        if (basisScalarDictionary.Count == 0 && basisScalarDictionary is not EmptyDictionary<IndexSet, T>)
            return processor.HigherKVectorZero(grade);

        if (basisScalarDictionary.Count == 1 && basisScalarDictionary is not SingleItemDictionary<IndexSet, T>)
            return processor.HigherKVectorTerm(basisScalarDictionary.First());

        return new XGaHigherKVector<T>(
            processor,

            grade,
            basisScalarDictionary
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> KVectorZero<T>(this XGaProcessor<T> processor, int grade)
    {
        if (grade < 0)
            throw new ArgumentOutOfRangeException(nameof(grade));

        return grade switch
        {
            0 => processor.ScalarZero,
            1 => processor.VectorZero,
            2 => processor.BivectorZero,
            _ => new XGaHigherKVector<T>(processor, grade)
        };
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> KVectorTerm<T>(this XGaProcessor<T> processor, KeyValuePair<IndexSet, T> term)
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
    public static XGaKVector<T> KVectorTerm<T>(this XGaProcessor<T> processor, IndexSet basisBlade)
    {
        return processor.KVectorTerm(

            new KeyValuePair<IndexSet, T>(basisBlade, processor.ScalarProcessor.OneValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> KVectorTerm<T>(this XGaProcessor<T> processor, IndexSet basisBlade, T scalar)
    {
        var grade = basisBlade.Count;

        if (processor.ScalarProcessor.IsZero(scalar))
            return processor.KVectorZero(grade);

        return processor.KVectorTerm(

            new KeyValuePair<IndexSet, T>(basisBlade, scalar)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> KVectorTerm<T>(this XGaProcessor<T> processor, IndexSet basisBlade, IScalar<T> scalar)
    {
        return processor.KVectorTerm(basisBlade, scalar.ScalarValue);
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> KVector<T>(this XGaProcessor<T> processor, int grade, IReadOnlyDictionary<IndexSet, T> basisScalarDictionary)
    {
        if (basisScalarDictionary.Count == 0 && basisScalarDictionary is not EmptyDictionary<IndexSet, T>)
            return processor.KVectorZero(grade);

        if (basisScalarDictionary.Count == 1 && basisScalarDictionary is not SingleItemDictionary<IndexSet, T>)
            return processor.KVectorTerm(basisScalarDictionary.First());

        return grade switch
        {
            0 => new XGaScalar<T>(processor, basisScalarDictionary),
            1 => new XGaVector<T>(processor, basisScalarDictionary),
            2 => new XGaBivector<T>(processor, basisScalarDictionary),
            _ => new XGaHigherKVector<T>(processor, grade, basisScalarDictionary)
        };
    }
    
    public static XGaKVector<T> KVectorTerm<T>(this XGaProcessor<T> processor, IReadOnlyList<int> basisVectorIndexList)
    {
        var id = basisVectorIndexList.ToIndexSet(false);
        var grade = id.Grade();

        if (grade == 0)
            return processor.ScalarOne;

        var idScalarDictionary =
            new SingleItemDictionary<IndexSet, T>(id, processor.ScalarProcessor.OneValue);

        if (grade == 1)
            return processor.Vector(idScalarDictionary);
            
        if (grade == 2)
            return processor.Bivector(idScalarDictionary);

        return new XGaHigherKVector<T>(
            processor, 
            grade, 
            idScalarDictionary
        );
    }

    public static XGaKVector<T> KVectorTerm<T>(this XGaProcessor<T> metric, IReadOnlyList<int> basisVectorIndexList, T scalar)
    {
        var id = basisVectorIndexList.ToIndexSet(false);
        var grade = id.Grade();

        if (metric.ScalarProcessor.IsZero(scalar))
            return metric.ScalarZero;
            
        if (grade == 0)
            return metric.Scalar(scalar);

        var idScalarDictionary =
            new SingleItemDictionary<IndexSet, T>(id, scalar);

        if (grade == 1)
            return metric.Vector(idScalarDictionary);
            
        if (grade == 2)
            return metric.Bivector(idScalarDictionary);

        return new XGaHigherKVector<T>(
            metric, 
            grade, 
            idScalarDictionary
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> PseudoScalar<T>(this XGaProcessor<T> processor, int vSpaceDimensions)
    {
        var id = processor.GetBasisPseudoScalarId(vSpaceDimensions);

        return processor.KVectorTerm(

            new KeyValuePair<IndexSet, T>(id, processor.ScalarProcessor.OneValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> PseudoScalar<T>(this XGaProcessor<T> processor, int vSpaceDimensions, T scalarValue)
    {
        var id = processor.GetBasisPseudoScalarId(vSpaceDimensions);

        return processor.KVectorTerm(

            new KeyValuePair<IndexSet, T>(id, scalarValue)
        );
    }

    public static XGaKVector<T> PseudoScalarReverse<T>(this XGaProcessor<T> processor, int vSpaceDimensions)
    {
        var id =
            processor.GetBasisPseudoScalarId(vSpaceDimensions);

        var scalar =
            vSpaceDimensions.ReverseIsNegativeOfGrade()
                ? processor.ScalarProcessor.MinusOneValue
                : processor.ScalarProcessor.OneValue;

        return processor.KVectorTerm(

            new KeyValuePair<IndexSet, T>(id, scalar)
        );
    }

    public static XGaKVector<T> PseudoScalarConjugate<T>(this XGaProcessor<T> processor, int vSpaceDimensions)
    {
        var id =
            processor.GetBasisPseudoScalarId(vSpaceDimensions);

        var sign =
            processor.HermitianConjugateSign(id);

        if (sign.IsZero)
            throw new DivideByZeroException();

        var scalar = sign.ValueFromNumber(processor.ScalarProcessor);

        return processor.KVectorTerm(
            new KeyValuePair<IndexSet, T>(id, scalar)
        );
    }

    public static XGaKVector<T> PseudoScalarEInverse<T>(this XGaProcessor<T> processor, int vSpaceDimensions)
    {
        var id =
            processor.GetBasisPseudoScalarId(vSpaceDimensions);

        var sign =
            processor.EGpSquaredSign(id);

        var scalar = sign.ValueFromNumber(processor.ScalarProcessor);

        return processor.KVectorTerm(

            new KeyValuePair<IndexSet, T>(id, scalar)
        );
    }

    public static XGaKVector<T> PseudoScalarInverse<T>(this XGaProcessor<T> processor, int vSpaceDimensions)
    {
        var id =
            processor.GetBasisPseudoScalarId(vSpaceDimensions);

        var sign =
            processor.GpSquaredSign(id);

        if (sign.IsZero)
            throw new DivideByZeroException();

        var scalar = sign.ValueFromNumber(processor.ScalarProcessor);

        return processor.KVectorTerm(

            new KeyValuePair<IndexSet, T>(id, scalar)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> ToKVector<T>(this XGaBasisBlade basisBlade, XGaProcessor<T> processor)
    {
        return processor.KVectorTerm(
            basisBlade.Id,
            processor.ScalarProcessor.OneValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> ToKVector<T>(this XGaSignedBasisBlade basisBlade, XGaProcessor<T> processor)
    {
        return processor.KVectorTerm(
            basisBlade.Id,
            basisBlade.Sign.ValueFromNumber(processor.ScalarProcessor)
        );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaHigherKVector<T> ToXGaTrivector<T>(this LinTrivector3D<T> trivector, XGaProcessor<T> processor)
    {
        return processor
            .CreateComposer()
            .SetTrivectorTerm(0, 1, 2, trivector.Scalar123)
            .GetHigherKVector(3);
    }
}