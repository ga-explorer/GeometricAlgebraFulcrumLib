using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Combinations;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Random;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Basis;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Records;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;

public class RGaRandomComposer<T> :
    ScalarRandomComposer<T>
{
    private readonly IReadOnlyList<int> _kVectorSpaceDimensions;


    public RGaProcessor<T> Processor { get; private set; }

    public int VSpaceDimensions { get; }

    public int GaSpaceDimensions
        => 1 << VSpaceDimensions;

    public int MaxGrade
        => VSpaceDimensions;

    public int GradeCount
        => VSpaceDimensions + 1;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal RGaRandomComposer(RGaProcessor<T> processor, int vSpaceDimensions)
        : base(processor.ScalarProcessor)
    {
        if (vSpaceDimensions is < 2 or > 31)
            throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

        Processor = processor;
        VSpaceDimensions = vSpaceDimensions;

        _kVectorSpaceDimensions =
            GradeCount
                .GetRange()
                .Select(grade => (int)VSpaceDimensions.GetBinomialCoefficient(grade))
                .ToImmutableArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal RGaRandomComposer(RGaProcessor<T> processor, int vSpaceDimensions, int seed)
        : base(processor.ScalarProcessor, seed)
    {
        if (vSpaceDimensions is < 2 or > 31)
            throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

        Processor = processor;
        VSpaceDimensions = vSpaceDimensions;

        _kVectorSpaceDimensions =
            GradeCount
                .GetRange()
                .Select(grade => (int)VSpaceDimensions.GetBinomialCoefficient(grade))
                .ToImmutableArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal RGaRandomComposer(RGaProcessor<T> processor, IScalarProcessor<T> geometricProcessor, int vSpaceDimensions, Random randomGenerator)
        : base(geometricProcessor, randomGenerator)
    {
        if (vSpaceDimensions is < 2 or > 31)
            throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

        Processor = processor;
        VSpaceDimensions = vSpaceDimensions;

        _kVectorSpaceDimensions =
            GradeCount
                .GetRange()
                .Select(grade => (int)VSpaceDimensions.GetBinomialCoefficient(grade))
                .ToImmutableArray();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int GetGrade()
    {
        return RandomGenerator.Next(GradeCount + 1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int GetGrade(int minGrade)
    {
        if (minGrade < 0 || minGrade > VSpaceDimensions)
            throw new ArgumentOutOfRangeException(nameof(minGrade));

        return RandomGenerator.Next(minGrade, MaxGrade + 1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int GetGrade(int minGrade, int maxGrade)
    {
        if (minGrade < 0 || minGrade > VSpaceDimensions)
            throw new ArgumentOutOfRangeException(nameof(minGrade));

        if (maxGrade < 0 || maxGrade > VSpaceDimensions)
            throw new ArgumentOutOfRangeException(nameof(maxGrade));

        return minGrade <= maxGrade
            ? RandomGenerator.Next(minGrade, maxGrade + 1)
            : RandomGenerator.Next(maxGrade, minGrade + 1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ulong GetBasisVectorId()
    {
        return GetBasisVectorIndex().BasisVectorIndexToId();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ulong GetBasisBivectorId()
    {
        return GetBasisBivectorIndex().BasisBivectorIndexToId();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ulong GetBasisBladeId()
    {
        return (ulong)RandomGenerator.Next(1 << VSpaceDimensions);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ulong GetBasisBladeId(int grade)
    {
        return GetBasisBladeIndex(grade).BasisBladeIndexToId((uint)grade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaGradeKvIndexRecord GetBasisBladeGradeIndex()
    {
        return GetBasisBladeId().BasisBladeIdToGradeIndex();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int GetBasisVectorIndex()
    {
        return RandomGenerator.Next(VSpaceDimensions);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ulong GetBasisBivectorIndex()
    {
        return (ulong)RandomGenerator.Next(_kVectorSpaceDimensions[2]);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ulong GetBasisBladeIndex(int grade)
    {
        return (ulong)RandomGenerator.Next(_kVectorSpaceDimensions[grade]);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Dictionary<ulong, T> GetRGaKVectorIndexScalarDictionary(int grade)
    {
        var kvSpaceDimensions =
            _kVectorSpaceDimensions[grade];

        return Enumerable
            .Range(0, kvSpaceDimensions)
            .ToDictionary(
                index => (ulong)index,
                _ => GetScalar().ScalarValue
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Dictionary<ulong, T> GetRGaKVectorIndexScalarDictionary(int grade, double minValue, double maxValue)
    {
        var kvSpaceDimensions =
            _kVectorSpaceDimensions[grade];

        return Enumerable
            .Range(0, kvSpaceDimensions)
            .ToDictionary(
                index => (ulong)index,
                _ => GetScalar(minValue, maxValue).ScalarValue
            );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaBasisBlade GetRGaBasisVector()
    {
        var id =
            GetBasisVectorIndex().IndexToUInt64IndexSetBitPattern();

        return new RGaBasisBlade(Processor, id);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaBasisBlade GetRGaBasisBivector()
    {
        var indexList =
            RandomGenerator
                .GetUniqueIndices(2, VSpaceDimensions)
                .ToImmutableArray();

        var id =
            new Pair<int>(indexList[0], indexList[1]).IndexPairToUInt64IndexSetBitPattern();

        return new RGaBasisBlade(Processor, id);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaBasisBlade GetRGaBasisKVector()
    {
        return GetRGaBasisKVector(
            GetGrade(3)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaBasisBlade GetRGaBasisKVector(int grade)
    {
        if (grade < 3)
            throw new ArgumentOutOfRangeException(nameof(grade));

        var id =
            RandomGenerator
                .GetUniqueIndices(grade, VSpaceDimensions)
                .ToImmutableSortedSet()
                .ToUInt64IndexSetBitPattern();

        return new RGaBasisBlade(Processor, id);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaBasisBlade GetRGaBasisBlade()
    {
        return GetRGaBasisBlade(
            GetGrade()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaBasisBlade GetRGaBasisBlade(int grade)
    {
        return grade switch
        {
            < 0 => throw new ArgumentOutOfRangeException(nameof(grade)),
            0 => Processor.BasisScalar,
            1 => GetRGaBasisVector(),
            2 => GetRGaBasisBivector(),
            _ => GetRGaBasisKVector(grade)
        };
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade GetRGaSignedBasisScalar()
    {
        var sign = RandomGenerator.GetSign();

        return Processor.CreateSignedBasisScalar(sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaSignedBasisBlade GetRGaSignedBasisVector()
    {
        var sign = RandomGenerator.GetSign();

        return GetRGaBasisVector().ToSignedBasisBlade(sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaSignedBasisBlade GetRGaSignedBasisBivector()
    {
        var sign = RandomGenerator.GetSign();
        var basis = GetRGaBasisBivector();

        return basis.ToSignedBasisBlade(sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaSignedBasisBlade GetRGaSignedBasisKVector()
    {
        var sign = RandomGenerator.GetSign();
        var basis = GetRGaBasisKVector();

        return basis.ToSignedBasisBlade(sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaSignedBasisBlade GetRGaSignedBasisKVector(int grade)
    {
        var sign = RandomGenerator.GetSign();
        var basis = GetRGaBasisKVector(grade);

        return basis.ToSignedBasisBlade(sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaSignedBasisBlade GetRGaSignedBasisBlade()
    {
        var sign = RandomGenerator.GetSign();
        var basis = GetRGaBasisBlade();

        return basis.ToSignedBasisBlade(sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaSignedBasisBlade GetRGaSignedBasisBlade(int grade)
    {
        var sign = RandomGenerator.GetSign();
        var basis = GetRGaBasisBlade(grade);

        return basis.ToSignedBasisBlade(sign);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaScalar<T> GetRGaScalar()
    {
        return Processor.Scalar(GetScalar());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaVector<T> GetRGaVector(bool sparseVector = true)
    {
        if (sparseVector)
        {
            var termsCount = RandomGenerator.Next(VSpaceDimensions);

            var indexScalarDictionary =
                VSpaceDimensions
                    .GetRange()
                    .Shuffled(RandomGenerator)
                    .Take(termsCount)
                    .Select(p => 
                        new KeyValuePair<ulong, T>(
                            p.IndexToUInt64IndexSetBitPattern(),
                            GetScalar().ScalarValue
                        )
                    );

            return Processor
                .CreateComposer()
                .SetTerms(indexScalarDictionary)
                .GetVector();
        }
        else
        {
            var scalarArray =
                VSpaceDimensions
                    .GetRange()
                    .Select(_ => GetScalar().ScalarValue);

            return Processor.Vector(scalarArray);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaVector<T> GetRGaSparseVector(int termsCount)
    {
        if (termsCount > VSpaceDimensions)
            throw new ArgumentOutOfRangeException(nameof(termsCount));

        var indexScalarDictionary =
            VSpaceDimensions
                .GetRange()
                .Shuffled(RandomGenerator)
                .Take(termsCount)
                .Select(p => 
                    new KeyValuePair<ulong, T>(
                        p.IndexToUInt64IndexSetBitPattern(),
                        GetScalar().ScalarValue
                    )
                );

        return Processor
            .CreateComposer()
            .SetTerms(indexScalarDictionary)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaBivector<T> GetRGaBivector()
    {
        var kvSpaceDimensions =
            VSpaceDimensions.GetBinomialCoefficient(2);

        var indexScalarDictionary =
            Enumerable
                .Range(0, (int)kvSpaceDimensions)
                .Select(index => 
                    new KeyValuePair<ulong, T>(
                        index.BasisBivectorIndexToVectorIndexInt32Pair().IndexPairToUInt64IndexSetBitPattern(),
                        GetScalar().ScalarValue
                    )
                );

        return Processor
            .CreateComposer()
            .SetTerms(indexScalarDictionary)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaBivector<T> GetRGaBivector(double minValue, double maxValue)
    {
        var kvSpaceDimensions =
            VSpaceDimensions.GetBinomialCoefficient(2);

        var indexScalarDictionary =
            Enumerable
                .Range(0, (int)kvSpaceDimensions)
                .Select(index => 
                    new KeyValuePair<ulong, T>(
                        index.BasisBivectorIndexToVectorIndexInt32Pair().IndexPairToUInt64IndexSetBitPattern(),
                        GetScalar(minValue, maxValue).ScalarValue
                    )
                );

        return Processor
            .CreateComposer()
            .SetTerms(indexScalarDictionary)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaBivector<T> GetRGaSparseBivector(int termsCount)
    {
        var kvSpaceDimensions =
            (int)VSpaceDimensions.GetBinomialCoefficient(2);

        if (termsCount > kvSpaceDimensions)
            throw new ArgumentOutOfRangeException(nameof(termsCount));

        var indexScalarDictionary =
            Enumerable
                .Range(0, kvSpaceDimensions)
                .Shuffled(RandomGenerator)
                .Take(termsCount)
                .Select(index => 
                    new KeyValuePair<ulong, T>(
                        index.BasisBivectorIndexToVectorIndexInt32Pair().IndexPairToUInt64IndexSetBitPattern(),
                        GetScalar().ScalarValue
                    )
                );

        return Processor
            .CreateComposer()
            .SetTerms(indexScalarDictionary)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaBivector<T> GetRGaSparseBivector(int termsCount, double minValue, double maxValue)
    {
        var kvSpaceDimensions =
            (int)VSpaceDimensions.GetBinomialCoefficient(2);

        if (termsCount > kvSpaceDimensions)
            throw new ArgumentOutOfRangeException(nameof(termsCount));

        var indexScalarDictionary =
            Enumerable
                .Range(0, kvSpaceDimensions)
                .Shuffled(RandomGenerator)
                .Take(termsCount)
                .Select(index => 
                    new KeyValuePair<ulong, T>(
                        index.BasisBivectorIndexToVectorIndexInt32Pair().IndexPairToUInt64IndexSetBitPattern(),
                        GetScalar(minValue, maxValue).ScalarValue
                    )
                );

        return Processor
            .CreateComposer()
            .SetTerms(indexScalarDictionary)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaKVector<T> GetRGaKVector()
    {
        return GetRGaKVectorOfGrade(GetGrade());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaKVector<T> GetRGaKVector(double minValue, double maxValue)
    {
        return GetRGaKVectorOfGrade(GetGrade(), minValue, maxValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaKVector<T> GetRGaKVectorOfGrade(int grade)
    {
        var kvSpaceDimensions =
            VSpaceDimensions.GetBinomialCoefficient(grade);

        var indexScalarDictionary =
            Enumerable
                .Range(0, (int)kvSpaceDimensions)
                .Select(index => 
                    new KeyValuePair<ulong, T>(
                        BasisBladeUtils.BasisBladeGradeIndexToId((uint)grade, (ulong)index),
                        GetScalar().ScalarValue
                    )
                );

        return Processor
            .CreateComposer()
            .SetTerms(indexScalarDictionary)
            .GetKVector(grade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaKVector<T> GetRGaKVectorOfGrade(int grade, double minValue, double maxValue)
    {
        var kvSpaceDimensions =
            VSpaceDimensions.GetBinomialCoefficient(grade);

        var indexScalarDictionary =
            Enumerable
                .Range(0, (int)kvSpaceDimensions)
                .Select(index => 
                    new KeyValuePair<ulong, T>(
                        BasisBladeUtils.BasisBladeGradeIndexToId((uint)grade, (ulong)index),
                        GetScalar(minValue, maxValue).ScalarValue
                    )
                );

        return Processor
            .CreateComposer()
            .SetTerms(indexScalarDictionary)
            .GetKVector(grade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaKVector<T> GetRGaSparseKVectorOfGrade(int grade, int termsCount)
    {
        var kvSpaceDimensions =
            (int)VSpaceDimensions.GetBinomialCoefficient(grade);

        if (termsCount > kvSpaceDimensions)
            throw new ArgumentOutOfRangeException(nameof(termsCount));

        var indexScalarDictionary =
            Enumerable
                .Range(0, kvSpaceDimensions)
                .Shuffled(RandomGenerator)
                .Take(termsCount)
                .Select(index => 
                    new KeyValuePair<ulong, T>(
                        BasisBladeUtils.BasisBladeGradeIndexToId((uint)grade, (ulong)index),
                        GetScalar().ScalarValue
                    )
                );

        return Processor
            .CreateComposer()
            .SetTerms(indexScalarDictionary)
            .GetKVector(grade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaKVector<T> GetRGaSparseKVectorOfGrade(int grade, int termsCount, double minValue, double maxValue)
    {
        var kvSpaceDimensions =
            (int)VSpaceDimensions.GetBinomialCoefficient(grade);

        if (termsCount > kvSpaceDimensions)
            throw new ArgumentOutOfRangeException(nameof(termsCount));

        var indexScalarDictionary =
            Enumerable
                .Range(0, kvSpaceDimensions)
                .Shuffled(RandomGenerator)
                .Take(termsCount)
                .Select(index => 
                    new KeyValuePair<ulong, T>(
                        BasisBladeUtils.BasisBladeGradeIndexToId((uint)grade, (ulong)index),
                        GetScalar(minValue, maxValue).ScalarValue
                    )
                );

        return Processor
            .CreateComposer()
            .SetTerms(indexScalarDictionary)
            .GetKVector(grade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaGradedMultivector<T> GetRGaMultivector()
    {
        var gaSpaceDimensions = GaSpaceDimensions;

        var idScalarDictionary =
            Enumerable
                .Range(0, gaSpaceDimensions)
                .Select(index => 
                    new KeyValuePair<ulong, T>(
                        (ulong)index,
                        GetScalar().ScalarValue
                    )
                );

        return Processor
            .CreateComposer()
            .SetTerms(idScalarDictionary)
            .GetMultivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaGradedMultivector<T> GetRGaMultivector(double minValue, double maxValue)
    {
        var gaSpaceDimensions = GaSpaceDimensions;

        var idScalarDictionary =
            Enumerable
                .Range(0, gaSpaceDimensions)
                .Select(index => 
                    new KeyValuePair<ulong, T>(
                        (ulong)index,
                        GetScalar(minValue, maxValue).ScalarValue
                    )
                );

        return Processor
            .CreateComposer()
            .SetTerms(idScalarDictionary)
            .GetMultivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaGradedMultivector<T> GetRGaMultivector(int termsCount)
    {
        var gaSpaceDimensions = GaSpaceDimensions;

        if (termsCount > gaSpaceDimensions)
            throw new ArgumentOutOfRangeException(nameof(termsCount));

        var idScalarDictionary =
            Enumerable
                .Range(0, gaSpaceDimensions)
                .Shuffled(RandomGenerator)
                .Take(termsCount)
                .Select(index => 
                    new KeyValuePair<ulong, T>(
                        (ulong)index,
                        GetScalar().ScalarValue
                    )
                );

        return Processor
            .CreateComposer()
            .SetTerms(idScalarDictionary)
            .GetMultivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaGradedMultivector<T> GetRGaMultivector(int termsCount, double minValue, double maxValue)
    {
        var gaSpaceDimensions = GaSpaceDimensions;

        if (termsCount > gaSpaceDimensions)
            throw new ArgumentOutOfRangeException(nameof(termsCount));

        var idScalarDictionary =
            Enumerable
                .Range(0, gaSpaceDimensions)
                .Shuffled(RandomGenerator)
                .Take(termsCount)
                .Select(index => 
                    new KeyValuePair<ulong, T>(
                        (ulong)index,
                        GetScalar(minValue, maxValue).ScalarValue
                    )
                );

        return Processor
            .CreateComposer()
            .SetTerms(idScalarDictionary)
            .GetMultivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<RGaVector<T>> GetRGaVectors(int count)
    {
        while (count > 0)
        {
            yield return GetRGaVector();
            count--;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaKVector<T> GetRGaBlade(int grade)
    {
        if (grade == 0U)
            return GetRGaScalar();

        if (grade == 1U)
            return GetRGaVector();

        return GetRGaVectors(grade).Op(Processor);
    }

}