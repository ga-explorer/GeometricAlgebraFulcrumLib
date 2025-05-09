using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Dictionary;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Float64.Multivectors.Composers;

public static class XGaFloat64KVectorComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64HigherKVector HigherKVectorZero(this XGaFloat64Processor processor, int grade)
    {
        return new XGaFloat64HigherKVector(processor, grade);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64HigherKVector HigherKVectorTerm(this XGaFloat64Processor processor, IIndexSet id)
    {
        var grade = id.Count;

        return new XGaFloat64HigherKVector(
            processor,
            grade,
            new SingleItemDictionary<IIndexSet, double>(id, 1d)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64HigherKVector HigherKVectorTerm(this XGaFloat64Processor processor, IIndexSet id, double scalar)
    {
        var grade = id.Count;

        return scalar.IsZero()
            ? new XGaFloat64HigherKVector(processor, grade)
            : new XGaFloat64HigherKVector(processor, grade, new SingleItemDictionary<IIndexSet, double>(id, scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64HigherKVector HigherKVectorTerm(this XGaFloat64Processor processor, KeyValuePair<IIndexSet, double> term)
    {
        var (id, scalar) = term;

        var grade = id.Count;

        return scalar.IsZero()
            ? new XGaFloat64HigherKVector(processor, grade)
            : new XGaFloat64HigherKVector(processor, grade, new SingleItemDictionary<IIndexSet, double>(id, scalar));
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64HigherKVector HigherKVector(this XGaFloat64Processor processor, int grade, IReadOnlyDictionary<IIndexSet, double> basisScalarDictionary)
    {
        if (basisScalarDictionary.Count == 0 && basisScalarDictionary is not EmptyDictionary<IIndexSet, double>)
            return processor.HigherKVectorZero(grade);

        if (basisScalarDictionary.Count == 1 && basisScalarDictionary is not SingleItemDictionary<IIndexSet, double>)
            return processor.HigherKVectorTerm(basisScalarDictionary.First());

        return new XGaFloat64HigherKVector(
            processor,
            grade,
            basisScalarDictionary
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector KVectorZero(this XGaFloat64Processor processor, int grade)
    {
        if (grade < 0)
            throw new ArgumentOutOfRangeException(nameof(grade));

        return grade switch
        {
            0 => processor.ScalarZero,
            1 => processor.VectorZero,
            2 => processor.BivectorZero,
            _ => new XGaFloat64HigherKVector(processor, grade)
        };
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector KVectorTerm(this XGaFloat64Processor processor, KeyValuePair<IIndexSet, double> term)
    {
        var grade = term.Key.Count;

        return grade switch
        {
            0 => new XGaFloat64Scalar(processor, term.Value),
            1 => new XGaFloat64Vector(processor, term),
            2 => new XGaFloat64Bivector(processor, term),
            _ => new XGaFloat64HigherKVector(processor, term)
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector KVectorTerm(this XGaFloat64Processor processor, IIndexSet basisBlade)
    {
        return processor.KVectorTerm(
            new KeyValuePair<IIndexSet, double>(basisBlade, 1d)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector KVectorTerm(this XGaFloat64Processor processor, IIndexSet basisBlade, double scalar)
    {
        var grade = basisBlade.Count;

        if (scalar.IsZero())
            return processor.KVectorZero(grade);

        return processor.KVectorTerm(
            new KeyValuePair<IIndexSet, double>(basisBlade, scalar)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector KVectorTerm(this XGaFloat64Processor processor, ulong basisBlade)
    {
        return processor.KVectorTerm(
            new KeyValuePair<IIndexSet, double>(basisBlade.BitPatternToUInt64IndexSet(), 1d)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector KVectorTerm(this XGaFloat64Processor processor, ulong basisBlade, double scalar)
    {
        var grade = basisBlade.Grade();

        if (scalar.IsZero())
            return processor.KVectorZero(grade);

        return processor.KVectorTerm(
            new KeyValuePair<IIndexSet, double>(basisBlade.BitPatternToUInt64IndexSet(), scalar)
        );
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector KVector(this XGaFloat64Processor processor, int grade, IReadOnlyDictionary<IIndexSet, double> basisScalarDictionary)
    {
        if (basisScalarDictionary.Count == 0 && basisScalarDictionary is not EmptyDictionary<IIndexSet, double>)
            return processor.KVectorZero(grade);

        if (basisScalarDictionary.Count == 1 && basisScalarDictionary is not SingleItemDictionary<IIndexSet, double>)
            return processor.KVectorTerm(basisScalarDictionary.First());

        return grade switch
        {
            0 => new XGaFloat64Scalar(processor, basisScalarDictionary),
            1 => new XGaFloat64Vector(processor, basisScalarDictionary),
            2 => new XGaFloat64Bivector(processor, basisScalarDictionary),
            _ => new XGaFloat64HigherKVector(processor, grade, basisScalarDictionary)
        };
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector PseudoScalar(this XGaFloat64Processor processor, int vSpaceDimensions)
    {
        var id = processor.GetBasisPseudoScalarId(vSpaceDimensions);

        return processor.KVectorTerm(
            new KeyValuePair<IIndexSet, double>(id, 1d)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector PseudoScalar(this XGaFloat64Processor processor, int vSpaceDimensions, double scalarValue)
    {
        var id = processor.GetBasisPseudoScalarId(vSpaceDimensions);

        return processor.KVectorTerm(
            new KeyValuePair<IIndexSet, double>(id, scalarValue)
        );
    }

    public static XGaFloat64KVector PseudoScalarReverse(this XGaFloat64Processor processor, int vSpaceDimensions)
    {
        var id =
            processor.GetBasisPseudoScalarId(vSpaceDimensions);

        var scalar =
            vSpaceDimensions.ReverseIsNegativeOfGrade()
                ? -1d : 1d;

        return processor.KVectorTerm(
            new KeyValuePair<IIndexSet, double>(id, scalar)
        );
    }

    public static XGaFloat64KVector PseudoScalarConjugate(this XGaFloat64Processor processor, int vSpaceDimensions)
    {
        var id =
            processor.GetBasisPseudoScalarId(vSpaceDimensions);

        var sign =
            processor.HermitianConjugateSign(id);

        if (sign.IsZero)
            throw new DivideByZeroException();

        var scalar = sign.ToFloat64();

        return processor.KVectorTerm(
            new KeyValuePair<IIndexSet, double>(id, scalar)
        );
    }

    public static XGaFloat64KVector PseudoScalarEInverse(this XGaFloat64Processor processor, int vSpaceDimensions)
    {
        var id =
            processor.GetBasisPseudoScalarId(vSpaceDimensions);

        var sign =
            processor.EGpSquaredSign(id);

        var scalar = sign.ToFloat64();

        return processor.KVectorTerm(
            new KeyValuePair<IIndexSet, double>(id, scalar)
        );
    }

    public static XGaFloat64KVector PseudoScalarInverse(this XGaFloat64Processor processor, int vSpaceDimensions)
    {
        var id =
            processor.GetBasisPseudoScalarId(vSpaceDimensions);

        var sign =
            processor.GpSquaredSign(id);

        if (sign.IsZero)
            throw new DivideByZeroException();

        var scalar = sign.ToFloat64();

        return processor.KVectorTerm(
            new KeyValuePair<IIndexSet, double>(id, scalar)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector ToKVector(this XGaBasisBlade basisBlade)
    {
        var processor =
            (XGaFloat64Processor)basisBlade.Metric;

        return processor.KVectorTerm(
            basisBlade.Id,
            1d
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector ToKVector(this XGaSignedBasisBlade basisBlade)
    {
        var processor =
            (XGaFloat64Processor)basisBlade.Metric;

        return processor.KVectorTerm(
            basisBlade.Id,
            basisBlade.Sign.ToFloat64()
        );
    }


}