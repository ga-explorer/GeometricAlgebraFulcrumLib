using System;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.LinearMaps.Rotors;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Matlab.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Combibnations;
using GeometricAlgebraFulcrumLib.Matlab.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Random;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Multivectors;

public class XGaFloat64RandomComposer :
    Float64RandomComposer
{
    private readonly IReadOnlyList<int> _kVectorSpaceDimensions;


    public XGaFloat64Processor Processor { get; private set; }

    public XGaMetric Metric
        => Processor;

    public int VSpaceDimensions { get; }

    public int GaSpaceDimensions
        => 1 << VSpaceDimensions;

    public int MaxGrade
        => VSpaceDimensions;

    public int GradeCount
        => VSpaceDimensions + 1;


    
    internal XGaFloat64RandomComposer(XGaFloat64Processor metric, int vSpaceDimensions)
    {
        if (vSpaceDimensions is < 2 or > 31)
            throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

        Processor = metric;
        VSpaceDimensions = vSpaceDimensions;

        _kVectorSpaceDimensions =
            GradeCount
                .GetRange()
                .Select(grade => (int)VSpaceDimensions.GetBinomialCoefficient(grade))
                .ToArray();
    }

    
    internal XGaFloat64RandomComposer(XGaFloat64Processor metric, int vSpaceDimensions, int seed)
        : base(seed)
    {
        if (vSpaceDimensions is < 2 or > 31)
            throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

        Processor = metric;
        VSpaceDimensions = vSpaceDimensions;

        _kVectorSpaceDimensions =
            GradeCount
                .GetRange()
                .Select(grade => (int)VSpaceDimensions.GetBinomialCoefficient(grade))
                .ToArray();
    }

    
    internal XGaFloat64RandomComposer(XGaFloat64Processor metric, int vSpaceDimensions, Random randomGenerator)
        : base(randomGenerator)
    {
        if (vSpaceDimensions is < 2 or > 31)
            throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

        Processor = metric;
        VSpaceDimensions = vSpaceDimensions;

        _kVectorSpaceDimensions =
            GradeCount
                .GetRange()
                .Select(grade => (int)VSpaceDimensions.GetBinomialCoefficient(grade))
                .ToArray();
    }


    
    public int GetGrade()
    {
        return RandomGenerator.Next(GradeCount + 1);
    }

    
    public int GetGrade(int minGrade)
    {
        if (minGrade < 0 || minGrade > VSpaceDimensions)
            throw new ArgumentOutOfRangeException(nameof(minGrade));

        return RandomGenerator.Next(minGrade, MaxGrade + 1);
    }

    
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

    
    public IndexSet GetBasisVectorId()
    {
        return GetBasisVectorIndex().BasisVectorIndexToId();
    }

    
    public IndexSet GetBasisBivectorId()
    {
        return IndexSet.Create(
            RandomGenerator.GetDistinctIndices(2, VSpaceDimensions) 
        );
    }

    
    public IndexSet GetBasisBladeId()
    {
        return IndexSet.CreateFromUInt64Pattern(
            (ulong)RandomGenerator.Next(1 << VSpaceDimensions)
        );
    }

    
    public IndexSet GetBasisBladeId(int grade)
    {
        return IndexSet.Create(
            RandomGenerator.GetDistinctIndices(grade, VSpaceDimensions) 
        );
    }

    
    public (uint grade, ulong index) GetBasisBladeGradeIndex()
    {
        return GetBasisBladeId().BasisBladeIdToGradeIndex();
    }

    
    public int GetBasisVectorIndex()
    {
        return RandomGenerator.Next(VSpaceDimensions);
    }

    
    public ulong GetBasisBivectorIndex()
    {
        return (ulong)RandomGenerator.Next(_kVectorSpaceDimensions[2]);
    }

    
    public ulong GetBasisBladeIndex(int grade)
    {
        return (ulong)RandomGenerator.Next(_kVectorSpaceDimensions[grade]);
    }


    
    public Dictionary<ulong, double> GetKVectorIndexScalarDictionary(int grade)
    {
        var kvSpaceDimensions =
            _kVectorSpaceDimensions[grade];

        return Enumerable
            .Range(0, kvSpaceDimensions)
            .ToDictionary(
                index => (ulong)index,
                _ => GetScalarValue()
            );
    }

    
    public Dictionary<ulong, double> GetKVectorIndexScalarDictionary(int grade, double minValue, double maxValue)
    {
        var kvSpaceDimensions =
            _kVectorSpaceDimensions[grade];

        return Enumerable
            .Range(0, kvSpaceDimensions)
            .ToDictionary(
                index => (ulong)index,
                _ => GetScalarValue(minValue, maxValue)
            );
    }


    
    public XGaBasisBlade GetBasisVector()
    {
        var id =
            GetBasisVectorIndex().ToUnitIndexSet();

        return new XGaBasisBlade(Processor, id);
    }

    
    public XGaBasisBlade GetBasisBivector()
    {
        var indexList =
            RandomGenerator
                .GetDistinctIndices(2, VSpaceDimensions)
                .ToArray();

        var id =
            new Pair<int>(indexList[0], indexList[1]).ToPairIndexSet();

        return new XGaBasisBlade(Processor, id);
    }

    
    public XGaBasisBlade GetBasisKVector()
    {
        return GetBasisKVector(
            GetGrade(3)
        );
    }

    
    public XGaBasisBlade GetBasisKVector(int grade)
    {
        if (grade < 3)
            throw new ArgumentOutOfRangeException(nameof(grade));

        var id =
            RandomGenerator
                .GetDistinctIndices(grade, VSpaceDimensions)
                .ToIndexSet(false);

        return new XGaBasisBlade(Processor, id);
    }

    
    public XGaBasisBlade GetBasisBlade()
    {
        return GetBasisBlade(
            GetGrade()
        );
    }

    
    public XGaBasisBlade GetBasisBlade(int grade)
    {
        return grade switch
        {
            < 0 => throw new ArgumentOutOfRangeException(nameof(grade)),
            0 => Processor.UnitBasisScalar,
            1 => GetBasisVector(),
            2 => GetBasisBivector(),
            _ => GetBasisKVector(grade)
        };
    }


    
    public IXGaSignedBasisBlade GetSignedBasisScalar()
    {
        var sign = RandomGenerator.GetSign();

        return Processor.SignedBasisScalar(sign);
    }

    
    public XGaSignedBasisBlade GetSignedBasisVector()
    {
        var sign = RandomGenerator.GetSign();

        return GetBasisVector().ToSignedBasisBlade(sign);
    }

    
    public XGaSignedBasisBlade GetSignedBasisBivector()
    {
        var sign = RandomGenerator.GetSign();
        var basis = GetBasisBivector();

        return basis.ToSignedBasisBlade(sign);
    }

    
    public XGaSignedBasisBlade GetSignedBasisKVector()
    {
        var sign = RandomGenerator.GetSign();
        var basis = GetBasisKVector();

        return basis.ToSignedBasisBlade(sign);
    }

    
    public XGaSignedBasisBlade GetSignedBasisKVector(int grade)
    {
        var sign = RandomGenerator.GetSign();
        var basis = GetBasisKVector(grade);

        return basis.ToSignedBasisBlade(sign);
    }

    
    public XGaSignedBasisBlade GetSignedBasisBlade()
    {
        var sign = RandomGenerator.GetSign();
        var basis = GetBasisBlade();

        return basis.ToSignedBasisBlade(sign);
    }

    
    public XGaSignedBasisBlade GetSignedBasisBlade(int grade)
    {
        var sign = RandomGenerator.GetSign();
        var basis = GetBasisBlade(grade);

        return basis.ToSignedBasisBlade(sign);
    }


    
    public XGaFloat64Scalar GetScalar()
    {
        return Processor.Scalar(GetScalarValue());
    }
    
    
    public XGaFloat64Vector GetVector(int index)
    {
        return Processor.VectorTerm(index, GetScalarValue());
    }

    
    public XGaFloat64Bivector GetBivector(int index)
    {
        return Processor.BivectorTerm(
            index.BasisVectorIndexToId(),
            GetScalarValue()
        );
    }

    
    public XGaFloat64KVector GetKVector(ulong id)
    {
        return Processor.KVectorTerm(
            id,
            GetScalarValue()
        );
    }

    
    public XGaFloat64Vector GetVector(double minValue, double maxValue, bool sparseVector = true)
    {
        if (sparseVector)
        {
            var termsCount = RandomGenerator.Next(1, VSpaceDimensions);

            var indexScalarDictionary =
                VSpaceDimensions
                    .GetRange()
                    .Shuffled(RandomGenerator)
                    .Take(termsCount)
                    .Select(p => 
                        new KeyValuePair<IndexSet, double>(
                            p.ToUnitIndexSet(),
                            GetScalarValue(minValue, maxValue)
                        )
                    );

            return Processor
                .CreateVectorComposer()
                .SetTerms(indexScalarDictionary)
                .GetVector();
        }
        else
        {
            var scalarArray =
                VSpaceDimensions
                    .GetRange()
                    .Select(_ => GetScalarValue(minValue, maxValue));

            return Processor.Vector(scalarArray);
        }
    }

    
    public XGaFloat64Vector GetVector(bool sparseVector = true)
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
                        new KeyValuePair<IndexSet, double>(
                            p.ToUnitIndexSet(),
                            GetScalarValue()
                        )
                    );

            return Processor
                .CreateVectorComposer()
                .SetTerms(indexScalarDictionary)
                .GetVector();
        }
        else
        {
            var scalarArray =
                VSpaceDimensions
                    .GetRange()
                    .Select(_ => GetScalarValue());

            return Processor.Vector(scalarArray);
        }
    }

    
    public XGaFloat64Vector GetSparseVector(int termsCount)
    {
        if (termsCount > VSpaceDimensions)
            throw new ArgumentOutOfRangeException(nameof(termsCount));

        var indexScalarDictionary =
            VSpaceDimensions
                .GetRange()
                .Shuffled(RandomGenerator)
                .Take(termsCount)
                .Select(p => 
                    new KeyValuePair<IndexSet, double>(
                        p.ToUnitIndexSet(),
                        GetScalarValue()
                    )
                );

        return Processor
            .CreateVectorComposer()
            .SetTerms(indexScalarDictionary)
            .GetVector();
    }

    
    public XGaFloat64Bivector GetBivector()
    {
        var kvSpaceDimensions =
            VSpaceDimensions.GetBinomialCoefficient(2);

        var indexScalarDictionary =
            Enumerable
                .Range(0, (int)kvSpaceDimensions)
                .Select(index => 
                    new KeyValuePair<IndexSet, double>(
                        index.BasisBivectorIndexToId(),
                        GetScalarValue()
                    )
                );

        return Processor
            .CreateBivectorComposer()
            .SetTerms(indexScalarDictionary)
            .GetBivector();
    }

    
    public XGaFloat64Bivector GetBivector(double minValue, double maxValue)
    {
        var kvSpaceDimensions =
            VSpaceDimensions.GetBinomialCoefficient(2);

        var indexScalarDictionary =
            Enumerable
                .Range(0, (int)kvSpaceDimensions)
                .Select(index => 
                    new KeyValuePair<IndexSet, double>(
                        index.BasisBivectorIndexToId(),
                        GetScalarValue(minValue, maxValue)
                    )
                );

        return Processor
            .CreateBivectorComposer()
            .SetTerms(indexScalarDictionary)
            .GetBivector();
    }

    
    public XGaFloat64Bivector GetSparseBivector(int termsCount)
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
                    new KeyValuePair<IndexSet, double>(
                        index.BasisBivectorIndexToId(), 
                        GetScalarValue()
                    )
                );

        return Processor
            .CreateBivectorComposer()
            .SetTerms(indexScalarDictionary)
            .GetBivector();
    }

    
    public XGaFloat64Bivector GetSparseBivector(int termsCount, double minValue, double maxValue)
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
                    new KeyValuePair<IndexSet, double>(
                        index.BasisBivectorIndexToId(), 
                        GetScalarValue(minValue, maxValue)
                    )
                );

        return Processor
            .CreateBivectorComposer()
            .SetTerms(indexScalarDictionary)
            .GetBivector();
    }

    
    public XGaFloat64KVector GetKVector()
    {
        return GetKVectorOfGrade(GetGrade());
    }

    
    public XGaFloat64KVector GetKVector(double minValue, double maxValue)
    {
        return GetKVectorOfGrade(GetGrade(), minValue, maxValue);
    }

    
    public XGaFloat64KVector GetKVectorOfGrade(int grade)
    {
        var kvSpaceDimensions =
            VSpaceDimensions.GetBinomialCoefficient(grade);

        var indexScalarDictionary =
            Enumerable
                .Range(0, (int)kvSpaceDimensions)
                .ToDictionary(
                    index => BasisBladeUtils.BasisBladeGradeIndexToId((uint)grade, (ulong)index),
                    _ => GetScalarValue()
                );

        return Processor.KVector(grade, indexScalarDictionary);
    }

    
    public XGaFloat64KVector GetKVectorOfGrade(int grade, double minValue, double maxValue)
    {
        var kvSpaceDimensions =
            VSpaceDimensions.GetBinomialCoefficient(grade);

        var indexScalarDictionary =
            Enumerable
                .Range(0, (int)kvSpaceDimensions)
                .ToDictionary(
                    index => BasisBladeUtils.BasisBladeGradeIndexToId((uint)grade, (ulong)index),
                    _ => GetScalarValue(minValue, maxValue)
                );

        return Processor.KVector(grade, indexScalarDictionary);
    }

    
    public XGaFloat64KVector GetSparseKVectorOfGrade(int grade, int termsCount)
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
                .ToDictionary(
                    index => BasisBladeUtils.BasisBladeGradeIndexToId((uint)grade, (ulong)index),
                    _ => GetScalarValue()
                );

        return Processor.KVector(grade, indexScalarDictionary);
    }

    
    public XGaFloat64KVector GetSparseKVectorOfGrade(int grade, int termsCount, double minValue, double maxValue)
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
                .ToDictionary(
                    index => BasisBladeUtils.BasisBladeGradeIndexToId((uint)grade, (ulong)index),
                    _ => GetScalarValue(minValue, maxValue)
                );

        return Processor.KVector(grade, indexScalarDictionary);
    }

    
    public XGaFloat64GradedMultivector GetMultivector()
    {
        var gaSpaceDimensions = GaSpaceDimensions;

        var idScalarDictionary =
            Enumerable
                .Range(0, gaSpaceDimensions)
                .Select(index => 
                    new KeyValuePair<IndexSet, double>(
                        ((ulong)index).ToUInt64IndexSet(),
                        GetScalarValue()
                    )
                );

        return Processor
            .CreateMultivectorComposer()
            .SetTerms(idScalarDictionary)
            .GetGradedMultivector();
    }

    
    public XGaFloat64GradedMultivector GetMultivector(double minValue, double maxValue)
    {
        var gaSpaceDimensions = GaSpaceDimensions;
            
        var idScalarDictionary =
            Enumerable
                .Range(0, gaSpaceDimensions)
                .Select(index => 
                    new KeyValuePair<IndexSet, double>(
                        ((ulong)index).ToUInt64IndexSet(),
                        GetScalarValue(minValue, maxValue)
                    )
                );

        return Processor
            .CreateMultivectorComposer()
            .SetTerms(idScalarDictionary)
            .GetGradedMultivector();
    }

    
    public XGaFloat64GradedMultivector GetMultivector(int termsCount)
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
                    new KeyValuePair<IndexSet, double>(
                        ((ulong)index).ToUInt64IndexSet(),
                        GetScalarValue()
                    )
                );

        return Processor
            .CreateMultivectorComposer()
            .SetTerms(idScalarDictionary)
            .GetGradedMultivector();
    }

    
    public XGaFloat64GradedMultivector GetMultivector(int termsCount, double minValue, double maxValue)
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
                    new KeyValuePair<IndexSet, double>(
                        ((ulong)index).ToUInt64IndexSet(),
                        GetScalarValue(minValue, maxValue)
                    )
                );

        return Processor
            .CreateMultivectorComposer()
            .SetTerms(idScalarDictionary)
            .GetGradedMultivector();
    }
    
    
    public XGaFloat64UniformMultivector GetUniformMultivector(int termsCount)
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
                    new KeyValuePair<IndexSet, double>(
                        (IndexSet)index,
                        GetScalarValue()
                    )
                );

        return Processor
            .CreateMultivectorComposer()
            .SetTerms(idScalarDictionary)
            .GetUniformMultivector();
    }

    
    public IEnumerable<XGaFloat64Vector> GetVectors(int count)
    {
        while (count > 0)
        {
            yield return GetVector();
            count--;
        }
    }

    
    public XGaFloat64KVector GetBlade(int grade)
    {
        if (grade == 0U)
            return GetScalar();

        if (grade == 1U)
            return GetVector();

        return GetVectors(grade).Op(Processor);
    }

    
    public IEnumerable<double> GetScalars(int count)
    {
        return Enumerable
            .Range(0, count)
            .Select(_ => GetScalarValue());
    }
    
    
    public XGaFloat64PureRotor GetEuclideanPureRotor()
    {
        return GetVector().CreatePureRotor(GetVector());
    }

    public double[,] GetArray(int rowsCount, int columnsCount)
    {
        var array = new double[rowsCount, columnsCount];

        for (var i = 0; i < rowsCount; i++)
        for (var j = 0; j < columnsCount; j++)
            array[i, j] = GetScalarValue();

        return array;
    }

    public double[,] GetPermutationArray(int size)
    {
        var array = new double[size, size];

        var indexList = Enumerable
            .Range(0, size)
            .Shuffled(RandomGenerator);

        var i = 0;
        foreach (var colIndex in indexList)
        {
            for (var j = 0; j < size; j++)
                array[i, j] = j == colIndex ? 1 : 0;

            i++;
        }

        return array;
    }
}