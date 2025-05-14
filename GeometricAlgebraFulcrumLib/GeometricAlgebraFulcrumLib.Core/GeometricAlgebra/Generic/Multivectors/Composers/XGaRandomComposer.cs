using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Core.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Core.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Core.Structures.Combinations;
using GeometricAlgebraFulcrumLib.Core.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Core.Structures.Random;
using GeometricAlgebraFulcrumLib.Core.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Generic.Multivectors.Composers;

public class XGaRandomComposer<T> :
    ScalarRandomComposer<T>
{
    private readonly IReadOnlyList<int> _kVectorSpaceDimensions;


    public XGaProcessor<T> Processor { get; private set; }

    public int VSpaceDimensions { get; }

    public int GaSpaceDimensions
        => 1 << VSpaceDimensions;

    public int MaxGrade
        => VSpaceDimensions;

    public int GradeCount
        => VSpaceDimensions + 1;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaRandomComposer(XGaProcessor<T> processor, int vSpaceDimensions)
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
    internal XGaRandomComposer(XGaProcessor<T> processor, int vSpaceDimensions, int seed)
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
    public IndexSet GetBasisVectorId()
    {
        return GetBasisVectorIndex().BasisVectorIndexToId();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IndexSet GetBasisBivectorId()
    {
        return IndexSet.Create(
            RandomGenerator.GetUniqueIndices(2, VSpaceDimensions) 
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IndexSet GetBasisBladeId()
    {
        return IndexSet.CreateFromUInt64Pattern(
            (ulong)RandomGenerator.Next(1 << VSpaceDimensions)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IndexSet GetBasisBladeId(int grade)
    {
        return IndexSet.Create(
            RandomGenerator.GetUniqueIndices(grade, VSpaceDimensions) 
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public (uint grade, ulong index) GetBasisBladeGradeIndex()
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
    public Dictionary<ulong, T> GetXGaKVectorIndexScalarDictionary(int grade)
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
    public Dictionary<ulong, T> GetXGaKVectorIndexScalarDictionary(int grade, double minValue, double maxValue)
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
    public XGaBasisBlade GetXGaBasisVector()
    {
        var id =
            GetBasisVectorIndex().ToUnitIndexSet();

        return new XGaBasisBlade(Processor, id);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaBasisBlade GetXGaBasisBivector()
    {
        var indexList =
            RandomGenerator
                .GetUniqueIndices(2, VSpaceDimensions)
                .ToImmutableArray();

        var id =
            new Pair<int>(indexList[0], indexList[1]).ToPairIndexSet();

        return new XGaBasisBlade(Processor, id);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaBasisBlade GetXGaBasisKVector()
    {
        return GetXGaBasisKVector(
            GetGrade(3)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaBasisBlade GetXGaBasisKVector(int grade)
    {
        if (grade < 3)
            throw new ArgumentOutOfRangeException(nameof(grade));

        var id =
            RandomGenerator
                .GetUniqueIndices(grade, VSpaceDimensions)
                .ToIndexSet(false);

        return new XGaBasisBlade(Processor, id);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaBasisBlade GetXGaBasisBlade()
    {
        return GetXGaBasisBlade(
            GetGrade()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaBasisBlade GetXGaBasisBlade(int grade)
    {
        return grade switch
        {
            < 0 => throw new ArgumentOutOfRangeException(nameof(grade)),
            0 => Processor.BasisScalar,
            1 => GetXGaBasisVector(),
            2 => GetXGaBasisBivector(),
            _ => GetXGaBasisKVector(grade)
        };
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade GetXGaSignedBasisScalar()
    {
        var sign = RandomGenerator.GetSign();

        return Processor.CreateSignedBasisScalar(sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaSignedBasisBlade GetXGaSignedBasisVector()
    {
        var sign = RandomGenerator.GetSign();

        return GetXGaBasisVector().ToSignedBasisBlade(sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaSignedBasisBlade GetXGaSignedBasisBivector()
    {
        var sign = RandomGenerator.GetSign();
        var basis = GetXGaBasisBivector();

        return basis.ToSignedBasisBlade(sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaSignedBasisBlade GetXGaSignedBasisKVector()
    {
        var sign = RandomGenerator.GetSign();
        var basis = GetXGaBasisKVector();

        return basis.ToSignedBasisBlade(sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaSignedBasisBlade GetXGaSignedBasisKVector(int grade)
    {
        var sign = RandomGenerator.GetSign();
        var basis = GetXGaBasisKVector(grade);

        return basis.ToSignedBasisBlade(sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaSignedBasisBlade GetXGaSignedBasisBlade()
    {
        var sign = RandomGenerator.GetSign();
        var basis = GetXGaBasisBlade();

        return basis.ToSignedBasisBlade(sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaSignedBasisBlade GetXGaSignedBasisBlade(int grade)
    {
        var sign = RandomGenerator.GetSign();
        var basis = GetXGaBasisBlade(grade);

        return basis.ToSignedBasisBlade(sign);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaScalar<T> GetXGaScalar()
    {
        return Processor.Scalar(GetScalar());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> GetXGaVector(bool sparseVector = true)
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
                        new KeyValuePair<IndexSet, T>(
                            p.ToUnitIndexSet(),
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
    public XGaVector<T> GetXGaSparseVector(int termsCount)
    {
        if (termsCount > VSpaceDimensions)
            throw new ArgumentOutOfRangeException(nameof(termsCount));

        var indexScalarDictionary =
            VSpaceDimensions
                .GetRange()
                .Shuffled(RandomGenerator)
                .Take(termsCount)
                .Select(p => 
                    new KeyValuePair<IndexSet, T>(
                        p.ToUnitIndexSet(),
                        GetScalar().ScalarValue
                    )
                );

        return Processor
            .CreateComposer()
            .SetTerms(indexScalarDictionary)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaBivector<T> GetXGaBivector()
    {
        var kvSpaceDimensions =
            VSpaceDimensions.GetBinomialCoefficient(2);

        var indexScalarDictionary =
            Enumerable
                .Range(0, (int)kvSpaceDimensions)
                .Select(index => 
                    new KeyValuePair<IndexSet, T>(
                        index.BasisBivectorIndexToId(),
                        GetScalar().ScalarValue
                    )
                );

        return Processor
            .CreateComposer()
            .SetTerms(indexScalarDictionary)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaBivector<T> GetXGaBivector(double minValue, double maxValue)
    {
        var kvSpaceDimensions =
            VSpaceDimensions.GetBinomialCoefficient(2);
            
        var indexScalarDictionary =
            Enumerable
                .Range(0, (int)kvSpaceDimensions)
                .Select(index => 
                    new KeyValuePair<IndexSet, T>(
                        index.BasisBivectorIndexToId(),
                        GetScalar(minValue, maxValue).ScalarValue
                    )
                );

        return Processor
            .CreateComposer()
            .SetTerms(indexScalarDictionary)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaBivector<T> GetXGaSparseBivector(int termsCount)
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
                    new KeyValuePair<IndexSet, T>(
                        index.BasisBivectorIndexToId(),
                        GetScalar().ScalarValue
                    )
                );

        return Processor
            .CreateComposer()
            .SetTerms(indexScalarDictionary)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaBivector<T> GetXGaSparseBivector(int termsCount, double minValue, double maxValue)
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
                    new KeyValuePair<IndexSet, T>(
                        index.BasisBivectorIndexToId(),
                        GetScalar(minValue, maxValue).ScalarValue
                    )
                );

        return Processor
            .CreateComposer()
            .SetTerms(indexScalarDictionary)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVector<T> GetXGaKVector()
    {
        return GetXGaKVectorOfGrade(GetGrade());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVector<T> GetXGaKVector(double minValue, double maxValue)
    {
        return GetXGaKVectorOfGrade(GetGrade(), minValue, maxValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVector<T> GetXGaKVectorOfGrade(int grade)
    {
        var kvSpaceDimensions =
            VSpaceDimensions.GetBinomialCoefficient(grade);

        var indexScalarDictionary =
            Enumerable
                .Range(0, (int)kvSpaceDimensions)
                .Select(index => 
                    new KeyValuePair<IndexSet, T>(
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
    public XGaKVector<T> GetXGaKVectorOfGrade(int grade, double minValue, double maxValue)
    {
        var kvSpaceDimensions =
            VSpaceDimensions.GetBinomialCoefficient(grade);

        var indexScalarDictionary =
            Enumerable
                .Range(0, (int)kvSpaceDimensions)
                .Select(index => 
                    new KeyValuePair<IndexSet, T>(
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
    public XGaKVector<T> GetXGaSparseKVectorOfGrade(int grade, int termsCount)
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
                    new KeyValuePair<IndexSet, T>(
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
    public XGaKVector<T> GetXGaSparseKVectorOfGrade(int grade, int termsCount, double minValue, double maxValue)
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
                    new KeyValuePair<IndexSet, T>(
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
    public XGaGradedMultivector<T> GetXGaMultivector()
    {
        var gaSpaceDimensions = GaSpaceDimensions;

        var idScalarDictionary =
            Enumerable
                .Range(0, gaSpaceDimensions)
                .Select(index => 
                    new KeyValuePair<IndexSet, T>(
                        ((ulong)index).ToUInt64IndexSet(),
                        GetScalar().ScalarValue
                    )
                );

        return Processor
            .CreateComposer()
            .SetTerms(idScalarDictionary)
            .GetMultivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaGradedMultivector<T> GetXGaMultivector(double minValue, double maxValue)
    {
        var gaSpaceDimensions = GaSpaceDimensions;

        var idScalarDictionary =
            Enumerable
                .Range(0, gaSpaceDimensions)
                .Select(index => 
                    new KeyValuePair<IndexSet, T>(
                        ((ulong)index).ToUInt64IndexSet(),
                        GetScalar(minValue, maxValue).ScalarValue
                    )
                );

        return Processor
            .CreateComposer()
            .SetTerms(idScalarDictionary)
            .GetMultivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaGradedMultivector<T> GetXGaMultivector(int termsCount)
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
                    new KeyValuePair<IndexSet, T>(
                        ((ulong)index).ToUInt64IndexSet(),
                        GetScalar().ScalarValue
                    )
                );

        return Processor
            .CreateComposer()
            .SetTerms(idScalarDictionary)
            .GetMultivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaGradedMultivector<T> GetXGaMultivector(int termsCount, double minValue, double maxValue)
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
                    new KeyValuePair<IndexSet, T>(
                        ((ulong)index).ToUInt64IndexSet(),
                        GetScalar(minValue, maxValue).ScalarValue
                    )
                );

        return Processor
            .CreateComposer()
            .SetTerms(idScalarDictionary)
            .GetMultivector();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaUniformMultivector<T> GetUniformMultivector(int termsCount)
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
                    new KeyValuePair<IndexSet, T>(
                        (IndexSet)index,
                        GetScalar().ScalarValue
                    )
                );

        return Processor
            .CreateComposer()
            .SetTerms(idScalarDictionary)
            .GetUniformMultivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<XGaVector<T>> GetXGaVectors(int count)
    {
        while (count > 0)
        {
            yield return GetXGaVector();
            count--;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVector<T> GetXGaBlade(int grade)
    {
        if (grade == 0U)
            return GetXGaScalar();

        if (grade == 1U)
            return GetXGaVector();

        return GetXGaVectors(grade).Op(Processor);
    }

}